IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[UpdateResultsPick]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[UpdateResultsPick]
END
GO

/****** Object:  StoredProcedure [dbo].[UpdateResultsPick]    Script Date: 10/30/2020 8:23:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateResultsPick] 
	-- Add the parameters for the stored procedure here
	@categoryId int,
	@gameId int,
	@intVal int = null,
	@floatVal float = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @myId int;
	BEGIN TRANSACTION
    -- Insert statements for procedure here
	DECLARE @fieldName varchar(30);
	if (@categoryId = 0)
	BEGIN
		set @fieldName = 'CombinedScore';
	END

	if (@categoryId = 1)
	BEGIN
		set @fieldName = 'FirstInningWickets';
	END

	if (@categoryId = 2)
	BEGIN
		set @fieldName = 'SecondInningWickets';
	END

	if (@categoryId = 3)
	BEGIN
		set @fieldName = 'HighestScore';
	END

	if (@categoryId = 4)
	BEGIN
		set @fieldName = 'HighestWickets';
	END

	if (@categoryId = 5)
	BEGIN
		set @fieldName = 'OversChase';
	END

	if (@categoryId = 6)
	BEGIN
		set @fieldName = 'Total4s';
	END

	if (@categoryId = 7)
	BEGIN
		set @fieldName = 'Total6s';
	END

	if (@categoryId = 9)
	BEGIN
		set @fieldName = 'MaxSingleOverScore';
	END

	--update results 
	--set 
	--	firstinningwickets = @intVal
	--where
	--	gameId = @gameId;


		Declare @sql1 nvarchar(500), @sql2 nvarchar(500), @sql3 nvarchar(500);
		set @sql1 = 'update results set ' + @fieldName + '= ''' + convert(varchar(4), @intVal) + ''' where gameId = ''' + 
		convert(varchar(4), @gameId) + '''';

		if (@categoryId = 5)
		BEGIN
			set @sql1 = 'update results set ' + @fieldName + '= ''' + convert(varchar(4), @floatVal) + ''' where gameId = ''' + 
			convert(varchar(4), @gameId) + '''';
		END
		
		exec(@sql1);

	
	Create table #tbl1(userId int not null, playerPickAbsVal int not null, points int null)

	set @sql2 = 'insert into #tbl1(userId, playerPickAbsVal) ' + 
	'select userId, abs(' + @fieldName + ' - ' + convert(varchar(4), @intval) + ') as ppAbs from playerpicks order by ppAbs desc';

	if (@categoryId = 5)
	BEGIN
		set @sql2 = 'insert into #tbl1(userId, playerPickAbsVal) ' + 

		'select userId, abs(Floor(OversChase)*6 + (OversChase - Floor(OversChase))*10 - ' + convert(varchar(4), Floor(@floatval)*6 + (@floatval - Floor(@floatval))*10) + ') as ppAbs from playerpicks order by ppAbs desc';

	END
	exec (@sql2);

	create table #tbl2(id int identity(1,1), pointVals int, points int null);

	insert into #tbl2 (pointVals)
	select distinct playerPickAbsVal from #tbl1 order by playerPickAbsVal;

	update #tbl2
	set 
		points = 15 - id + 1;

	select * from #tbl2;

	update #tbl1 
	set 
		#tbl1.points = b.points
	from
		#tbl1 a
		inner join #tbl2 b on
		a.playerPickAbsVal = b.pointVals;
		

		set @sql3 = 'update points set points.' + @fieldName + ' = b.points from points a inner join #tbl1 b on a.userId = b.userId';
		exec(@sql3);

	drop table #tbl1, #tbl2; 



	COMMIT TRANSACTION

END
GO


