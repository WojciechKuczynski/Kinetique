using Kinetique.Nfz.Application.Dtos;
using Kinetique.Nfz.Application.Procedures;
using Kinetique.Nfz.Application.Procedures.Handlers;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Nfz.API.Controllers;

public class ProceduresController(IProcedureEmptyListHandler procedureEmptyListHandler) : BaseController
{
    private readonly IProcedureEmptyListHandler _procedureEmptyListQuery = procedureEmptyListHandler;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientProcedureDto>>> GetProceduresToFill()
    {
        return Ok(await _procedureEmptyListQuery.Handle(new ProcedureEmptyListQuery()));
    }
}