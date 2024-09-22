using Kinetique.Appointment.Application.Dtos;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments;

public record AppointmentSingleQuery(long Id) : IQuery<AppointmentDto>;