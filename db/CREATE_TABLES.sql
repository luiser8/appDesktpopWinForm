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

-- Creates a table to store banks information
CREATE TABLE [dbo].[Banks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Name] VARCHAR(155) NOT NULL,
	[Abbreviation] VARCHAR(15) NOT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Creates a table to store pay methods information
CREATE TABLE [dbo].[PayMethods]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Name] VARCHAR(155) NOT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Table to store invoice information
CREATE TABLE [dbo].[Invoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[InvoiceId] VARCHAR(MAX) NULL,
	[PayMethodId] INT NOT NULL,
	[BankId] INT NOT NULL,
    [Date] DATETIME NULL, 
    [Subtotal] VARCHAR(50) NULL, 
    [Cash] VARCHAR(50) NULL, 
    [DiscountPercent] VARCHAR(50) NULL, 
    [DiscountAmount] VARCHAR(50) NULL, 
    [Total] VARCHAR(50) NULL, 
    [Change] VARCHAR(50) NULL, 
    [Uid] INT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_PAY_METHOD_ID FOREIGN KEY (PayMethodId) REFERENCES dbo.PayMethods (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT FK_BANK_ID FOREIGN KEY (BankId) REFERENCES dbo.Banks (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Table to store ordered items in a transaction
CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[InvoiceId] VARCHAR(MAX) NULL,
    [Name] VARCHAR(50) NULL, 
    [Price] VARCHAR(50) NULL, 
    [Quantity] INT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
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
	[FirstName] VARCHAR(155) NOT NULL,
	[LastName] VARCHAR(155) NOT NULL,
    [Username] VARCHAR(50) NOT NULL UNIQUE, 
    [Password] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NULL UNIQUE,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_ROL_ID FOREIGN KEY (RolId) REFERENCES dbo.Rol (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Creates a table to store audit information
CREATE TABLE [dbo].[Audit]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] INT NOT NULL,
	[Actions] VARCHAR(155) NOT NULL,
	[Tables] VARCHAR(15) NOT NULL,
	[Events] VARCHAR(155) NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_AUDIT_ID FOREIGN KEY (UserId) REFERENCES dbo.Account (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

-- Creates a table to store policies information
CREATE TABLE [dbo].[Policies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Name] VARCHAR(155) NOT NULL,
	[Description] VARCHAR(155) NOT NULL,
	[Status] TINYINT NOT NULL DEFAULT 1,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);

-- Creates a table to store rolpolicies information
CREATE TABLE [dbo].[RolPolicies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[RolId] INT NOT NULL,
	[PolicyId] INT NOT NULL,
	CONSTRAINT FK_ROL_POLICY_ID FOREIGN KEY (RolId) REFERENCES dbo.Rol (Id) ON DELETE CASCADE ON UPDATE NO ACTION,
	CONSTRAINT FK_POLICY_ID FOREIGN KEY (PolicyId) REFERENCES dbo.Policies (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);
