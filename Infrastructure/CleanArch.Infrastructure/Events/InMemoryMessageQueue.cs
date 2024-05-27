using CleanArch.Application.EventBus;
using System.Threading.Channels;

namespace CleanArch.Infrastructure.Events;

internal sealed class InMemoryMessageQueue
{
    private readonly Channel<IIntegrationEvent> _channel = Channel.CreateUnbounded<IIntegrationEvent>();

    /// <summary>
    /// Gets the writable half of this channel.
    /// </summary>
    public ChannelWriter<IIntegrationEvent> Writer => _channel.Writer;

    /// <summary>
    /// Gets the readable half of this channel.
    /// </summary>
    public ChannelReader<IIntegrationEvent> Reader => _channel.Reader;
}
