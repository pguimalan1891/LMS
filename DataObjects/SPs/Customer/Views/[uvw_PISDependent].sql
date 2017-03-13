USE [FINAL_TESTING]
GO

/****** Object:  View [dbo].[uvw_PISDependent]    Script Date: 3/6/2017 1:33:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('[uvw_PISDependent]') IS NOT NUll
	DROP VIEW [dbo].[uvw_PISDependent]
GO
CREATE VIEW [dbo].[uvw_PISDependent]
AS
SELECT        pisDep.ID, pisDep.PIS_ID, pisDep.FIRST_NAME, pisDep.MIDDLE_NAME, pisDep.LAST_NAME, pisDep.GENDER_ID, gend.DESCRIPTION AS Gender, pisDep.STREET_ADDRESS, pisDep.CITY_ID, 
                         dbo.city.DESCRIPTION AS City, prov.ID AS ProvinceID, prov.DESCRIPTION AS Province, pisDep.RELATIONSHIP_TYPE_ID, rela.DESCRIPTION AS RelationshipType, pisDep.BIRTH_DATE, 
                         pisDep.SCHOOL_ADDRESS, pisDep.CONTACT_NO
FROM            dbo.pis_dependent AS pisDep LEFT OUTER JOIN
                         dbo.city ON pisDep.CITY_ID = dbo.city.ID LEFT OUTER JOIN
                         dbo.province AS prov ON dbo.city.PROVINCE_ID = prov.ID LEFT OUTER JOIN
                         dbo.gender AS gend ON pisDep.GENDER_ID = gend.ID LEFT OUTER JOIN
                         dbo.relationship_type AS rela ON pisDep.RELATIONSHIP_TYPE_ID = rela.ID

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
         Begin Table = "pisDep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "city"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 6
               Left = 917
               Bottom = 136
               Right = 1087
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gend"
            Begin Extent = 
               Top = 6
               Left = 501
               Bottom = 136
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rela"
            Begin Extent = 
               Top = 6
               Left = 709
               Bottom = 119
               Right = 879
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
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Output = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'uvw_PISDependent'
GO


