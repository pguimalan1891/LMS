USE [FINAL_TESTING]
GO

/****** Object:  View [dbo].[uvw_PISData]    Script Date: 3/6/2017 1:33:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('[uvw_PISData]') IS NOT NUll
	DROP VIEW [dbo].[uvw_PISData]
GO
CREATE VIEW [dbo].[uvw_PISData]
AS
SELECT        a.ID, a.CODE, a.DATETIME_CREATED, a.ORGANIZATION_ID, org.DESCRIPTION AS Organization, dist.ID AS DistrictID, dist.DESCRIPTION AS District, a.FIRST_NAME, a.LAST_NAME, a.MIDDLE_NAME, 
                         a.GENDER_ID, gend.DESCRIPTION AS Gender, a.CIVIL_STATUS_ID, civi.DESCRIPTION AS CivilStatus, CONVERT(varchar, a.DATE_OF_MARRIAGE, 101) AS DateOfMarriage, a.CITIZENSHIP_ID, 
                         citi.DESCRIPTION AS Citizenship, CONVERT(varchar, a.DATE_OF_BIRTH, 101) AS DateOfBirth, a.GSIS_NUMBER, a.SSS_NUMBER, a.TIN_NUMBER, a.RCN, a.RCN_PLACE_ISSUED, a.RCN_DATE_ISSUED, 
                         a.BORROWER_TYPE_ID, borrT.DESCRIPTION AS BorrowerType, borrG.DESCRIPTION AS BorrowerGroup, a.LEAD_SOURCE_ID, lead.DESCRIPTION AS LeadSource, a.AGENT_PROFILE_ID, 
                         agentProf.LAST_NAME AS AgentLastName, agentProf.FIRST_NAME AS AgentFirstName, agentProf.MIDDLE_NAME AS AgentMiddleName, agentProf.CODE AS AgentCode, agentType.DESCRIPTION AS AgentType, 
                         docuStatus.DESCRIPTION AS DocumentationStatus, a.APPLICATION_TYPE_ID, appType.DESCRIPTION AS ApplicationType, a.SPOUSE_FIRST_NAME, a.SPOUSE_MIDDLE_NAME, a.SPOUSE_LAST_NAME, 
                         a.SPOUSE_DATE_OF_BIRTH, a.SPOUSE_CONTACT_NUMBER, a.OWNER_CODE, a.OWNER_ID, a.PREPARED_BY_ID, a.PREPARED_BY_DATETIME, a.DOCUMENT_STATUS_CODE, a.PERMISSION, 
                         a.NOTES
FROM            dbo.pis AS a LEFT OUTER JOIN
                         dbo.organization AS org ON a.ORGANIZATION_ID = org.ID LEFT OUTER JOIN
                         dbo.district AS dist ON org.DISTRICT_ID = dist.ID LEFT OUTER JOIN
                         dbo.gender AS gend ON a.GENDER_ID = gend.ID LEFT OUTER JOIN
                         dbo.civil_status AS civi ON a.CIVIL_STATUS_ID = civi.ID LEFT OUTER JOIN
                         dbo.citizenship AS citi ON a.CITIZENSHIP_ID = citi.ID LEFT OUTER JOIN
                         dbo.borrower_type AS borrT ON a.BORROWER_TYPE_ID = borrT.ID LEFT OUTER JOIN
                         dbo.borrower_group AS borrG ON borrT.BORROWER_GROUP_ID = borrG.ID LEFT OUTER JOIN
                         dbo.lead_source AS lead ON a.LEAD_SOURCE_ID = lead.ID LEFT OUTER JOIN
                         dbo.agent_profile AS agentProf ON a.AGENT_PROFILE_ID = agentProf.ID LEFT OUTER JOIN
                         dbo.agent_type AS agentType ON agentProf.AGENT_TYPE_ID = agentType.ID LEFT OUTER JOIN
                         dbo.document_status_map AS docuStatus ON agentProf.DOCUMENT_STATUS_CODE = docuStatus.CODE LEFT OUTER JOIN
                         dbo.application_type AS appType ON a.APPLICATION_TYPE_ID = appType.ID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[5] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "org"
            Begin Extent = 
               Top = 6
               Left = 321
               Bottom = 136
               Right = 528
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dist"
            Begin Extent = 
               Top = 6
               Left = 566
               Bottom = 136
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gend"
            Begin Extent = 
               Top = 6
               Left = 808
               Bottom = 136
               Right = 978
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "civi"
            Begin Extent = 
               Top = 6
               Left = 1016
               Bottom = 136
               Right = 1186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "citi"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "borrT"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         End
         Begin Table = "borrG"
            Begin Extent = 
               Top = 138
               Left = 496
               Bottom = 251
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lead"
            Begin Extent = 
               Top = 138
               Left = 704
               Bottom = 268
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentProf"
            Begin Extent = 
               Top = 138
               Left = 912
               Bottom = 268
               Right = 1150
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentType"
            Begin Extent = 
               Top = 252
               Left = 496
               Bottom = 365
               Right = 666
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "docuStatus"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 366
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "appType"
            Begin Extent = 
               Top = 270
               Left = 246
               Bottom = 400
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 50
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3240
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISData'
GO


