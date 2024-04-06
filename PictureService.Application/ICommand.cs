namespace PictureService.Application;

internal interface ICommandFactory { }

internal interface ICommand<out TResponse> : MediatR.IRequest<TResponse>, ICommandFactory
{
}

internal interface ICommand : MediatR.IRequest, ICommandFactory
{
}