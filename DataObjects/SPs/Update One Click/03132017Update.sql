USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_updDevelopmentToolsLibrary]    Script Date: 3/13/2017 6:28:29 PM ******/
IF OBJECT_ID('[usp_updDevelopmentToolsLibrary]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_updDevelopmentToolsLibrary]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- Exec usp_updDevelopmentToolsLibrary 'Role',1,'1f491bda-ee7d-424e-8e1d-7a168c6aebe3|A.COYKIE.SEGARINOs|A.COYKIE SEGARINO|'
-- Exec usp_updDevelopmentToolsLibrary 'loan_terms',0,'1199f388-fff4-418b-8e6b-4faa06e585e2|0066|128|'
-- Select * from [tempCompValues]
Create PROCEDURE [dbo].[usp_updDevelopmentToolsLibrary]
	@DType varchar(50),
	@OpCode int,
	@Components nvarchar(1000)
AS
BEGIN
	SET NOCOUNT ON;
	--opCode 0=add,1=edit	Delete from Company_Type where Code = 'IND0018'
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempCompValues]') AND type in (N'U'))
		Begin			
			Drop Table [tempCompValues]
		End
	Exec dbo.fsp_splitHorizontal @Components,'|'
	Declare @sqlQuery nvarchar(4000)

    if @DType in ('Company_Type','Industry','Gender','Civil_Status','Lead_Source','gl_account','payment_type'
	,'ledger_type','incentive_release_mode','agent_type','application_type','collateral_group','borrower_group','official_receipt_type','check_voucher_type'
	,'bank_account_type','remedial_type','machine_validation_type','realization_type','commission_level','waiver_type','amount_type','cmdm_account_type'
	,'cmdm_adjustment_type','cmdm_entry_type','Nature_of_business','loan_set','support_document','collateral_usage','color','fuel_type','support_group'
	,'district_group','payment_mode')
		Begin
			if @OpCode = 0
				Begin
					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'(ID,CODE,DESCRIPTION)
					Select c1 as ID,c2 as Code,c3 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'borrower_Type'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c4,c3 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.Borrower_Group_ID=c4,a.MIGID=b.c3 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'Loan_Type'		
		Begin
			if @OpCode = 0
				Begin
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c5,c6,c7,c4,c3 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.Require_Collateral=c5,a.With_DST=c6,a.With_CV=c7,a.Balloon_Payment=c4,a.MIGID=b.c3 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'loan_terms'
		Begin
			if @OpCode = 0
				Begin
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,Cast(c3 as varchar) + Case when Cast(c3 as int) = 1 then '' Month'' Else '' Months'' End as Description,Cast(c3 as int),Cast(c3 as varchar) + Case when Cast(c3 as int) = 1 then '' Month'' Else '' Months'' End as Description from dbo.[tempCompValues]					
					'
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=Cast(b.c3 as varchar) + Case when Cast(c3 as int) = 1 then '' Month'' Else '' Months'' End
					,Months=Cast(c3 as int),a.MIGID=Cast(b.c3 as varchar) + Case when Cast(c3 as int) = 1 then '' Month'' Else '' Months'' End from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'collateral_type'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c4,c3 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.collateral_Group_ID=c4,a.MIGID=b.c3 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'regional_office'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c4 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.support_group_id=c4 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'district'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c4,c5 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.district_group_id=c4,a.regional_office_id=c5 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'organization'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'(ID,CODE,DESCRIPTION,DISTRICT_ID,MIGID)
					Select c1 as ID,c2 as Code,c3 as Description,c4,c2 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.district_id=c4,a.MIGID=b.c2 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'bank'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into [dbo].'+@DType+'
					Select c1 as ID,c2 as Code,c3 as Description,c4,c3 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.DESCRIPTION=b.c3,a.bank_account_type_id=c4,a.MIGID=b.c3 from dbo.'+@DType+' a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.'+@DType+' where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	if @DType = 'Role'
		Begin
			if @OpCode = 0
				Begin					
					Set @sqlQuery = '					
					Insert into user_account(ID,CODE,ROLE_NAME,PASSWORD_NEVER_EXPIRES,STATUS,TYPE,LOCKOUT_COUNTER)
					Select c1 as ID,c2 as Code,c3,''N'',0,1,0 as Description from dbo.[tempCompValues]					
					'								
				End
			else if @OpCode = 1
				Begin
					Set @sqlQuery = 'Update a Set a.CODE = b.c2,a.ROLE_NAME=b.c3 from dbo.user_account a inner join dbo.[tempCompValues] b on a.ID = b.c1'					
				End
			else if @OpCode = 2
				Begin
					set @sqlQuery= 'Delete from dbo.user_account where ID in (Select c1 from dbo.[tempCompValues])'
				End
		End
	Begin Try 
		Exec(@sqlQuery)
		Select 1 as ErrorMessage
	End Try
	Begin Catch
		--Select ERROR_MESSAGE()
		Select ERROR_NUMBER() as ErrorMessage
	End Catch	
		
		
