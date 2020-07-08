USE [ExamRef70487-AccessingData]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 7/8/2020 2:35:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountAlias] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/8/2020 2:35:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AccountId] [int] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 7/8/2020 2:35:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Ammount] [smallmoney] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionDetail]    Script Date: 7/8/2020 2:35:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionDetail](
	[TrasactionDetailId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [int] NOT NULL,
	[Vendor] [nvarchar](50) NOT NULL,
	[ItemDescription] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_TransactionDetail] PRIMARY KEY CLUSTERED 
(
	[TrasactionDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_TransactionDate]  DEFAULT (getutcdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Account]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Customer]
GO
ALTER TABLE [dbo].[TransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDetail_Transaction] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Transaction] ([TransactionId])
GO
ALTER TABLE [dbo].[TransactionDetail] CHECK CONSTRAINT [FK_TransactionDetail_Transaction]
GO

INSERT INTO [dbo].[Account]
           ([AccountAlias])
     VALUES
           (N'TestAccount')
GO

INSERT INTO [dbo].[Customer]
           ([FirstName]
           ,[LastName]
           ,[AccountId])
     VALUES
           (N'Test'
           ,N'Customer1'
           ,1)
GO

INSERT INTO [dbo].[Customer]
           ([FirstName]
           ,[LastName]
           ,[AccountId])
     VALUES
           (N'Test'
           ,N'Customer2'
           ,1)
GO

INSERT INTO [dbo].[Customer]
           ([FirstName]
           ,[LastName]
           ,[AccountId])
     VALUES
           (N'Test'
           ,N'Customer3'
           ,1)
GO
