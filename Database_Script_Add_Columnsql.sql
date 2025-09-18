USE [JTBD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Stories] ADD [GroupsIdGroup] [int] NULL
GO

ALTER TABLE [dbo].[Stories]  WITH CHECK ADD  CONSTRAINT [FK_Stories_Groups] FOREIGN KEY([GroupsIdGroup])
REFERENCES [dbo].[Groups] ([IdGroup])
GO
