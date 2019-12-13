USE [GD2C2019]

IF OBJECT_ID('tp.Carga') IS NOT NULL DROP TABLE tp.Carga;
IF OBJECT_ID('tp.Compra_Oferta') IS NOT NULL DROP TABLE tp.Compra_Oferta;
IF OBJECT_ID('tp.contraseñasMigracion') IS NOT NULL DROP TABLE tp.contraseñasMigracion;
IF OBJECT_ID('tp.Cupon') IS NOT NULL DROP TABLE tp.Cupon;
IF OBJECT_ID('tp.Factura') IS NOT NULL DROP TABLE tp.Factura;
IF OBJECT_ID('tp.Oferta') IS NOT NULL DROP TABLE tp.Oferta;
IF OBJECT_ID('tp.RolXFuncionalidad') IS NOT NULL DROP TABLE tp.RolXFuncionalidad;
IF OBJECT_ID('tp.Funcionalidad') IS NOT NULL DROP TABLE tp.Funcionalidad;
IF OBJECT_ID('tp.Usuario') IS NOT NULL DROP TABLE tp.Usuario;
IF OBJECT_ID('tp.Rol') IS NOT NULL DROP TABLE tp.Rol;
IF OBJECT_ID('tp.Tipo_Pago') IS NOT NULL DROP TABLE tp.Tipo_Pago;
IF OBJECT_ID('tp.Cliente') IS NOT NULL DROP TABLE tp.cliente;
IF OBJECT_ID('tp.Rubro') IS NOT NULL DROP TABLE tp.Rubro;
IF OBJECT_ID('tp.Proveedor') IS NOT NULL DROP TABLE tp.Proveedor;
IF OBJECT_ID('tp.descuento') IS NOT NULL DROP FUNCTION tp.descuento;

IF EXISTS (SELECT * FROM sys.schemas WHERE name = N'tp')
	DROP SCHEMA tp

GO
CREATE SCHEMA tp AUTHORIZATION [gd]

GO
SET ANSI_NULLS ON  -- Only compare nulls with IS and IS NOT

GO
SET QUOTED_IDENTIFIER ON  -- Allow double quoting to use reserved keywords as table names 

---------------------------------- CREATE TABLES -------------------------------------------
CREATE TABLE tp.Cliente(
	id INT IDENTITY(1,1) PRIMARY KEY,
	dni NUMERIC(18,0) NOT NULL,
	nombre VARCHAR(255) NOT NULL,
	apellido VARCHAR(255) NOT NULL,
	direccion VARCHAR(255) NOT NULL,
	telefono NUMERIC(18,0) NOT NULL, /*UNIQUE?*/
	mail VARCHAR(255) NOT NULL, /*UNIQUE hay dos minas que comparte mail, no sÃ© si considerarlo un problema*/
	fecha_Nac DATE, /*deberia ser NOT NULL pero no solucione el tema de la conversion*/
	ciudad VARCHAR(255) NOT NULL,
	saldo DECIMAL(32,2) NOT NULL DEFAULT 0,
	UNIQUE(nombre,apellido,dni,telefono,mail,fecha_Nac) 
	--no pongo todos los valores porque el unique funciona con un index, y los index no soportan mas de 900 bytes.
	--puede que sea mejor idea manejar la no duplicidad con un trigger? los 2 tienen sus ventajas y desventajas,
	--pero creo que me quedo con este.
	--deberia ser todo NOT NULL?
)
INSERT INTO tp.Cliente (dni, nombre, apellido, direccion, telefono, mail, fecha_Nac, ciudad)
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad
FROM gd_esquema.Maestra 

CREATE TABLE tp.Tipo_Pago(
	id INT IDENTITY(1,1) PRIMARY KEY,
	descripcion NVARCHAR(100),
	)
INSERT INTO tp.Tipo_Pago
SELECT DISTINCT Tipo_Pago_Desc
FROM gd_esquema.Maestra
WHERE Tipo_Pago_Desc IS NOT NULL


CREATE TABLE tp.Carga(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES tp.Cliente(id),
	credito NUMERIC(18,2), --seria redundante ponerle NOT NULL a estos?
	fecha DATETIME,
	tipo_Pago INT REFERENCES tp.Tipo_Pago(id), -- ni idea de que es esto pero es algo de carga. Podria ser un enum
	)

INSERT INTO tp.Carga
SELECT (SELECT id FROM tp.Cliente WHERE dni=Cli_Dni),Carga_Credito,Carga_Fecha, 
		(select id from tp.Tipo_Pago where Tipo_Pago_Desc=Tipo_Pago.descripcion)
