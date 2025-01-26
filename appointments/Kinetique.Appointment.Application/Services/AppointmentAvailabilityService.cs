using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Services;

internal class AppointmentAvailabilityService : IAppointmentAvailabilityService
{
    private readonly IAppointmentRepository _appointmentRepository;
    
    public AppointmentAvailabilityService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<AppointmentDto> TryBook(AppointmentCreateCommand dto)
    {
        AppointmentCycle? cycle = null;
        var appointmentDto = dto.Appointment;
        if (appointmentDto.CycleId is > 0)
        {
            cycle = await _appointmentRepository.Get(appointmentDto.CycleId.Value);
        }
        else
        {
            // if there is no cycle, create one if it is possible
            cycle = await _appointmentRepository.GetOngoingCycleForPatient(appointmentDto.PatientPesel);
            if (cycle == null)
            {
                cycle = new AppointmentCycle(10) // default is 10 appointment cycle
                {
                    PatientPesel = appointmentDto.PatientPesel,
                    DoctorCode = appointmentDto.DoctorCode
                };
                if (dto.Referral != null)
                {
                    cycle.AddReferral(dto.Referral.MapToEntity());
                }
            }
            else
            {
                if (cycle.DoctorCode == appointmentDto.DoctorCode)
                    throw new Exception("Patient already has active cycle with different Doctor");
            }
        }

        if (cycle is not { CycleReady: true })
            throw new Exception("You can create appointment only to valid cycle");
        
        var doctors = await
            _appointmentRepository.GetAppointmentsForDoctor(cycle.DoctorCode, appointmentDto.StartDate,
                appointmentDto.StartDate.Add(appointmentDto.Duration));
        
        var patients = await
            _appointmentRepository.GetAppointmentsForPatient(cycle.PatientPesel, appointmentDto.StartDate,
                appointmentDto.StartDate.Add(appointmentDto.Duration));

        // if there is at least 1 slot for patient or doctor in same date range.
        if (doctors.Count + patients.Count != 0)
            throw new TimeSlotAlreadyOccupiedException(appointmentDto.StartDate, appointmentDto.Duration);

        var appointment = appointmentDto.MapToEntity();
        cycle.AddAppointment(appointment);
        await _appointmentRepository.AddOrUpdate(cycle);
        return appointment.MapToDto();
    }
}