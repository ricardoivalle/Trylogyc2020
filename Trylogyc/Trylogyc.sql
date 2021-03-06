USE [Trylogyc]
GO
/****** Object:  Table [dbo].[Conexiones]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conexiones](
	[IDConexion] [int] IDENTITY(1,1) NOT NULL,
	[XmlSocio] [int] NOT NULL,
	[XmlConexion] [int] NOT NULL,
	[ConCGP] [varchar](100) NULL,
	[ConDireccion] [varchar](250) NOT NULL,
	[ConLocalidad] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Conexiones] PRIMARY KEY CLUSTERED 
(
	[IDConexion] ASC,
	[XmlSocio] ASC,
	[XmlConexion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[IDFactura] [int] IDENTITY(1,1) NOT NULL,
	[XmlSocio] [int] NOT NULL,
	[XmlConexion] [int] NOT NULL,
	[FacPeriodo] [varchar](50) NOT NULL,
	[FacFechaEmision] [date] NOT NULL,
	[FacFechaVto] [date] NOT NULL,
	[FacImporte] [money] NOT NULL,
	[FacNumero] [varchar](100) NOT NULL,
	[FacGrupo] [varchar](100) NOT NULL,
	[FacLetra] [varchar](5) NOT NULL,
	[FacPtoVenta] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Facturas_1] PRIMARY KEY CLUSTERED 
(
	[IDFactura] ASC,
	[XmlConexion] ASC,
	[XmlSocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socios]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socios](
	[IDSocio] [int] IDENTITY(1,1) NOT NULL,
	[XmlSocio] [int] NOT NULL,
	[SocNombre] [varchar](250) NOT NULL,
	[SocDni] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Socios_1] PRIMARY KEY CLUSTERED 
(
	[IDSocio] ASC,
	[XmlSocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SociosConexiones]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SociosConexiones](
	[idSocCon] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idSocio] [int] NULL,
	[idConexion] [int] NULL,
 CONSTRAINT [PK_SociosConexiones] PRIMARY KEY CLUSTERED 
(
	[idSocCon] ASC,
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IDUsuario] [int] IDENTITY(1,1) NOT NULL,
	[XmlSocio] [int] NOT NULL,
	[userEmail] [varchar](100) NOT NULL,
	[userName] [varchar](100) NOT NULL,
	[userPass] [varchar](50) NOT NULL,
	[foto] [varchar](200) NULL,
	[ruta] [varchar](100) NULL,
 CONSTRAINT [PK_Usuario_1] PRIMARY KEY CLUSTERED 
(
	[IDUsuario] ASC,
	[XmlSocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SociosConexiones] ON 

INSERT [dbo].[SociosConexiones] ([idSocCon], [idUsuario], [idSocio], [idConexion]) VALUES (9, 6, 7, 1)
INSERT [dbo].[SociosConexiones] ([idSocCon], [idUsuario], [idSocio], [idConexion]) VALUES (10, 6, 7, 2)
SET IDENTITY_INSERT [dbo].[SociosConexiones] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IDUsuario], [XmlSocio], [userEmail], [userName], [userPass], [foto], [ruta]) VALUES (6, 7, N'mivancich@trylogyc.com.ar', N'mivancich@trylogyc.com.ar', N'niko2015', N'~/images/aguasunch.jpg', N'~/Default.aspx')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Conexiones] ADD  CONSTRAINT [DF_Conexiones_XmlConexion]  DEFAULT ((-1)) FOR [XmlConexion]
GO
ALTER TABLE [dbo].[Socios] ADD  CONSTRAINT [DF_Socios_IDUsuario]  DEFAULT ((-1)) FOR [XmlSocio]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_ruta]  DEFAULT ('') FOR [ruta]
GO
/****** Object:  StoredProcedure [dbo].[INS_SOCIOS_CONEXIONES]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[INS_SOCIOS_CONEXIONES]
@IDUsuario int,
@idSocio int,
@idConexion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @CNT INT
	SET @CNT = (select COUNT(idSocCon) from SociosConexiones 
	where idUsuario = @IDUsuario and 
	idSocio = @idSocio and
	idConexion = @idConexion)
	IF @CNT = 0 
	BEGIN
	INSERT INTO SociosConexiones(idUsuario,idSocio,idConexion) 
	VALUES(@IDUsuario,@idSocio,@idConexion)
	
	END
	
END


GO
/****** Object:  StoredProcedure [dbo].[INS_USUARIO]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[INS_USUARIO]
@email varchar(100),
@XmlSocio int,
@idConexion int
AS
BEGIN
DECLARE @techonthenet VARCHAR(50);
declare @foto VARCHAR(100);
declare @userPassne VARCHAR(100);
declare @userPass VARCHAR(100);
declare @log float;
--set @foto = (ltrim('~/images/Users/' + 'User') + ltrim(str(@XmlSocio)) + ltrim('.jpg'));
set @foto = (ltrim('~/images/') + 'aguasunch' + ltrim('.jpg'))
declare @pi float;
set @pi = PI();
set @log = LOG(((@XmlSocio * @pi)))
set @userPass = ltrim('u' + ltrim(str(@log)) + 's' + ltrim(str(@log*@pi)) + 'e' +ltrim(str(@log*@pi*@pi)) +'r')
declare @ruta varchar(50)
set @ruta = '~/Default.aspx'
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Usuario(XmlSocio, userEmail, userName, UserPass, foto, ruta)
	values(@XmlSocio, @email, @email, @userPass, @foto, @ruta)
	declare @ULT_ID INT
	set @ULT_ID = IDENT_CURRENT('Usuario')
	select Usuario.xmlsocio, Usuario.userpass from Usuario where Usuario.IDUsuario = @ULT_ID
	insert into SociosConexiones(idUsuario,idSocio,idConexion) values (@ULT_ID,@XmlSocio,@idConexion)
	
END


GO
/****** Object:  StoredProcedure [dbo].[InsertXML]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertXML]
@xml XML,
@xml2 XML
AS
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	delete from dbo.facturas
	delete from dbo.conexiones
	delete from dbo.socios
	
    -- Insert statements for procedure here
	INSERT INTO dbo.Socios(xmlSocio,SocNombre, SocDni) 
      SELECT distinct
      ltrim(Registro.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      isnull(ltrim(Registro.value('(Nombre/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro.value('(Documento/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro)
      --where
      --ltrim(Registro.value('(Socio/text())[1]','VARCHAR(100)')) not in
      --(select dbo.socios.XmlSocio from socios)
      
      insert into dbo.conexiones(XmlSocio, XmlConexion, ConCGP, ConDireccion, ConLocalidad)
      SELECT distinct
      ltrim(Registro2.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro2.value('(Conexion/text())[1]','VARCHAR(100)')), --TAG
      isnull(ltrim(Registro2.value('(CGP/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro2.value('(Direcion/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro2.value('(Localidad/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro2)   
      --where
      --ltrim(Registro2.value('(Conexion/text())[1]','VARCHAR(100)')) not in
      --(select dbo.Conexiones.XmlConexion from Conexiones)  
      
      
      insert into dbo.Facturas(
      XmlSocio,
      XmlConexion, 
      FacPeriodo,
      FacFechaEmision,
      FacFechaVto, 
      FacImporte, 
      FacNumero, 
      FacGrupo,
      FacLetra,
      FacPtoVenta)
      SELECT distinct
      ltrim(Registro3.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro3.value('(Conexion/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro3.value('(Periodo/text())[1]','VARCHAR(100)')), --TAG
      isnull(convert(date,ltrim(Registro3.value('(Fecha_Emision/text())[1]','VARCHAR(100)')),103),'10/03/2017'), --TAG
      isnull(convert(date,ltrim(Registro3.value('(Fecha_Vto/text())[1]','VARCHAR(100)')),103),'10/03/2017'), --TAG
      isnull(ltrim(Registro3.value('(Importe/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Factura/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Grupo_Fact/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Letra/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Pto_Venta/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml2.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro3)   
      --where
      --ltrim(Registro3.value('(Conexion/text())[1]','VARCHAR(100)')) not in
      --(select dbo.Conexiones.XmlConexion from Conexiones)  
      END


GO
/****** Object:  StoredProcedure [dbo].[QueryXML]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[QueryXML]
@xml XML,
@xml2 XML,
@IDSocio int
AS
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
    -- Insert statements for procedure here
	  SELECT distinct
      ltrim(Registro.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      isnull(ltrim(Registro.value('(Nombre/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro.value('(Documento/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro)
      where ltrim(Registro.value('(Socio/text())[1]','VARCHAR(100)')) = @IDSocio
      --where
      --ltrim(Registro.value('(Socio/text())[1]','VARCHAR(100)')) not in
      --(select dbo.socios.XmlSocio from socios)
      SELECT distinct
      ltrim(Registro2.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro2.value('(Conexion/text())[1]','VARCHAR(100)')), --TAG
      isnull(ltrim(Registro2.value('(CGP/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro2.value('(Direcion/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro2.value('(Localidad/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro2)   
      --where
      --ltrim(Registro2.value('(Conexion/text())[1]','VARCHAR(100)')) not in
      --(select dbo.Conexiones.XmlConexion from Conexiones)  
      SELECT distinct
      ltrim(Registro3.value('(Socio/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro3.value('(Conexion/text())[1]','VARCHAR(100)')), --TAG
      ltrim(Registro3.value('(Periodo/text())[1]','VARCHAR(100)')), --TAG
      isnull(convert(date,ltrim(Registro3.value('(Fecha_Emision/text())[1]','VARCHAR(100)')),103),'10/03/2017'), --TAG
      isnull(convert(date,ltrim(Registro3.value('(Fecha_Vto/text())[1]','VARCHAR(100)')),103),'10/03/2017'), --TAG
      isnull(ltrim(Registro3.value('(Importe/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Factura/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Grupo_Fact/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Letra/text())[1]','VARCHAR(100)')),''), --TAG
      isnull(ltrim(Registro3.value('(Pto_Venta/text())[1]','VARCHAR(100)')),'') --TAG
      FROM
      @xml2.nodes('/Cabecera/Registro')AS TEMPTABLE(Registro3)   
      --where
      --ltrim(Registro3.value('(Conexion/text())[1]','VARCHAR(100)')) not in
      --(select dbo.Conexiones.XmlConexion from Conexiones)  
      END


GO
/****** Object:  StoredProcedure [dbo].[SEL_CNT_SOCIOS_CGP]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_CNT_SOCIOS_CGP]
@SOCCGP int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(SOCIOS.IDSOCIO) FROM SOCIOS where socios.xmlsocio = @SOCCGP
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_CONEXION_DETALLES]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_CONEXION_DETALLES]
@XmlConexion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Conexiones.ConDireccion, Conexiones.ConLocalidad from Conexiones 
	where
	Conexiones.XmlConexion = @XmlConexion

END


GO
/****** Object:  StoredProcedure [dbo].[SEL_CONEXIONES_SOCIO]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_CONEXIONES_SOCIO]
@XmlSocio int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Conexiones where Conexiones.XmlSocio = @XmlSocio
	order by Conexiones.XmlConexion asc
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_FACTURAS_SOCIO_CONEXION]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_FACTURAS_SOCIO_CONEXION]
@XmlSocio int,
@XmlConexion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    if @XmlConexion > 0 
    sELECT facturas.idfactura, Facturas.FacPtoVenta as 'Pto. Vta', Facturas.FacNumero as 'Nro. Fact.', Facturas.FacLetra,Facturas.FacPeriodo as 'Periodo', Facturas.FacFechaEmision as 'Fec. Emisión', Facturas.FacFechaVto as 'Fec. Vto.', Facturas.FacGrupo as 'Grupo', Facturas.FacImporte as 'Importe'
	from Facturas
	WHERE 
	facturas.xmlsocio = @xmlsocio and
	Facturas.XmlConexion = @XmlConexion
	else
	   sELECT facturas.idfactura, Facturas.FacPtoVenta as 'Pto. Vta', Facturas.FacNumero as 'Nro. Fact.', Facturas.FacLetra,Facturas.FacPeriodo as 'Periodo', Facturas.FacFechaEmision as 'Fec. Emisión', Facturas.FacFechaVto as 'Fec. Vto.', Facturas.FacGrupo as 'Grupo', Facturas.FacImporte as 'Importe'
	from Facturas
	WHERE 
	facturas.xmlsocio = @xmlsocio 
	end
	


GO
/****** Object:  StoredProcedure [dbo].[SEL_SOCIOCONEXION]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_SOCIOCONEXION] 
@IDUsuario int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT SociosConexiones.idSocio, SociosConexiones.idConexion FROM SociosConexiones where idUsuario = @IDUsuario
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_SOCIOCONEXION_CNT]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_SOCIOCONEXION_CNT]
@IDUsuario int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 'Conexiones' as column1, COUNT(SociosConexiones.idSocCon) as column2 from SociosConexiones 
	where SociosConexiones.idUsuario = @IDUsuario
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_USUARIO]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_USUARIO]
@IDUSUARIO INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Usuario WHERE Usuario.IDUsuario = @IDUSUARIO
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_USUARIO_CGP]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_USUARIO_CGP]
@USERNAME varchar(100)
AS
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Usuario WHERE Usuario.userName = @USERNAME
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_USUARIO_COPS]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_USUARIO_COPS]
@IDUSUARIO INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 'Facturas', COUNT(FACTURAS.IDFactura) as 'cnt_facturas' from Facturas inner join usuario on
	usuario.XmlSocio = Facturas.XmlSocio where
	Usuario.IDUsuario = @IDUSUARIO
	
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_USUARIO_IDSOCIO_EMAIL]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_USUARIO_IDSOCIO_EMAIL]
@IDSOCIO INT,
@EMAIL VARCHAR(100)
AS
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(Usuario.IDUsuario) FROM Usuario WHERE Usuario.userEmail = @EMAIL
END


GO
/****** Object:  StoredProcedure [dbo].[SEL_USUARIO_REESTABLECER]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SEL_USUARIO_REESTABLECER]
@XmlSocio int,
@userEmail varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Usuario.userPass, Usuario.userEmail from Usuario where Usuario.XmlSocio =@XmlSocio and Usuario.userEmail =@userEmail
END


GO
/****** Object:  StoredProcedure [dbo].[UPD_USUARIO_CONTRASENA]    Script Date: 5/24/2020 7:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPD_USUARIO_CONTRASENA] 
@idUsuario int,
@passWord varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Usuario SET userPass = @passWord 
	WHERE  IDUsuario = @idUsuario
END


GO
