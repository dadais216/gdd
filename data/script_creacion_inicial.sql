USE [GD2C2019]

IF OBJECT_ID('LOS_SIN_VOZ.Carga') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Carga;
IF OBJECT_ID('LOS_SIN_VOZ.Compra_Oferta') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Compra_Oferta;
IF OBJECT_ID('LOS_SIN_VOZ.contraseñasMigracion') IS NOT NULL DROP TABLE LOS_SIN_VOZ.contraseñasMigracion;
IF OBJECT_ID('LOS_SIN_VOZ.Cupon') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Cupon;
IF OBJECT_ID('LOS_SIN_VOZ.Factura') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Factura;
IF OBJECT_ID('LOS_SIN_VOZ.Oferta') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Oferta;
IF OBJECT_ID('LOS_SIN_VOZ.RolXFuncionalidad') IS NOT NULL DROP TABLE LOS_SIN_VOZ.RolXFuncionalidad;
IF OBJECT_ID('LOS_SIN_VOZ.Funcionalidad') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Funcionalidad;
IF OBJECT_ID('LOS_SIN_VOZ.Usuario') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Usuario;
IF OBJECT_ID('LOS_SIN_VOZ.Rol') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Rol;
IF OBJECT_ID('LOS_SIN_VOZ.Tipo_Pago') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Tipo_Pago;
IF OBJECT_ID('LOS_SIN_VOZ.Cliente') IS NOT NULL DROP TABLE LOS_SIN_VOZ.cliente;
IF OBJECT_ID('LOS_SIN_VOZ.Proveedor') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Proveedor;
IF OBJECT_ID('LOS_SIN_VOZ.Rubro') IS NOT NULL DROP TABLE LOS_SIN_VOZ.Rubro;

IF OBJECT_ID('LOS_SIN_VOZ.descuento') IS NOT NULL DROP FUNCTION LOS_SIN_VOZ.descuento;
IF OBJECT_ID('LOS_SIN_VOZ.sp_facturar') IS NOT NULL DROP PROCEDURE LOS_SIN_VOZ.sp_facturar;


IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'LOS_SIN_VOZ')
	DROP SCHEMA LOS_SIN_VOZ

GO

-- CREACION DE USUARIO PARA CORRER LA APLICACION
-- Autogenerado modificado a partir de la opcion Script Action To File de SQL Server Management Studio
-- Se asume que en la base de datos no va a existir el usuario ni el login y que si existe, ya tiene los permisos necesarios
-- Esto es una peculiaridad del tp ya que no conocemos el entorno de prueba. Contemplamos el caso que exista para evitar el error
-- y asumimos que si existe, tiene los permisos adecuados.
USE [master]
GO

-- CREO LOGIN SI NO EXISTE
IF NOT EXISTS(SELECT * FROM sys.sql_logins WHERE name='gdCupon2019')
	CREATE LOGIN [gdCupon2019] WITH PASSWORD=N'gd2019', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

-- LE AGREGO PERMISOS DE sysadmin
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [gdCupon2019]

-- Agrego el usuario para la conexion a la db de la aplicación
USE [GD2C2019]	
GO
IF NOT EXISTS(SELECT * FROM sys.sysusers WHERE name='gdCupon2019')
	CREATE USER [gdCupon2019] FOR LOGIN [gdCupon2019]

GO
CREATE SCHEMA LOS_SIN_VOZ AUTHORIZATION [gdCupon2019]

GO
SET ANSI_NULLS ON  -- Only compare nulls with IS and IS NOT

GO
SET QUOTED_IDENTIFIER ON  -- Allow double quoting to use reserved keywords as table names 

