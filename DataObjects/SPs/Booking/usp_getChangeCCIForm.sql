
--usp_getChangeCCIForm 10
alter procedure usp_getChangeCCIForm
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