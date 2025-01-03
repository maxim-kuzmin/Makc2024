﻿namespace Makc2024.Dummy.Writer.DomainUseCases.DummyItem.Actions.GetList;

public class DummyItemGetListActionHandler(IDummyItemQueryService _service) :
  IQueryHandler<DummyItemGetListActionQuery, Result<DummyItemGetListActionDTO>>
{
  public Task<Result<DummyItemGetListActionDTO>> Handle(
    DummyItemGetListActionQuery request,
    CancellationToken cancellationToken)
  {
    return _service.GetList(request, cancellationToken);
  }
}
