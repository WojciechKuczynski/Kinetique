using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Services;

internal class AppointmentAvailabilityService : IAppointmentAvailabilityService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly DataContext _dataContext;
    public AppointmentAvailabilityService(IAppointmentRepository appointmentRepository, DataContext dataContext)
    {
        _appointmentRepository = appointmentRepository;
        _dataContext = dataContext;
    }
    
    public async Task<AppointmentDto> TryBook(AppointmentDto dto)
    {
        //TODO: Check in schedule for doctor
        var cycle = await _dataContext.AppointmentCycles.FindAsync(dto.CycleId);

        if (cycle is not { CycleReady: true })
            throw new Exception("You can create appointment only to valid cycle");
        
        var doctors = await
            _appointmentRepository.GetAppointmentsForDoctor(dto.DoctorId, dto.StartDate,
                dto.StartDate.Add(dto.Duration));
        
        var patients = await
            _appointmentRepository.GetAppointmentsForPatient(dto.PatientId.Value, dto.StartDate,
                dto.StartDate.Add(dto.Duration));

        // if there is at least 1 slot for patient or doctor in same date range.
        if (doctors.Count + patients.Count != 0)
            throw new TimeSlotAlreadyOccupiedException(dto.StartDate, dto.Duration);

        var appointment = dto.MapToEntity();
        cycle.AddAppointment(appointment);
        await _dataContext.SaveChangesAsync();
        return appointment.MapToDto();
    }
}