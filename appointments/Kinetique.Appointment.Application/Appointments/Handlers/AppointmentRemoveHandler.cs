using Kinetique.Appointment.Application.Storage;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Messaging.Messages;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentRemoveHandler : ICommandHandler<AppointmentRemoveCommand>;

public class AppointmentRemoveHandler : IAppointmentRemoveHandler
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IRabbitPublisher _rabbitPublisher;
    
    public AppointmentRemoveHandler(IAppointmentRepository appointmentRepository, IRabbitPublisher rabbitPublisher)
    {
        _appointmentRepository = appointmentRepository;
        _rabbitPublisher = rabbitPublisher;
    }
    public async Task Handle(AppointmentRemoveCommand command, CancellationToken token = default)
    {
        var appointmentInDb = await _appointmentRepository.GetById(command.appointment.Id);
        if (appointmentInDb == null)
            return;
        var message = new AppointmentRemovedEvent(appointmentInDb.Cycle.DoctorCode, appointmentInDb.StartDate,
            appointmentInDb.StartDate.Add(appointmentInDb.Duration));
        var cycle = appointmentInDb.Cycle;
        cycle.RemoveAppointment(appointmentInDb);
        await _appointmentRepository.Update(cycle);
        _rabbitPublisher.PublishToExchange(message, "appointment", "appointment.removed");
    }
}