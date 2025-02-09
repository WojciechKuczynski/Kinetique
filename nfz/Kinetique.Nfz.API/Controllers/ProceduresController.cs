using Kinetique.Nfz.Application.Appointments;
using Kinetique.Nfz.Application.Appointments.Handlers;
using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.Application.Procedures;
using Kinetique.Nfz.Application.Procedures.Handlers;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Nfz.API.Controllers;

public class ProceduresController(IProcedureEmptyListHandler procedureEmptyListHandler,
    IProcedureAllListHandler procedureAllListHandler, IProcedureAddHandler procedureAddHandler,IAppointmentAcceptHandler appointmentAcceptHandler) : BaseController
{
    private readonly IProcedureEmptyListHandler _procedureEmptyListQuery = procedureEmptyListHandler;
    private readonly IProcedureAllListHandler _procedureAllListHandler = procedureAllListHandler;
    private readonly IProcedureAddHandler _procedureAddHandler = procedureAddHandler;
    private readonly IAppointmentAcceptHandler _appointmentAcceptHandler = appointmentAcceptHandler;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientProcedureDto>>> GetProceduresToFill()
    {
        return Ok(await _procedureEmptyListQuery.Handle(new ProcedureEmptyListQuery()));
    }
    
    [HttpPost("appointment")]
    public async Task<ActionResult> AddProcedureToAppointment(PatientProcedureDto dto)
    {
        await _procedureAddHandler.Handle(new ProceduresAddCommand(dto.AppointmentExternalId, dto.Procedures));
        return Ok();
    }

    [HttpPost("appointment/accept")]
    public async Task<ActionResult> AcceptAppointment(long appointmentId)
    {
        await _appointmentAcceptHandler.Handle(new AppointmentAcceptCommand(appointmentId));
        return Ok();
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<TreatmentProcedureDto>>> GetTreatmentProcedures()
    {
        return Ok(await _procedureAllListHandler.Handle(new ProcedureAllListQuery()));
    }
    
    //TODO: zrobić
    // [HttpPost("referral")]
    // public async Task<ActionResult> AddReferral()
}