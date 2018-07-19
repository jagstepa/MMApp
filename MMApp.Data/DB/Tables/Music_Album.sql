USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_Album]    Script Date: 2018/07/18 2:25:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Music_Album](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumName] [varchar](50) NOT NULL,
	[TypeId] [int] NOT NULL,
	[Website] [varchar](50) NULL,
	[Year] [int] NOT NULL,
	[Released] [int] NULL,
	[Recorded] [int] NULL,
	[Length] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Music_Album] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


