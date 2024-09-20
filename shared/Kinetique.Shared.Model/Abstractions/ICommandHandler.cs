namespace Kinetique.Shared.Model.Abstractions;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommandRequest
{
    Task Handle(TCommand command, CancellationToken token = default);
}