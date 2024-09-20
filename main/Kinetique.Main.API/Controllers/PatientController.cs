using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Patients;
using Kinetique.Main.Application.Patients.Handlers;
using Kinetique.Main.Application.Storage;
using Kinetique.Shared.Model.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Main.API.Controllers;

public class PatientController(IPatientCreateHandler _patientCreateHandler, IPatientSingleHandler _patientSingleHandler,
    IPatientListHandler _patientListHandler, IResponseStorage _responseStorage) : BaseController
{

    [HttpGet]
    public async Task<ActionResult<IList<PatientDto>>> GetAll()
    {
        return Ok(await _patientListHandler.Handle(new PatientListQuery()));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<IList<PatientDto>>> GetById(long id)
    {
        return Ok(await _patientSingleHandler.Handle(new PatientSingleQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create(PatientDto patient)
    {
        await _patientCreateHandler.Handle(new PatientCreateCommand(patient));
        var res = _responseStorage.Get(ObjectConstants.Patient);
        return CreatedAtAction(nameof(GetById), new { id = res }, null);
    }
}