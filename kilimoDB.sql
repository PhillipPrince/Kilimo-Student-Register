USE [master]
GO
/****** Object:  Database [Kilimo]    Script Date: 8/20/2024 1:33:02 PM ******/
CREATE DATABASE [Kilimo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Kilimo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Kilimo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Kilimo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Kilimo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Kilimo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kilimo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kilimo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kilimo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kilimo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kilimo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kilimo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kilimo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Kilimo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kilimo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kilimo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kilimo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kilimo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kilimo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kilimo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kilimo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kilimo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kilimo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kilimo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kilimo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kilimo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kilimo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kilimo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kilimo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kilimo] SET RECOVERY FULL 
GO
ALTER DATABASE [Kilimo] SET  MULTI_USER 
GO
ALTER DATABASE [Kilimo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kilimo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kilimo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kilimo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Kilimo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Kilimo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Kilimo', N'ON'
GO
ALTER DATABASE [Kilimo] SET QUERY_STORE = ON
GO
ALTER DATABASE [Kilimo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Kilimo]
GO
/****** Object:  Table [dbo].[ClassStreams]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassStreams](
	[StreamId] [int] IDENTITY(1,1) NOT NULL,
	[StreamName] [varchar](100) NOT NULL,
	[Description] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[StreamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [nvarchar](50) NOT NULL,
	[StudentName] [nvarchar](100) NOT NULL,
	[ClassStreamId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([ClassStreamId])
REFERENCES [dbo].[ClassStreams] ([StreamId])
GO
/****** Object:  StoredProcedure [dbo].[CreateClassStream]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateClassStream]
    @StreamName NVARCHAR(100),
    @Description NVARCHAR(200)
AS
BEGIN
    INSERT INTO ClassStreams (StreamName, Description)
    VALUES (@StreamName, @Description);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateStudent]
    @StudentName NVARCHAR(100),
    @StudentId NVARCHAR(50),
    @ClassStreamId INT
AS
BEGIN
    INSERT INTO Student (StudentName, StudentId, ClassStreamId)
    VALUES (@StudentName, @StudentId, @ClassStreamId);
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteStudent]
    @StudentId NVARCHAR(50)
AS
BEGIN
    DELETE FROM Student
    WHERE StudentId = @StudentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetClassStreamById]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetClassStreamById]
    @StreamId INT
AS
BEGIN
    SELECT StreamId, StreamName, Description
    FROM ClassStream
    WHERE StreamId = @StreamId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetClassStreams]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetClassStreams]
AS
BEGIN
    SELECT StreamId, StreamName, Description
    FROM ClassStream;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentById]
    @StudentId NVARCHAR(50)
AS
BEGIN
    SELECT StudentName, StudentId, ClassStreamId
    FROM Student
    WHERE StudentId = @StudentId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetStudents]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudents]
AS
BEGIN
    SELECT StudentName, StudentId, ClassStreamId
    FROM Student;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 8/20/2024 1:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudent]
    @StudentName NVARCHAR(100),
    @StudentId NVARCHAR(50),
    @ClassStreamId INT
AS
BEGIN
    UPDATE Student
    SET StudentName = @StudentName,
        ClassStreamId = @ClassStreamId
    WHERE StudentId = @StudentId;
END;
GO
USE [master]
GO
ALTER DATABASE [Kilimo] SET  READ_WRITE 
GO
