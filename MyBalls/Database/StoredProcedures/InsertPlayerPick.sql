IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[InsertPlayerPick]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[InsertPlayerPick]
END
GO

/****** Object:  StoredProcedure [dbo].[InsertPlayerPick]    Script Date: 10/30/2020 8:23:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPlayerPick] 
	-- Add the parameters for the stored procedure here
	@combinedscore int,
	@firstInningWickets int,
	@secondinningwickets int,
	@HighestScore int,
	@highestWickets int,
	@overschase float,
	@Total4s int,
	@Total6s int,
	@TeamPick int,
	@MaxSingleOverScore int,
    @userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @myId int;
	BEGIN TRANSACTION
    -- Insert statements for procedure here

	if exists(select * from playerpicks where userId = @userId)
	BEGIN
		delete from playerpicks where userId = @userId;
	END
	

	Insert into playerpicks (
			[CombinedScore], 
			[FirstInningWickets], 
			[SecondInningWickets], 
			[HighestScore], 
			[HighestWickets], 
			[OversChase], 
			[Total4s], 
			[Total6s], 
			[TeamPick], 
			[MaxSingleOverScore],
			[userId], 
			[addDt]) 
		values(
			@CombinedScore, 
			@FirstInningWickets, 
			@SecondInningWickets, 
			@HighestScore, 
			@HighestWickets, 
			@OversChase, 
			@Total4s, 
			@Total6s, 
			@TeamPick, 
			@MaxSingleOverScore,
			@userId, 
			GetDate())

	COMMIT TRANSACTION

END
GO


