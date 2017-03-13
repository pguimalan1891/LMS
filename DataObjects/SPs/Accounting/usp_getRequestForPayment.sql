
--usp_getRequestForPayment 3
alter procedure usp_getRequestForPayment(
	@statuscode int=0
)
as
begin
select request_for_payment.document_status_code,request_for_payment.id,document_status_map.description as dsm_description, request_for_payment.code, request_for_payment.datetime_created, organization.description branch_name,
 expense_type.description et_description, request_for_payment.amount, request_for_payment.payee, prepared_by.last_name  +  ', '  + prepared_by.first_name  as prepared_by_name 
 --,row_number() OVER (INSERT ORDER BY HERE) as row_num 
 into #temp 
 from Final_Testing.dbo.request_for_payment  
 inner join Final_Testing.dbo.document_status_map   on(request_for_payment.document_status_code = document_status_map.code) 
  inner join Final_Testing.dbo.user_account prepared_by   on(request_for_payment.prepared_by_id = prepared_by.id)  inner join Final_Testing.dbo.expense_type   on(request_for_payment.expense_type_id = expense_type.id)
  inner join Final_Testing.dbo.organization   on(request_for_payment.organization_id = organization.id)  where  1 = 1 

 AND Final_Testing.dbo.organization.id IN ('c85e042d-9187-4b51-92bc-cc0d0d4e84f0','A9CCBB1C-D3CD-402A-9BEF-B73BCF545255','{187FFF20-BC01-4E11-9501-3526B9CA0DBE}')

 select id, dsm_description, code, datetime_created, branch_name, et_description, amount, payee, prepared_by_name from #temp where 
 document_status_code=@statuscode
 
 drop table #temp
 end