USE [master]
GO
/****** Object:  Database [ToysStore]    Script Date: 8/15/2022 11:28:30 AM ******/
CREATE DATABASE [ToysStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToysStore', FILENAME = N'C:\Users\gdjemont\ToysStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ),
( NAME = N'test', FILENAME = N'C:\Users\gdjemont\test.ndf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToysStore_log', FILENAME = N'C:\Users\gdjemont\ToysStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ToysStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToysStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToysStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToysStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToysStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToysStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToysStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToysStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToysStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToysStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToysStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToysStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToysStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToysStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToysStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToysStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToysStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToysStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToysStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ToysStore] SET  MULTI_USER 
GO
ALTER DATABASE [ToysStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToysStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToysStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToysStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ToysStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ToysStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ToysStore] SET QUERY_STORE = OFF
GO
USE [ToysStore]
GO
/****** Object:  Table [dbo].[company]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company](
	[Id] [int] NULL,
	[name] [varchar](50) NULL,
	[logo] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[toys]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[toys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](100) NULL,
	[age] [int] NULL,
	[price] [float] NULL,
	[IdCompany] [varchar](50) NULL,
	[img] [varchar](50) NULL,
	[dateCreated] [datetime] NULL,
	[lastUpdate] [datetime] NULL,
 CONSTRAINT [PK_toys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[toysHistorical]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[toysHistorical](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[description] [varchar](100) NULL,
	[age] [int] NULL,
	[price] [decimal](18, 0) NULL,
	[company] [varchar](50) NULL,
	[dateCreated] [datetime] NULL,
	[lastUpdate] [datetime] NULL,
	[status] [varchar](50) NULL,
	[idtoy] [int] NULL,
 CONSTRAINT [PK_toysHistorical] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_deletetoy]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_deletetoy]
@idToy int
AS
BEGIN


	insert into toysHistorical ([name],[description],age,price,company,dateCreated,lastUpdate, [status], idtoy) 
	select 
		[name]
		,[description]
		,age
		,price
		,idcompany
		,dateCreated
		,getdate()
		,'borrado'
		,@idToy
	from toys where id = @idToy

	delete toys where id = @idToy
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getToys]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getToys]
AS
BEGIN
	select * from toys
END
GO
/****** Object:  StoredProcedure [dbo].[sp_saveNewToy]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_saveNewToy]
@name varchar(50),
@description varchar(100),
@age int,
@price decimal,
@Company varchar(50),
@imgtoy varchar(50)
AS
BEGIN

	insert into toys (name,description,age,price,IdCompany,img,datecreated,lastupdate)
	values(@name,@description,@age,cast(round(@price,2) as numeric(36,2)),@Company,@imgtoy,getdate(),getdate())

	select * from toys
	
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updateToy]    Script Date: 8/15/2022 11:28:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_updateToy]
@name varchar(50),
@description varchar(100),
@age int,
@price float,
@Company varchar(50),
@idToy int
--@imgtoy varchar(50)
AS
BEGIN

	insert into toysHistorical ([name],[description],age,price,company,dateCreated,lastUpdate, [status], idtoy) 
		select 
			[name]
			,[description]
			,age
			,price
			,idcompany
			,dateCreated
			,getdate()
			,'actualizado'
			,@idToy
		from toys where id = @idToy

	update toys set name = @name,[description] = @description,age = @age, price = cast(round(@price,2) as numeric(36,2)),IdCompany = @company,lastupdate = getdate()
	where id = @idToy
	select * from toys
			
END



GO
USE [master]
GO
ALTER DATABASE [ToysStore] SET  READ_WRITE 
GO
