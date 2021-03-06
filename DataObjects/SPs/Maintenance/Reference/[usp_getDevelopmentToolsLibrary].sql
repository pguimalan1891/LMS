USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_getDevelopmentToolsLibrary]    Script Date: 3/6/2017 1:30:28 PM ******/
IF OBJECT_ID('[usp_getDevelopmentToolsLibrary]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_getDevelopmentToolsLibrary]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--[usp_getDevelopmentToolsLibrary] 'CompanyTypeUpdCom'
CREATE PROCEDURE [dbo].[usp_getDevelopmentToolsLibrary]
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


END
