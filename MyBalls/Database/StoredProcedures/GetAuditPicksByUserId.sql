IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[GetAuditPicksByUserId]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[GetAuditPicksByUserId]
END
GO

/****** Object:  StoredProcedure [dbo].[GetAuditPicksByUserId]    Script Date: 10/30/2020 8:23:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAuditPicksByUserId] 
	-- Add the parameters for the stored procedure here
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from playerpicks_audit a 
	where a.userId = @userId
	order by addDt desc
END


GO


