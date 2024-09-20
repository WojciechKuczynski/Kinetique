using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Appointments.Handlers;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Shared.Model.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Appointment.API.Controllers;

public class AppointmentController(IAppointmentCreateHandler _appointmentCreateHandler, IResponseStorage _storage) : BaseController
{

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AppointmentDto>> GetById(long id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appointmentDto)
    {
        await _appointmentCreateHandler.Handle(new AppointmentCreateCommand(appointmentDto));
        var res = _storage.Get(ObjectConstants.Appointment);
        
        return CreatedAtAction(nameof(GetById), new { id = res }, null);
    }
}