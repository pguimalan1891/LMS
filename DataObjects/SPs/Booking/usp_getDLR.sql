USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_getDLR]    Script Date: 6/16/2017 1:45:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--usp_getDLR 'DLR-2016112400001'
ALTER procedure [dbo].[usp_getDLR](
@lmsno varchar(30)
)
as
begin
SELECT
        a.id,
        a.code,
        a.bis_code,
        a.datetime_created,
        a.organization_id,
        a.loan_application_id,
        a.first_due_date,
        a.maturity_date,
        a.approved_mlv,
        a.add_on_rate,
        a.assigned_proceeds,
        a.mico_pip_range_id,
        a.rfc_pip_range_id,
        a.ppd_rate_id,
        a.agent_incentive_id,
        a.dealer_incentive_id,
        a.gibco_first_year,
        a.gibco_basic_1,
        a.gmmu_1,
        a.gibco_date_1,
        a.gibco_commission_rfc_1,
        a.gibco_commission_emp_1,
        a.gibco_second_year,
        a.gibco_basic_2,
        a.gmmu_2,
        a.gibco_date_2,
        a.gibco_commission_rfc_2,
        a.gibco_commission_emp_2,
        a.gibco_third_year,
        a.gibco_basic_3,
        a.gmmu_3,
        a.gibco_date_3,
        a.gibco_commission_rfc_3,
        a.gibco_commission_emp_3,
        a.gibco_fourth_year,
        a.gibco_basic_4,
        a.gmmu_4,
        a.gibco_date_4,
        a.gibco_commission_rfc_4,
        a.gibco_commission_emp_4,
        a.gibco_fifth_year,
        a.gibco_basic_5,
        a.gmmu_5,
        a.gibco_date_5,
        a.gibco_commission_rfc_5,
        a.gibco_commission_emp_5,
        a.net_monthly_installment,
        a.pnv_total,
        a.pnv_total_mi,
        a.pnv_pip,
        a.pnv_pip_mi,
        a.pnv_gibco,
        a.pnv_gibco_mi,
        a.pnv_rfc,
        a.pnv_rfc_mi,
        a.pnv_mico,
        a.pnv_mico_mi,
        a.pip_basic_premium,
        a.less_rppd,
        a.less_handling_fee,
        a.less_handling_fee_total,
        a.less_handling_fee_wo_pdc,
        a.less_handling_fee_wo_insurance,
        a.less_finance_charges,
        a.handling_fee,
        a.handling_fee_total,
        a.processing_fee,
        a.dst,
        a.pip_pvao,
        a.advance_rfc_mi,
        a.restructure_pip_amount,
        a.previous_dlr_id,
        a.previous_outstanding_balance,
        a.previous_uppd,
        a.previous_acceleration_discount,
        a.previous_waived_penalty,
        a.previous_price_differential,
        a.previous_accelerated_balance,
        a.previous_pip,
        a.previous_gibco,
        a.previous_net,
        a.deduct_gibco,
        a.deduct_handling_fee,
        a.deduct_dst,
        a.deduct_processing_fee,
        a.deduct_pip_pvao,
        a.deduct_restructure_pip_amount,
        a.deduct_trading_income,
        a.deduct_others,
        a.deduct_others_description,
        a.deduct_sub_total,
        a.proceeds_before_ec,
        a.deduct_ec_adjustment,
        a.net_amount_due,
        a.add_dealer_incentive,
        a.pip_mark_up,
        a.pip_commission_rfc,
        a.pip_commission_emp,
        a.add_other_addition,
        a.total_proceeds,
        a.total_ppd,
        a.total_handling_fee_wo_pdc,
        a.total_handling_fee_wo_insurance,
        a.net_proceeds,
        a.or_amount_handling,
        a.or_handling_id,
        a.or_amount_dst,
        a.or_dst_id,
        a.or_amount_restructuring,
        a.or_restructuring_id,
        a.or_amount_sales_repo,
        a.or_sales_repo_id,
        a.or_amount_gibco,
        a.or_gibco_id,
        a.or_amount_processing_fee,
        a.or_processing_fee_id,
        a.or_amount_pip_pvao,
        a.or_pip_pvao_id,
        a.or_amount_downpayment,
        a.or_downpayment_id,
        a.or_amount_restructure_pip,
        a.or_restructure_pip_id,
        a.pip_detail_code,
        a.pip_detail_no,
        a.pip_detail_date,
        a.gibco_confirmation_no,
        a.gibco_confirmation_date,
        a.agent_incentive_amount,
        a.is_matured,
        a.due_date,
        a.due_amount,
        a.balance,
        a.irr_percent,
        a.irr_guess,
        a.irr_amount,
        a.requested_by_id,
        a.requested_by_datetime,
        a.prepared_by_id,
        a.prepared_by_datetime,
        a.document_status_code,
        a.permission,
        a.notes,
		b.DESCRIPTION as branch,
		c.CODE as loan_application_code,
		c.*,
		d.OWNER_CODE,d.FIRST_NAME,d.LAST_NAME,d.MIDDLE_NAME,
		d.APPLICATION_TYPE_ID,
		e.DESCRIPTION as ApplicationTypeDesc,
		pis_address.address_type_id,
        pis_address.street_address,
        pis_address.barangay_name,
        pis_address.city_id,
        pis_address.postal_code,
        pis_address.phone_number,
        pis_address.mobile_number,
        pis_address.resident_date,
        pis_address.home_ownership_id,
        address_type.description address_type_desc,
        city.description city_name,
        city.province_id,
        province.description province_name,
        home_ownership.description ho_description,
		loan_type.DESCRIPTION as loan_type_desc,
		loan_set.DESCRIPTION as loan_set_desc,
		loan_terms.DESCRIPTION as loan_term_desc
        FROM
        Final_Testing.dbo.direct_loan_receipt a
		left join FINAL_TESTING.dbo.organization b
		on a.ORGANIZATION_ID=b.ID
		left join FINAL_TESTING.dbo.loan_application c
		on c.ID=a.LOAN_APPLICATION_ID
		left join FINAL_TESTING.dbo.pis d
		on c.CURRENT_PIS_ID=d.id
		left join Final_Testing.dbo.application_type e
		on e.ID=d.APPLICATION_TYPE_ID
		left join Final_Testing.dbo.pis_address
		on pis_address.PIS_ID=c.CURRENT_PIS_ID
        left JOIN Final_Testing.dbo.address_type
        ON (pis_address.address_type_id = address_type.id)
        left JOIN Final_Testing.dbo.home_ownership
        ON (pis_address.home_ownership_id = home_ownership.id)
        left JOIN Final_Testing.dbo.city
        ON (pis_address.city_id = city.id)
        left JOIN Final_Testing.dbo.province
        ON (city.province_id = province.id)
		left join FINAL_TESTING.dbo.loan_type
		on (loan_Type.ID=c.LOAN_TYPE_ID)
		left join FINAL_TESTING.dbo.loan_Set
		on (loan_set.ID=c.LOAN_SET_ID)
		left join FINAL_TESTING.dbo.loan_terms
		on (loan_terms.ID=c.LOAN_TERMS_ID)
	   WHERE a.code = @lmsno

end