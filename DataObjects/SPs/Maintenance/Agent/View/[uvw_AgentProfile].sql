USE [FINAL_TESTING]
GO

/****** Object:  View [dbo].[uvw_AgentProfile]    Script Date: 3/6/2017 1:36:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('[uvw_AgentProfile]') IS NOT NUll
	DROP VIEW [dbo].[uvw_AgentProfile]
GO
CREATE VIEW [dbo].[uvw_AgentProfile]
AS
SELECT        agentProf.ID, agentProf.CODE, agentProf.ORGANIZATION_ID, org.DESCRIPTION AS Organization, dbo.district.ID AS DistrictID, dbo.district.DESCRIPTION AS District, agentProf.FIRST_NAME, agentProf.LAST_NAME, 
                         agentProf.MIDDLE_NAME, agentProf.GENDER_ID, dbo.gender.DESCRIPTION AS Gender, agentProf.CIVIL_STATUS_ID, civil.DESCRIPTION AS CivilStatus, agentProf.DATE_OF_BIRTH, agentProf.AGENT_TYPE_ID, 
                         aType.DESCRIPTION AS AgentType, agentProf.WITH_CASH_CARD, agentProf.PREPARED_BY_ID, userAcc.LAST_NAME + ', ' + userAcc.FIRST_NAME + ' ' + userAcc.MIDDLE_NAME AS PreparedBy, 
                         agentProf.DOCUMENT_STATUS_CODE, docu.DESCRIPTION AS DocumentStatus, agentProf.PERMISSION, agentProf.NOTES
FROM            dbo.agent_profile AS agentProf INNER JOIN
                         dbo.document_status_map AS docu ON agentProf.DOCUMENT_STATUS_CODE = docu.CODE INNER JOIN
                         dbo.user_account AS userAcc ON agentProf.PREPARED_BY_ID = userAcc.ID INNER JOIN
                         dbo.gender ON agentProf.GENDER_ID = dbo.gender.ID INNER JOIN
                         dbo.civil_status AS civil ON agentProf.CIVIL_STATUS_ID = civil.ID INNER JOIN
                         dbo.agent_profile_address AS agentaddress ON agentaddress.AGENT_PROFILE_ID = agentProf.ID AND agentaddress.ADDRESS_TYPE_ID = '0' INNER JOIN
                         dbo.agent_type AS aType ON agentProf.AGENT_TYPE_ID = aType.ID INNER JOIN
                         dbo.organization AS org ON agentProf.ORGANIZATION_ID = org.ID INNER JOIN
                         dbo.district ON org.DISTRICT_ID = dbo.district.ID
WHERE        (1 = 1)

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "agentProf"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "docu"
            Begin Extent = 
               Top = 6
               Left = 314
               Bottom = 102
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "userAcc"
            Begin Extent = 
               Top = 6
               Left = 522
               Bottom = 136
               Right = 760
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gender"
            Begin Extent = 
               Top = 6
               Left = 798
               Bottom = 136
               Right = 968
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "civil"
            Begin Extent = 
               Top = 6
               Left = 1006
               Bottom = 136
               Right = 1176
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "agentaddress"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aType"
            Begin Extent = 
               Top = 102
               Left = 314
               Bottom = 215
               Right = 484
            End
            DisplayFlags = 280
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         TopColumn = 0
         End
         Begin Table = "org"
            Begin Extent = 
               Top = 138
               Left = 522
               Bottom = 268
               Right = 729
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "district"
            Begin Extent = 
               Top = 138
               Left = 767
               Bottom = 268
               Right = 971
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
      Begin ColumnWidths = 9
         Width = 284
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_AgentProfile'
GO


