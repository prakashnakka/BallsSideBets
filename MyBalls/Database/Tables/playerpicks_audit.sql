USE [balls]
GO

/****** Object:  Table [dbo].[playerpicks_audit]    Script Date: 10/30/2020 8:21:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[playerpicks_audit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CombinedScore] [int] NULL,
	[FirstInningWickets] [int] NULL,
	[SecondInningWickets] [int] NULL,
	[HighestScore] [int] NULL,
	[HighestWickets] [int] NULL,
	[OversChase] [decimal](4,1) NULL,
	[Total4s] [int] NULL,
	[Total6s] [int] NULL,
	[TeamPick] [int] NULL,
	[MaxSingleOverScore] [int] NULL,
	[userId] [nvarchar](50) NULL,
	[addDt] [datetime] NULL,
 CONSTRAINT [PK_playerpicks_audit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


