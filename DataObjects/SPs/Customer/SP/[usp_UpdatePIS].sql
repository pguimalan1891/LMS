USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePIS]    Script Date: 3/6/2017 1:31:42 PM ******/
IF OBJECT_ID('[usp_UpdatePIS]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePIS]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePIS]
	@ID varchar(100),
	@Code varchar(100),
	@OrganizationID varchar(100),
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@LastName varchar(100),
	@GenderID varchar(100),
	@CivilStatusID varchar(100),
	@DateOfMarriage varchar(100),
	@CitizenShipID varchar(100),
	@DateofBirth varchar(100),
	@GSISNumber varchar(100),
	@SSSNumber varchar(100),
	@TINNumber varchar(100),
	@RCN varchar(100),
	@RCNPlaceIssued varchar(100),
	@RCNDateIssued varchar(100),
	@BorrowerTypeID varchar(100),
	@LeadSourceID varchar(100),
	@ApplicationTypeID varchar(100),
	@AgentProfileID varchar(100),
	@SpouseFirstName varchar(100),
	@SpouseMiddleName varchar(100),
	@SpouseLastName varchar(100),
	@SpouseDateofBirth varchar(100),
	@SpouseContactNumber varchar(100),
	@PreparedBYID varchar(100),
	@PreparedByDateTime varchar(100),
	@DocumentStatusCode varchar(100),
	@Permission varchar(100),
	@Notes varchar(100),
	@OwnerCode varchar(100),
	@OwnerID varchar(100)

AS
BEGIN
	SET NOCOUNT ON;
	Declare @ret int = 0
    if @Code = '' or @Code is null
		Begin
			Declare @Series varchar(100)
			Select @Code=Code from organization where ID = @OrganizationID
			Set @Code = rtrim(@Code) + '-' + Replace(CONVERT(VARCHAR(10), GetDate(), 112),'/','')
			Select top(1) @Series=right(rtrim(Code),6) from PIS where Code like @Code+'%' order by Code Desc
			Set @Series = isnull(@Series,'000000')
			Set @Code = @Code + Right(('000000' + Rtrim(Cast((Cast(@Series as int) + 1) as varchar))),6)
			
			Insert into PIS(ID,CODE,DATETIME_CREATED,ORGANIZATION_ID,FIRST_NAME,LAST_NAME,MIDDLE_NAME,GENDER_ID,
			CIVIL_STATUS_ID,DATE_OF_MARRIAGE,CITIZENSHIP_ID,DATE_OF_BIRTH,GSIS_NUMBER,SSS_NUMBER,TIN_NUMBER,RCN,RCN_PLACE_ISSUED,RCN_DATE_ISSUED,
			BORROWER_TYPE_ID,LEAD_SOURCE_ID,APPLICATION_TYPE_ID,AGENT_PROFILE_ID,SPOUSE_FIRST_NAME,SPOUSE_MIDDLE_NAME,SPOUSE_LAST_NAME,SPOUSE_DATE_OF_BIRTH,
			SPOUSE_CONTACT_NUMBER,PREPARED_BY_ID,PREPARED_BY_DATETIME,DOCUMENT_STATUS_CODE,PERMISSION,NOTES,OWNER_CODE,OWNER_ID)
			Select @ID,@Code,GETDATE(),@OrganizationID,@FirstName,@LastName,@MiddleName,@GenderID,
			@CivilStatusID,@DateOfMarriage,@CitizenShipID,@DateofBirth,@GSISNumber,@SSSNumber,@TINNumber,@RCN,@RCNPlaceIssued,@RCNDateIssued,
			@BorrowerTypeID,@LeadSourceID,@ApplicationTypeID,@AgentProfileID,@SpouseFirstName,@SpouseMiddleName,@SpouseLastName,@SpouseDateofBirth,
			@SpouseContactNumber,@PreparedBYID,GETDATE(),@DocumentStatusCode,@Permission,@Notes,@Code,@ID						
			Set @ret = 1
		End
	else
		Begin
			Update pis Set ORGANIZATION_ID = @OrganizationID,FIRST_NAME = @FirstName,MIDDLE_NAME = @MiddleName, LAST_NAME = @LastName, GENDER_ID = @GenderID,
			CIVIL_STATUS_ID = @CivilStatusID, DATE_OF_MARRIAGE = @DateofMarriage,CITIZENSHIP_ID = @CitizenShipID,DATE_OF_BIRTH = @DateofBirth,GSIS_NUMBER=@GSISNumber,
			SSS_NUMBER = @SSSNumber, TIN_NUMBER = @TINNumber, RCN = @RCN, RCN_PLACE_ISSUED = @RCNPlaceIssued, RCN_DATE_ISSUED = @RCNDateIssued, BORROWER_TYPE_ID = @BorrowerTypeID,
			LEAD_SOURCE_ID = @LeadSourceID, APPLICATION_TYPE_ID = @ApplicationTypeID,AGENT_PROFILE_ID=@AgentProfileID, SPOUSE_FIRST_NAME = @SpouseFirstName, SPOUSE_MIDDLE_NAME = @SpouseMiddleName,
			SPOUSE_LAST_NAME = @SpouseLastName, SPOUSE_DATE_OF_BIRTH = @SpouseDateofBirth, SPOUSE_CONTACT_NUMBER = @SpouseContactNumber  where CODE = @Code
			Set @ret = 1		
		End
	Select @ret
END