---------------------------------- CREATE TABLES -------------------------------------------
CREATE TABLE LOS_SIN_VOZ.Cliente(
	id INT IDENTITY(1,1) PRIMARY KEY,
	dni NUMERIC(18,0) NOT NULL,
	nombre VARCHAR(255) NOT NULL,
	apellido VARCHAR(255) NOT NULL,
	direccion VARCHAR(255) NOT NULL,
	telefono NUMERIC(18,0) NOT NULL,
	mail VARCHAR(255) NOT NULL,
	fecha_Nac DATE,
	ciudad VARCHAR(255) NOT NULL,
	saldo DECIMAL(32,2) NOT NULL DEFAULT 0,
	UNIQUE(nombre,apellido,dni,telefono,mail,fecha_Nac) 
	--no pongo todos los valores porque el unique funciona con un index, y los index no soportan mas de 900 bytes.
	--puede que sea mejor idea manejar la no duplicidad con un trigger? los 2 tienen sus ventajas y desventajas,
	--pero creo que me quedo con este.
	--deberia ser todo NOT NULL?
)
INSERT INTO LOS_SIN_VOZ.Cliente (dni, nombre, apellido, direccion, telefono, mail, fecha_Nac, ciudad)
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad
FROM gd_esquema.Maestra 

CREATE TABLE LOS_SIN_VOZ.Tipo_Pago(
	id INT IDENTITY(1,1) PRIMARY KEY,
	descripcion NVARCHAR(100),
	)
INSERT INTO LOS_SIN_VOZ.Tipo_Pago
VALUES ('Efectivo'),('Crédito'),('Débito')


CREATE TABLE LOS_SIN_VOZ.Carga(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES LOS_SIN_VOZ.Cliente(id),
	credito NUMERIC(18,2), --seria redundante ponerle NOT NULL a estos?
	fecha DATETIME,
	tipo_Pago INT REFERENCES LOS_SIN_VOZ.Tipo_Pago(id), -- ni idea de que es esto pero es algo de carga. Podria ser un enum
	numeroTarjeta NUMERIC(16,0) DEFAULT null --no me parece que valga la pena hacer otra tabla
	--es null para cargas en efectivo y cargas de tarjeta que vengan de la base vieja
	)

INSERT INTO LOS_SIN_VOZ.Carga
SELECT (SELECT id FROM LOS_SIN_VOZ.Cliente WHERE dni=Cli_Dni),Carga_Credito,Carga_Fecha, 
		(select id from LOS_SIN_VOZ.Tipo_Pago where Tipo_Pago_Desc=Tipo_Pago.descripcion),null
FROM gd_esquema.Maestra AS M
WHERE Carga_Credito IS NOT NULL

CREATE TABLE LOS_SIN_VOZ.Rubro(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nombre NVARCHAR(100),
	)
INSERT INTO LOS_SIN_VOZ.Rubro
SELECT DISTINCT Provee_Rubro
FROM gd_esquema.Maestra
WHERE Provee_RS IS NOT NULL

CREATE TABLE LOS_SIN_VOZ.Proveedor(
	id INT IDENTITY(1,1) PRIMARY KEY,
	RS VARCHAR(100) UNIQUE, --no uso esto como PK porque es mas lento y solo es unico dentro de un pais
	dom VARCHAR(255),
	ciudad VARCHAR(255),
	telefono NUMERIC(18,0),
	CUIT VARCHAR(16) UNIQUE,
	rubro INT REFERENCES LOS_SIN_VOZ.Rubro(id),
	mail VARCHAR(255) DEFAULT null,
	codigoPostal INT DEFAULT null, 
	contacto VARCHAR(255) DEFAULT null,
	--estoy casi seguro de que el rs y el cuit no son unicos globalmente, pero lo pide el enunciado
	--la tabla maestra no tiene mail ni codigoPostal ni contacto, pero se da a entender que son datos que tienen
	--los proveedores y que se van a agregar mas adelante
	)


INSERT INTO LOS_SIN_VOZ.Proveedor (RS,dom,ciudad,telefono,CUIT,rubro)
SELECT DISTINCT Provee_RS,Provee_Dom,Provee_Ciudad,Provee_Telefono,Provee_CUIT, 
(SELECT id FROM LOS_SIN_VOZ.Rubro WHERE nombre=Provee_Rubro)
FROM gd_esquema.Maestra m
WHERE Provee_RS IS NOT NULL

