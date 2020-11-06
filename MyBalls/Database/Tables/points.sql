USE [balls]
GO

/****** Object:  Table [dbo].[points]    Script Date: 10/30/2020 8:22:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[points](
	[CombinedScore] [int] NULL,
	[FirstInningWickets] [int] NULL,
	[SecondInningWickets] [int] NULL,
	[HighestScore] [int] NULL,
	[HighestWickets] [int] NULL,
	[OversChase] [int] NULL,
	[Total4s] [int] NULL,
	[Total6s] [int] NULL,
	[TeamPick] [int] NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_points] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_CombinedScore]  DEFAULT ((0)) FOR [CombinedScore]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_FirstInningWickets]  DEFAULT ((0)) FOR [FirstInningWickets]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_SecondInningWickets]  DEFAULT ((0)) FOR [SecondInningWickets]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_HighestScore]  DEFAULT ((0)) FOR [HighestScore]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_HighestWickets]  DEFAULT ((0)) FOR [HighestWickets]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_OversChase]  DEFAULT ((0)) FOR [OversChase]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_Total4s]  DEFAULT ((0)) FOR [Total4s]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_Total6s]  DEFAULT ((0)) FOR [Total6s]
GO

ALTER TABLE [dbo].[points] ADD  CONSTRAINT [DF_points_TeamPick]  DEFAULT ((0)) FOR [TeamPick]
GO


