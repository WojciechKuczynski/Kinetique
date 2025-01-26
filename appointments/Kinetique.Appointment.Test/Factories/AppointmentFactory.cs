using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.ValueObjects;

namespace Kinetique.Appointment.Test.Factories;

internal class AppointmentFactory
{
    internal AppointmentCreateCommand CreateAppointmentCommandWithFakeReferral(string doctorCode, Pesel patientPesel,
        DateTime startDate, TimeSpan duration, long appointmentId)
    {
        return new AppointmentCreateCommand(new AppointmentDto()
        {
            DoctorCode = doctorCode,
            PatientPesel = patientPesel,
            StartDate = startDate,
            Duration = duration,
            Id = appointmentId
        },
        new ReferralDto()
        {
            Code = "ABCDEF",
            Pesel = "93100656374",
            Uid = Guid.NewGuid().ToString()
        });
    }

    internal AppointmentCycle CreateCycleWithOneAppointment(string doctorCode, Pesel patientPesel,
        DateTime startDate, TimeSpan duration, long appointmentId)
    {
        var cycle = new AppointmentCycle(10)
        {
            DoctorCode = doctorCode,
            PatientPesel = patientPesel
        };
        cycle.AddAppointment(new Model.Appointment()
        {
            Duration = duration, StartDate = startDate, Id = appointmentId
        });
        
        return cycle;
    }
}