END

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdateUserAccounts]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdateUserAccounts]
/****** Object:  StoredProcedure [dbo].[usp_UpdateUserAccounts]    Script Date: 3/13/2017 6:28:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdateUserAccounts] 
	@ProcessType varchar(100),
	@ID varchar(100),
	@Code varchar(100),
	@LastName varchar(100),
	@FirstName varchar(100),
	@MiddleName varchar(100),
	@PostalAddress varchar(100),
	@EmailAddress varchar(100),
	@PositionID varchar(100),
	@OrganizationID varchar(100),
	@CompanyID varchar(100),
	@PasswordNeverExpires varchar(100),
	@Status varchar(100),
	@RegisterByID varchar(100),
	@PasswordID varchar(100),
	@Password varchar(100)	
AS
BEGIN
	SET NOCOUNT ON;
	Declare @Ret as varchar(5) = '0'
	
    if @ProcessType = 'Update' 
		Begin
			if exists(Select 1 from user_account where CODE = @Code and ID <> @ID)
			Begin
				Set @Ret = '2'
				Select @Ret as Status
				return
			End
			Update user_account Set Code = @Code,LAST_NAME = @LastName, FIRST_NAME = @FirstName, MIDDLE_NAME = @MiddleName, POSTAL_ADDRESS = @PostalAddress,
			EMAIL_ADDRESS = @EmailAddress, POSITION_ID = @PositionID, ASSIGNED_OFFICE_ID = @OrganizationID, COMPANY_ID = @CompanyID, PASSWORD_NEVER_EXPIRES = @PasswordNeverExpires,
			STATUS = @Status, FULL_NAME = @LastName + ', ' + @FirstName + ' ' + @MiddleName where id = @ID
			Set @Ret = '1'
			Select @Ret as Status
			Return
		End
	Else
		Begin			
			if exists(Select 1 from user_account where CODE = @Code)
			Begin
				Set @Ret = '2'
				Select @Ret as Status
				return
			End			
			Insert into user_account(ID,CODE,LAST_NAME,FIRST_NAME,MIDDLE_NAME,POSTAL_ADDRESS,EMAIL_ADDRESS,POSITION_ID,ASSIGNED_OFFICE_ID,COMPANY_ID,PASSWORD_NEVER_EXPIRES,STATUS,TYPE,REGISTERED_BY_ID,DATETIME_REGISTERED,FULL_NAME)
			Select @ID,@Code,@LastName,@FirstName,@MiddleName,@PostalAddress,@EmailAddress,@PositionID,@OrganizationID,@CompanyID,@PasswordNeverExpires,@Status,0,@RegisterByID,GETDATE(),@LastName + ', ' + @FirstName + ' ' + @MiddleName
			
			Insert into user_password(ID,ITEM_NO,USER_ID,PASSWORD,CURRENT_RECORD,EXPIRED,DATETIME_CREATED)
			Select @PasswordID,0,@ID,@Password,'Y','N',GETDATE()			

			Set @Ret = '1'
			Select @Ret as Status
			Return
		End
END
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePISEmployment]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISEmployment]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISEmployment]    Script Date: 3/13/2017 6:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePISEmployment]
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
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePISEducation]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISEducation]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISEducation]    Script Date: 3/13/2017 6:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePISEducation]
	@ID varchar(100),
	@PISID varchar(100),
	@EducationTypeID varchar(100),
	@SchoolName varchar(100),
	@GraduationDate varchar(100)		
AS
BEGIN
	SET NOCOUNT ON;
	Declare @ret int = 0
	
	Insert into pis_education(ID,PIS_ID,EDUCATION_TYPE_ID,SCHOOL_NAME,GRADUATION_DATE)
	Select @ID,@PISID,@EducationTypeID,@SchoolName,@GraduationDate
	Set @ret = 1
	
	Select @ret
END
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePISDependent]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISDependent]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISDependent]    Script Date: 3/13/2017 6:28:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePISDependent]
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

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePISCharacter]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISCharacter]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISCharacter]    Script Date: 3/13/2017 6:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePISCharacter]
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
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePISAddress]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISAddress]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISAddress]    Script Date: 3/13/2017 6:28:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePISAddress]
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

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdatePIS]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePIS]
/****** Object:  StoredProcedure [dbo].[usp_UpdatePIS]    Script Date: 3/13/2017 6:28:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdatePIS]
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

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdateAgentProfile]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdateAgentProfile]
/****** Object:  StoredProcedure [dbo].[usp_UpdateAgentProfile]    Script Date: 3/13/2017 6:28:12 PM ******/
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

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_UpdateAgentAddress]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdateAgentAddress]
/****** Object:  StoredProcedure [dbo].[usp_UpdateAgentAddress]    Script Date: 3/13/2017 6:28:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_UpdateAgentAddress]
	@ID varchar(100),
	@AgentProfileID varchar(100),
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
	
	Insert into agent_profile_address(ID,AGENT_PROFILE_ID,ADDRESS_TYPE_ID,STREET_ADDRESS,BARANGAY_NAME,CITY_ID,POSTAL_CODE,PHONE_NUMBER,MOBILE_NUMBER,RESIDENT_DATE,HOME_OWNERSHIP_ID)
	Select @ID,@AgentProfileID,@AddressTypeID,@StreetAddress,@BarangayName,@CityID,@PostalCode,@PhoneNumber,@MobileNumber,@ResidentDate,@HomeOwnershipID
	Set @ret = 1
	
	Select @ret
END
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_Login]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_Login]
/****** Object:  StoredProcedure [dbo].[usp_Login]    Script Date: 3/13/2017 6:28:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[usp_Login]
	-- Add the parameters for the stored procedure here
	@username varchar(50),
	@password varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure her
	SELECT top 1 s.DESCRIPTION as [Status], p.DESCRIPTION, ua.LAST_NAME+','+ ua.FIRST_NAME as NAME from user_account ua inner join user_password up on ua.ID = up.USER_ID inner join position p on p.ID = ua.POSITION_ID inner join user_account_status_map s on s.CODE=ua.STATUS where RTRIM(LTRIM(ua.CODE))=@username and [PASSWORD]=@password
	
END
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_getDisbursementVoucher]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getDisbursementVoucher]
/****** Object:  StoredProcedure [dbo].[usp_getDisbursementVoucher]    Script Date: 3/13/2017 6:28:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--usp_getDisbursementVoucher 48
Create procedure [dbo].[usp_getDisbursementVoucher](
	@statuscode int = 0
)
as
begin
select disbursement_voucher.id,document_status_map.description as dsm_description, disbursement_voucher.code,
 convert(Varchar(10),disbursement_voucher.datetime_created,101) as datetime_created, 
 organization.description branch_name, direct_loan_receipt.code as dlr_code, direct_loan_receipt.bis_code as bis_dlr_code, 
cast('~/Transactions/RFC/DirectLoanReceipt.aspx?state=4&code=' + direct_loan_receipt.code as char(100)) as dlr_hyperlink, 
cir_form.code as cir_form_code, cast('~/Transactions/RFC/CIRForm.aspx?state=4&code=' + cir_form.code as char(100)) as cir_form_hyperlink,
 request_for_payment.code as rfp_code, cast('~/Transactions/RFC/RequestForPayment.aspx?state=4&code=' + request_for_payment.code as char(100)) as rfp_hyperlink, 
 expense_type.description et_description, disbursement_voucher.amount, disbursement_voucher.payee,
 convert(Varchar(10), disbursement_voucher.prepared_by_datetime,101) as prepared_by_datetime, prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name 
  --,row_number() OVER (INSERT ORDER BY HERE) as row_num
  ,disbursement_voucher.DOCUMENT_STATUS_CODE
  into #temp
    
  from Final_Testing.dbo.disbursement_voucher  
  inner join Final_Testing.dbo.document_status_map   on(disbursement_voucher.document_status_code = document_status_map.code) 
   inner join Final_Testing.dbo.user_account prepared_by   on(disbursement_voucher.prepared_by_id = prepared_by.id)  
   left join (Final_Testing.dbo.cir_form      inner join Final_Testing.dbo.direct_loan_receipt       
    on (cir_form.direct_loan_receipt_id = direct_loan_receipt.id)           )  
	on (disbursement_voucher.cir_form_id = cir_form.id)  left join (Final_Testing.dbo.request_for_payment     
	 inner join Final_Testing.dbo.expense_type        on (request_for_payment.expense_type_id = expense_type.id)           )  on (disbursement_voucher.request_for_payment_id = request_for_payment.id)  inner join Final_Testing.dbo.organization   on(disbursement_voucher.organization_id = organization.id)  where  1 = 1 
AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')


select id, dsm_description, code, datetime_created, branch_name, dlr_code, 
bis_dlr_code, dlr_hyperlink, cir_form_code, cir_form_hyperlink, rfp_code, 
rfp_hyperlink, et_description, amount, payee, prepared_by_datetime, prepared_by_name 
from #temp 
--where document_status_code='48'
where DOCUMENT_STATUS_CODE=@statuscode

 drop table #temp
 end
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_getDevelopmentToolsLibrary]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getDevelopmentToolsLibrary]
/****** Object:  StoredProcedure [dbo].[usp_getDevelopmentToolsLibrary]    Script Date: 3/13/2017 6:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--[usp_getDevelopmentToolsLibrary] 'Industry'
Create PROCEDURE [dbo].[usp_getDevelopmentToolsLibrary]
	@DType varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	Create Table #UpdateComponent(
		FieldisHide nvarchar(255),
		FieldName nvarchar(255),		
		FieldType nvarchar(255),
		FieldComponentType nvarchar(255),
		FieldComponentID nvarchar(255),
		FieldLink nvarchar(255),
		FieldisHideColIndex nvarchar(255),
		FieldDisplay nvarchar(255),
		FieldValue nvarchar(255)
	)	
	
	Declare @SqlQuery nvarchar(4000)	
	if @DType in ('Company_Type','Industry','Gender','Civil_Status','Lead_Source','gl_account','payment_type'
	,'ledger_type','incentive_release_mode','agent_type','application_type','collateral_group','borrower_group','official_receipt_type','check_voucher_type'
	,'bank_account_type','remedial_type','machine_validation_type','realization_type','commission_level','waiver_type','amount_type','cmdm_account_type'
	,'cmdm_adjustment_type','cmdm_entry_type','Nature_of_business','loan_set','support_document','collateral_usage','color','fuel_type'
	,'support_group','district_group','payment_mode','expense_type')
		Begin
			Set @SqlQuery = 'Select ID,CODE,DESCRIPTION from ' + @DType + ' Order by Code'
			Exec(@SqlQuery)
			return;
		End

	if @DType in ('Company_TypeUpdCom','IndustryUpdCom','GenderUpdCom','Civil_StatusUpdCom','Lead_SourceUpdCom','gl_accountUpdCom','payment_typeUpdCom'
	,'ledger_typeUpdCom','incentive_release_modeUpdCom','agent_typeUpdCom','application_typeUpdCom','collateral_groupUpdCom','borrower_groupUpdCom','official_receipt_typeUpdCom','check_voucher_typeUpdCom'
	,'bank_account_typeUpdCom','remedial_typeUpdCom','machine_validation_typeUpdCom','realization_typeUpdCom','commission_levelUpdCom','waiver_typeUpdCom','amount_typeUpdCom','cmdm_account_typeUpdCom'
	,'cmdm_adjustment_typeUpdCom','cmdm_entry_typeUpdCom','Nature_of_businessUpdCom','loan_setUpdCom','support_documentUpdCom','collateral_usageUpdCom','colorUpdCom','fuel_typeUpdCom'
	,'support_groupUpdCom','district_groupUpdCom','payment_modeUpdCom','expense_typeUpdCom')
		Begin
			Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Select * from #UpdateComponent
			return;
		End
	--Select * from borrower_Type
	if @DType = 'borrower_Type'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [BORROWER GROUP] from dbo.' + @DType + ' a inner join dbo.borrower_group b on a.borrower_group_id = b.ID'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'borrower_TypeUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','BORROWER GROUP','string','select','BORROWERGROUP','ID','4',DESCRIPTION,ID from borrower_group
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'Loan_Type'
		Begin
			Set @SqlQuery = 'Select ID,CODE,DESCRIPTION,
			Case When REQUIRE_COLLATERAL = ''Y'' then ''YES'' else ''NO'' end as [REQUIRE COLLATERAL],
			Case When WITH_DST = ''Y'' then ''YES'' else ''NO'' end as [WITH DST],
			Case When WITH_CV = ''Y'' then ''YES'' else ''NO'' end as [WITH CV],BALLOON_PAYMENT as [BALLOON PAYMENT] from loan_type Order by Code'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'Loan_TypeUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','REQUIRE COLLATERAL','string','select','REQUIRECOLLATERAL','ID','4','NO','N'
			Insert into #UpdateComponent
			Select 'Show','REQUIRE COLLATERAL','string','select','REQUIRECOLLATERAL','ID','4','YES','Y'
			Insert into #UpdateComponent
			Select 'Show','WITH DST','string','select','WITHDST','ID','5','NO','N'
			Insert into #UpdateComponent
			Select 'Show','WITH DST','string','select','WITHDST','ID','5','YES','Y'
			Insert into #UpdateComponent
			Select 'Show','WITH CV','string','select','WITHCV','ID','6','NO','N'
			Insert into #UpdateComponent
			Select 'Show','WITH CV','string','select','WITHCV','ID','6','YES','Y'
			Insert into #UpdateComponent
			Select 'Show','BALLOON PAYMENT','string','single','BALLOONPAYMENT','ID','7','',''
			Select * from #UpdateComponent			
			return;
		End
	if @DType = 'loan_terms'
		Begin
			Set @SqlQuery = 'Select ID,CODE,DESCRIPTION,MONTHS from ' + @DType + ' Order by Description'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'loan_termsUpdCom'
		Begin
			Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','MONTHS','string','single','MONTHS','ID','3','',''
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'collateral_type'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [COLLATERAL GROUP] from dbo.' + @DType + ' a inner join dbo.collateral_group b on a.collateral_group_id = b.ID Order by a.Code'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'collateral_typeUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','COLLATERAL GROUP','string','select','COLLATERALGROUP','ID','4',DESCRIPTION,ID from collateral_group
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'regional_office'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [SUPPORT GROUP] from dbo.' + @DType + ' a inner join dbo.support_group b on a.support_group_id = b.ID Order by a.Code'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'regional_officeUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','SUPPORT GROUP','string','select','SUPPORTGROUP','ID','4',DESCRIPTION,ID from support_group
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'district'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [DISTRICT GROUP],c.Description as [REGIONAL OFFICE] from dbo.' + @DType + ' a 
			inner join dbo.district_group b on a.district_group_id = b.ID
			inner join dbo.regional_office c on a.regional_office_id = C.ID Order by a.Code
			'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'districtUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','DISTRICT GROUP','string','select','DISTRICTGROUP','ID','4',DESCRIPTION,ID from district_group
			Insert into #UpdateComponent
			Select 'Show','REGIONAL OFFICE','string','select','REGIONALOFFICE','ID','4',DESCRIPTION,ID from regional_office
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'organization'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [DISTRICT] from dbo.' + @DType + ' a inner join dbo.district b on a.district_id = b.ID Order by a.Code'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'organizationUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','DISTRICT','string','select','DISTRICT','ID','4',DESCRIPTION,ID from district
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'bank'
		Begin
			Set @SqlQuery = 'Select a.ID,a.CODE,a.DESCRIPTION,b.Description as [BANK ACCOUNT TYPE] from dbo.' + @DType + ' a inner join dbo.bank_account_type b on a.bank_account_type_id = b.ID Order by a.Code'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'bankUpdCom'
		Begin
		Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','DESCRIPTION','string','single','DESCRIPTION','ID','3','',''
			Insert into #UpdateComponent
			Select 'Show','BANK ACCOUNT TYPE','string','select','BANKACCOUNTTYPE','ID','4',DESCRIPTION,ID from bank_account_type
			Select * from #UpdateComponent
			return;
		End
	if @DType = 'Role'
		Begin
			Set @SqlQuery = 'Select ID,Code as CODE,ROLE_NAME from user_account where TYPE = 1 Order by CODE'
			Exec(@SqlQuery)
			return;
		End
	if @DType = 'RoleUpdCom'
		Begin
			Insert into #UpdateComponent
			Select 'Hide','ID','string','single','ID','ID','1','',''
			Insert into #UpdateComponent
			Select 'Show','CODE','string','single','CODE','ID','2','',''
			Insert into #UpdateComponent
			Select 'Show','ROLE_NAME','string','single','ROLE_NAME','ID','3','',''
			Select * from #UpdateComponent
			return;
		End


END

GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_GetCustomerRecord]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_GetCustomerRecord]
/****** Object:  StoredProcedure [dbo].[usp_GetCustomerRecord]    Script Date: 3/13/2017 6:28:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--Exec usp_GetCustomerRecord
Create PROCEDURE [dbo].[usp_GetCustomerRecord] 	
AS
BEGIN
	SET NOCOUNT ON;
    select pis.id,document_status_map.description as [Status], pis.code as [Code], pis.last_name as [Last Name], pis.first_name as [First Name], pis.middle_name as [Middle Name], Convert(varchar,pis.date_of_birth,101) as [Date of Birth], 
    gender.description as [Gender], civil_status.description as [Civil Status], pis_address.street_address as [Address], city.description as [City] 
    from Final_Testing.dbo.pis inner join Final_Testing.dbo.document_status_map   on (pis.document_status_code = document_status_map.code)  inner join Final_Testing.dbo.user_account prepared_by   on (pis.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.gender   on (pis.gender_id = gender.id)  inner join Final_Testing.dbo.civil_status   on (pis.civil_status_id = civil_status.id)  left join Final_Testing.dbo.pis_address   on (pis_address.pis_id = pis.id and pis_address.address_type_id = '0')  inner join Final_Testing.dbo.city on(pis_address.city_id = city.id)  inner join Final_Testing.dbo.province   on(city.province_id = province.id)  inner join Final_Testing.dbo.organization   on(pis.organization_id = organization.id)  where pis.permission > 0 
	Order by pis.DATETIME_CREATED desc		
	
END
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_getCIRForm]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getCIRForm]
/****** Object:  StoredProcedure [dbo].[usp_getCIRForm]    Script Date: 3/13/2017 6:28:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--usp_getCIRForm '10'
Create procedure [dbo].[usp_getCIRForm]
	@statuscode int = 0
as
begin

select cir_form.document_status_code,cir_form.id,document_status_map.description as dsm_description, cir_form.code, cir_form.datetime_created, organization.description branch_name, 
loan_application.code loan_application_code, cast('~/Transactions/RFC/LoanApplication.aspx?state=4&code=' + loan_application.code as char(100)) as loan_application_hyperlink,
 loan_type.description loan_type_description, loan_terms.description loan_terms_description, direct_loan_receipt.code as dlr_code, direct_loan_receipt.bis_code as bis_dlr_code,
  cast('~/Transactions/RFC/DirectLoanReceipt.aspx?state=4&code=' + direct_loan_receipt.code as char(100)) as dlr_hyperlink, 
  agent_profile.last_name  +  ', '  + agent_profile.first_name  +  ' '  + agent_profile.middle_name  as agent_profile_name, 
  cast('~/Transactions/RFC/AgentProfile.aspx?state=4&code=' + agent_profile.code as char(100)) as agent_profile_hyperlink, cir_form.commission_amount 
  --,row_number() OVER (INSERT ORDER BY HERE) as row_num  
  into #temp
  from Final_Testing.dbo.cir_form  inner join Final_Testing.dbo.document_status_map   on(cir_form.document_status_code = document_status_map.code)  
  inner join Final_Testing.dbo.user_account prepared_by   on(cir_form.prepared_by_id = prepared_by.id)  
  inner join Final_Testing.dbo.direct_loan_receipt   on(cir_form.direct_loan_receipt_id = direct_loan_receipt.id)  
  inner join Final_Testing.dbo.loan_application   on(direct_loan_receipt.loan_application_id = loan_application.id)  
  inner join Final_Testing.dbo.pis   on(loan_application.current_pis_id = pis.id) 
   inner join Final_Testing.dbo.loan_type   on(loan_application.loan_type_id = loan_type.id)  
   inner join Final_Testing.dbo.loan_terms   on(loan_application.loan_terms_id = loan_terms.id)  
   inner join Final_Testing.dbo.agent_profile   on(pis.agent_profile_id = agent_profile.id)  
   inner join Final_Testing.dbo.organization   on(cir_form.organization_id = organization.id)  where  1 = 1

	select id, dsm_description, code, datetime_created, branch_name, loan_application_code, loan_application_hyperlink, loan_type_description, 
	loan_terms_description, dlr_code, bis_dlr_code, dlr_hyperlink, agent_profile_name, agent_profile_hyperlink, commission_amount 
	from #temp
	where document_status_code=@statuscode

   drop table #temp
end
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_getCheckVoucher]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getCheckVoucher]
/****** Object:  StoredProcedure [dbo].[usp_getCheckVoucher]    Script Date: 3/13/2017 6:27:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--usp_getCheckVoucher 33
Create procedure[dbo].[usp_getCheckVoucher]
	@statuscode int = 0	 
as
Begin
    select check_voucher.DOCUMENT_STATUS_CODE as document_status_code, check_voucher.id,document_status_map.description as dsm_description, check_voucher.code, check_voucher.datetime_created, 
	organization.description branch_name, direct_loan_receipt.code as dlr_code, direct_loan_receipt.bis_code as bis_dlr_code, 

    cast('~/Transactions/RFC/DirectLoanReceipt.aspx?state=4&code=' + direct_loan_receipt.code as char(100)) as dlr_hyperlink, 
	check_voucher_type.description as cvt_description, bank_account_type.description as bat_description, bank.description as bank_description, 
	check_voucher.amount, check_voucher.payee, pis.first_name  +  ' '  + pis.middle_name  +  ' '  + pis.last_name  as applicants_name, 

    cast('~/Transactions/RFC/PIS.aspx?state=4&id=' + pis.id as char(100)) as pis_hyperlink, check_voucher.check_no, check_voucher.check_date, 
	check_voucher.prepared_by_datetime as prepared_by_datetime, prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name 
	--,row_number() OVER(INSERT ORDER BY HERE) as row_num
   into #temp
	from Final_Testing.dbo.check_voucher

     inner join Final_Testing.dbo.document_status_map on(check_voucher.document_status_code = document_status_map.code)

      inner join Final_Testing.dbo.user_account prepared_by   on(check_voucher.prepared_by_id = prepared_by.id)

       inner join Final_Testing.dbo.direct_loan_receipt on(check_voucher.direct_loan_receipt_id = direct_loan_receipt.id)


        inner join Final_Testing.dbo.loan_application on(direct_loan_receipt.loan_application_id = loan_application.id)

         inner join Final_Testing.dbo.pis on(loan_application.current_pis_id = pis.id)

         inner join Final_Testing.dbo.check_voucher_type on(check_voucher.check_voucher_type_id = check_voucher_type.id)

          left join(Final_Testing.dbo.cheque_setup inner join Final_Testing.dbo.bank on (cheque_setup.bank_id = bank.id)

             inner join Final_Testing.dbo.bank_account_type on(bank.bank_account_type_id = bank_account_type.id)            )  on(check_voucher.cheque_setup_id = cheque_setup.id)

             inner join Final_Testing.dbo.organization on(check_voucher.organization_id = organization.id)

              inner join Final_Testing.dbo.district on(organization.district_id = district.id) where  1 = 1 


     AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')
	
	if @statuscode =99

        begin


            select

            id, dsm_description, code, convert(varchar(10), datetime_created,101) as datetime_created, branch_name, dlr_code, bis_dlr_code, dlr_hyperlink,
            cvt_description, bat_description, bank_description,
			--amount,
            REPLACE(CONVERT(varchar(20), (CAST(amount AS money)), 1), '.00', '') as amount,
            payee, applicants_name, pis_hyperlink,
            check_no,
            convert(Varchar(10), check_date,101) as check_date,
            prepared_by_datetime, prepared_by_name

            from #temp
			--where document_status_code=@statuscode

            order by code desc


        end
	else

        begin


            select

            id, dsm_description, code, convert(varchar(10), datetime_created,101) as datetime_created, branch_name, dlr_code, bis_dlr_code, dlr_hyperlink,
            cvt_description, bat_description, bank_description,
			--amount,
            REPLACE(CONVERT(varchar(20), (CAST(amount AS money)), 1), '.00', '') as amount,
            payee, applicants_name, pis_hyperlink,
            check_no,
            convert(Varchar(10), check_date,101) as check_date,
            prepared_by_datetime, prepared_by_name

            from #temp
			where document_status_code=@statuscode

            order by code desc


        end


    drop table #temp

End
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[usp_getChangeCCIForm]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getChangeCCIForm]
/****** Object:  StoredProcedure [dbo].[usp_getChangeCCIForm]    Script Date: 3/13/2017 6:27:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--usp_getChangeCCIForm 10
Create procedure [dbo].[usp_getChangeCCIForm]
(
	@statuscode int = 0
)
as
begin
select change_cci_form.document_status_code,change_cci_form.id,document_status_map.description as dsm_description, 
change_cci_form.code,
 convert(Varchar(10),change_cci_form.datetime_created,101) as datetime_created, 
 organization.description branch_name, 
loan_application.code as la_code, 
cast('~/Transactions/RFC/LoanApplication.aspx?state=4&code=' + loan_application.code as char(100)) as la_hyperlink, pis.first_name  +  ' '  + pis.middle_name  +  ' '  + pis.last_name  as applicants_name, 
cast('~/Transactions/RFC/PIS.aspx?state=4&code=' + pis.code as char(100)) as pis_hyperlink, previous_cci.last_name  +  ', '  + previous_cci.first_name  as previous_cci_name, current_cci.last_name  +  ', '  + current_cci.first_name  as current_cci_name, 
change_cci_form.prepared_by_datetime as prepared_by_datetime, prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name
 --,row_number() OVER (INSERT ORDER BY HERE) as row_num
 
 into #temp
   
from Final_Testing.dbo.change_cci_form  
inner join Final_Testing.dbo.document_status_map   on(change_cci_form.document_status_code = document_status_map.code) 
 inner join Final_Testing.dbo.user_account prepared_by   on(change_cci_form.prepared_by_id = prepared_by.id)  
 inner join Final_Testing.dbo.loan_application   on(change_cci_form.loan_application_id = loan_application.id)  
 inner join Final_Testing.dbo.pis   on(loan_application.current_pis_id = pis.id)  
 inner join Final_Testing.dbo.user_account previous_cci   on(change_cci_form.previous_cci_id = previous_cci.id)  
 inner join Final_Testing.dbo.user_account current_cci   on(change_cci_form.current_cci_id = current_cci.id) 
  inner join Final_Testing.dbo.organization   on(change_cci_form.organization_id = organization.id)  
where  1 = 1 AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')

select id, dsm_description, code, datetime_created, branch_name, la_code, 
la_hyperlink, applicants_name, pis_hyperlink, previous_cci_name, current_cci_name, 
prepared_by_datetime, prepared_by_name from #temp
where document_status_code=@statuscode

drop table #temp

end
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[RetrieveBookingRecords]') IS NOT NUll
	DROP PROCEDURE [dbo].[RetrieveBookingRecords]
/****** Object:  StoredProcedure [dbo].[RetrieveBookingRecords]    Script Date: 2/23/2017 4:08:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec RetrieveBookingRecords 14
Create Procedure [dbo].[RetrieveBookingRecords]
	@statuscode int
as
begin
	select 
	'' as blank,ROW_NUMBER() OVER(ORDER BY direct_loan_receipt.id ASC) AS [Seq],
	--direct_loan_receipt.id,
	document_status_map.description as Status,
	direct_loan_receipt.code as [LMS DLR No], 
	isnull(direct_loan_receipt.bis_code,'') as [DLR No], 
	convert(Varchar(10),direct_loan_receipt.datetime_created,101) as [Date],
	organization.description as [Branch], 
	loan_application.code as [Account No], 
	--cast('~/Transactions/RFC/LoanApplication.aspx?state=4&code=' + loan_application.code as char(100)) as loan_application_hyperlink, 
	pis.first_name  +  ' '  + pis.middle_name  +  ' '  + pis.last_name  as [Customer], 
	--cast('~/Transactions/RFC/PIS.aspx?state=4&id=' + pis.id as char(100)) as pis_hyperlink, 
	loan_type.description as [Loan Type], loan_terms.description as [Loan Terms], 
	loan_set.description as [Loan Set],
	
	--direct_loan_receipt.approved_mlv as [MLV],
	REPLACE(CONVERT(varchar(20), (CAST(direct_loan_receipt.approved_mlv AS money)), 1), '.00', '') as [MLV]
	--,
	
	--convert(varchar(10),direct_loan_receipt.prepared_by_datetime,101) as prepared_by_datetime,

	--prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name 
	--,
	--row_number() OVER (INSERT ORDER BY HERE) as row_num  
	,direct_loan_receipt.document_status_Code as [StatusCode]
	into #TEMP

	from Final_Testing.dbo.direct_loan_receipt  inner join Final_Testing.dbo.document_status_map   on(direct_loan_receipt.document_status_code = document_status_map.code) 
	inner join Final_Testing.dbo.user_account prepared_by   on(direct_loan_receipt.prepared_by_id = prepared_by.id) 
	inner join Final_Testing.dbo.loan_application   on(direct_loan_receipt.loan_application_id = loan_application.id) 
	inner join Final_Testing.dbo.pis   on(loan_application.current_pis_id = pis.id)  inner join Final_Testing.dbo.loan_type   on(loan_application.loan_type_id = loan_type.id) 
	inner join Final_Testing.dbo.loan_terms   on(loan_application.loan_terms_id = loan_terms.id) 
	inner join Final_Testing.dbo.loan_set   on(loan_application.loan_set_id = loan_set.id) 
	inner join Final_Testing.dbo.organization   on(direct_loan_receipt.organization_id = organization.id)  where  1 = 1 
	AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')
	--Select * from Final_Testing.dbo.organization
	--and direct_loan_receipt.document_status_Code = @statuscode

	if (Select Count(Seq) from #TEMP where StatusCode=@statuscode) > 0
		begin
			Select blank,Seq,Status,[LMS DLR No],[DLR No],Date,Branch,[Account No],Customer,[Loan Type],[Loan Terms],[Loan Set],MLV from #TEMP where StatusCode = @statuscode
		end
	else
		begin
			Select blank,Seq,Status,[LMS DLR No],[DLR No],Date,Branch,[Account No],Customer,[Loan Type],[Loan Terms],[Loan Set],MLV from #TEMP --where StatusCode = @statuscode
		end

	DROP TABLE #TEMP

end
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[fsp_splitHorizontal]') IS NOT NUll
	DROP PROCEDURE [dbo].[fsp_splitHorizontal]
/****** Object:  StoredProcedure [dbo].[fsp_splitHorizontal]    Script Date: 3/13/2017 6:47:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- fsp_splitHorizontal 'ccaaa|ddasdasdasdasdas,,as','|'
Create PROCEDURE [dbo].[fsp_splitHorizontal]
	@String nvarchar(1000),
	@Delimiter char(1)
AS
BEGIN
	SET NOCOUNT ON;

    declare @idx int        
    declare @slice varchar(max)        
    	
    Select 'c'+Cast(ROW_NUMBER() OVER (ORDER BY (SELECT 100)) as varchar) AS SNO,parameter into #tempTable from dbo.split(@String,@Delimiter)
		
	DECLARE
    @SqlQuery nvarchar(4000),
    @ColumnList nvarchar(4000),
	@ColumnNew nvarchar(4000)
	
	SELECT @ColumnList=
		   COALESCE(@ColumnList + ',','') + QUOTENAME(parameter)
	FROM #tempTable

	SELECT @ColumnNew=
		   COALESCE(@ColumnNew + ',','') + QUOTENAME(SNO)
	FROM #tempTable
	

	SET @SqlQuery = '
	WITH PivotData AS
	(
		   SELECT SNO,parameter
		   FROM #tempTable
	)
	SELECT
		' + @ColumnNew + ' into tempCompValues
	FROM
		PivotData
	PIVOT
	(
		MAX(parameter)
		FOR SNO
		IN (' + @ColumnNew + ')
	) AS PivotResult'

	EXEC (@SqlQuery)

END
GO
USE [FINAL_TESTING]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 3/13/2017 6:47:36 PM ******/
IF OBJECT_ID('[Split]') IS NOT NUll
	DROP FUNCTION [dbo].[Split]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[Split](@String varchar(max), @Delimiter char(1))        
