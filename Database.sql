USE [PatientsAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/23/2022 22:24:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacciene]    Script Date: 10/23/2022 22:24:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacciene](
	[VaccieneId] [int] IDENTITY(1,1) NOT NULL,
	[VaccieneName] [nvarchar](max) NOT NULL,
	[ActiveMonths] [int] NOT NULL,
 CONSTRAINT [PK_Vacciene] PRIMARY KEY CLUSTERED 
(
	[VaccieneId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 10/23/2022 22:24:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Nationality] [nvarchar](max) NOT NULL,
	[EmiratesID] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientHistories]    Script Date: 10/23/2022 22:24:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[EntryCreatedDate] [datetime2](7) NOT NULL,
	[VaccieneId] [int] NOT NULL,
 CONSTRAINT [PK_PatientHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_PatientHistories_Patients_PatientId]    Script Date: 10/23/2022 22:24:46 ******/
ALTER TABLE [dbo].[PatientHistories]  WITH CHECK ADD  CONSTRAINT [FK_PatientHistories_Patients_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientHistories] CHECK CONSTRAINT [FK_PatientHistories_Patients_PatientId]
GO
/****** Object:  ForeignKey [FK_PatientHistories_Vacciene_VaccieneId]    Script Date: 10/23/2022 22:24:46 ******/
ALTER TABLE [dbo].[PatientHistories]  WITH CHECK ADD  CONSTRAINT [FK_PatientHistories_Vacciene_VaccieneId] FOREIGN KEY([VaccieneId])
REFERENCES [dbo].[Vacciene] ([VaccieneId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientHistories] CHECK CONSTRAINT [FK_PatientHistories_Vacciene_VaccieneId]
GO
