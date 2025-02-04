using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Exceptions;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.Application.Services;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Appointment.Model;
using Kinetique.Appointment.Test.Factories;
using Kinetique.Shared;
using Kinetique.Shared.Model.Abstractions;
using Xunit;

namespace Kinetique.Appointment.Test;


public class AppointmentServiceTest
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentAvailabilityService _appointmentAvailabilityService;
    private readonly IClock _clock;
    private readonly AppointmentFactory _appointmentFactory = new();
    private const string Doctor_Code = "000444";
    private const string Patient_Pesel = "98051094857";
    
    
    public AppointmentServiceTest()
    {
        _clock = new UtcClock();
        _appointmentRepository = new InMemoryAppointmentRepository(_clock);
        _appointmentAvailabilityService = new AppointmentAvailabilityService(_appointmentRepository);
    }
    
    [Fact]
    public async Task for_2_appointments_at_same_time_2nd_booking_is_not_available()
    {
        var appointment1 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, Patient_Pesel, _clock.GetNow(), TimeSpan.FromHours(2), 1);
        

        var appointmentResponse = await _appointmentAvailabilityService.TryBook(appointment1);

        var appointment2 = new AppointmentCreateCommand(
            appointment: new AppointmentDto()
            {
                Id = 2,
                DoctorCode = Doctor_Code,
                PatientPesel = Patient_Pesel,
                StartDate = _clock.GetNow().AddHours(1),
                Duration = TimeSpan.FromHours(1),
                CycleId = appointmentResponse.CycleId.Value
            }
        );
        
        await Assert.ThrowsAsync<TimeSlotAlreadyOccupiedException>(async () => await _appointmentAvailabilityService.TryBook(appointment2));
    }
    
    [Fact]
    public void patient_new_appointment_with_referral_cycle_is_created()
    {
        var command =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, Patient_Pesel, _clock.GetNow(), TimeSpan.FromHours(2), 1);
        
        Assert.NotNull(_appointmentAvailabilityService.TryBook(command).Result);
    }

    [Fact]
    public async Task for_2_appointments_for_different_patient_but_same_doctor_is_not_available()
    {
        var appointmentCommand1 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, Patient_Pesel, _clock.GetNow(), TimeSpan.FromHours(2), 1);

        var appointmentCommand2 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383756", _clock.GetNow().AddHours(1),
                TimeSpan.FromHours(1),2);
        
        await _appointmentAvailabilityService.TryBook(appointmentCommand1);
        
        await Assert.ThrowsAsync<TimeSlotAlreadyOccupiedException>(async () => await _appointmentAvailabilityService.TryBook(appointmentCommand2));
    }

    [Fact]
    public async Task for_3_appointments_for_same_doctor_should_return_3_appointments()
    {
        var startDate = _clock.GetNow();
        var duration = TimeSpan.FromHours(1);

        var appointmentCommand1 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383756", startDate, duration, 1);
        var appointmentCommand2 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383755", startDate.AddHours(1),
                duration, 2);
        var appointmentCommand3 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383754", startDate.AddHours(2), duration, 3);
        
        await _appointmentAvailabilityService.TryBook(appointmentCommand1);
        await _appointmentAvailabilityService.TryBook(appointmentCommand2);
        await _appointmentAvailabilityService.TryBook(appointmentCommand3);
        
        var appointmentsInDb = await _appointmentRepository.GetAppointmentsForDoctor(Doctor_Code);
        Assert.Equal(3,appointmentsInDb.Count);
    }
    
    [Fact]
    public async Task for_3_appointments_only_1_should_be_in_given_timeslot()
    {
        var startDate = _clock.GetNow();
        var duration = TimeSpan.FromHours(1);

        var appointmentCommand1 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, Patient_Pesel, startDate, duration, 1);
        var appointmentCommand2 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383756", startDate.AddHours(3), duration, 2);
        var appointmentCommand3 =
            _appointmentFactory.CreateAppointmentCommandWithFakeReferral(Doctor_Code, "93884383752", startDate.AddHours(4), duration, 3);
        
        await _appointmentAvailabilityService.TryBook(appointmentCommand1);
        await _appointmentAvailabilityService.TryBook(appointmentCommand2);
        await _appointmentAvailabilityService.TryBook(appointmentCommand3);
        
        var appointmentsInDb = await _appointmentRepository.GetAppointmentsForDoctor(Doctor_Code,startDate, startDate.AddHours(2));
        Assert.Single(appointmentsInDb);
    }
}