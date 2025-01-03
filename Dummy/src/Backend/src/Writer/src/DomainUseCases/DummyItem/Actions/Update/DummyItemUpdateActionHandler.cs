﻿namespace Makc2024.Dummy.Writer.DomainUseCases.DummyItem.Actions.Update;

public class DummyItemUpdateActionHandler(IDummyItemCommandService _service) :
  ICommandHandler<DummyItemUpdateActionCommand, Result<DummyItemGetActionDTO>>
{
  public Task<Result<DummyItemGetActionDTO>> Handle(
    DummyItemUpdateActionCommand request,
    CancellationToken cancellationToken)
  {
    return _service.Update(request, cancellationToken);
  }
}
