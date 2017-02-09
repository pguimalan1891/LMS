USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[RetrieveBookingRecords]    Script Date: 2/8/2017 10:22:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec RetrieveBookingRecords
ALTER Procedure [dbo].[RetrieveBookingRecords]
	@statuscode int = 0
as
begin
	select 
	'' as blank,ROW_NUMBER() OVER(ORDER BY direct_loan_receipt.id ASC) AS [Seq],
	--direct_loan_receipt.id,
	document_status_map.description as Status,
	direct_loan_receipt.code as [LMS DLR No], 
	direct_loan_receipt.bis_code as [DLR No], 
	convert(Varchar(10),direct_loan_receipt.datetime_created,101) as [Date],
	organization.description as [Branch], 
	loan_application.code as [Account No], 
	--cast('~/Transactions/RFC/LoanApplication.aspx?state=4&code=' + loan_application.code as char(100)) as loan_application_hyperlink, 
	pis.first_name  +  ' '  + pis.middle_name  +  ' '  + pis.last_name  as [Customer], 
	--cast('~/Transactions/RFC/PIS.aspx?state=4&id=' + pis.id as char(100)) as pis_hyperlink, 
	loan_type.description as [Loan Type], loan_terms.description as [Loan Terms], 
	loan_set.description as [Loan Set],direct_loan_receipt.approved_mlv as [MLV],direct_loan_receipt.prepared_by_datetime as prepared_by_datetime,
	prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name 
	--,
	--row_number() OVER (INSERT ORDER BY HERE) as row_num  
	from Final_Testing.dbo.direct_loan_receipt  inner join Final_Testing.dbo.document_status_map   on(direct_loan_receipt.document_status_code = document_status_map.code) 
	inner join Final_Testing.dbo.user_account prepared_by   on(direct_loan_receipt.prepared_by_id = prepared_by.id) 
	inner join Final_Testing.dbo.loan_application   on(direct_loan_receipt.loan_application_id = loan_application.id) 
	inner join Final_Testing.dbo.pis   on(loan_application.current_pis_id = pis.id)  inner join Final_Testing.dbo.loan_type   on(loan_application.loan_type_id = loan_type.id) 
	inner join Final_Testing.dbo.loan_terms   on(loan_application.loan_terms_id = loan_terms.id) 
	inner join Final_Testing.dbo.loan_set   on(loan_application.loan_set_id = loan_set.id) 
	inner join Final_Testing.dbo.organization   on(direct_loan_receipt.organization_id = organization.id)  where  1 = 1 
	AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')
	and direct_loan_receipt.document_status_Code = @statuscode
end