using Kinetique.Appointment.Application.Dtos;
using Kinetique.Appointment.Application.Mappers;
using Kinetique.Appointment.DAL;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.Application.Appointments.Handlers;

public interface IAppointmentJournalHandler : IQueryHandler<AppointmentJournalQuery, List<AppointmentJournalDto>>;

public class AppointmentJournalHandler(DataContext context) : IAppointmentJournalHandler
{
    public async Task<List<AppointmentJournalDto>> Handle(AppointmentJournalQuery query, CancellationToken token = default)
    {
        return (await context.Journals.ToListAsync(token)).Select(x => x.MapToDto()).ToList();
    }
}