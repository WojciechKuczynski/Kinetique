using Kinetique.Appointment.Application.Appointments;
using Kinetique.Appointment.Application.Appointments.Handlers;
using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Appointment.API.Controllers;

public class AppointmentController(IAppointmentCreateHandler _appointmentCreateHandler,IAppointmentSingleHandler _appointmentSingleHandler, 
    IAppointmentCycleListHandler _appointmentListHandler,IResponseStorage _storage, IAppointmentJournalHandler _appointmentJournalHandler,
    IAppointmentReferralAddHandler _appointmentReferralAddHandler, IAppointmentReferralRemoveHandler _appointmentReferralRemoveHandler,
    IAppointmentRemoveHandler _appointmentRemoveHandler) : BaseController
{

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AppointmentDto>> GetById(long id)
    {
        return Ok(await _appointmentSingleHandler.Handle(new AppointmentSingleQuery(id)));
    }
    
    [HttpGet("cycle/{id:long}")]
    public async Task<ActionResult<AppointmentDto>> GetCycleById(long id)
    {
        return Ok(await _appointmentSingleHandler.Handle(new AppointmentCycleSingleQuery(id)));
    }
    
    [HttpGet("journal")]
    public async Task<ActionResult<AppointmentDto>> GetJournal()
    {
        return Ok(await _appointmentJournalHandler.Handle(new AppointmentJournalQuery()));
    }
    
    [HttpGet]
    public async Task<ActionResult<AppointmentCycleDto>> GetAll()
    {
        return Ok(await _appointmentListHandler.Handle(new AppointmentCycleListQuery()));
    }
    
    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appointmentDto)
    {
        await _appointmentCreateHandler.Handle(new AppointmentCreateCommand(appointmentDto));
        var res = _storage.Get(ObjectConstants.Appointment);
        
        return CreatedAtAction(nameof(GetById), new { id = res }, null);
    }
    
    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Create(long id)
    {
        await _appointmentRemoveHandler.Handle(new AppointmentRemoveCommand(new AppointmentDto {Id = id}));

        return Ok();
    }
    
    [HttpPost("cycle")]
    public async Task<ActionResult<AppointmentCycleDto>> CreateCycle(AppointmentCycleDto appointmentCycleDto)
    {
        await _appointmentCreateHandler.Handle(new AppointmentCycleCreateCommand(appointmentCycleDto));
        var res = _storage.Get(ObjectConstants.AppointmentCycle);
        
        return CreatedAtAction(nameof(GetCycleById), new { id = res }, null);
    }
    
    [HttpPost("referral")]
    public async Task<ActionResult<AppointmentCycleDto>> AddReferral(ReferralDto referralDto)
    {
        await _appointmentReferralAddHandler.Handle(new AppointmentReferralAddCommand(referralDto));
        
        return Ok();
    }
    
    [HttpDelete("referral/{id:long}")]
    public async Task<ActionResult<AppointmentCycleDto>> RemoveReferral(long id)
    {
        await _appointmentReferralRemoveHandler.Handle(new AppointmentReferralRemoveCommand(id));
        return Ok();
    }
}