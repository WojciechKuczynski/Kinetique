using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Rpc;
using Microsoft.AspNetCore.Mvc;

namespace Kinetique.Main.API.Controllers;

public class ReservationController(IRabbitPublisher rabbitPublisher) : BaseController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        // var message = new PatientAppointmentRequest(1);
        // var client = new RpcClient<PatientAppointmentRequest,PatientAppointmentResponse>("localhost");
        // var cts = new CancellationTokenSource();
        // var token = cts.Token;
        // cts.CancelAfter(TimeSpan.FromSeconds(15));
        // var response = await client.CallAsync(message,token);
        return Ok();
    }
}