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

INSERT INTO [dbo].[TEXT] ([TextContent], [TextLength])
VALUES (N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.'
        , N'574');
GO

COMMIT;
GO

