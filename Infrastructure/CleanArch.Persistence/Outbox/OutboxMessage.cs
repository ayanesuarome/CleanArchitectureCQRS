namespace CleanArch.Persistence.Outbox;

/// <summary>
/// Represents the outbox message.
/// </summary>
public sealed class OutboxMessage
{
    /// <summary>
    /// Initializes a new instance of the class <see cref="OutboxMessage"/>.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="occurredOn">The occurred on date and time.</param>
    /// <param name="type">The type.</param>
    /// <param name="content">The content.</param>
    public OutboxMessage(
        Guid id,
        DateTimeOffset occurredOn,
        string type,
        string content)
    {
        Id = id;
        OccurredOn = occurredOn;
        Content = content;
        Type = type;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string Content { get; init; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    public DateTimeOffset OccurredOn { get; init; }

    /// <summary>
    /// Gets the processed on date and time, if it exists.
    /// </summary>
    public DateTimeOffset? ProcessedOn { get; private set; }

    /// <summary>
    /// Gets the error, if it exists.
    /// </summary>
    public string? Error { get; private set; }

    public void UpdateProcessedOn(DateTimeOffset? processedOn)
    {
        ProcessedOn = processedOn;
    }

    public void UpdateError(string? error)
    {
        Error = error;
    }
}
