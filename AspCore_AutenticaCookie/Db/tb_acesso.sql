CREATE DATABASE [DB_ACESSO]
USE [DB_ACESSO]
GO

/****** Object:  Table [dbo].[TB_ACCESS]    Script Date: 12/20/2018 10:00:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_ACCESS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DS_EMAIL] [varchar](200) NOT NULL,
	[DS_PASSWORD] [varchar](200) NOT NULL,
	[ST_ACTIVE] [bit] NOT NULL,
	[DS_NAME] [varchar](255) NOT NULL,
	[DS_LOGIN] [varchar](100) NOT NULL,
	[DT_ACCESS] [datetime] NULL,
 CONSTRAINT [PK_dbo.TB_ACCESS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_ACCESS] ADD  CONSTRAINT [DF_TB_ACCESS_ST_ACTIVE]  DEFAULT ((0)) FOR [ST_ACTIVE]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 12/20/2018 10:01:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[RegistrarUsuario]
(
	@Login varchar(100),
	@Nome varchar(100),
	@Email varchar(100),
	@Senha varchar(100)
)
as begin 
	declare @resultado varchar(10) = 'Falhou'
if not exists(SELECT 1 FROM TB_ACCESS where DS_EMAIL = @Email)
	begin
	insert into [dbo].[TB_ACCESS] (DS_EMAIL, DS_NAME, DS_LOGIN, DS_PASSWORD) values (@Email, @Nome, @Login, @Senha)
	set @resultado = 'Sucesso'
	end
		Select @resultado as Resultado
end

/****** Object:  StoredProcedure [dbo].[ValidarUsuario]    Script Date: 12/20/2018 10:02:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ValidarUsuario](@Login varchar(100), @Email varchar(100), @Senha varchar(100)) 
as 
	begin
		declare @autenticacao varchar(10)='Falhou'
		if exists(Select 1 from TB_ACCESS WHERE (DS_EMAIL = @Email AND DS_PASSWORD = @Senha) OR (DS_LOGIN = @Login AND DS_PASSWORD = @Senha))
	begin
		set @autenticacao ='Sucesso'
	end
		select @autenticacao
end