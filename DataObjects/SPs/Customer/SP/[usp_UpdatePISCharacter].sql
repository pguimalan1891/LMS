USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISCharacter]    Script Date: 3/6/2017 1:32:01 PM ******/
IF OBJECT_ID('[usp_UpdatePISCharacter]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISCharacter]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePISCharacter]
	@ID varchar(100),
	@PISID varchar(100),
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@LastName varchar(100),
	@RelationShip varchar(100),
	@StreetAddress varchar(100),
	@CityID varchar(100),
	@ContactNo varchar(100)
	
AS
BEGIN
	SET NOCOUNT ON;
	Declare @ret int = 0
	
	Insert into pis_character(ID,PIS_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,RELATIONSHIP,STREET_ADDRESS,CITY_ID,CONTACT_NO)
	Select @ID,@PISID,@FirstName,@MiddleName,@LastName,@RelationShip,@StreetAddress,@CityID,@ContactNo
	Set @ret = 1
	
	Select @ret
END
