using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Model;

namespace Kinetique.Appointment.Test.Factories;

internal class AppointmentFactory
{
    internal AppointmentCreateCommand CreateAppointmentCommandWithFakeReferral(long doctorId, long patientId,
        DateTime startDate, TimeSpan duration)
    {
        return new AppointmentCreateCommand(new AppointmentDto()
        {
            DoctorId = doctorId,
            PatientId = patientId,
            StartDate = startDate,
            Duration = duration
        },
        new ReferralDto()
        {
            Code = "ABCDEF",
            Pesel = "93100656374",
            Uid = Guid.NewGuid().ToString()
        });
    }

    internal AppointmentCycle CreateCycleWithOneAppointment(long doctorId, long patientId,
        DateTime startDate, TimeSpan duration)
    {
        var cycle = new AppointmentCycle(10)
        {
            DoctorId = doctorId,
            PatientId = patientId
        };
        cycle.AddAppointment(new Model.Appointment()
        {
            Duration = duration, StartDate = startDate
        });
        
        return cycle;
    }
}