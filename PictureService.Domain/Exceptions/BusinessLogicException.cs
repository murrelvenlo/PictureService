namespace PictureService.Domain.Exceptions;

public class BusinessLogicException : Exception
{
    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(string message, string description) : base(message)
    {
        Description = description;
    }

    public BusinessLogicException(string message, string description, IEnumerable<string> parameters) :
        base(description)
    {
        Description = description;
        Parameters = parameters;
    }

    public string Description { get; } = string.Empty;

    public IEnumerable<string> Parameters { get; } = Enumerable.Empty<string>();
}
