USE [balls]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_PlayerPicked]    Script Date: 1/17/2017 12:23:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_PlayerPicked] 
(
	-- Add the parameters for the function here
	@userId int
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @pickCount int
	DECLARE @isPicked bit

	-- Add the T-SQL statements to compute the return value here
	SELECT @pickCount = count(*) from playerpicks where userId = @userId

	-- Return the result of the function
	if (@pickCount > 0)
	BEGIN
		SET @isPicked = 1
	END
	ELSE
	BEGIN
		SET @isPicked = 0
	END 
	
	RETURN @isPicked
END

