USE[FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_getCheckVoucher]    Script Date: 2/23/2017 4:09:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--usp_getCheckVoucher 33
ALTER procedure[dbo].[usp_getCheckVoucher]
(
	@statuscode int = 0 
)
as
begin
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

end