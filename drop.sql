USE [TrainingTaskDb]
GO
ALTER TABLE [dbo].[TASK] DROP CONSTRAINT [Relation_PROJECT_TASK_FK]
GO
ALTER TABLE [dbo].[RELATION_EMPLOYEE_TASK] DROP CONSTRAINT [TASK_ID]
GO
ALTER TABLE [dbo].[RELATION_EMPLOYEE_TASK] DROP CONSTRAINT [Relation_1_TASK_FK]
GO
ALTER TABLE [dbo].[RELATION_EMPLOYEE_TASK] DROP CONSTRAINT [Relation_1_Employee_FK]
GO
ALTER TABLE [dbo].[RELATION_EMPLOYEE_TASK] DROP CONSTRAINT [FK_51]
GO
/****** Object:  Table [dbo].[TASK]    Script Date: 4/6/2020 12:48:00 PM ******/
DROP TABLE [dbo].[TASK]
GO
/****** Object:  Table [dbo].[RELATION_EMPLOYEE_TASK]    Script Date: 4/6/2020 12:48:00 PM ******/
DROP TABLE [dbo].[RELATION_EMPLOYEE_TASK]
GO
/****** Object:  Table [dbo].[PROJECT]    Script Date: 4/6/2020 12:48:00 PM ******/
DROP TABLE [dbo].[PROJECT]
GO
/****** Object:  Table [dbo].[EMPLOYEE]    Script Date: 4/6/2020 12:48:00 PM ******/
DROP TABLE [dbo].[EMPLOYEE]
GO
USE [master]
GO
/****** Object:  Database [TrainingTaskDb]    Script Date: 4/6/2020 12:48:00 PM ******/
DROP DATABASE [TrainingTaskDb]
GO
