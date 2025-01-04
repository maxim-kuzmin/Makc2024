﻿namespace Makc2024.Dummy.Writer.Infrastructure.App.Db.For.PostgreSQL.Settings.Entities;

/// <summary>
/// Настройки базы данных сущности события приложения для PostgreSQL.
/// </summary>
public class AppEventEntityDbSettingsForPostgreSQL : AppEventEntityDbSettings
{
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="schema">Схема.</param>
  public AppEventEntityDbSettingsForPostgreSQL(string schema)
  {
    Schema = schema;

    Table = "app_event";

    PrimaryKey = $"pk_{Table}";

    ColumnForCreationDate = "creation_date";
    ColumnForId = "id";
    ColumnForIsPublished = "is_published";
    ColumnForName = "name";

    MaxLengthForName = 255;
    
    UniqueIndexForName = $"ux_{Table}_{ColumnForName}";
  }
}