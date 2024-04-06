using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PictureService.Domain;

namespace PictureService.Application.Infrastructure;

internal class QueryPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public QueryPipeline(ILoggerFactory logger, IUnitOfWork unitOfWork, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger.CreateLogger(GetType());
        _unitOfWork = unitOfWork;
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{typeof(TRequest).Name} query triggered, request data: {JsonConvert.SerializeObject(request)}");

        // Pre-execution: validate the request
        if (_validators != null)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(async x => await x.ValidateAsync(context, cancellationToken))
                .SelectMany(result => result.Result.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        _unitOfWork.Open();
        var result = await next().ConfigureAwait(true);
        _unitOfWork.Dispose();

        return result;
    }

}