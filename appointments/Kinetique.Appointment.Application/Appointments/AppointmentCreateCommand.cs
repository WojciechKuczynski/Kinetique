using Kinetique.Appointment.Application.Dtos;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Appointment.Application.Appointments;

public class AppointmentCreateCommand(AppointmentDto appointment, ReferralDto? referral = null) : ICommandRequest
{
    public AppointmentDto Appointment = appointment;
    public ReferralDto? Referral = referral;
}