CREATE TABLE LOS_SIN_VOZ.Oferta(
	codigo VARCHAR(50) PRIMARY KEY,
	descripcion VARCHAR(255),
	cantidad NUMERIC(18,0), --es el stock
	fecha DATE NOT NULL,
	fecha_Venc DATE NOT NULL,
	precio NUMERIC(18,2) NOT NULL,
	precio_Ficticio NUMERIC(18,2) NOT NULL,
	proveedor INT REFERENCES LOS_SIN_VOZ.Proveedor(id)
)
INSERT INTO LOS_SIN_VOZ.Oferta
SELECT DISTINCT Oferta_Codigo,
				Oferta_Descripcion,
				Oferta_Cantidad,
				Oferta_Fecha, 
				Oferta_Fecha_Venc, 
				Oferta_Precio, 
				Oferta_Precio_Ficticio,
				(SELECT id FROM LOS_SIN_VOZ.Proveedor p WHERE p.RS=m.Provee_RS)
FROM gd_esquema.Maestra m
WHERE Oferta_Codigo IS NOT NULL 


CREATE TABLE LOS_SIN_VOZ.Factura(
	nro NUMERIC(18,0) IDENTITY(200000, 1) PRIMARY KEY,
	fecha DATETIME,
	proveedor INT REFERENCES LOS_SIN_VOZ.Proveedor(id)
)
-- Esto es necesario porque los futuros nro de factura van a ser autogenerados pero los de la migracion no
-- SQL se queja cuando le metes de prepo una pk a una tabla. y te exige que le apses el flag IDENTITY_INSERT
-- Tambien hace obligatorio que pongas las columnas despues del INSERT INTO LOS_SIN_VOZ.TABLA (campo1, campo2) como para que quede bien claro que la estas cagando
SET IDENTITY_INSERT LOS_SIN_VOZ.Factura ON 
INSERT INTO LOS_SIN_VOZ.Factura (nro, fecha, proveedor)
SELECT DISTINCT Factura_Nro,Factura_Fecha,(SELECT id FROM LOS_SIN_VOZ.Proveedor p WHERE p.RS=m.Provee_RS) 
FROM gd_esquema.Maestra m 
WHERE Factura_Nro IS NOT NULL
SET IDENTITY_INSERT LOS_SIN_VOZ.Factura OFF -- A partir de ahora el nro de la factura va a ser autogenerado.


CREATE TABLE LOS_SIN_VOZ.Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES LOS_SIN_VOZ.Cliente(id),
	oferta VARCHAR(50) REFERENCES LOS_SIN_VOZ.Oferta(codigo),
	factura NUMERIC(18,0) REFERENCES LOS_SIN_VOZ.Factura(nro) DEFAULT null,
	fecha_Compra DATETIME,
	fueCanjeado BIT DEFAULT 0,
	fecha_Entrega DATETIME
)

-- Esto tira 'Warning: Null value is eliminated by an aggregate or other SET operation.' Porque estamos descartando una row que tiene NULL en factura y fecha compra y es justamente lo deseado, no es un error.
INSERT INTO LOS_SIN_VOZ.Compra_Oferta (cliente, oferta, factura, fecha_Compra, fecha_Entrega)
SELECT  (SELECT id FROM LOS_SIN_VOZ.Cliente WHERE dni=Cli_Dni) AS Cliente,
		Oferta_Codigo,
		MAX(Factura_Nro),
		MAX(Oferta_Fecha_Compra),
		MAX(Oferta_Entregado_Fecha) 
FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo

CREATE TABLE LOS_SIN_VOZ.Cupon(
	codigo INT IDENTITY(1000,1) PRIMARY KEY, --podria ser algo mas complejo esto, o podria no serlo
	cliente INT REFERENCES LOS_SIN_VOZ.Cliente(id),
	fecha_Consumo DATETIME,
)

INSERT INTO LOS_SIN_VOZ.Cupon
SELECT Cliente, fecha_Entrega 
FROM LOS_SIN_VOZ.Compra_Oferta
WHERE fecha_Entrega IS NOT null

ALTER TABLE LOS_SIN_VOZ.Compra_Oferta DROP COLUMN fecha_Entrega
/*
se podrian juntar la compra oferta con el cupon, porque lo modelamos para que sea una relacion 1 a 1.
El id de la tabla seria el codigo cupon, que no significaria nada en compras sin cupon
Y se ahorraria tener el campo 'fue canjeado' en compra oferta
Y se tendria la informacion de de que compra oferta nacio el cupon.
No lo hago para no alejarme mucho de lo que parece querer el LOS_SIN_VOZ. igual no es nada muy loco
*/