returns @temptable TABLE (parameter varchar(max))        
as        
begin        
    declare @idx int        
    declare @slice varchar(max)        
       
    select @idx = 1        
        if len(@String)<1 or @String is null  return        
       
    while @idx!= 0        
    begin        
        set @idx = charindex(@Delimiter,@String)        
        if @idx!=0        
            set @slice = left(@String,@idx - 1)        
        else        
            set @slice = @String        
           
        if(len(@slice)>0)   
            insert into @temptable(parameter) values(@slice)        
  
        set @String = right(@String,len(@String) - @idx)        
        if len(@String) = 0 break        
    end    
return        
end 


GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_UserAccounts]') IS NOT NUll
	DROP VIEW [dbo].[uvw_UserAccounts]
/****** Object:  View [dbo].[uvw_UserAccounts]    Script Date: 3/13/2017 6:50:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_UserAccounts]
AS
SELECT        ua.ID, ua.CODE, ua.LAST_NAME, ua.FIRST_NAME, ua.MIDDLE_NAME, ua.LAST_NAME + ', ' + ua.FIRST_NAME + ' ' + ua.MIDDLE_NAME AS full_name, ua.ROLE_NAME, ua.POSTAL_ADDRESS, 
                         ua.EMAIL_ADDRESS, ua.POSITION_ID, ua.RANK_ID, dbo.rank.DESCRIPTION AS Rank, ua.ASSIGNED_OFFICE_ID, ua.COMPANY_ID, ua.PASSWORD_NEVER_EXPIRES, ua.STATUS, ua.TYPE, 
                         ua.REGISTERED_BY_ID, ua.DATETIME_REGISTERED, ua.LOCKOUT_COUNTER, ua.DATETIME_LOGIN_FAILED, ua.DATETIME_LOCKOUT, ua.EXPIRY_DATE, position.CODE AS position_code, 
                         position.DESCRIPTION AS position_desc, cost_center.CODE AS assigned_office_code, cost_center.DESCRIPTION AS assigned_office_desc, company.CODE AS company_code, 
                         company.NAME AS company_name, uasm.DESCRIPTION AS uasm_desc
FROM            dbo.user_account AS ua LEFT OUTER JOIN
                         dbo.position AS position ON ua.POSITION_ID = position.ID LEFT OUTER JOIN
                         dbo.organization AS cost_center ON ua.ASSIGNED_OFFICE_ID = cost_center.ID LEFT OUTER JOIN
                         dbo.company AS company ON ua.COMPANY_ID = company.ID INNER JOIN
                         dbo.user_account_status_map AS uasm ON ua.STATUS = uasm.CODE AND ua.TYPE = 0 LEFT OUTER JOIN
                         dbo.rank ON ua.RANK_ID = dbo.rank.ID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ua"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "position"
            Begin Extent = 
               Top = 6
               Left = 314
               Bottom = 119
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cost_center"
            Begin Extent = 
               Top = 6
               Left = 522
               Bottom = 136
               Right = 729
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "company"
            Begin Extent = 
               Top = 6
               Left = 767
               Bottom = 136
               Right = 1012
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "uasm"
            Begin Extent = 
               Top = 6
               Left = 1050
               Bottom = 102
               Right = 1220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rank"
            Begin Extent = 
               Top = 102
               Left = 1050
               Bottom = 215
               Right = 1220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         W' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_UserAccounts'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'idth = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_UserAccounts'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_UserAccounts'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISEmployment]') IS NOT NUll
	DROP VIEW [dbo].[uvw_PISEmployment]
/****** Object:  View [dbo].[uvw_PISEmployment]    Script Date: 3/13/2017 6:50:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISEmployment]
AS
SELECT        pisE.ID, pisE.PIS_ID, pisE.BUSINESS_TYPE_ID, busType.DESCRIPTION AS BusinessType, pisE.EMPLOYER_NAME, pisE.INCOME, pisE.CONTACT_NO, CONVERT(varchar, pisE.FROM_DATE, 101) AS FromDate, 
                         CONVERT(varchar, pisE.TO_DATE, 101) AS ToDate, pisE.IS_ACTIVE, pisE.IS_SPOUSE, pisE.NATURE_OF_BUSINESS_ID, natBusi.DESCRIPTION AS NatureOfBusiness
FROM            dbo.pis_employment AS pisE LEFT OUTER JOIN
                         dbo.business_type AS busType ON pisE.BUSINESS_TYPE_ID = busType.ID LEFT OUTER JOIN
                         dbo.nature_of_business AS natBusi ON pisE.NATURE_OF_BUSINESS_ID = natBusi.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pisE"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "busType"
            Begin Extent = 
               Top = 6
               Left = 302
               Bottom = 119
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "natBusi"
            Begin Extent = 
               Top = 6
               Left = 510
               Bottom = 119
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISEmployment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISEmployment'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISEducation]') IS NOT NUll
	DROP VIEW [dbo].[uvw_PISEducation]
/****** Object:  View [dbo].[uvw_PISEducation]    Script Date: 3/13/2017 6:50:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISEducation]
AS
SELECT        pisEdu.ID, pisEdu.PIS_ID, pisEdu.EDUCATION_TYPE_ID, eduType.DESCRIPTION AS EducationType, pisEdu.SCHOOL_NAME, pisEdu.GRADUATION_DATE
FROM            dbo.pis_education AS pisEdu LEFT OUTER JOIN
                         dbo.education_type AS eduType ON pisEdu.EDUCATION_TYPE_ID = eduType.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pisEdu"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "eduType"
            Begin Extent = 
               Top = 6
               Left = 279
               Bottom = 119
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISEducation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISEducation'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISDependent]') IS NOT NUll
	DROP VIEW [uvw_PISDependent]
/****** Object:  View [dbo].[uvw_PISDependent]    Script Date: 3/13/2017 6:50:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISDependent]
AS
SELECT        pisDep.ID, pisDep.PIS_ID, pisDep.FIRST_NAME, pisDep.MIDDLE_NAME, pisDep.LAST_NAME, pisDep.GENDER_ID, gend.DESCRIPTION AS Gender, pisDep.STREET_ADDRESS, pisDep.CITY_ID, 
                         dbo.city.DESCRIPTION AS City, prov.ID AS ProvinceID, prov.DESCRIPTION AS Province, pisDep.RELATIONSHIP_TYPE_ID, rela.DESCRIPTION AS RelationshipType, pisDep.BIRTH_DATE, 
                         pisDep.SCHOOL_ADDRESS, pisDep.CONTACT_NO
FROM            dbo.pis_dependent AS pisDep LEFT OUTER JOIN
                         dbo.city ON pisDep.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.province AS prov ON dbo.city.PROVINCE_ID = prov.ID LEFT OUTER JOIN
                         dbo.gender AS gend ON pisDep.GENDER_ID = gend.ID LEFT OUTER JOIN
                         dbo.relationship_type AS rela ON pisDep.RELATIONSHIP_TYPE_ID = rela.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pisDep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 6
               Left = 917
               Bottom = 136
               Right = 1087
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gend"
            Begin Extent = 
               Top = 6
               Left = 501
               Bottom = 136
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rela"
            Begin Extent = 
               Top = 6
               Left = 709
               Bottom = 119
               Right = 879
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISData]') IS NOT NUll
	DROP VIEW [uvw_PISData]
/****** Object:  View [dbo].[uvw_PISData]    Script Date: 3/13/2017 6:50:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISData]
AS
SELECT        a.ID, a.CODE, a.DATETIME_CREATED, a.ORGANIZATION_ID, org.DESCRIPTION AS Organization, dist.ID AS DistrictID, dist.DESCRIPTION AS District, a.FIRST_NAME, a.LAST_NAME, a.MIDDLE_NAME, 
                         a.GENDER_ID, gend.DESCRIPTION AS Gender, a.CIVIL_STATUS_ID, civi.DESCRIPTION AS CivilStatus, CONVERT(varchar, a.DATE_OF_MARRIAGE, 101) AS DateOfMarriage, a.CITIZENSHIP_ID, 
                         citi.DESCRIPTION AS Citizenship, CONVERT(varchar, a.DATE_OF_BIRTH, 101) AS DateOfBirth, a.GSIS_NUMBER, a.SSS_NUMBER, a.TIN_NUMBER, a.RCN, a.RCN_PLACE_ISSUED, a.RCN_DATE_ISSUED, 
                         a.BORROWER_TYPE_ID, borrT.DESCRIPTION AS BorrowerType, borrG.DESCRIPTION AS BorrowerGroup, a.LEAD_SOURCE_ID, lead.DESCRIPTION AS LeadSource, a.AGENT_PROFILE_ID, 
                         agentProf.LAST_NAME AS AgentLastName, agentProf.FIRST_NAME AS AgentFirstName, agentProf.MIDDLE_NAME AS AgentMiddleName, agentProf.CODE AS AgentCode, agentType.DESCRIPTION AS AgentType, 
                         docuStatus.DESCRIPTION AS DocumentationStatus, a.APPLICATION_TYPE_ID, appType.DESCRIPTION AS ApplicationType, a.SPOUSE_FIRST_NAME, a.SPOUSE_MIDDLE_NAME, a.SPOUSE_LAST_NAME, 
                         a.SPOUSE_DATE_OF_BIRTH, a.SPOUSE_CONTACT_NUMBER, a.OWNER_CODE, a.OWNER_ID, a.PREPARED_BY_ID, a.PREPARED_BY_DATETIME, a.DOCUMENT_STATUS_CODE, 
                         docuStatus.DESCRIPTION AS DocumentStatus, a.PERMISSION, a.NOTES
FROM            dbo.pis AS a LEFT OUTER JOIN
                         dbo.organization AS org ON a.ORGANIZATION_ID = org.ID LEFT OUTER JOIN
                         dbo.district AS dist ON org.DISTRICT_ID = dist.ID LEFT OUTER JOIN
                         dbo.gender AS gend ON a.GENDER_ID = gend.ID LEFT OUTER JOIN
                         dbo.civil_status AS civi ON a.CIVIL_STATUS_ID = civi.ID LEFT OUTER JOIN
                         dbo.citizenship AS citi ON a.CITIZENSHIP_ID = citi.ID LEFT OUTER JOIN
                         dbo.borrower_type AS borrT ON a.BORROWER_TYPE_ID = borrT.ID LEFT OUTER JOIN
                         dbo.borrower_group AS borrG ON borrT.BORROWER_GROUP_ID = borrG.ID LEFT OUTER JOIN
                         dbo.lead_source AS lead ON a.LEAD_SOURCE_ID = lead.ID LEFT OUTER JOIN
                         dbo.agent_profile AS agentProf ON a.AGENT_PROFILE_ID = agentProf.ID LEFT OUTER JOIN
                         dbo.agent_type AS agentType ON agentProf.AGENT_TYPE_ID = agentType.ID LEFT OUTER JOIN
                         dbo.document_status_map AS docuStatus ON agentProf.DOCUMENT_STATUS_CODE = docuStatus.CODE LEFT OUTER JOIN
                         dbo.application_type AS appType ON a.APPLICATION_TYPE_ID = appType.ID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[5] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "org"
            Begin Extent = 
               Top = 6
               Left = 321
               Bottom = 136
               Right = 528
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dist"
            Begin Extent = 
               Top = 6
               Left = 566
               Bottom = 136
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gend"
            Begin Extent = 
               Top = 6
               Left = 808
               Bottom = 136
               Right = 978
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "civi"
            Begin Extent = 
               Top = 6
               Left = 1016
               Bottom = 136
               Right = 1186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "citi"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "borrT"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         End
         Begin Table = "borrG"
            Begin Extent = 
               Top = 138
               Left = 496
               Bottom = 251
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lead"
            Begin Extent = 
               Top = 138
               Left = 704
               Bottom = 268
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentProf"
            Begin Extent = 
               Top = 138
               Left = 912
               Bottom = 268
               Right = 1150
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentType"
            Begin Extent = 
               Top = 252
               Left = 496
               Bottom = 365
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "docuStatus"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 366
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "appType"
            Begin Extent = 
               Top = 270
               Left = 246
               Bottom = 400
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 50
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3240
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISCharacter]') IS NOT NUll
	DROP VIEW [uvw_PISCharacter]
/****** Object:  View [dbo].[uvw_PISCharacter]    Script Date: 3/13/2017 6:50:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISCharacter]
AS
SELECT        pisChar.ID, pisChar.PIS_ID, pisChar.FIRST_NAME, pisChar.MIDDLE_NAME, pisChar.LAST_NAME, pisChar.RELATIONSHIP, pisChar.STREET_ADDRESS, pisChar.CITY_ID, dbo.city.DESCRIPTION AS City, 
                         prov.ID AS ProvinceID, prov.DESCRIPTION AS Province, pisChar.CONTACT_NO
FROM            dbo.pis_character AS pisChar LEFT OUTER JOIN
                         dbo.city ON pisChar.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.province AS prov ON dbo.city.PROVINCE_ID = prov.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pisChar"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 136
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 6
               Left = 466
               Bottom = 136
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISCharacter'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISCharacter'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_PISAddress]') IS NOT NUll
	DROP VIEW [uvw_PISAddress]
/****** Object:  View [dbo].[uvw_PISAddress]    Script Date: 3/13/2017 6:50:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_PISAddress]
AS
SELECT        pisAdd.ID, pisAdd.PIS_ID, pisAdd.ADDRESS_TYPE_ID, addType.DESCRIPTION AS AddressType, pisAdd.STREET_ADDRESS, pisAdd.BARANGAY_NAME, pisAdd.CITY_ID, dbo.city.DESCRIPTION AS City, 
                         prov.ID AS ProvinceID, prov.DESCRIPTION AS Province, coun.DESCRIPTION AS Country, pisAdd.POSTAL_CODE, pisAdd.PHONE_NUMBER, pisAdd.MOBILE_NUMBER, pisAdd.RESIDENT_DATE, 
                         pisAdd.HOME_OWNERSHIP_ID, homeOwn.DESCRIPTION AS HomeOwnership
FROM            dbo.pis_address AS pisAdd LEFT OUTER JOIN
                         dbo.address_type AS addType ON pisAdd.ADDRESS_TYPE_ID = addType.ID LEFT OUTER JOIN
                         dbo.home_ownership AS homeOwn ON pisAdd.HOME_OWNERSHIP_ID = homeOwn.ID LEFT OUTER JOIN
                         dbo.city ON pisAdd.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.province AS prov ON dbo.city.PROVINCE_ID = prov.ID LEFT OUTER JOIN
                         dbo.country AS coun ON prov.COUNTRY_ID = coun.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pisAdd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "addType"
            Begin Extent = 
               Top = 6
               Left = 288
               Bottom = 119
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "homeOwn"
            Begin Extent = 
               Top = 6
               Left = 496
               Bottom = 136
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 120
               Left = 288
               Bottom = 250
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "coun"
            Begin Extent = 
               Top = 138
               Left = 496
               Bottom = 268
               Right = 668
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISAddress'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_Company]') IS NOT NUll
	DROP VIEW [uvw_Company]
/****** Object:  View [dbo].[uvw_Company]    Script Date: 3/13/2017 6:49:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_Company]
AS
SELECT        cmpny.ID, cmpny.CODE, cmpny.NAME, cmpny.SHORT_DESCRIPTION, cmpny.ORGANIZATION_ID, org.DESCRIPTION AS OrganizationID, cmpny.PHONE_NUMBER, cmpny.MOBILE_NUMBER, 
                         cmpny.EMAIL_ADDRESS, cmpny.CONTACT_PERSON, cmpny.CONTACT_POSITION, cmpny.CONTACT_PHONE_NUMBER, cmpny.CONTACT_MOBILE_NUMBER, cmpny.CONTACT_EMAIL_ADDRESS, 
                         cmpny.TIN_NUMBER, cmpny.ACCOUNT_NUMBER, cmpny.STREET_NAME, cmpny.CITY_ID, dbo.city.DESCRIPTION AS City, cmpny.POSTAL_CODE, cmpny.PO_BOX, cmpny.WEBSITE, cmpny.INDUSTRY_ID, 
                         ind.DESCRIPTION AS Industry, cmpny.LEAD_SOURCE_ID, lead.DESCRIPTION AS LeadSource, cmpny.COMPANY_TYPE_ID, ctype.DESCRIPTION AS CompanyType, cmpny.PREPARED_BY_ID, 
                         cmpny.PREPARED_BY_DATETIME, cmpny.DOCUMENT_STATUS_CODE, docu.DESCRIPTION AS DocumentStatus, cmpny.PERMISSION, cmpny.NOTES
FROM            dbo.company AS cmpny LEFT OUTER JOIN
                         dbo.organization AS org ON cmpny.ORGANIZATION_ID = org.ID LEFT OUTER JOIN
                         dbo.city ON cmpny.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.industry AS ind ON cmpny.INDUSTRY_ID = ind.ID LEFT OUTER JOIN
                         dbo.lead_source AS lead ON cmpny.LEAD_SOURCE_ID = lead.ID LEFT OUTER JOIN
                         dbo.company_type AS ctype ON cmpny.COMPANY_TYPE_ID = ctype.ID LEFT OUTER JOIN
                         dbo.document_status_map AS docu ON cmpny.DOCUMENT_STATUS_CODE = docu.CODE

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cmpny"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "org"
            Begin Extent = 
               Top = 6
               Left = 321
               Bottom = 136
               Right = 528
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 6
               Left = 566
               Bottom = 136
               Right = 736
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ind"
            Begin Extent = 
               Top = 6
               Left = 774
               Bottom = 119
               Right = 944
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lead"
            Begin Extent = 
               Top = 120
               Left = 774
               Bottom = 250
               Right = 944
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ctype"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "docu"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 234
               Right = 416
            End
            DisplayFlags = 280
            TopColumn =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_Company'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_Company'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_Company'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_AgentProfileAddress]') IS NOT NUll
	DROP VIEW [uvw_AgentProfileAddress]
/****** Object:  View [dbo].[uvw_AgentProfileAddress]    Script Date: 3/13/2017 6:49:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_AgentProfileAddress]
AS
SELECT        AgentProfAdd.ID, AgentProfAdd.AGENT_PROFILE_ID, AgentProfAdd.ADDRESS_TYPE_ID, addType.DESCRIPTION AS AddressType, AgentProfAdd.STREET_ADDRESS, AgentProfAdd.BARANGAY_NAME, 
                         AgentProfAdd.CITY_ID, dbo.city.DESCRIPTION AS City, prov.ID AS ProvinceID, prov.DESCRIPTION AS Province, coun.DESCRIPTION AS Country, AgentProfAdd.POSTAL_CODE, AgentProfAdd.PHONE_NUMBER, 
                         AgentProfAdd.MOBILE_NUMBER, AgentProfAdd.RESIDENT_DATE, AgentProfAdd.HOME_OWNERSHIP_ID, homeOwn.DESCRIPTION AS HomeOwnership
FROM            dbo.agent_profile_address AS AgentProfAdd LEFT OUTER JOIN
                         dbo.address_type AS addType ON AgentProfAdd.ADDRESS_TYPE_ID = addType.ID LEFT OUTER JOIN
                         dbo.home_ownership AS homeOwn ON AgentProfAdd.HOME_OWNERSHIP_ID = homeOwn.ID LEFT OUTER JOIN
                         dbo.city ON AgentProfAdd.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.province AS prov ON dbo.city.PROVINCE_ID = prov.ID LEFT OUTER JOIN
                         dbo.country AS coun ON prov.COUNTRY_ID = coun.ID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AgentProfAdd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "addType"
            Begin Extent = 
               Top = 6
               Left = 288
               Bottom = 119
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "homeOwn"
            Begin Extent = 
               Top = 6
               Left = 496
               Bottom = 136
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 6
               Left = 704
               Bottom = 136
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 6
               Left = 912
               Bottom = 136
               Right = 1082
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "coun"
            Begin Extent = 
               Top = 6
               Left = 1120
               Bottom = 136
               Right = 1292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Widt' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfileAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'h = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfileAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfileAddress'
GO
USE [FINAL_TESTING]
GO
IF OBJECT_ID('[uvw_AgentProfile]') IS NOT NUll
	DROP VIEW [uvw_AgentProfile]
/****** Object:  View [dbo].[uvw_AgentProfile]    Script Date: 3/13/2017 6:49:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uvw_AgentProfile]
AS
SELECT        agentProf.ID, agentProf.CODE, agentProf.ORGANIZATION_ID, org.DESCRIPTION AS Organization, dbo.district.ID AS DistrictID, dbo.district.DESCRIPTION AS District, agentProf.FIRST_NAME, agentProf.LAST_NAME, 
                         agentProf.MIDDLE_NAME, agentProf.GENDER_ID, dbo.gender.DESCRIPTION AS Gender, agentProf.CIVIL_STATUS_ID, civil.DESCRIPTION AS CivilStatus, agentProf.DATE_OF_BIRTH, agentProf.AGENT_TYPE_ID, 
                         aType.DESCRIPTION AS AgentType, agentProf.WITH_CASH_CARD, agentProf.PREPARED_BY_ID, userAcc.LAST_NAME + ', ' + userAcc.FIRST_NAME + ' ' + userAcc.MIDDLE_NAME AS PreparedBy, 
                         agentProf.DOCUMENT_STATUS_CODE, docu.DESCRIPTION AS DocumentStatus, agentProf.PERMISSION, agentProf.NOTES
FROM            dbo.agent_profile AS agentProf INNER JOIN
                         dbo.document_status_map AS docu ON agentProf.DOCUMENT_STATUS_CODE = docu.CODE INNER JOIN
                         dbo.user_account AS userAcc ON agentProf.PREPARED_BY_ID = userAcc.ID INNER JOIN
                         dbo.gender ON agentProf.GENDER_ID = dbo.gender.ID INNER JOIN
                         dbo.civil_status AS civil ON agentProf.CIVIL_STATUS_ID = civil.ID INNER JOIN
                         dbo.agent_profile_address AS agentaddress ON agentaddress.AGENT_PROFILE_ID = agentProf.ID AND agentaddress.ADDRESS_TYPE_ID = '0' INNER JOIN
                         dbo.agent_type AS aType ON agentProf.AGENT_TYPE_ID = aType.ID INNER JOIN
                         dbo.organization AS org ON agentProf.ORGANIZATION_ID = org.ID INNER JOIN
                         dbo.district ON org.DISTRICT_ID = dbo.district.ID
WHERE        (1 = 1)


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "agentProf"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "docu"
            Begin Extent = 
               Top = 6
               Left = 314
               Bottom = 102
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "userAcc"
            Begin Extent = 
               Top = 6
               Left = 522
               Bottom = 136
               Right = 760
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gender"
            Begin Extent = 
               Top = 6
               Left = 798
               Bottom = 136
               Right = 968
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "civil"
            Begin Extent = 
               Top = 6
               Left = 1006
               Bottom = 136
               Right = 1176
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentaddress"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aType"
            Begin Extent = 
               Top = 102
               Left = 314
               Bottom = 215
               Right = 484
            End
            DisplayFlags = 280
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         TopColumn = 0
         End
         Begin Table = "org"
            Begin Extent = 
               Top = 138
               Left = 522
               Bottom = 268
               Right = 729
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "district"
            Begin Extent = 
               Top = 138
               Left = 767
               Bottom = 268
               Right = 971
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO






















