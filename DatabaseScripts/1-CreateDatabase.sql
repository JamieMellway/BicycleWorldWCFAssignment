﻿USE [master]
GO

/****** Object:  Database [BicycleWorld]    Script Date: 03/15/2013 10:36:22 ******/
CREATE DATABASE [BicycleWorld] ON  PRIMARY 
( NAME = N'BicycleWorld', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008R2D\MSSQL\DATA\BicycleWorld.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BicycleWorld_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008R2D\MSSQL\DATA\BicycleWorld_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [BicycleWorld] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BicycleWorld].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO

ALTER DATABASE [BicycleWorld] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [BicycleWorld] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [BicycleWorld] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [BicycleWorld] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [BicycleWorld] SET ARITHABORT OFF 
GO

ALTER DATABASE [BicycleWorld] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [BicycleWorld] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [BicycleWorld] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [BicycleWorld] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [BicycleWorld] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [BicycleWorld] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [BicycleWorld] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [BicycleWorld] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [BicycleWorld] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [BicycleWorld] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [BicycleWorld] SET  ENABLE_BROKER 
GO

ALTER DATABASE [BicycleWorld] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [BicycleWorld] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [BicycleWorld] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [BicycleWorld] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [BicycleWorld] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [BicycleWorld] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [BicycleWorld] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [BicycleWorld] SET  READ_WRITE 
GO

ALTER DATABASE [BicycleWorld] SET RECOVERY FULL 
GO

ALTER DATABASE [BicycleWorld] SET  MULTI_USER 
GO

ALTER DATABASE [BicycleWorld] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [BicycleWorld] SET DB_CHAINING OFF 
GO


