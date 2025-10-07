USE [JTBD]
GO

/****** Object:  Table [dbo].[StoriesGroupsPulls]    Script Date: 6/10/2025 19:46:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StoriesGroupsPulls](
	[StoriesIdStorie] [int] NOT NULL,
	[GroupsIdGroup] [int] NOT NULL,
	[ValorPull] [int] NOT NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StoriesGroupsPushes]    Script Date: 6/10/2025 19:46:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StoriesGroupsPushes](
	[StoriesIdStorie] [int] NOT NULL,
	[GroupsIdGroup] [int] NOT NULL,
	[ValorPush] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StoriesGroupsPulls]  WITH CHECK ADD  CONSTRAINT [FK_StoriesGroupsPulls_Groups] FOREIGN KEY([GroupsIdGroup])
REFERENCES [dbo].[Groups] ([IdGroup])
GO

ALTER TABLE [dbo].[StoriesGroupsPulls] CHECK CONSTRAINT [FK_StoriesGroupsPulls_Groups]
GO

ALTER TABLE [dbo].[StoriesGroupsPulls]  WITH CHECK ADD  CONSTRAINT [FK_StoriesGroupsPulls_Stories] FOREIGN KEY([StoriesIdStorie])
REFERENCES [dbo].[Stories] ([IdStorie])
GO

ALTER TABLE [dbo].[StoriesGroupsPulls] CHECK CONSTRAINT [FK_StoriesGroupsPulls_Stories]
GO

ALTER TABLE [dbo].[StoriesGroupsPushes]  WITH CHECK ADD  CONSTRAINT [FK_StoriesGroupsPushes_Groups] FOREIGN KEY([GroupsIdGroup])
REFERENCES [dbo].[Groups] ([IdGroup])
GO

ALTER TABLE [dbo].[StoriesGroupsPushes] CHECK CONSTRAINT [FK_StoriesGroupsPushes_Groups]
GO

ALTER TABLE [dbo].[StoriesGroupsPushes]  WITH CHECK ADD  CONSTRAINT [FK_StoriesGroupsPushes_Stories] FOREIGN KEY([StoriesIdStorie])
REFERENCES [dbo].[Stories] ([IdStorie])
GO

ALTER TABLE [dbo].[StoriesGroupsPushes] CHECK CONSTRAINT [FK_StoriesGroupsPushes_Stories]
GO
