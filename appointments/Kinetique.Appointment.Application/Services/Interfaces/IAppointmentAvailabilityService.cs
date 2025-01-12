using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Dtos;

namespace Kinetique.Appointment.Application.Services.Interfaces;

public interface IAppointmentAvailabilityService
{
    public Task<AppointmentDto> TryBook(AppointmentCreateCommand dto);
}