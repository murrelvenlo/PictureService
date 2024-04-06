namespace PictureService.Application;

internal interface IQuery<out TResponse> : MediatR.IRequest<TResponse>
{
}