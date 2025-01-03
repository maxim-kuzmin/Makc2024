﻿namespace Makc2024.Dummy.Writer.DomainUseCases.DummyItem.Actions.Create;

public class DummyItemCreateActionHandler(IDummyItemCommandService _service) :
  ICommandHandler<DummyItemCreateActionCommand, Result<DummyItemGetActionDTO>>
{
  public Task<Result<DummyItemGetActionDTO>> Handle(
    DummyItemCreateActionCommand request,
    CancellationToken cancellationToken)
  {
    return _service.Create(request, cancellationToken);
  }
}