CREATE TABLE LOS_SIN_VOZ.Funcionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
)

CREATE TABLE LOS_SIN_VOZ.Rol(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
	habilitado bit DEFAULT 1,
)

CREATE TABLE LOS_SIN_VOZ.RolxFuncionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	funcionalidad int FOREIGN KEY REFERENCES LOS_SIN_VOZ.Funcionalidad(id),
	rol int FOREIGN KEY REFERENCES LOS_SIN_VOZ.Rol(id),
)


INSERT INTO LOS_SIN_VOZ.Funcionalidad (nombre)
VALUES ('abm rol'), ('abm cliente'),('abm proveedor'),('carga credito')
,('confeccion y publicacion de oferta'),('compra oferta'),('consumo oferta')
,('facturacion a proveedor'),('listado estadistico');
--interpreto que loguearse y registrarse no son funcionalidades valores asignables a un rol
--podria interpretarse que son funcionalidades de un rol 'no logueado', pero tambien podrian tratarse
--aparte.


INSERT INTO LOS_SIN_VOZ.Rol (nombre)
VALUES ('cliente'),('proveedor'),('administrador'),('administrador general');

INSERT INTO LOS_SIN_VOZ.RolxFuncionalidad (rol,funcionalidad) -- Dado que es un script de migración único a ejecutar por única vez, es permisible esto
VALUES  (1,4),(1,6),
		(2,5),(2,7),
		(3,1),(3,2),(3,3),(3,5),(3,8),(3,9);
INSERT INTO LOS_SIN_VOZ.RolxFuncionalidad (rol,funcionalidad)
(SELECT 4,id FROM LOS_SIN_VOZ.Funcionalidad);



UPDATE LOS_SIN_VOZ.Cliente SET saldo = (SELECT SUM(credito) FROM LOS_SIN_VOZ.Carga 
WHERE LOS_SIN_VOZ.Cliente.id=cliente)
WHERE EXISTS (SELECT credito FROM LOS_SIN_VOZ.Carga WHERE LOS_SIN_VOZ.Cliente.id=cliente)

UPDATE LOS_SIN_VOZ.Cliente SET saldo = saldo - (SELECT SUM(precio) 
	FROM LOS_SIN_VOZ.Oferta o JOIN LOS_SIN_VOZ.Compra_Oferta c 
	ON o.codigo=c.oferta WHERE c.cliente=LOS_SIN_VOZ.Cliente.id)






/*
Entiendo que un nombre y apellido no son unicos pero se da la casualidad de que
si son unicos en la base de datos de la que se esta migrando:

SELECT c.Cli_Nombre FROM LOS_SIN_VOZ.Cliente c JOIN LOS_SIN_VOZ.Cliente b 
ON c.Cli_Nombre=b.Cli_Nombre AND c.Cli_Apellido=b.Cli_Apellido AND c.id!=b.id;

da vacio.
Esto es conveniente porque permite generar usuarios sin numeros ni cosas raras.
Si hubieran repeticiones se habria agregado un numero a cada usuario, probablemente concatenado su id

con proveedores pasa algo parecido. RS es unico solo dentro de un pais, por lo que podria darse que
haya 2 sociedades con el mismo nombre pero no se dio
*/

CREATE TABLE LOS_SIN_VOZ.Usuario(
	id INT IDENTITY(1,1) PRIMARY KEY, -- estario bueno que la PK sea (cliente,proveedor), pero sql no se banca que parte de una pk sea null
	nombre VARCHAR(128) NOT NULL UNIQUE, --este podria ser la PK, pero *creo* que es mas lento
	contraseña BINARY(32) NOT NULL,
	rol INT FOREIGN KEY REFERENCES LOS_SIN_VOZ.Rol(id),
	fallosLogin INT DEFAULT 0,
	habilitado BIT DEFAULT 1,
	cliente INT FOREIGN KEY REFERENCES LOS_SIN_VOZ.Cliente(id),
	proveedor INT FOREIGN KEY REFERENCES LOS_SIN_VOZ.Proveedor(id),
	--se podrian guardar la FK del LOS_SIN_VOZ.Cliente y del proveedor en un mismo campo y discriminar por el rol
	--si es que eso existe en sql ni idea
)

