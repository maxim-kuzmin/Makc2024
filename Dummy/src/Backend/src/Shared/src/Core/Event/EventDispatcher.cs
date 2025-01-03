﻿namespace Makc2024.Dummy.Shared.Core.Event;

public class EventDispatcher(IMediator _mediator) : IEventDispatcher
{
  public async Task DispatchAndClearEvents(IEventSource eventSource, CancellationToken cancellationToken = default)
  {
      var events = eventSource.GetEvents().ToArray();

      eventSource.ClearEvents();

      foreach (var evt in events)
      {
        await _mediator.Publish(evt, cancellationToken).ConfigureAwait(false);
      }
  }
}

