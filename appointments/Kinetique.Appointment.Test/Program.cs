// See https://aka.ms/new-console-template for more information

using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Rpc;

Console.WriteLine("Hello, World!");



using var srv = new RcpServer<PatientAppointmentRequest, PatientAppointmentResponse>("appointment-queue", TestResponse);
Console.ReadKey();
srv.Dispose();
return;

PatientAppointmentResponse TestResponse(PatientAppointmentRequest request)
{
    return new PatientAppointmentResponse("test");
}

