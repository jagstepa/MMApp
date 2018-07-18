USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_Musician]    Script Date: 2018/07/18 7:06:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Music_Musician](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StageName] [varchar](50) NOT NULL,
	[BirthName] [varchar](50) NOT NULL,
	[Website] [varchar](50) NULL,
	[YearsActiveFrom] [varchar](50) NOT NULL,
	[YearsActiveTo] [varchar](50) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[DOD] [datetime] NULL,
	[CityId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Music_Musician] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Music_Musician]  WITH CHECK ADD  CONSTRAINT [FK_Music_Musician_Music_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Music_Country] ([Id])
GO

ALTER TABLE [dbo].[Music_Musician] CHECK CONSTRAINT [FK_Music_Musician_Music_Country]
GO


