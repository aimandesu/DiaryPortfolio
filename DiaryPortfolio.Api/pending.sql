BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260427103321_updateExperiencelocationNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260427103321_updateExperiencelocationNullable', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    ALTER TABLE [Projects] ADD [ProjectTypeId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    CREATE TABLE [ProjectTypeModel] (
        [Id] uniqueidentifier NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [PortfolioProfileId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ProjectTypeModel] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectTypeModel_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    CREATE INDEX [IX_Projects_ProjectTypeId] ON [Projects] ([ProjectTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    CREATE INDEX [IX_ProjectTypeModel_PortfolioProfileId] ON [ProjectTypeModel] ([PortfolioProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_ProjectTypeModel_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [ProjectTypeModel] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260428113754_addProjectTypeModelAndItsRelation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260428113754_addProjectTypeModelAndItsRelation', N'9.0.10');
END;

COMMIT;
GO

