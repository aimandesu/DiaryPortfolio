BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260714232126_updateModelsLatest'
)
BEGIN
    ALTER TABLE [Skills] DROP CONSTRAINT [FK_Skills_Selections_SkillLevelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260714232126_updateModelsLatest'
)
BEGIN
    DROP INDEX [IX_Skills_SkillLevelId] ON [Skills];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260714232126_updateModelsLatest'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Skills]') AND [c].[name] = N'SkillLevelId');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Skills] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Skills] DROP COLUMN [SkillLevelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260714232126_updateModelsLatest'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260714232126_updateModelsLatest', N'9.0.10');
END;

COMMIT;
GO

