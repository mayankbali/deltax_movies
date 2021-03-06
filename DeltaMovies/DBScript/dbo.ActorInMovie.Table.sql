USE [DeltaMovies]
GO
/****** Object:  Table [dbo].[ActorInMovie]    Script Date: 23-06-2017 03:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorInMovie](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ActorId] [int] NOT NULL,
	[MovieId] [bigint] NOT NULL,
 CONSTRAINT [PK_ActorInMovie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[ActorId] ASC,
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ActorInMovie]  WITH CHECK ADD  CONSTRAINT [FK_ActorInMovie_ActorInfo] FOREIGN KEY([ActorId])
REFERENCES [dbo].[ActorInfo] ([ActorId])
GO
ALTER TABLE [dbo].[ActorInMovie] CHECK CONSTRAINT [FK_ActorInMovie_ActorInfo]
GO
ALTER TABLE [dbo].[ActorInMovie]  WITH CHECK ADD  CONSTRAINT [FK_ActorInMovie_MovieInfo] FOREIGN KEY([MovieId])
REFERENCES [dbo].[MovieInfo] ([MovieId])
GO
ALTER TABLE [dbo].[ActorInMovie] CHECK CONSTRAINT [FK_ActorInMovie_MovieInfo]
GO
