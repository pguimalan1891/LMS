USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISEmployment]    Script Date: 3/6/2017 1:32:05 PM ******/
IF OBJECT_ID('[usp_UpdatePISEmployment]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISEmployment]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePISEmployment]
	@ID varchar(100),
	@PISID varchar(100),
	@BusinessTypeID varchar(100),
	@EmployerName varchar(100),
	@Income varchar(100),
	@ContactNo varchar(100),
	@FromDate varchar(100),
	@ToDate varchar(100),
	@IsSpouse varchar(100),
	@NatureOfBusinessID varchar(100)

AS
BEGIN
	SET NOCOUNT ON;	
	Declare @IsACtive varchar(10) = 'N'
	Declare @ret int = 0
	if @ToDate = '' or @ToDate is null
		Begin
			Set @IsACtive = 'Y'
		End
	
	Insert into pis_employment (ID,PIS_ID,BUSINESS_TYPE_ID,EMPLOYER_NAME,INCOME,CONTACT_NO,FROM_DATE,TO_DATE,IS_ACTIVE,IS_SPOUSE,NATURE_OF_BUSINESS_ID)
	Select @ID,@PISID,@BusinessTypeID,@EmployerName,@Income,@ContactNo,@FromDate,@ToDate,@IsACtive,@IsSpouse,@NatureOfBusinessID
	Set @ret = 1
	
	Select @ret
END
