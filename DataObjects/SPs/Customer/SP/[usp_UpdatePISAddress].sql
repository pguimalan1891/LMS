USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISAddress]    Script Date: 3/6/2017 1:31:59 PM ******/
IF OBJECT_ID('[usp_UpdatePISAddress]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISAddress]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePISAddress]
	@ID varchar(100),
	@PISID varchar(100),
	@AddressTypeID varchar(100),
	@StreetAddress varchar(100),
	@BarangayName varchar(100),
	@CityID varchar(100),
	@PostalCode varchar(100),
	@PhoneNumber varchar(100),
	@MobileNumber varchar(100),
	@ResidentDate varchar(100),
	@HomeOwnershipID varchar(100)

AS
BEGIN
	SET NOCOUNT ON;
	Declare @ret int = 0		
	
	Insert into pis_address (ID,PIS_ID,ADDRESS_TYPE_ID,STREET_ADDRESS,BARANGAY_NAME,CITY_ID,POSTAL_CODE,PHONE_NUMBER,MOBILE_NUMBER,RESIDENT_DATE,HOME_OWNERSHIP_ID)
	Select @ID,@PISID,@AddressTypeID,@StreetAddress,@BarangayName,@CityID,@PostalCode,@PhoneNumber,@MobileNumber,@ResidentDate,@HomeOwnershipID
	Set @ret = 1
	
	Select @ret
END