FROM gd_esquema.Maestra AS M
WHERE Carga_Credito IS NOT NULL

CREATE TABLE tp.Rubro(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nombre NVARCHAR(100),
	)
INSERT INTO tp.Rubro
SELECT DISTINCT Provee_Rubro
FROM gd_esquema.Maestra
WHERE Provee_RS IS NOT NULL

CREATE TABLE tp.Proveedor(
	id INT IDENTITY(1,1) PRIMARY KEY,
	RS VARCHAR(100) UNIQUE, --no uso esto como PK porque es mas lento y solo es unico dentro de un pais
	dom VARCHAR(255),
	ciudad VARCHAR(255),
	telefono NUMERIC(18,0),
	CUIT VARCHAR(16) UNIQUE,
	rubro INT REFERENCES tp.Rubro(id),
	mail VARCHAR(255) DEFAULT null,
	codigoPostal INT DEFAULT null, 
	contacto VARCHAR(255) DEFAULT null,
	--estoy casi seguro de que el rs y el cuit no son unicos globalmente, pero lo pide el enunciado
	--la tabla maestra no tiene mail ni codigoPostal ni contacto, pero se da a entender que son datos que tienen
	--los proveedores y que se van a agregar mas adelante
	)

INSERT INTO tp.Proveedor (RS,dom,ciudad,telefono,CUIT,rubro)
SELECT DISTINCT Provee_RS,Provee_Dom,Provee_Ciudad,Provee_Telefono,Provee_CUIT, (SELECT id FROM tp.Rubro WHERE nombre=Provee_Rubro)
FROM gd_esquema.Maestra m
WHERE Provee_RS IS NOT NULL

CREATE TABLE tp.Oferta(
	codigo VARCHAR(50) PRIMARY KEY,
	descripcion VARCHAR(255),
	cantidad NUMERIC(18,0), --es el stock
	fecha DATE NOT NULL,
	fecha_Venc DATE NOT NULL,
	precio NUMERIC(18,2) NOT NULL,
	precio_Ficticio NUMERIC(18,2) NOT NULL,
	proveedor INT REFERENCES tp.Proveedor(id)
)
INSERT INTO tp.Oferta
SELECT DISTINCT Oferta_Codigo,
				Oferta_Descripcion,
				Oferta_Cantidad,
				Oferta_Fecha, 
				Oferta_Fecha_Venc, 
				Oferta_Precio, 
				Oferta_Precio_Ficticio,
				(SELECT id FROM tp.Proveedor p WHERE p.RS=m.Provee_RS)
FROM gd_esquema.Maestra m
WHERE Oferta_Codigo IS NOT NULL 


CREATE TABLE tp.Factura(
	nro NUMERIC(18,0) IDENTITY(200000, 1) PRIMARY KEY,
	fecha DATETIME,
	proveedor INT REFERENCES tp.Proveedor(id)
)
-- Esto es necesario porque los futuros nro de factura van a ser autogenerados pero los de la migracion no
-- SQL se queja cuando le metes de prepo una pk a una tabla. y te exige que le apses el flag IDENTITY_INSERT
-- Tambiex hace obligatorio que pongas las columnas despues del INSERT INTO tp.TABLA (campo1, campo2) como para que quede bien claro que la estas cagando
SET IDENTITY_INSERT tp.Factura ON 
INSERT INTO tp.Factura (nro, fecha, proveedor)
SELECT DISTINCT Factura_Nro,Factura_Fecha,(SELECT id FROM tp.Proveedor p WHERE p.RS=m.Provee_RS) 
FROM gd_esquema.Maestra m 
WHERE Factura_Nro IS NOT NULL
SET IDENTITY_INSERT tp.Factura OFF -- A partir de ahora el nro de la factura va a ser autogenerado.
/*

haciendo
SELECT Cli_Dni,Oferta_Codigo,COUNT(*) FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo
se puede ver que no hay registros de facturacion y entrega para todas las compra ventas.

En un principio pense en primero llenar la tabla con todas las compra ventas y despues ir haciendo
updates con joins para conseguir la factura y entrega de los que la tengan.
Pero me di cuenta de que poniendo un max se hace lo que quiero, que es dejar el null cuando no hay
registros y dejar el registro cuando esta, ya que solo hay 2 casos, o esta el registro o no esta.


CREATE TABLE tp.tp.ompra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY, --no estoy seguro de si la combinacion de las 3 FK es una PK
	cliente INT REFERENCES tp.Cliente(id),
	oferta VARCHAR(50) REFERENCES tp.Oferta(codigo),
	fecha_Compra DATETIME,
	fecha_Entrega DATETIME,

	--en la tabla maestra hay 3 tipos de filas,
	--unas tienen la factura (indica la compra)
	--otras tienen la entrega (indica entrega)
	--y otros de la compra en si
	)


probando
SELECT (SELECT id FROM tp.Cliente WHERE dni=Cli_Dni),Oferta_Codigo,COUNT(*) FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo
el count da siempre 1 o 3, 1 cuando solo hubo una compra, 3 cuando ademas de la compra hubo una entrega y una factura.

puede verse que solo hay una combinacion de cada tp.Cliente con codigo oferta (ningun tp.Cliente compro una oferta 2 veces),
lo que es conveniente porque simplifica el query para cargar compras ya que se puede agrupar por tp.Cliente y codigo.
y hacer un max para poder traer los otros campos y no se queje por el group by 

El max se encarga de dejar el valor que exista, e ignorar nulls, ya que solo hay dos casos posibles,
o esta el valor unico o hay null. Que sea un max especificamente no significa nada.

Cargo el valor de entrega porque hace mas simple construir la tabla de cupones, despues se lo saco

*/