--tabla temporal donde se guardan las contraseÃ±as autogeneradas para los usuarios, para entregarselas
--a estos. La idea es que cambien esta contraseÃ±a provisional por una de verdad.
--Sin hacer esta tabla se le estaria asignando contraseÃ±as aleatorias a todos los usuarios, que no conoce nadie
CREATE TABLE LOS_SIN_VOZ.contraseñasMigracion(
	id INT PRIMARY KEY FOREIGN KEY REFERENCES LOS_SIN_VOZ.Usuario(id),
	contraseñaDesnuda VARCHAR(128)
)

DECLARE @nombre VARCHAR(128)
DECLARE @apellido VARCHAR(128)
DECLARE @id INT
DECLARE @rol INT
DECLARE @contraseñaDesnuda VARCHAR(64)
SET @rol = (SELECT id FROM LOS_SIN_VOZ.Rol WHERE nombre='Cliente');

DECLARE cur CURSOR FOR (SELECT nombre,apellido,id FROM LOS_SIN_VOZ.Cliente);
OPEN cur  
FETCH NEXT FROM cur INTO @nombre,@apellido,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO LOS_SIN_VOZ.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(CONCAT(@nombre,' ',@apellido),HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,@id,null)
	
	INSERT INTO LOS_SIN_VOZ.contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @nombre,@apellido,@id
END 
CLOSE cur  
DEALLOCATE cur 

DECLARE @rs VARCHAR(100)
SET @rol = (SELECT id FROM LOS_SIN_VOZ.Rol WHERE nombre='Proveedor');

DECLARE cur CURSOR FOR (SELECT RS,id FROM LOS_SIN_VOZ.Proveedor);
OPEN cur  
FETCH NEXT FROM cur INTO @RS,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO LOS_SIN_VOZ.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(@RS,HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,null,@id)
	
	INSERT INTO LOS_SIN_VOZ.contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @RS,@id
END 
CLOSE cur  
DEALLOCATE cur ;


INSERT INTO LOS_SIN_VOZ.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES ('admin',HASHBYTES('SHA2_256','w23e'),4,null,null)

-- Funciones
-- Dado el precio de ahora y el precio de antes. Calcula el descuento.
-- Devuelve valor entre 0 y 1. Usar FORMAT(func(), 'p') para imprimir lindo
GO
CREATE FUNCTION LOS_SIN_VOZ.descuento(@precio_venta NUMERIC(18,2), @precio_original NUMERIC(18,2))
RETURNS NUMERIC(18, 2)
AS
BEGIN
	RETURN @precio_venta/@precio_original 
	--que sea tan simple me hace dudar que sea necesario que sea una funcion
END
GO


-- Stored Procedure
-- Dado que la facturación es un proceso sensible, se engloba en una transacción implicita asociada a la stored procedure
CREATE PROCEDURE LOS_SIN_VOZ.sp_facturar(@prov int, @fecha_facturacion DATETIME, @desde DATETIME, @hasta DATETIME)
AS
BEGIN
	BEGIN TRAN facturacion

	DECLARE @factura int
	DECLARE @inserted table (id NUMERIC(18))
	INSERT INTO LOS_SIN_VOZ.Factura 
	OUTPUT INSERTED.nro into @inserted
	VALUES
		(@fecha_facturacion, @prov)

    -- OBTENER SU ID
	SELECT @factura = id from @inserted

	-- UPDATEAR COMPRAS con factura asociada
	UPDATE
		LOS_SIN_VOZ.Compra_Oferta
	SET
		factura = @factura
	FROM 
		LOS_SIN_VOZ.Compra_Oferta
		JOIN LOS_SIN_VOZ.Oferta ON 
			 LOS_SIN_VOZ.Oferta.codigo = LOS_SIN_VOZ.Compra_Oferta.oferta
	WHERE
		--Only set factura for compras that dont have a factura yet. And they match the time period and proveedor
	    LOS_SIN_VOZ.Compra_Oferta.factura IS NULL AND
		LOS_SIN_VOZ.Oferta.proveedor=@prov AND
	    LOS_SIN_VOZ.Compra_Oferta.fecha_Compra >= @desde AND
        LOS_SIN_VOZ.Compra_Oferta.fecha_Compra <= @hasta

    COMMIT TRAN facturacion
END


