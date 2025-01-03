﻿namespace Makc2024.Dummy.Writer.Infrastructure.App.Config.Options;

/// <summary>
/// Секция PostgreSQL в параметрах конфигурации приложения.
/// </summary>
public record AppConfigOptionsPostgreSQL
{
  public string ConnectionStringName { get; set; } = string.Empty;

  public string Database { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public int Port { get; set; }

  public string Server { get; set; } = string.Empty;

  public string UserId { get; set; } = string.Empty;

  public string ToConnectionString(string template)
  {
    return template
      .Replace("{Database}", Database)
      .Replace("{Password}", Password)
      .Replace("{Port}", Port.ToString())
      .Replace("{Server}", Server)
      .Replace("{UserId}", UserId);
  }
}
