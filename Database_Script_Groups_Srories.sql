ALTER TABLE [dbo].[StoriesPulls] ADD [GroupsIdGroup] [int] NULL

ALTER TABLE [dbo].[StoriesPushes] ADD [GroupsIdGroup] [int] NULL

GO

ALTER TABLE [dbo].[StoriesPulls]  WITH CHECK ADD  CONSTRAINT [FK_StoriesPulls_Groups] FOREIGN KEY([GroupsIdGroup])
REFERENCES [dbo].[Groups] ([IdGroup])
GO

ALTER TABLE [dbo].[StoriesPushes]  WITH CHECK ADD  CONSTRAINT [FK_StoriesPushes_Groups] FOREIGN KEY([GroupsIdGroup])
REFERENCES [dbo].[Groups] ([IdGroup])
GO
