IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[InsertUserAccount]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[InsertUserAccount]
END
GO

/****** Object:  StoredProcedure [dbo].[InsertUserAccount]    Script Date: 10/30/2020 8:23:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUserAccount] 
	-- Add the parameters for the stored procedure here
	@Email varchar(50),
	@Pwd varchar(100),
	@displayname varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @myId int;
	BEGIN TRANSACTION
    -- Insert statements for procedure here

	INSERT INTO UserAccount(email, pwd, displayname)
	VALUES(
	@email, @pwd, @displayname)

	SELECT @myId = SCOPE_IDENTITY()

	INSERT INTO POINTS (userid)
	values (@myId)

	COMMIT TRANSACTION

	select * from UserAccount where UserId = @myId;
END
GO


