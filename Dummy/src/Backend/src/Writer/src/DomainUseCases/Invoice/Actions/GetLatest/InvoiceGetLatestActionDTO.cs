﻿namespace Makc2024.Dummy.Writer.DomainUseCases.Invoice.Actions.GetLatest;

public record InvoiceGetLatestActionDTO(
  int Amount,
  string Email,
  string Name,
  Guid Id,
  string ImageUrl);