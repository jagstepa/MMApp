USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_BandLabel]    Script Date: 2018/07/18 2:30:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_BandLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BandId] [int] NOT NULL,
	[LabelId] [int] NOT NULL,
 CONSTRAINT [PK_Music_BandLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_BandLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandLabel_Music_Band] FOREIGN KEY([BandId])
REFERENCES [dbo].[Music_Band] ([Id])
GO

ALTER TABLE [dbo].[Music_BandLabel] CHECK CONSTRAINT [FK_Music_BandLabel_Music_Band]
GO

ALTER TABLE [dbo].[Music_BandLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandLabel_Music_Label] FOREIGN KEY([LabelId])
REFERENCES [dbo].[Music_Label] ([Id])
GO

ALTER TABLE [dbo].[Music_BandLabel] CHECK CONSTRAINT [FK_Music_BandLabel_Music_Label]
GO


