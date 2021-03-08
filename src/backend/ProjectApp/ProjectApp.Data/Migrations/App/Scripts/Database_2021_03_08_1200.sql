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
GO

CREATE TABLE [dbo].[TEXT] (
    [TextId] int NOT NULL IDENTITY,
    [TextContent] nvarchar(1000) NOT NULL,
    [TextLength] int NOT NULL,
    CONSTRAINT [PK_TEXT] PRIMARY KEY ([TextId])
);
GO

CREATE TABLE [dbo].[USER_PROFILE] (
    [UserId] int NOT NULL,
    [UserType] int NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Username] nvarchar(max) NULL,
    [Email] nvarchar(100) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_USER_PROFILE] PRIMARY KEY ([UserId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210308105526_Initial_App_Database', N'5.0.3');
GO

COMMIT;
GO

