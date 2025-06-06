USE [master]
GO
/****** Object:  Database [DB_ProgramacionAvanzadaWeb]    Script Date: 6/6/2025 10:57:18 ******/
CREATE DATABASE [DB_ProgramacionAvanzadaWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_ProgramacionAvanzadaWeb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DB_ProgramacionAvanzadaWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_ProgramacionAvanzadaWeb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DB_ProgramacionAvanzadaWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_ProgramacionAvanzadaWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET  MULTI_USER 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_ProgramacionAvanzadaWeb', N'ON'
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET QUERY_STORE = ON
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DB_ProgramacionAvanzadaWeb]
GO
/****** Object:  Table [dbo].[Tabla_Usuario]    Script Date: 6/6/2025 10:57:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla_Usuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[NombreUsuario] [varchar](20) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Tabla_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tabla_Usuario] ON 

INSERT [dbo].[Tabla_Usuario] ([IdUsuario], [Nombre], [Correo], [NombreUsuario], [Contrasenna], [Estado]) VALUES (1, N'José Jiménez Badilla', N'josejb425@gmail.com', N'josejb2510', N'1234', 1)
SET IDENTITY_INSERT [dbo].[Tabla_Usuario] OFF
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 6/6/2025 10:57:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesion]
@NombreUsuario varchar(20),
@Contrasenna varchar(10)

AS
BEGIN

	SELECT IdUsuario,Nombre,Correo,NombreUsuario,Contrasenna,Estado
	  FROM [dbo].[Tabla_Usuario]
	  WHERE NombreUsuario = @NombreUsuario 
	  AND Contrasenna = @Contrasenna
	  AND Estado=1

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 6/6/2025 10:57:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(255),
	@Correo varchar(100),
	@NombreUsuario varchar(20),
	@Contrasenna varchar(10),
	@Estado bit

AS
BEGIN

		INSERT INTO [dbo].[Tabla_Usuario](Nombre,Correo,NombreUsuario,Contrasenna,Estado)
		VALUES(@Nombre,@Correo,@NombreUsuario,@Contrasenna, @Estado)

END
GO
USE [master]
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET  READ_WRITE 
GO
