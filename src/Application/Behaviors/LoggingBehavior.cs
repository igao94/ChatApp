using MediatR;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing {Request}.", requestName);

        var response = await next(cancellationToken);

        if (response is IResult result)
        {
            if (result.IsSuccess)
            {
                logger.LogInformation("Completed {Request}.", requestName);
            }
            else
            {
                logger.LogError("Completed {Request} with error: {Error}.", requestName, result.Error);
            }
        }
        else
        {
            logger.LogInformation("Completed {Request}.", requestName);
        }

        return response;
    }
}
