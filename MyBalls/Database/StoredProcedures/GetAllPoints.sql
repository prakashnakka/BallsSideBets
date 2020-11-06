IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[GetAllPoints]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[GetAllPoints]
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPoints]    Script Date: 10/30/2020 8:23:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPoints] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		p.userId,
		displayname, 
		CombinedScore,
		FirstInningWickets,
		SecondInningWickets,
		HighestScore,
		HighestWickets,
		OversChase,
		Total4s,
		Total6s,
		TeamPick,
		MaxSingleOverScore,
		(CombinedScore + FirstInningWickets + SecondInningWickets + HighestScore + HighestWickets + OversChase + Total4s + Total6s + TeamPick + MaxSingleOverScore) as TotalPoints
	from points p 
	inner join useraccount ua on p.userId = ua.userId
	order by TotalPoints desc, displayname;
END


GO