CREATE TABLE tp.Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES tp.Cliente(id),
	oferta VARCHAR(50) REFERENCES tp.Oferta(codigo),
	factura NUMERIC(18,0) REFERENCES tp.Factura(nro) DEFAULT null,
	fecha_Compra DATETIME,
	fueCanjeado BIT DEFAULT 0,
	fecha_Entrega DATETIME
)

-- Esto tira 'Warning: Null value is eliminated by an aggregate or other SET operation.' Porque estamos descartando una row que tiene NULL en factura y fecha compra y es justamente lo deseado, no es un error.
INSERT INTO tp.Compra_Oferta (cliente, oferta, factura, fecha_Compra, fecha_Entrega)
SELECT  (SELECT id FROM tp.Cliente WHERE dni=Cli_Dni) AS Cliente,
		Oferta_Codigo,
		MAX(Factura_Nro),
		MAX(Oferta_Fecha_Compra),
		MAX(Oferta_Entregado_Fecha) 
FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo

CREATE TABLE tp.Cupon(
	codigo INT IDENTITY(1000,1) PRIMARY KEY, --podria ser algo mas complejo esto, o podria no serlo
	cliente INT REFERENCES tp.Cliente(id),
	fecha_Consumo DATETIME,
)

INSERT INTO tp.Cupon
SELECT Cliente, fecha_Entrega 
FROM tp.Compra_Oferta
WHERE fecha_Entrega IS NOT null

ALTER TABLE tp.Compra_Oferta DROP COLUMN fecha_Entrega
/*
se podrian juntar la compra oferta con el cupon, porque lo modelamos para que sea una relacion 1 a 1.
El id de la tabla seria el codigo cupon, que no significaria nada en compras sin cupon
Y se ahorraria tener el campo 'fue canjeado' en compra oferta
Y se tendria la informacion de de que compra oferta nacio el cupon.
No lo hago para no alejarme mucho de lo que parece querer el tp, igual no es nada muy loco
*/


CREATE TABLE tp.Funcionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
)

CREATE TABLE tp.Rol(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
	habilitado bit DEFAULT 1,
)

CREATE TABLE tp.RolxFuncionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	funcionalidad int FOREIGN KEY REFERENCES tp.Funcionalidad(id),
	rol int FOREIGN KEY REFERENCES tp.Rol(id),
)


INSERT INTO tp.Funcionalidad (nombre)
VALUES ('abm rol'), ('abm cliente'),('abm proveedor'),('carga credito')
,('confeccion y publicacion de oferta'),('compra oferta'),('consumo oferta')
,('facturacion a proveedor'),('listado estadistico');
--interpreto que loguearse y registrarse no son funcionalidades valores asignables a un rol
--podria interpretarse que son funcionalidades de un rol 'no logueado', pero tambien podrian tratarse
--aparte.


INSERT INTO tp.Rol (nombre)
VALUES ('cliente'),('proveedor'),('administrador'),('administrador general');

INSERT INTO tp.RolxFuncionalidad (rol,funcionalidad) -- Dado que es un script de migración único a ejecutar por única vez, es permisible esto
VALUES  (1,4),(1,6),
		(2,5),(2,7),
		(3,1),(3,2),(3,3),(3,8),(3,9);
INSERT INTO tp.RolxFuncionalidad (rol,funcionalidad)
(SELECT 4,id FROM tp.Funcionalidad);


