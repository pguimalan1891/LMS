USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_getCIRForm]    Script Date: 2/23/2017 4:08:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--usp_getCIRForm '10'
ALTER procedure [dbo].[usp_getCIRForm]
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