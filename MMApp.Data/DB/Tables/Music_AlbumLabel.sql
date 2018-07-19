USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_AlbumLabel]    Script Date: 2018/07/18 2:26:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_AlbumLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[LabelId] [int] NOT NULL,
 CONSTRAINT [PK_Music_AlbumLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_AlbumLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumLabel_Music_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Music_Album] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumLabel] CHECK CONSTRAINT [FK_Music_AlbumLabel_Music_Album]
GO

ALTER TABLE [dbo].[Music_AlbumLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumLabel_Music_Label] FOREIGN KEY([LabelId])
REFERENCES [dbo].[Music_Label] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumLabel] CHECK CONSTRAINT [FK_Music_AlbumLabel_Music_Label]
GO