UPDATE tp.Cliente SET saldo = (SELECT SUM(credito) FROM tp.Carga WHERE tp.Cliente.id=cliente)
WHERE EXISTS (SELECT credito FROM tp.Carga WHERE tp.Cliente.id=cliente)

UPDATE tp.Cliente SET saldo = saldo - (SELECT SUM(precio) 
	FROM tp.Oferta o JOIN tp.Compra_Oferta c ON o.codigo=c.oferta WHERE c.cliente=tp.Cliente.id)





/*
Entiendo que un nombre y apellido no son unicos pero se da la casualidad de que
si son unicos en la base de datos de la que se esta migrando:

SELECT c.Cli_Nombre FROM tp.Cliente c JOIN tp.Cliente b 
ON c.Cli_Nombre=b.Cli_Nombre AND c.Cli_Apellido=b.Cli_Apellido AND c.id!=b.id;

da vacio.
Esto es conveniente porque permite generar usuarios sin numeros ni cosas raras.
Si hubieran repeticiones se habria agregado un numero a cada usuario, probablemente concatenado su id

con proveedores pasa algo parecido. RS es unico solo dentro de un pais, por lo que podria darse que
haya 2 sociedades con el mismo nombre pero no se dio
*/

CREATE TABLE tp.Usuario(
	id INT IDENTITY(1,1) PRIMARY KEY, -- estario bueno que la PK sea (cliente,proveedor), pero sql no se banca que parte de una pk sea null
	nombre VARCHAR(128) NOT NULL UNIQUE, --este podria ser la PK, pero *creo* que es mas lento
	contraseña BINARY(32) NOT NULL,
	rol INT FOREIGN KEY REFERENCES tp.Rol(id),
	fallosLogin INT DEFAULT 0,
	habilitado BIT DEFAULT 1,
	cliente INT FOREIGN KEY REFERENCES tp.Cliente(id),
	proveedor INT FOREIGN KEY REFERENCES tp.Proveedor(id),
	--se podrian guardar la FK del tp.Cliente y del proveedor en un mismo campo y discriminar por el rol
	--si es que eso existe en sql ni idea
)

--tabla temporal donde se guardan las contraseÃ±as autogeneradas para los usuarios, para entregarselas
--a estos. La idea es que cambien esta contraseÃ±a provisional por una de verdad.
--Sin hacer esta tabla se le estaria asignando contraseÃ±as aleatorias a todos los usuarios, que no conoce nadie
CREATE TABLE tp.contraseñasMigracion(
	id INT PRIMARY KEY FOREIGN KEY REFERENCES tp.Usuario(id),
	contraseñaDesnuda VARCHAR(128)
)

DECLARE @nombre VARCHAR(128)
DECLARE @apellido VARCHAR(128)
DECLARE @id INT
DECLARE @rol INT
DECLARE @contraseñaDesnuda VARCHAR(64)
SET @rol = (SELECT id FROM tp.Rol WHERE nombre='Cliente');

DECLARE cur CURSOR FOR (SELECT nombre,apellido,id FROM tp.Cliente);
OPEN cur  
FETCH NEXT FROM cur INTO @nombre,@apellido,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO tp.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(CONCAT(@nombre,' ',@apellido),HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,@id,null)
	
	INSERT INTO tp.contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @nombre,@apellido,@id
END 
CLOSE cur  
DEALLOCATE cur 

DECLARE @rs VARCHAR(100)
SET @rol = (SELECT id FROM tp.Rol WHERE nombre='Proveedor');

DECLARE cur CURSOR FOR (SELECT RS,id FROM tp.Proveedor);
OPEN cur  
FETCH NEXT FROM cur INTO @RS,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO tp.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(@RS,HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,null,@id)
	
	INSERT INTO tp.contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @RS,@id
END 
CLOSE cur  
DEALLOCATE cur ;


-- Funciones
-- Dado el precio de ahora y el precio de antes. Calcula el descuento.
-- Devuelve valor entre 0 y 1. Usar FORMAT(func(), 'p') para imprimir lindo
GO
CREATE FUNCTION tp.descuento(@precio_venta NUMERIC(18,2), @precio_original NUMERIC(18,2))
RETURNS NUMERIC(18, 2)
AS
BEGIN
	RETURN (@precio_original - @precio_venta)
			/
			NULLIF(@precio_venta, 0)
END
GO


INSERT INTO tp.Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES ('admin',HASHBYTES('SHA2_256','w23e'),4,null,null)
