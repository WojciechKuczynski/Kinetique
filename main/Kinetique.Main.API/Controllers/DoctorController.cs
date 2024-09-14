using Kinetique.Main.Application.Doctors;
using Kinetique.Main.Application.Doctors.Handlers;
using Kinetique.Main.Application.Dtos;
using Kinetique.Main.Application.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Main.API.Controllers;

public class DoctorController(IDoctorCreateHandler _doctorCreateHandler, IDoctorSingleHandler _doctorSingleHandler,
    IDoctorListHandler _doctorListHandler, IResponseStorage _responseStorage) : BaseController
{

    [HttpGet]
    public async Task<ActionResult<IList<DoctorDto>>> GetAll()
    {
        return Ok(await _doctorListHandler.Handle(new DoctorListQuery()));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<IList<DoctorDto>>> GetById(long id)
    {
        return Ok(await _doctorSingleHandler.Handle(new DoctorSingleQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Create(DoctorDto doctor)
    {
        await _doctorCreateHandler.Handle(new DoctorCreateCommand(doctor));
        var res = _responseStorage.Get(ObjectConstants.Doctor);
        return CreatedAtAction(nameof(GetById), new { id = res }, null);
    }
}