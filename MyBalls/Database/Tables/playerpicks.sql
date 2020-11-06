USE [balls]
GO

/****** Object:  Table [dbo].[playerpicks]    Script Date: 10/30/2020 8:20:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[playerpicks](
	[CombinedScore] [int] NULL,
	[FirstInningWickets] [int] NULL,
	[SecondInningWickets] [int] NULL,
	[HighestScore] [int] NULL,
	[HighestWickets] [int] NULL,
	[OversChase] [decimal](4,1) NULL,
	[Total4s] [int] NULL,
	[Total6s] [int] NULL,
	[TeamPick] [int] NULL,
	[userId] [nvarchar](50) NOT NULL,
	[addDt] [datetime] NULL,
	[updateDt] [datetime] NULL,
 CONSTRAINT [PK_playerpicks] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


