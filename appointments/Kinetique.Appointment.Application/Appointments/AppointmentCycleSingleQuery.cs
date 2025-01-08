using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments;

public record AppointmentCycleSingleQuery(long Id) : IQuery<AppointmentCycleDto>;
