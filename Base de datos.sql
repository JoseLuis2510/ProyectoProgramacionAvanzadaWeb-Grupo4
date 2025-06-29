USE [master]
GO
/****** Object:  Database [DB_ProgramacionAvanzadaWeb]    Script Date: 28/6/2025 09:26:18 ******/
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
/****** Object:  Table [dbo].[Tabla_Cita]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla_Cita](
	[Consecutivo] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[IdHorario] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tabla_Error]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla_Error](
	[IdError] [bigint] IDENTITY(1,1) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[DescripcionError] [varchar](max) NOT NULL,
	[Origen] [varchar](255) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
 CONSTRAINT [PK_TError] PRIMARY KEY CLUSTERED 
(
	[IdError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tabla_Horario]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla_Horario](
	[IdHorario] [bigint] IDENTITY(1,1) NOT NULL,
	[HoraFecha] [datetime] NOT NULL,
	[Observacion] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Tabla_Horario] PRIMARY KEY CLUSTERED 
(
	[IdHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tabla_Usuario]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla_Usuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Tabla_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tabla_Error] ON 

INSERT [dbo].[Tabla_Error] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (1, CAST(N'2025-06-20T18:14:53.160' AS DateTime), N'Procedure or function ''IniciarSesion'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Login/Index', 0)
INSERT [dbo].[Tabla_Error] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (2, CAST(N'2025-06-20T18:32:43.590' AS DateTime), N'Procedure or function ''IniciarSesion'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Login/Index', 0)
INSERT [dbo].[Tabla_Error] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (3, CAST(N'2025-06-20T18:42:33.407' AS DateTime), N'Procedure or function ''IniciarSesion'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Login/Index', 0)
INSERT [dbo].[Tabla_Error] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (4, CAST(N'2025-06-27T11:06:33.290' AS DateTime), N'Procedure or function ''RegistrarHorario'' expects parameter ''@HoraFecha'', which was not supplied.', N'/api/Horario/CrearHorario', 0)
INSERT [dbo].[Tabla_Error] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (5, CAST(N'2025-06-27T11:44:56.610' AS DateTime), N'Could not find stored procedure ''VerHojrario''.', N'/api/Horario/VerHorario', 0)
SET IDENTITY_INSERT [dbo].[Tabla_Error] OFF
GO
SET IDENTITY_INSERT [dbo].[Tabla_Horario] ON 

INSERT [dbo].[Tabla_Horario] ([IdHorario], [HoraFecha], [Observacion], [Estado]) VALUES (1, CAST(N'2025-06-27T17:00:47.570' AS DateTime), N'prueba', 0)
INSERT [dbo].[Tabla_Horario] ([IdHorario], [HoraFecha], [Observacion], [Estado]) VALUES (2, CAST(N'2025-07-04T16:10:00.000' AS DateTime), N'prueba2', 0)
INSERT [dbo].[Tabla_Horario] ([IdHorario], [HoraFecha], [Observacion], [Estado]) VALUES (3, CAST(N'2025-07-05T18:57:00.000' AS DateTime), N'Prueba', 0)
INSERT [dbo].[Tabla_Horario] ([IdHorario], [HoraFecha], [Observacion], [Estado]) VALUES (4, CAST(N'2025-06-29T13:59:00.000' AS DateTime), N'Prueba', 0)
SET IDENTITY_INSERT [dbo].[Tabla_Horario] OFF
GO
SET IDENTITY_INSERT [dbo].[Tabla_Usuario] ON 

INSERT [dbo].[Tabla_Usuario] ([IdUsuario], [Nombre], [Correo], [Identificacion], [Contrasenna], [Estado]) VALUES (1, N'José Jiménez Badilla', N'josejb425@gmail.com', N'305480726', N'1234', 1)
INSERT [dbo].[Tabla_Usuario] ([IdUsuario], [Nombre], [Correo], [Identificacion], [Contrasenna], [Estado]) VALUES (2, N'Andres', N'jss@gmail.com', N'3044', N'22', 1)
SET IDENTITY_INSERT [dbo].[Tabla_Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uk_Correo]    Script Date: 28/6/2025 09:26:19 ******/
ALTER TABLE [dbo].[Tabla_Usuario] ADD  CONSTRAINT [uk_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uk_NombreUsuario]    Script Date: 28/6/2025 09:26:19 ******/
ALTER TABLE [dbo].[Tabla_Usuario] ADD  CONSTRAINT [uk_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tabla_Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Horario] FOREIGN KEY([IdHorario])
REFERENCES [dbo].[Tabla_Horario] ([IdHorario])
GO
ALTER TABLE [dbo].[Tabla_Cita] CHECK CONSTRAINT [FK_Cita_Horario]
GO
ALTER TABLE [dbo].[Tabla_Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Tabla_Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Tabla_Cita] CHECK CONSTRAINT [FK_Cita_Usuario]
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesion]
@Correo varchar(20),
@Contrasenna varchar(10)

AS
BEGIN

	SELECT IdUsuario,Nombre,Correo,Identificacion,Contrasenna,Estado
	  FROM [dbo].[Tabla_Usuario]
	  WHERE Correo = @Correo 
	  AND Contrasenna = @Contrasenna
	  AND Estado=1

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCita]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarCita]
    @IdUsuario bigint,
    @Descripcion varchar(255),
    @IdHorario bigint
AS
BEGIN
    -- Insertar la cita
    INSERT INTO Tabla_Cita (IdUsuario, Descripcion, IdHorario)
    VALUES (@IdUsuario, @Descripcion, @IdHorario)

    -- Marcar el horario como no disponible
    UPDATE Tabla_Horario
    SET Estado = 0
    WHERE IdHorario = @IdHorario
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarError]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarError]
	@DescripcionError varchar(max),
	@Origen varchar(255),
	@IdUsuario bigint
AS
BEGIN

	INSERT INTO dbo.Tabla_Error (FechaHora,DescripcionError,Origen,IdUsuario)
	VALUES (GETDATE(),@DescripcionError,@Origen,@IdUsuario)

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarHorario]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarHorario]
    @HoraFecha datetime,
    @Observacion varchar(50),
	@Estado bit
AS
BEGIN
    INSERT INTO [dbo].[Tabla_Horario](HoraFecha, Observacion, Estado)
    VALUES (@HoraFecha, @Observacion, @Estado)
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(255),
	@Correo varchar(100),
	@Identificacion varchar(20),
	@Contrasenna varchar(10),
	@Estado bit

AS
BEGIN
		IF NOT EXISTS (SELECT 1 FROM [dbo].[Tabla_Usuario]
						WHERE Identificacion=@Identificacion OR CORREO = @CORREO)
	BEGIN
	
		INSERT INTO [dbo].[Tabla_Usuario](Nombre,Correo,Identificacion,Contrasenna,Estado)
		VALUES(@Nombre,@Correo,@Identificacion,@Contrasenna, @Estado)
	END

END
GO
/****** Object:  StoredProcedure [dbo].[VerHorario]    Script Date: 28/6/2025 09:26:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VerHorario]
AS
BEGIN

	SELECT IdHorario,HoraFecha,Observacion
	  FROM [dbo].[Tabla_Horario]
	  WHERE 
      Estado = 1

END
GO
USE [master]
GO
ALTER DATABASE [DB_ProgramacionAvanzadaWeb] SET  READ_WRITE 
GO
