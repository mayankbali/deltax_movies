USE [DeltaMovies]
GO
/****** Object:  Table [dbo].[MovieInfo]    Script Date: 6/22/2017 7:42:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieInfo](
	[MovieId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[YearOfRelease] [int] NOT NULL,
	[Plot] [nvarchar](500) NULL,
	[Poster] [nvarchar](500) NULL,
	[ProducedBy] [int] NOT NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_MovieInfo_Status]  DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_MovieInfo_CreatedBy]  DEFAULT (getdate()),
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MovieInfo] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MovieInfo]  WITH CHECK ADD  CONSTRAINT [FK_MovieInfo_ProducerInfo] FOREIGN KEY([ProducedBy])
REFERENCES [dbo].[ProducerInfo] ([ProducerId])
GO
ALTER TABLE [dbo].[MovieInfo] CHECK CONSTRAINT [FK_MovieInfo_ProducerInfo]
GO
