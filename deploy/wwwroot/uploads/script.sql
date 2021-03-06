USE [TestRealtyZoom]
GO
/****** Object:  Table [dbo].[ZipCodes]    Script Date: 4/20/2017 6:19:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZipCodes](
	[ZipCodeID] [int] NOT NULL,
	[StateID] [int] NULL,
	[MLSID] [bigint] NULL,
	[CityName] [varchar](50) NULL,
	[Population] [int] NULL,
	[County] [varchar](50) NULL,
 CONSTRAINT [PK_ZipCodes] PRIMARY KEY CLUSTERED 
(
	[ZipCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
