﻿USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_Instrument]    Script Date: 2018/07/18 7:05:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Music_Instrument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstrumentName] [varchar](50) NOT NULL,
	[Website] [varchar](50) NULL,
 CONSTRAINT [PK_Music_Instrument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


