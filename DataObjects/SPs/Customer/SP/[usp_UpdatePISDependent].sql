USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISDependent]    Script Date: 3/6/2017 1:32:02 PM ******/
IF OBJECT_ID('[usp_UpdatePISDependent]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISDependent]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePISDependent]
	@ID varchar(100),
	@PISID varchar(100),
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@LastName varchar(100),
	@GenderID varchar(100),
	@StreetAddress varchar(100),
	@CityID varchar(100),
	@RelationShipTypeID varchar(100),
	@BirthDate varchar(100),
	@SchoolAddress varchar(100),
	@ContactNo varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @ret int = 0		
	
	Insert into pis_dependent(ID,PIS_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,GENDER_ID,STREET_ADDRESS,CITY_ID,RELATIONSHIP_TYPE_ID,BIRTH_DATE,SCHOOL_ADDRESS,CONTACT_NO)
	Select @ID,@PISID,@FirstName,@MiddleName,@LastName,@GenderID,@StreetAddress,@CityID,@RelationShipTypeID,@BirthDate,@SchoolAddress,@ContactNo
	Set @ret = 1
	
	Select @ret
END
