USE [ExamRef70487-AccessingData]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 6/25/2020 12:48:11 PM ******/
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
