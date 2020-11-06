IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[UpdateTeamPick]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[UpdateTeamPick]
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTeamPick]    Script Date: 10/30/2020 8:25:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTeamPick] 
	-- Add the parameters for the stored procedure here
	@gameId int,
	@intVal int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
    -- Insert statements for procedure here
	
	update results
		set
			Teampick = @intval
		where gameId = @gameId;

		declare @winnerCount int;

	select @winnerCount = count(*) from playerpicks where teamPick <> @intval;

	--print @winnerCount;
	update points set teampick = 0;
	
	update points
	set
		points.teampick = @winnerCount
	from points p
	inner join playerpicks pp on (p.userId = pp.userId and pp.TeamPick = @intVal);

	COMMIT TRANSACTION

END
GO


