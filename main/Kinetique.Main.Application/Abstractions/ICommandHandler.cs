namespace Kinetique.Main.Application.Abstractions;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommandRequest
{
    Task Handle(TCommand command, CancellationToken token = default);
}