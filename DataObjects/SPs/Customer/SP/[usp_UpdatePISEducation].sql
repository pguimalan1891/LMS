USE [FINAL_TESTING]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePISEducation]    Script Date: 3/6/2017 1:32:04 PM ******/
IF OBJECT_ID('[usp_UpdatePISEducation]') IS NOT NUll
	DROP PROCEDURE [dbo].[usp_UpdatePISEducation]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePISEducation]
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
