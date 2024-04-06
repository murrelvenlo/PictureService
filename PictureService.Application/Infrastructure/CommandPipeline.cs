using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PictureService.Domain;

namespace PictureService.Application.Infrastructure;

internal class CommandPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandFactory
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandPipeline(ILoggerFactory logger, IUnitOfWork unitOfWork, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger.CreateLogger(GetType());
        _unitOfWork = unitOfWork;
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{typeof(TRequest).Name} command triggered, request data: {JsonConvert.SerializeObject(request)}");

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

        // Execute the request Start a new transaction
        _unitOfWork.Open();
        _unitOfWork.Begin();
        try
        {
            var response = await next().ConfigureAwait(false);

            _logger.LogDebug($"Command response: {JsonConvert.SerializeObject(response)}");

            _unitOfWork.Commit();

            _logger.LogInformation($"Command successfully committed.");

            return response;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}