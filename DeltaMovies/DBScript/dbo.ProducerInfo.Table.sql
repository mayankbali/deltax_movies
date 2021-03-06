USE [DeltaMovies]
GO
/****** Object:  Table [dbo].[ProducerInfo]    Script Date: 6/22/2017 7:42:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProducerInfo](
	[ProducerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Sex] [char](1) NOT NULL,
	[DOB] [date] NOT NULL,
	[Bio] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_ProducerInfo_CreatedDate]  DEFAULT (getdate()),
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProducerInfp] PRIMARY KEY CLUSTERED 
(
	[ProducerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
