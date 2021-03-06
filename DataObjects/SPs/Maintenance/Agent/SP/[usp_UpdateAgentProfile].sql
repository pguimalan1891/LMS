USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateAgentProfile]    Script Date: 3/6/2017 2:34:22 PM ******/
IF OBJECT_ID('[usp_UpdateAgentProfile]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdateAgentProfile]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdateAgentProfile]
	@ID varchar(100),
	@Code varchar(100),
	@OrganizationID varchar(100),
	@LastName varchar(100),
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@GenderID varchar(100),
	@CivilStatusID varchar(100),
	@DateofBirth varchar(100),
	@AgentTypeID varchar(100),
	@WithCashCard varchar(100),
	@PreparedBYID varchar(100),
	@DocumentStatusCode varchar(100),
	@Permission varchar(100),
	@Notes varchar(100)
AS
BEGIN
	
	Declare @ret as int = 0
	SET NOCOUNT ON;
	if @Code is null or @Code = ''
		Begin
			Declare @Series varchar(100)
			Set @Code = ''
			Set @Code += Replace(CONVERT(VARCHAR(10), GetDate(), 112),'/','')
			Select top(1) @Series=right(rtrim(Code),6) from agent_profile where Code like @Code+'%' order by Code Desc
			Set @Series = isnull(@Series,'0000')
			Set @Code = @Code + Right(('0000' + Rtrim(Cast((Cast(@Series as int) + 1) as varchar))),6)
			Insert into agent_profile(ID,CODE,ORGANIZATION_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,GENDER_ID,CIVIL_STATUS_ID,DATE_OF_BIRTH,AGENT_TYPE_ID,
			WITH_CASH_CARD,PREPARED_BY_ID,PREPARED_BY_DATETIME,DOCUMENT_STATUS_CODE,PERMISSION,NOTES)
			Select @ID,@Code,@OrganizationID,@FirstName,@MiddleName,@LastName,@GenderID,@CivilStatusID,@DateofBirth,@AgentTypeID,@WithCashCard,@PreparedBYID,GETDATE(),
			@DocumentStatusCode,@Permission,@Notes
			Set @ret = 1
		End
	Else
		Begin
			Update dbo.agent_profile set ORGANIZATION_ID = @OrganizationID, LAST_NAME = @LastName, FIRST_NAME = @FirstName, MIDDLE_NAME = @MiddleName,
			GENDER_ID = @GenderID, CIVIL_STATUS_ID = @CivilStatusID, DATE_OF_BIRTH = @DateofBirth, AGENT_TYPE_ID = @AgentTypeID, WITH_CASH_CARD = @WithCashCard
			where CODE = @Code
			Set @ret = 1
		End
	Select @ret

    
END
