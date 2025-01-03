﻿namespace Makc2024.Dummy.Gateway.Apps.WebApp.DummyItem.Endpoints.GetList;

public class DummyItemGetListEndpointHandler(IMediator _mediator) :
  Endpoint<DummyItemGetListEndpointRequest, IEnumerable<DummyItemGetListActionDTOItem>>
{
  public override void Configure()
  {
    Get(DummyItemGetListEndpointSettings.Route);
    //AllowAnonymous();
  }

  public override async Task HandleAsync(DummyItemGetListEndpointRequest request, CancellationToken cancellationToken)
  {
    DummyItemGetListActionQuery query = new(
      new QueryPage(request.CurrentPage, request.ItemsPerPage),
      new DummyItemGetListActionQueryFilter(request.Query));

    var result = await _mediator.Send(query, cancellationToken);

    await SendResultAsync(result.ToMinimalApiResult());
  }
}
