BEGIN TRANSACTION;
ALTER TABLE [Educations] DROP CONSTRAINT [FK_Educations_Selections_EducationTierId];

DROP INDEX [IX_Educations_EducationTierId] ON [Educations];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Educations]') AND [c].[name] = N'EducationTierId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Educations] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Educations] DROP COLUMN [EducationTierId];

CREATE INDEX [IX_Educations_SelectionId] ON [Educations] ([SelectionId]);

ALTER TABLE [Educations] ADD CONSTRAINT [FK_Educations_Selections_SelectionId] FOREIGN KEY ([SelectionId]) REFERENCES [Selections] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260427072655_updateEducationNamingConvention', N'9.0.10');

COMMIT;
GO

