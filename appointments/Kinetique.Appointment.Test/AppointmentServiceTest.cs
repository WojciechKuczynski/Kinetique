using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.Application.Services;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL.Repositories;
using Xunit;

namespace Kinetique.Appointment.Test;

public class AppointmentServiceTest
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentAvailabilityService _appointmentAvailabilityService;

    public AppointmentServiceTest()
    {
        _appointmentRepository = new InMemoryAppointmentRepository();
        _appointmentAvailabilityService = new AppointmentAvailabilityService(_appointmentRepository);
    }
    
    [Fact]
    public async Task for_2_appointments_at_same_time_2nd_booking_is_not_available()
    {
        var appointment1 = new Model.Appointment()
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            StartDate = DateTime.UtcNow,
            Duration = TimeSpan.FromHours(2)
        };
        var appointment2 = new Model.Appointment()
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            StartDate = DateTime.UtcNow.AddHours(1),
            Duration = TimeSpan.FromHours(1)
        };

        _appointmentAvailabilityService.TryBook(appointment1.MapToDto());
        
        await Assert.ThrowsAsync<TimeSlotAlreadyOccupiedException>(async () => await _appointmentAvailabilityService.TryBook(appointment2.MapToDto()));
    }
    
    [Fact]
    public void for_appointment_booking_is_available()
    {
        var appointment = new Model.Appointment()
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            StartDate = DateTime.UtcNow,
            Duration = TimeSpan.FromHours(2)
        }.MapToDto();
        
        Assert.NotNull(_appointmentAvailabilityService.TryBook(appointment).Result);
    }
}