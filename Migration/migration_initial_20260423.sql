IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Collections] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Collections] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Spaces] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Spaces] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Spaces_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Medias] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [MediaStatus] int NOT NULL,
        [MediaType] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [SpaceId] uniqueidentifier NOT NULL,
        [SpaceModelId] uniqueidentifier NULL,
        [TextId] uniqueidentifier NOT NULL,
        [CollectionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Medias] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Medias_Collections_CollectionId] FOREIGN KEY ([CollectionId]) REFERENCES [Collections] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Medias_Spaces_SpaceModelId] FOREIGN KEY ([SpaceModelId]) REFERENCES [Spaces] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Conditions] (
        [Id] uniqueidentifier NOT NULL,
        [AvailableTime] datetime2 NOT NULL,
        [DeletedTime] datetime2 NULL,
        [MediaId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Conditions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Conditions_Medias_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Locations] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Latitude] nvarchar(max) NOT NULL,
        [Longitude] nvarchar(max) NOT NULL,
        [MediaId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Locations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Locations_Medias_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Photos] (
        [Id] uniqueidentifier NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [Mime] nvarchar(max) NOT NULL,
        [Width] float NOT NULL,
        [Height] float NOT NULL,
        [Size] float NOT NULL,
        [MediaId] uniqueidentifier NOT NULL,
        [MediaModelId] uniqueidentifier NULL,
        CONSTRAINT [PK_Photos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Photos_Medias_MediaModelId] FOREIGN KEY ([MediaModelId]) REFERENCES [Medias] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [TextStyle] (
        [Id] uniqueidentifier NOT NULL,
        [TextStyle] int NOT NULL,
        [FontSize] int NOT NULL,
        [MediaId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_TextStyle] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TextStyle_Medias_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE TABLE [Videos] (
        [Id] uniqueidentifier NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [Duration] int NOT NULL,
        [Size] float NOT NULL,
        [MediaId] uniqueidentifier NOT NULL,
        [MediaModelId] uniqueidentifier NULL,
        CONSTRAINT [PK_Videos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Videos_Medias_MediaModelId] FOREIGN KEY ([MediaModelId]) REFERENCES [Medias] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Conditions_MediaId] ON [Conditions] ([MediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Locations_MediaId] ON [Locations] ([MediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_Medias_CollectionId] ON [Medias] ([CollectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_Medias_SpaceModelId] ON [Medias] ([SpaceModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_Photos_MediaModelId] ON [Photos] ([MediaModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_Spaces_UserId] ON [Spaces] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TextStyle_MediaId] ON [TextStyle] ([MediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    CREATE INDEX [IX_Videos_MediaModelId] ON [Videos] ([MediaModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251115162141_Init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251115162141_Init', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    ALTER TABLE [TextStyle] DROP CONSTRAINT [FK_TextStyle_Medias_MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    DROP INDEX [IX_TextStyle_MediaId] ON [TextStyle];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TextStyle]') AND [c].[name] = N'MediaId');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [TextStyle] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [TextStyle] DROP COLUMN [MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    ALTER TABLE [Medias] ADD [TextModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    CREATE INDEX [IX_Medias_TextModelId] ON [Medias] ([TextModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_TextStyle_TextModelId] FOREIGN KEY ([TextModelId]) REFERENCES [TextStyle] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251212224931_change_textmodel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251212224931_change_textmodel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_TextStyle_TextModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    DROP INDEX [IX_Medias_TextModelId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'TextModelId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Medias] DROP COLUMN [TextModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Medias_TextId] ON [Medias] ([TextId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_TextStyle_TextId] FOREIGN KEY ([TextId]) REFERENCES [TextStyle] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251213112317_update_textmodel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251213112317_update_textmodel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251214002506_FixTextIdUniqueConstraint'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_TextStyle_TextId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251214002506_FixTextIdUniqueConstraint'
)
BEGIN
    DROP INDEX [IX_Medias_TextId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251214002506_FixTextIdUniqueConstraint'
)
BEGIN
    CREATE INDEX [IX_Medias_TextId] ON [Medias] ([TextId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251214002506_FixTextIdUniqueConstraint'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_TextStyle_TextId] FOREIGN KEY ([TextId]) REFERENCES [TextStyle] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251214002506_FixTextIdUniqueConstraint'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251214002506_FixTextIdUniqueConstraint', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Collections_CollectionId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    DROP INDEX [IX_Medias_CollectionId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'CollectionId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Medias] DROP COLUMN [CollectionId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    ALTER TABLE [Videos] ADD [Mime] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    ALTER TABLE [Medias] ADD [CollectionModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    CREATE INDEX [IX_Medias_CollectionModelId] ON [Medias] ([CollectionModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Collections_CollectionModelId] FOREIGN KEY ([CollectionModelId]) REFERENCES [Collections] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251220080302_removeCollectionIdandAddMimeTypeToVideoModel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Collections_CollectionModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Photos] DROP CONSTRAINT [FK_Photos_Medias_MediaModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Medias_MediaModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    DROP INDEX [IX_Videos_MediaModelId] ON [Videos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    DROP INDEX [IX_Photos_MediaModelId] ON [Photos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'MediaModelId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Videos] DROP COLUMN [MediaModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'MediaModelId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Photos] DROP COLUMN [MediaModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    EXEC sp_rename N'[Medias].[CollectionModelId]', N'CollectionId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    EXEC sp_rename N'[Medias].[IX_Medias_CollectionModelId]', N'IX_Medias_CollectionId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    CREATE INDEX [IX_Videos_MediaId] ON [Videos] ([MediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    CREATE INDEX [IX_Photos_MediaId] ON [Photos] ([MediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Collections_CollectionId] FOREIGN KEY ([CollectionId]) REFERENCES [Collections] ([Id]) ON DELETE SET NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Photos] ADD CONSTRAINT [FK_Photos_Medias_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Medias_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251220104613_addCollectionIdIntoMediaModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251220104613_addCollectionIdIntoMediaModel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Spaces_SpaceModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    DROP INDEX [IX_Medias_SpaceModelId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'SpaceModelId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Medias] DROP COLUMN [SpaceModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TextStyle]') AND [c].[name] = N'TextStyle');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [TextStyle] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [TextStyle] ALTER COLUMN [TextStyle] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    CREATE INDEX [IX_Medias_SpaceId] ON [Medias] ([SpaceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Spaces_SpaceId] FOREIGN KEY ([SpaceId]) REFERENCES [Spaces] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227102255_addConditionForTextStyleandSpaceModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251227102255_addConditionForTextStyleandSpaceModel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227120521_makeSpaceModelTitleUnique'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Spaces]') AND [c].[name] = N'Title');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Spaces] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Spaces] ALTER COLUMN [Title] nvarchar(450) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227120521_makeSpaceModelTitleUnique'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Spaces_Title] ON [Spaces] ([Title]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227120521_makeSpaceModelTitleUnique'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251227120521_makeSpaceModelTitleUnique', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227125704_updateMediaTypeandStatusToStringConversion'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'MediaType');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Medias] ALTER COLUMN [MediaType] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227125704_updateMediaTypeandStatusToStringConversion'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'MediaStatus');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Medias] ALTER COLUMN [MediaStatus] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251227125704_updateMediaTypeandStatusToStringConversion'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251227125704_updateMediaTypeandStatusToStringConversion', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215015746_updateWithOne-to-WithMany-for-TextModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260215015746_updateWithOne-to-WithMany-for-TextModel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Medias_MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    DROP INDEX [IX_Locations_MediaId] ON [Locations];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'MediaStatus');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Medias] DROP COLUMN [MediaStatus];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'MediaType');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Medias] DROP COLUMN [MediaType];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'MediaId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Locations] DROP COLUMN [MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [LocationId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [LocationModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [MediaStatusSelectionId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [MediaTypeSelectionId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [SelectionMediaStatusModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD [SelectionMediaTypeModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [About] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Age] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LocationId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [MediaId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ProfileMediaId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ResumeId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Title] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [CustomUrls] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CustomUrls] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CustomUrls_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Experiences] (
        [Id] uniqueidentifier NOT NULL,
        [Company] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [UserId] uniqueidentifier NOT NULL,
        [LocationId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Experiences] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Experiences_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Experiences_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Resume] (
        [Id] uniqueidentifier NOT NULL,
        [FileId] uniqueidentifier NULL,
        [TemplateId] uniqueidentifier NULL,
        CONSTRAINT [PK_Resume] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [ResumeTemplate] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ResumeTemplate] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Types] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Types] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Selections] (
        [Id] uniqueidentifier NOT NULL,
        [Selection] nvarchar(max) NOT NULL,
        [TypeId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Selections] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Selections_Types_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Files] (
        [Id] uniqueidentifier NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [SelectionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Files] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Files_Selections_SelectionId] FOREIGN KEY ([SelectionId]) REFERENCES [Selections] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Skills] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [SelectionId] uniqueidentifier NOT NULL,
        [SkillLevelId] uniqueidentifier NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Skills_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Skills_Selections_SkillLevelId] FOREIGN KEY ([SkillLevelId]) REFERENCES [Selections] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Educations] (
        [Id] uniqueidentifier NOT NULL,
        [Institution] nvarchar(max) NOT NULL,
        [Achievement] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [UserId] uniqueidentifier NOT NULL,
        [LocationId] uniqueidentifier NOT NULL,
        [FileId] uniqueidentifier NULL,
        [EducationFileId] uniqueidentifier NULL,
        [SelectionId] uniqueidentifier NOT NULL,
        [EducationTierId] uniqueidentifier NULL,
        CONSTRAINT [PK_Educations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Educations_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Educations_Files_EducationFileId] FOREIGN KEY ([EducationFileId]) REFERENCES [Files] ([Id]),
        CONSTRAINT [FK_Educations_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Educations_Selections_EducationTierId] FOREIGN KEY ([EducationTierId]) REFERENCES [Selections] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [SelectionId] uniqueidentifier NOT NULL,
        [ProjectTypeId] uniqueidentifier NULL,
        [UserId] uniqueidentifier NOT NULL,
        [FileId] uniqueidentifier NULL,
        [ProjectFileId] uniqueidentifier NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Projects_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Projects_Files_ProjectFileId] FOREIGN KEY ([ProjectFileId]) REFERENCES [Files] ([Id]),
        CONSTRAINT [FK_Projects_Selections_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [Selections] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Medias_LocationModelId] ON [Medias] ([LocationModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Medias_SelectionMediaStatusModelId] ON [Medias] ([SelectionMediaStatusModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Medias_SelectionMediaTypeModelId] ON [Medias] ([SelectionMediaTypeModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_AspNetUsers_LocationId] ON [AspNetUsers] ([LocationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_AspNetUsers_ProfileMediaId] ON [AspNetUsers] ([ProfileMediaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_AspNetUsers_ResumeId] ON [AspNetUsers] ([ResumeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_CustomUrls_UserId] ON [CustomUrls] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Educations_EducationFileId] ON [Educations] ([EducationFileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Educations_EducationTierId] ON [Educations] ([EducationTierId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Educations_LocationId] ON [Educations] ([LocationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Educations_UserId] ON [Educations] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Experiences_LocationId] ON [Experiences] ([LocationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Experiences_UserId] ON [Experiences] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Files_SelectionId] ON [Files] ([SelectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Projects_ProjectFileId] ON [Projects] ([ProjectFileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Projects_ProjectTypeId] ON [Projects] ([ProjectTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Projects_UserId] ON [Projects] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Selections_TypeId] ON [Selections] ([TypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Skills_SkillLevelId] ON [Skills] ([SkillLevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    CREATE INDEX [IX_Skills_UserId] ON [Skills] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Medias_ProfileMediaId] FOREIGN KEY ([ProfileMediaId]) REFERENCES [Medias] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Resume_ResumeId] FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Locations_LocationModelId] FOREIGN KEY ([LocationModelId]) REFERENCES [Locations] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Selections_SelectionMediaStatusModelId] FOREIGN KEY ([SelectionMediaStatusModelId]) REFERENCES [Selections] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Selections_SelectionMediaTypeModelId] FOREIGN KEY ([SelectionMediaTypeModelId]) REFERENCES [Selections] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260402083443_NewSchema'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260402083443_NewSchema', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Medias_ProfileMediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'MediaId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    EXEC sp_rename N'[AspNetUsers].[ProfileMediaId]', N'ProfilePhotoId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    EXEC sp_rename N'[AspNetUsers].[IX_AspNetUsers_ProfileMediaId]', N'IX_AspNetUsers_ProfilePhotoId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Photos_ProfilePhotoId] FOREIGN KEY ([ProfilePhotoId]) REFERENCES [Photos] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403100058_updateMediaToPhotoModelForProfilePictureUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260403100058_updateMediaToPhotoModelForProfilePictureUser', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    ALTER TABLE [Resume] ADD [ResumeFileId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    ALTER TABLE [Resume] ADD [ResumeTemplateId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    CREATE INDEX [IX_Resume_ResumeFileId] ON [Resume] ([ResumeFileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    CREATE INDEX [IX_Resume_ResumeTemplateId] ON [Resume] ([ResumeTemplateId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    ALTER TABLE [Resume] ADD CONSTRAINT [FK_Resume_Files_ResumeFileId] FOREIGN KEY ([ResumeFileId]) REFERENCES [Files] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    ALTER TABLE [Resume] ADD CONSTRAINT [FK_Resume_ResumeTemplate_ResumeTemplateId] FOREIGN KEY ([ResumeTemplateId]) REFERENCES [ResumeTemplate] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260403113005_updateResumeFileId'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260403113005_updateResumeFileId', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Locations_LocationId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Photos_ProfilePhotoId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Resume_ResumeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DROP INDEX [IX_AspNetUsers_LocationId] ON [AspNetUsers];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DROP INDEX [IX_AspNetUsers_ProfilePhotoId] ON [AspNetUsers];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DROP INDEX [IX_AspNetUsers_ResumeId] ON [AspNetUsers];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'About');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [About];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Address];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Age');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Age];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LocationId');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [LocationId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Name');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Name];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Title');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Title];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    EXEC sp_rename N'[AspNetUsers].[ResumeId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    EXEC sp_rename N'[AspNetUsers].[ProfilePhotoId]', N'DiaryProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    ALTER TABLE [Spaces] ADD [DiaryProfileUserId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE TABLE [DiaryProfile] (
        [UserId] uniqueidentifier NOT NULL,
        [Id] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_DiaryProfile] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_DiaryProfile_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE TABLE [PortfolioProfile] (
        [UserId] uniqueidentifier NOT NULL,
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Age] int NULL,
        [Title] nvarchar(max) NOT NULL,
        [About] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [LocationId] uniqueidentifier NULL,
        [ResumeId] uniqueidentifier NULL,
        [ProfilePhotoId] uniqueidentifier NULL,
        CONSTRAINT [PK_PortfolioProfile] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_PortfolioProfile_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PortfolioProfile_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]),
        CONSTRAINT [FK_PortfolioProfile_Photos_ProfilePhotoId] FOREIGN KEY ([ProfilePhotoId]) REFERENCES [Photos] ([Id]),
        CONSTRAINT [FK_PortfolioProfile_Resume_ResumeId] FOREIGN KEY ([ResumeId]) REFERENCES [Resume] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE INDEX [IX_Spaces_DiaryProfileUserId] ON [Spaces] ([DiaryProfileUserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE INDEX [IX_PortfolioProfile_LocationId] ON [PortfolioProfile] ([LocationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE INDEX [IX_PortfolioProfile_ProfilePhotoId] ON [PortfolioProfile] ([ProfilePhotoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    CREATE INDEX [IX_PortfolioProfile_ResumeId] ON [PortfolioProfile] ([ResumeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    ALTER TABLE [Spaces] ADD CONSTRAINT [FK_Spaces_DiaryProfile_DiaryProfileUserId] FOREIGN KEY ([DiaryProfileUserId]) REFERENCES [DiaryProfile] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404024604_seperate_profile_for_services'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404024604_seperate_profile_for_services', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [CustomUrls] DROP CONSTRAINT [FK_CustomUrls_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Educations] DROP CONSTRAINT [FK_Educations_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Experiences] DROP CONSTRAINT [FK_Experiences_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Projects] DROP CONSTRAINT [FK_Projects_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Skills] DROP CONSTRAINT [FK_Skills_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Spaces] DROP CONSTRAINT [FK_Spaces_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Spaces] DROP CONSTRAINT [FK_Spaces_DiaryProfile_DiaryProfileUserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    DROP INDEX [IX_Spaces_DiaryProfileUserId] ON [Spaces];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Spaces]') AND [c].[name] = N'DiaryProfileUserId');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Spaces] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Spaces] DROP COLUMN [DiaryProfileUserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Spaces].[UserId]', N'DiaryProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Spaces].[IX_Spaces_UserId]', N'IX_Spaces_DiaryProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Skills].[UserId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Skills].[IX_Skills_UserId]', N'IX_Skills_PortfolioProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Projects].[UserId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Projects].[IX_Projects_UserId]', N'IX_Projects_PortfolioProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Experiences].[UserId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Experiences].[IX_Experiences_UserId]', N'IX_Experiences_PortfolioProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Educations].[UserId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[Educations].[IX_Educations_UserId]', N'IX_Educations_PortfolioProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[CustomUrls].[UserId]', N'PortfolioProfileId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    EXEC sp_rename N'[CustomUrls].[IX_CustomUrls_UserId]', N'IX_CustomUrls_PortfolioProfileId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [CustomUrls] ADD CONSTRAINT [FK_CustomUrls_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Educations] ADD CONSTRAINT [FK_Educations_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Experiences] ADD CONSTRAINT [FK_Experiences_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Skills] ADD CONSTRAINT [FK_Skills_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    ALTER TABLE [Spaces] ADD CONSTRAINT [FK_Spaces_DiaryProfile_DiaryProfileId] FOREIGN KEY ([DiaryProfileId]) REFERENCES [DiaryProfile] ([UserId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404031021_update_each_table_itsfk'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404031021_update_each_table_itsfk', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404032437_update_to_include_guid'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404032437_update_to_include_guid', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [CustomUrls] DROP CONSTRAINT [FK_CustomUrls_PortfolioProfile_PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Educations] DROP CONSTRAINT [FK_Educations_PortfolioProfile_PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Experiences] DROP CONSTRAINT [FK_Experiences_PortfolioProfile_PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Projects] DROP CONSTRAINT [FK_Projects_PortfolioProfile_PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Skills] DROP CONSTRAINT [FK_Skills_PortfolioProfile_PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Spaces] DROP CONSTRAINT [FK_Spaces_DiaryProfile_DiaryProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [PortfolioProfile] DROP CONSTRAINT [PK_PortfolioProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [DiaryProfile] DROP CONSTRAINT [PK_DiaryProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'DiaryProfileId');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [DiaryProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'PortfolioProfileId');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [PortfolioProfileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [PortfolioProfile] ADD CONSTRAINT [PK_PortfolioProfile] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [DiaryProfile] ADD CONSTRAINT [PK_DiaryProfile] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PortfolioProfile_UserId] ON [PortfolioProfile] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    CREATE UNIQUE INDEX [IX_DiaryProfile_UserId] ON [DiaryProfile] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [CustomUrls] ADD CONSTRAINT [FK_CustomUrls_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Educations] ADD CONSTRAINT [FK_Educations_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Experiences] ADD CONSTRAINT [FK_Experiences_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Skills] ADD CONSTRAINT [FK_Skills_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    ALTER TABLE [Spaces] ADD CONSTRAINT [FK_Spaces_DiaryProfile_DiaryProfileId] FOREIGN KEY ([DiaryProfileId]) REFERENCES [DiaryProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404050554_update_usermodel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404050554_update_usermodel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404093352_remove_tabletext'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_TextStyle_TextId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404093352_remove_tabletext'
)
BEGIN
    DROP TABLE [TextStyle];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404093352_remove_tabletext'
)
BEGIN
    DROP INDEX [IX_Medias_TextId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404093352_remove_tabletext'
)
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'TextId');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Medias] DROP COLUMN [TextId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404093352_remove_tabletext'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404093352_remove_tabletext', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Selections_SelectionMediaStatusModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Selections_SelectionMediaTypeModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    DROP INDEX [IX_Medias_SelectionMediaStatusModelId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    DROP INDEX [IX_Medias_SelectionMediaTypeModelId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'SelectionMediaStatusModelId');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Medias] DROP COLUMN [SelectionMediaStatusModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'SelectionMediaTypeModelId');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [Medias] DROP COLUMN [SelectionMediaTypeModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    CREATE INDEX [IX_Medias_MediaStatusSelectionId] ON [Medias] ([MediaStatusSelectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    CREATE INDEX [IX_Medias_MediaTypeSelectionId] ON [Medias] ([MediaTypeSelectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Selections_MediaStatusSelectionId] FOREIGN KEY ([MediaStatusSelectionId]) REFERENCES [Selections] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Selections_MediaTypeSelectionId] FOREIGN KEY ([MediaTypeSelectionId]) REFERENCES [Selections] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404094408_condition_mediamodel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404094408_condition_mediamodel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    ALTER TABLE [Projects] DROP CONSTRAINT [FK_Projects_Selections_ProjectTypeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    DROP INDEX [IX_Projects_ProjectTypeId] ON [Projects];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'ProjectTypeId');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [Projects] DROP COLUMN [ProjectTypeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'SelectionId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [Projects] DROP COLUMN [SelectionId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    ALTER TABLE [Videos] ADD [ProjectModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    ALTER TABLE [Photos] ADD [ProjectModelId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    CREATE INDEX [IX_Videos_ProjectModelId] ON [Videos] ([ProjectModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    CREATE INDEX [IX_Photos_ProjectModelId] ON [Photos] ([ProjectModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    ALTER TABLE [Photos] ADD CONSTRAINT [FK_Photos_Projects_ProjectModelId] FOREIGN KEY ([ProjectModelId]) REFERENCES [Projects] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Projects_ProjectModelId] FOREIGN KEY ([ProjectModelId]) REFERENCES [Projects] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404102012_update_projectmodel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404102012_update_projectmodel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404121225_MakeFileModelSelectionIdNullable'
)
BEGIN
    ALTER TABLE [Files] DROP CONSTRAINT [FK_Files_Selections_SelectionId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404121225_MakeFileModelSelectionIdNullable'
)
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'MediaId');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [Photos] ALTER COLUMN [MediaId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404121225_MakeFileModelSelectionIdNullable'
)
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Files]') AND [c].[name] = N'SelectionId');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Files] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [Files] ALTER COLUMN [SelectionId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404121225_MakeFileModelSelectionIdNullable'
)
BEGIN
    ALTER TABLE [Files] ADD CONSTRAINT [FK_Files_Selections_SelectionId] FOREIGN KEY ([SelectionId]) REFERENCES [Selections] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404121225_MakeFileModelSelectionIdNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404121225_MakeFileModelSelectionIdNullable', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    ALTER TABLE [Resume] DROP CONSTRAINT [FK_Resume_Files_ResumeFileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    ALTER TABLE [Resume] DROP CONSTRAINT [FK_Resume_ResumeTemplate_ResumeTemplateId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DROP INDEX [IX_Resume_ResumeFileId] ON [Resume];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DROP INDEX [IX_Resume_ResumeTemplateId] ON [Resume];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DROP INDEX [IX_PortfolioProfile_LocationId] ON [PortfolioProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DROP INDEX [IX_PortfolioProfile_ProfilePhotoId] ON [PortfolioProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DROP INDEX [IX_PortfolioProfile_ResumeId] ON [PortfolioProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Resume]') AND [c].[name] = N'ResumeFileId');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Resume] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [Resume] DROP COLUMN [ResumeFileId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Resume]') AND [c].[name] = N'ResumeTemplateId');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Resume] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [Resume] DROP COLUMN [ResumeTemplateId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Resume_FileId] ON [Resume] ([FileId]) WHERE [FileId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Resume_TemplateId] ON [Resume] ([TemplateId]) WHERE [TemplateId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_PortfolioProfile_LocationId] ON [PortfolioProfile] ([LocationId]) WHERE [LocationId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_PortfolioProfile_ProfilePhotoId] ON [PortfolioProfile] ([ProfilePhotoId]) WHERE [ProfilePhotoId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_PortfolioProfile_ResumeId] ON [PortfolioProfile] ([ResumeId]) WHERE [ResumeId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    ALTER TABLE [Resume] ADD CONSTRAINT [FK_Resume_Files_FileId] FOREIGN KEY ([FileId]) REFERENCES [Files] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    ALTER TABLE [Resume] ADD CONSTRAINT [FK_Resume_ResumeTemplate_TemplateId] FOREIGN KEY ([TemplateId]) REFERENCES [ResumeTemplate] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404123207_AddExplicitRelationshipConfigs'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404123207_AddExplicitRelationshipConfigs', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    ALTER TABLE [Photos] DROP CONSTRAINT [FK_Photos_Medias_MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    ALTER TABLE [Photos] DROP CONSTRAINT [FK_Photos_Projects_ProjectModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Medias_MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Projects_ProjectModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DROP INDEX [IX_Videos_MediaId] ON [Videos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DROP INDEX [IX_Videos_ProjectModelId] ON [Videos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DROP INDEX [IX_Photos_MediaId] ON [Photos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DROP INDEX [IX_Photos_ProjectModelId] ON [Photos];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'MediaId');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [Videos] DROP COLUMN [MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Videos]') AND [c].[name] = N'ProjectModelId');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Videos] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [Videos] DROP COLUMN [ProjectModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'MediaId');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [Photos] DROP COLUMN [MediaId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'ProjectModelId');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [Photos] DROP COLUMN [ProjectModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE TABLE [MediaPhotoModel] (
        [MediaModelId] uniqueidentifier NOT NULL,
        [PhotoModelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MediaPhotoModel] PRIMARY KEY ([MediaModelId], [PhotoModelId]),
        CONSTRAINT [FK_MediaPhotoModel_Medias_MediaModelId] FOREIGN KEY ([MediaModelId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MediaPhotoModel_Photos_PhotoModelId] FOREIGN KEY ([PhotoModelId]) REFERENCES [Photos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE TABLE [MediaVideoModel] (
        [MediaModelId] uniqueidentifier NOT NULL,
        [VideoModelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MediaVideoModel] PRIMARY KEY ([MediaModelId], [VideoModelId]),
        CONSTRAINT [FK_MediaVideoModel_Medias_MediaModelId] FOREIGN KEY ([MediaModelId]) REFERENCES [Medias] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MediaVideoModel_Videos_VideoModelId] FOREIGN KEY ([VideoModelId]) REFERENCES [Videos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE TABLE [ProjectPhotoModel] (
        [ProjectModelId] uniqueidentifier NOT NULL,
        [PhotoModelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ProjectPhotoModel] PRIMARY KEY ([ProjectModelId], [PhotoModelId]),
        CONSTRAINT [FK_ProjectPhotoModel_Photos_PhotoModelId] FOREIGN KEY ([PhotoModelId]) REFERENCES [Photos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProjectPhotoModel_Projects_ProjectModelId] FOREIGN KEY ([ProjectModelId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE TABLE [ProjectVideoModel] (
        [ProjectModelId] uniqueidentifier NOT NULL,
        [VideoModelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ProjectVideoModel] PRIMARY KEY ([ProjectModelId], [VideoModelId]),
        CONSTRAINT [FK_ProjectVideoModel_Projects_ProjectModelId] FOREIGN KEY ([ProjectModelId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProjectVideoModel_Videos_VideoModelId] FOREIGN KEY ([VideoModelId]) REFERENCES [Videos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE INDEX [IX_MediaPhotoModel_PhotoModelId] ON [MediaPhotoModel] ([PhotoModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE INDEX [IX_MediaVideoModel_VideoModelId] ON [MediaVideoModel] ([VideoModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE INDEX [IX_ProjectPhotoModel_PhotoModelId] ON [ProjectPhotoModel] ([PhotoModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    CREATE INDEX [IX_ProjectVideoModel_VideoModelId] ON [ProjectVideoModel] ([VideoModelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260404150203_AddMediaPhotoVideoJoinTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260404150203_AddMediaPhotoVideoJoinTables', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260405140635_update_constraint_titlespace_for_differentuseronly'
)
BEGIN
    DROP INDEX [IX_Spaces_DiaryProfileId] ON [Spaces];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260405140635_update_constraint_titlespace_for_differentuseronly'
)
BEGIN
    DROP INDEX [IX_Spaces_Title] ON [Spaces];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260405140635_update_constraint_titlespace_for_differentuseronly'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Spaces_DiaryProfileId_Title] ON [Spaces] ([DiaryProfileId], [Title]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260405140635_update_constraint_titlespace_for_differentuseronly'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260405140635_update_constraint_titlespace_for_differentuseronly', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260410224935_addDiaryProfileandPortfolio'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260410224935_addDiaryProfileandPortfolio', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260417014615_updateResumeLogic'
)
BEGIN
    DROP INDEX [IX_Resume_FileId] ON [Resume];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260417014615_updateResumeLogic'
)
BEGIN
    DROP INDEX [IX_Resume_TemplateId] ON [Resume];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260417014615_updateResumeLogic'
)
BEGIN
    CREATE INDEX [IX_Resume_FileId] ON [Resume] ([FileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260417014615_updateResumeLogic'
)
BEGIN
    CREATE INDEX [IX_Resume_TemplateId] ON [Resume] ([TemplateId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260417014615_updateResumeLogic'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260417014615_updateResumeLogic', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260418162232_renamingSkillModel'
)
BEGIN
    CREATE INDEX [IX_Skills_SelectionId] ON [Skills] ([SelectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260418162232_renamingSkillModel'
)
BEGIN
    ALTER TABLE [Skills] ADD CONSTRAINT [FK_Skills_Selections_SelectionId] FOREIGN KEY ([SelectionId]) REFERENCES [Selections] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260418162232_renamingSkillModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260418162232_renamingSkillModel', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    EXEC sp_rename N'[Locations].[Name]', N'AddressLine2', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'Longitude');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [Locations] ALTER COLUMN [Longitude] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    DECLARE @var37 sysname;
    SELECT @var37 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'Latitude');
    IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var37 + '];');
    ALTER TABLE [Locations] ALTER COLUMN [Latitude] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    ALTER TABLE [Locations] ADD [AddressLine1] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    ALTER TABLE [Locations] ADD [PostalCodeId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE TABLE [States] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_States] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE TABLE [Cities] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [StateId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Cities_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [States] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE TABLE [PostalCodes] (
        [Id] uniqueidentifier NOT NULL,
        [PostalNumber] nvarchar(max) NOT NULL,
        [CityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PostalCodes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PostalCodes_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE INDEX [IX_Locations_PostalCodeId] ON [Locations] ([PostalCodeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE INDEX [IX_Cities_StateId] ON [Cities] ([StateId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    CREATE INDEX [IX_PostalCodes_CityId] ON [PostalCodes] ([CityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    ALTER TABLE [Locations] ADD CONSTRAINT [FK_Locations_PostalCodes_PostalCodeId] FOREIGN KEY ([PostalCodeId]) REFERENCES [PostalCodes] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260419153957_updateLocationModelAndAddAttribute'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260419153957_updateLocationModelAndAddAttribute', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260420042830_updateLocationModelAndAddAttributeDouble'
)
BEGIN
    DECLARE @var38 sysname;
    SELECT @var38 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'Longitude');
    IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var38 + '];');
    ALTER TABLE [Locations] ALTER COLUMN [Longitude] float NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260420042830_updateLocationModelAndAddAttributeDouble'
)
BEGIN
    DECLARE @var39 sysname;
    SELECT @var39 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'Latitude');
    IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var39 + '];');
    ALTER TABLE [Locations] ALTER COLUMN [Latitude] float NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260420042830_updateLocationModelAndAddAttributeDouble'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260420042830_updateLocationModelAndAddAttributeDouble', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    ALTER TABLE [Medias] DROP CONSTRAINT [FK_Medias_Locations_LocationModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    DROP INDEX [IX_Medias_LocationModelId] ON [Medias];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    DECLARE @var40 sysname;
    SELECT @var40 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medias]') AND [c].[name] = N'LocationModelId');
    IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Medias] DROP CONSTRAINT [' + @var40 + '];');
    ALTER TABLE [Medias] DROP COLUMN [LocationModelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Medias_LocationId] ON [Medias] ([LocationId]) WHERE [LocationId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    ALTER TABLE [Medias] ADD CONSTRAINT [FK_Medias_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE SET NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260421230333_FixLocationForeignKey'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260421230333_FixLocationForeignKey', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422044326_updateResumeToIncludePortfolioProfile'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260422044326_updateResumeToIncludePortfolioProfile', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    ALTER TABLE [PortfolioProfile] DROP CONSTRAINT [FK_PortfolioProfile_Resume_ResumeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    DROP INDEX [IX_PortfolioProfile_ResumeId] ON [PortfolioProfile];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    DECLARE @var41 sysname;
    SELECT @var41 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PortfolioProfile]') AND [c].[name] = N'ResumeId');
    IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [PortfolioProfile] DROP CONSTRAINT [' + @var41 + '];');
    ALTER TABLE [PortfolioProfile] DROP COLUMN [ResumeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    ALTER TABLE [Resume] ADD [PortfolioProfileId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Resume_PortfolioProfileId] ON [Resume] ([PortfolioProfileId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    ALTER TABLE [Resume] ADD CONSTRAINT [FK_Resume_PortfolioProfile_PortfolioProfileId] FOREIGN KEY ([PortfolioProfileId]) REFERENCES [PortfolioProfile] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260422050125_updateResumeToIncludePortfolioProfile2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260422050125_updateResumeToIncludePortfolioProfile2', N'9.0.10');
END;

COMMIT;
GO

