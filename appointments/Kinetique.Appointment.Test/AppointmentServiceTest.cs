using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.Application.Services;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared;
using Kinetique.Shared.Model.Abstractions;
using Xunit;

namespace Kinetique.Appointment.Test;

public class AppointmentServiceTest
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentAvailabilityService _appointmentAvailabilityService;
    private readonly IClock _clock;

    public AppointmentServiceTest()
    {
        _clock = new UtcClock();
        _appointmentRepository = new InMemoryAppointmentRepository(_clock);
        _appointmentAvailabilityService = new AppointmentAvailabilityService(_appointmentRepository, new DataContext(null,_clock));
    }
    
    [Fact]
    public async Task for_2_appointments_at_same_time_2nd_booking_is_not_available()
    {
        //TODO: Fix tests
        // var appointment1 = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = 1,
        //     StartDate = _clock.GetNow(),
        //     Duration = TimeSpan.FromHours(2)
        // };
        // var appointment2 = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = 1,
        //     StartDate = _clock.GetNow().AddHours(1),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // await _appointmentAvailabilityService.TryBook(appointment1.MapToDto());
        //
        // await Assert.ThrowsAsync<TimeSlotAlreadyOccupiedException>(async () => await _appointmentAvailabilityService.TryBook(appointment2.MapToDto()));
    }
    
    [Fact]
    public void for_appointment_booking_is_available()
    {
        // var appointment = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = 1,
        //     StartDate = _clock.GetNow(),
        //     Duration = TimeSpan.FromHours(2)
        // }.MapToDto();
        //
        // Assert.NotNull(_appointmentAvailabilityService.TryBook(appointment).Result);
    }

    [Fact]
    public async Task for_2_appointments_for_different_patient_but_same_doctor_is_not_available()
    {
        // var appointment1 = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = 1,
        //     StartDate = _clock.GetNow(),
        //     Duration = TimeSpan.FromHours(2)
        // };
        // var appointment2 = new Model.Appointment()
        // {
        //     Id = 2,
        //     PatientId = 2,
        //     DoctorId = 1,
        //     StartDate = _clock.GetNow().AddHours(1),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // await _appointmentAvailabilityService.TryBook(appointment1.MapToDto());
        //
        // await Assert.ThrowsAsync<TimeSlotAlreadyOccupiedException>(async () => await _appointmentAvailabilityService.TryBook(appointment2.MapToDto()));
    }

    [Fact]
    public async Task for_3_appointments_for_same_doctor_should_return_3_appointments()
    {
        // const long doctorId = 1;
        // var startDate = _clock.GetNow();
        //
        // var appointment1 = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = doctorId,
        //     StartDate = startDate,
        //     Duration = TimeSpan.FromHours(1)
        // };
        // var appointment2 = new Model.Appointment()
        // {
        //     Id = 2,
        //     PatientId = 2,
        //     DoctorId = doctorId,
        //     StartDate = startDate.AddHours(1),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // var appointment3 = new Model.Appointment()
        // {
        //     Id = 3,
        //     PatientId = 3,
        //     DoctorId = doctorId,
        //     StartDate = startDate.AddHours(2),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // await _appointmentAvailabilityService.TryBook(appointment1.MapToDto());
        // await _appointmentAvailabilityService.TryBook(appointment2.MapToDto());
        // await _appointmentAvailabilityService.TryBook(appointment3.MapToDto());
        //
        // var appointmentsInDb = await _appointmentRepository.GetAppointmentsForDoctor(doctorId);
        // Assert.Equal(3,appointmentsInDb.Count);
    }
    
    [Fact]
    public async Task for_3_appointments_only_1_should_be_in_given_timeslot()
    {
        // const int doctorId = 1;
        //
        // var startDate = _clock.GetNow();
        //
        // var appointment1 = new Model.Appointment()
        // {
        //     Id = 1,
        //     PatientId = 1,
        //     DoctorId = doctorId,
        //     StartDate = startDate,
        //     Duration = TimeSpan.FromHours(1)
        // };
        // var appointment2 = new Model.Appointment()
        // {
        //     Id = 2,
        //     PatientId = 2,
        //     DoctorId = doctorId,
        //     StartDate = startDate.AddHours(3),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // var appointment3 = new Model.Appointment()
        // {
        //     Id = 3,
        //     PatientId = 3,
        //     DoctorId = doctorId,
        //     StartDate = startDate.AddHours(4),
        //     Duration = TimeSpan.FromHours(1)
        // };
        //
        // await _appointmentAvailabilityService.TryBook(appointment1.MapToDto());
        // await _appointmentAvailabilityService.TryBook(appointment2.MapToDto());
        // await _appointmentAvailabilityService.TryBook(appointment3.MapToDto());
        //
        // var appointmentsInDb = await _appointmentRepository.GetAppointmentsForDoctor(doctorId,startDate, startDate.AddHours(2));
        // Assert.Single(appointmentsInDb);
    }
}