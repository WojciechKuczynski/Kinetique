using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL.Repositories;

namespace Kinetique.Appointment.Application.Services;

internal class AppointmentAvailabilityService : IAppointmentAvailabilityService
{
    private readonly IAppointmentRepository _appointmentRepository;
    public AppointmentAvailabilityService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<AppointmentDto> TryBook(AppointmentDto dto)
    {
        var doctors =
            _appointmentRepository.GetAppointmentsForDoctor(dto.DoctorId, dto.StartDate,
                dto.StartDate.Add(dto.Duration));
        
        var patients =
            _appointmentRepository.GetAppointmentsForPatient(dto.PatientId.Value, dto.StartDate,
                dto.StartDate.Add(dto.Duration));

        if (doctors.Count + patients.Count != 0)
            throw new TimeSlotAlreadyOccupiedException(dto.StartDate, dto.Duration);
        
        var result = await _appointmentRepository.Add(dto.MapToEntity());
        return result.MapToDto();

    }
}