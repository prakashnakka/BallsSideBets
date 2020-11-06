USE [balls]
GO

/****** Object:  Table [dbo].[results]    Script Date: 10/30/2020 8:22:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[results](
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
	[gameId] [int] NOT NULL,
 CONSTRAINT [PK_results] PRIMARY KEY CLUSTERED 
(
	[gameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


