USE [FINAL_TESTING]
GO
/****** Object:  Table [dbo].[user_menu]    Script Date: 3/17/2017 4:28:07 PM ******/
if OBJECT_ID('[user_menu]') IS NOT NULL
	DROP TABLE [user_menu]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_menu](
	[MenuID] [int] NULL,
	[ParentID] [int] NULL,
	[MenuName] [varchar](255) NULL,
	[DisplayName] [varchar](255) NULL,
	[LnkAddress] [varchar](255) NULL,
	[Ordering] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (1, 0, N'Application', N'Application', N'', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (2, 1, N'BorrowerProfile', N'Borrower Profile', N'Customer', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (3, 1, N'LoanApplication', N'Loan Application', N'Application', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (4, 1, N'CreditInvestigation', N'Credit Investigation', N'Customer', 2)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (5, 0, N'Booking', N'Booking', N'', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (6, 5, N'DirectLoanReceipt', N'Direct Loan Receipt', N'Booking/DirectLoanReceipt', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (7, 5, N'CheckVoucher', N'Check Voucher', N'Booking/CheckVoucher', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (8, 5, N'CIRForm', N'CIR Form', N'Booking/CIRForm', 2)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (9, 5, N'DisbursementVoucher', N'Disbursement Voucher', N'Booking/DisbursementVoucher', 3)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (10, 5, N'PromissoryNoteAllocation', N'Promissory Note Allocation', N'Booking/PromissoryNoteAllocation', 4)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (11, 5, N'ChangeCCI', N'Change CCI', N'Booking/ChangeCCI', 5)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (12, 0, N'Collections', N'Collections', N'', 2)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (13, 12, N'OfficialReceipt', N'Official Receipt', N'', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (14, 13, N'SundryCollection', N'Sundry Collection', N'OfficialReceipt/NewSundry', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (15, 13, N'ORListing', N'OR Listing', N'Home/Contact', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (16, 12, N'PDCAcknowledgementReceipt', N'PDC Acknowledgement Receipt', N'Home/Contact', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (17, 12, N'PDCBatchEncoding', N'PDC Batch Encoding', N'Home/Contact', 2)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (18, 12, N'ReturnCheckNotice', N'Return Check Notice', N'Home/Contact', 3)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (19, 12, N'DepositSlip', N'Deposit Slip', N'Home/Contact', 4)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (20, 0, N'Accounting', N'Accounting', N'', 3)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (21, 20, N'RequestforPayment', N'Requestfor Payment', N'Accounting/RequestforPayment', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (22, 0, N'Maintenance', N'Maintenance', N'', 4)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (23, 22, N'AgentProfile', N'Agent Profile', N'AgentProfile', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (24, 22, N'Reference', N'Reference', N'Reference', 1)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (25, 0, N'DeveloopmentTool', N'Develoopment Tool', N'', 5)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (26, 25, N'SecurityManager', N'Security Manager', N'SecurityManager', 0)
GO
INSERT [dbo].[user_menu] ([MenuID], [ParentID], [MenuName], [DisplayName], [LnkAddress], [Ordering]) VALUES (27, 25, N'Library', N'Library', N'LibraryList', 1)
GO
