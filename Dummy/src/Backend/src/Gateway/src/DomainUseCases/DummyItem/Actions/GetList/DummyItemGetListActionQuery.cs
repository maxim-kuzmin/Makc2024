﻿namespace Makc2024.Dummy.Gateway.DomainUseCases.DummyItem.Actions.GetList;

public record DummyItemGetListActionQuery(
  QueryPage Page,
  DummyItemGetListActionQueryFilter Filter) : IQuery<Result<DummyItemGetListActionDTO>>;
