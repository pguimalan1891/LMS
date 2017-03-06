USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_getDisbursementVoucher]    Script Date: 3/3/2017 3:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--usp_getDisbursementVoucher 48
ALTER procedure [dbo].[usp_getDisbursementVoucher](
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