USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_updDevelopmentToolsLibrary]    Script Date: 3/6/2017 1:31:29 PM ******/
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
-- Exec usp_updDevelopmentToolsLibrary 'Industry',0,'1|0001|Customer|'
-- Exec usp_updDevelopmentToolsLibrary 'loan_terms',0,'1199f388-fff4-418b-8e6b-4faa06e585e2|0066|128|'
-- Select * from [tempCompValues]
CREATE PROCEDURE [dbo].[usp_updDevelopmentToolsLibrary]
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
	
	Begin Try 
		Exec(@sqlQuery)
		Select 1 as ErrorMessage
	End Try
	Begin Catch
		--Select ERROR_MESSAGE()
		Select ERROR_NUMBER() as ErrorMessage
	End Catch	
		
		
END
