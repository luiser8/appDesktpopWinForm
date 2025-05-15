-- Table to store product information
CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NULL, 
    [Price] DECIMAL NULL, 
    [Stock] INT NULL, 
    [Unit] INT NULL, 
    [Category] VARCHAR(50) NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Table to store category information
CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CategoryItem] VARCHAR(50) NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Table to track the history of stock updates
CREATE TABLE [dbo].[History]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProductID] INT NULL, 
    [AddedStocks] INT NULL, 
    [Date] DATETIME NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Table to store items in the shopping cart
CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NULL, 
    [Price] INT NULL, 
    [Quantity] INT NULL,
    [ProductId] INT NULL,
    [Uid] INT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_PRODUCT_ID FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Table to store transaction information
CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Date] DATETIME NULL, 
    [Subtotal] VARCHAR(50) NULL, 
    [Cash] VARCHAR(50) NULL, 
    [DiscountPercent] VARCHAR(50) NULL, 
    [DiscountAmount] VARCHAR(50) NULL, 
    [Total] VARCHAR(50) NULL, 
    [Change] VARCHAR(50) NULL, 
    [TransactionId] VARCHAR(MAX) NULL,
    [Uid] INT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Table to store ordered items in a transaction
CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    --[TransactionId] INT NULL,
	[TransactionId] VARCHAR(MAX) NULL,
    [Name] VARCHAR(50) NULL, 
    [Price] VARCHAR(50) NULL, 
    [Quantity] INT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	--CONSTRAINT FK_TRANSACTION_ID FOREIGN KEY (TransactionId) REFERENCES dbo.Transactions (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Creates a table to store rol information
CREATE TABLE [dbo].[Rol]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [RolName] VARCHAR(100) NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Creates a table to store user information
CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[RolId] INT NOT NULL,
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_ROL_ID FOREIGN KEY (RolId) REFERENCES dbo.rol (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

