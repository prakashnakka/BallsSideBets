USE [balls]
GO

/****** Object:  Table [dbo].[useraccount]    Script Date: 10/30/2020 8:23:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[useraccount](
	[userId] [int] IDENTITY(1267,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[pwd] [nvarchar](100) NOT NULL,
	[displayname] [nvarchar](100) NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[useraccount] ADD  CONSTRAINT [DF_useraccount_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO


