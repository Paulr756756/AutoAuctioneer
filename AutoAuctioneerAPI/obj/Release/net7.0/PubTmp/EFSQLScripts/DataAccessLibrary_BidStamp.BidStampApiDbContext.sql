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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230610150239_Initial Migration')
BEGIN
    CREATE TABLE [Bids] (
        [BidId] uniqueidentifier NOT NULL,
        [StampId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [BidAmount] decimal(18,2) NOT NULL,
        [BidTime] datetime2 NOT NULL,
        CONSTRAINT [PK_Bids] PRIMARY KEY ([BidId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230610150239_Initial Migration')
BEGIN
    CREATE TABLE [Stamps] (
        [StampId] uniqueidentifier NOT NULL,
        [StampTitle] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [ImageUrl] nvarchar(max) NULL,
        [Year] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [Condition] nvarchar(max) NULL,
        [CatalogNumber] nvarchar(max) NULL,
        [StartingBid] int NULL,
        [EndingBid] int NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_Stamps] PRIMARY KEY ([StampId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230610150239_Initial Migration')
BEGIN
    CREATE TABLE [Users] (
        [UserId] uniqueidentifier NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [RegistrationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230610150239_Initial Migration')
BEGIN
    CREATE TABLE [WatchLists] (
        [WatchlistId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [StampId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_WatchLists] PRIMARY KEY ([WatchlistId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230610150239_Initial Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230610150239_Initial Migration', N'7.0.5');
END;
GO

COMMIT;
GO

