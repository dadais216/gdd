

CREATE TABLE Cliente(
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
INSERT INTO Cliente (dni, nombre, apellido, direccion, telefono, mail, fecha_Nac, ciudad)
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad
FROM gd_esquema.Maestra 

CREATE TABLE Tipo_Pago(
	id INT IDENTITY(1,1) PRIMARY KEY,
	tipo_Pago_Desc NVARCHAR(100),
	)

CREATE TABLE Carga(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES Cliente(id),
	credito NUMERIC(18,2), --seria redundante ponerle NOT NULL a estos?
	fecha DATETIME,
	--tipo_Pago INT REFERENCES Tipo_Pago(id), -- ni idea de que es esto pero es algo de carga. Podria ser un enum
	)

INSERT INTO Carga
SELECT (SELECT id FROM Cliente WHERE dni=Cli_Dni),Carga_Credito,Carga_Fecha--,Tipo_Pago_Desc
FROM gd_esquema.Maestra AS M
WHERE Carga_Credito IS NOT NULL

CREATE TABLE Proveedor(
	id INT IDENTITY(1,1) PRIMARY KEY,
	RS VARCHAR(100) UNIQUE, --no uso esto como PK porque es mas lento y solo es unico dentro de un pais
	dom VARCHAR(255),
	ciudad VARCHAR(255),
	telefono NUMERIC(18,0),
	CUIT VARCHAR(16) UNIQUE,
	rubro VARCHAR(32),
	mail VARCHAR(255) DEFAULT null,
	codigoPostal INT DEFAULT null, 
	contacto VARCHAR(255) DEFAULT null,
	--estoy casi seguro de que el rs y el cuit no son unicos globalmente, pero lo pide el enunciado
	--la tabla maestra no tiene mail ni codigoPostal ni contacto, pero se da a entender que son datos que tienen
	--los proveedores y que se van a agregar mas adelante
	)

INSERT INTO Proveedor (RS,dom,ciudad,telefono,CUIT,rubro)
SELECT DISTINCT Provee_RS,Provee_Dom,Provee_Ciudad,Provee_Telefono,Provee_CUIT,Provee_Rubro
FROM gd_esquema.Maestra
WHERE Provee_RS IS NOT NULL

CREATE TABLE Oferta(
	codigo VARCHAR(50) PRIMARY KEY, --hay ofertas iguales con codigos distintos, sospechoso
	descripcion VARCHAR(255),
	cantidad NUMERIC(18,0), --es el stock
	fecha DATE NOT NULL,
	fecha_Venc DATE NOT NULL,
	precio NUMERIC(18,2) NOT NULL,
	precio_Ficticio NUMERIC(18,2) NOT NULL,
	proveedor INT REFERENCES Proveedor(id)
	)
INSERT INTO Oferta
SELECT DISTINCT Oferta_Codigo, Oferta_Descripcion, Oferta_Cantidad, Oferta_Fecha, Oferta_Fecha_Venc, Oferta_Precio, Oferta_Precio_Ficticio, (SELECT id FROM Proveedor p WHERE p.RS=m.Provee_RS)
FROM gd_esquema.Maestra m
WHERE Oferta_Codigo IS NOT NULL 

CREATE TABLE Factura(
	nro NUMERIC(18,0) PRIMARY KEY,
	fecha DATETIME,
	proveedor INT REFERENCES Proveedor(id)
	)
INSERT INTO Factura
SELECT DISTINCT Factura_Nro,Factura_Fecha,(SELECT id FROM Proveedor p WHERE p.RS=m.Provee_RS) 
FROM gd_esquema.Maestra m 
WHERE Factura_Nro IS NOT NULL

/*
la idea inicial era que compra_oferta tenga la factura a la que pertence y la fecha en que sus cupones
fueron entregados, de ahi esta formulacion
Despues decidimos manejar los cupones aparte, y las facturas tambien. 
Las compra oferta podrian tener un campo de factura a la que pertence, pero mantenerlo es costoso y no
parece util,  ademas de que es medio incomodo. Las compra ofertas se generarian con ese campo vacio
y se setearia al momento de generar la factura. Como es incomodo, no se pide y no se necesita decidimos
no hacerlo. Si se necesita saber si una compra oferta pertenece a una factura se puede obtener con un
query que filtre segun los datos de la factura (proveedor y fechas). Como es algo que no se necesita
hacer en ese sistema no vemos necesario gastar recursos en soportarlo.

CREATE TABLE Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES Cliente(id),
	oferta VARCHAR(50) REFERENCES Oferta(codigo),
	factura NUMERIC(18,0) REFERENCES Factura(nro),
	fecha_Compra DATETIME,
	fecha_Entrega DATETIME,
)

/*
INSERT INTO Compra_Oferta (
	cliente,
	oferta,
	factura,
	fecha_Compra,
	fecha_Entrega
)
SELECT	(SELECT id FROM Cliente C WHERE dni = A.Cli_Dni) as cliente,
		A.Oferta_Codigo,
		A.Factura_Nro,
		A.Oferta_Fecha_Compra,
		B.Oferta_Entregado_Fecha
FROM gd_esquema.Maestra A
JOIN gd_esquema.Maestra B ON
A.Factura_Nro IS NOT NULL AND A.Factura_Fecha IS NOT NULL AND
A.Oferta_Codigo=B.Oferta_Codigo
AND B.Oferta_Entregado_Fecha IS NOT NULL
AND B.Cli_Dni = A.Cli_Dni
*/
	--en la tabla maestra hay 3 tipos de filas,
	--unas tienen la factura (indica la compra)
	--otras tienen la entrega (indica entrega)
	--y otros de la compra en si
	)
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
*/


INSERT INTO Compra_Oferta
SELECT (SELECT id FROM Cliente WHERE dni=Cli_Dni),Oferta_Codigo,MAX(Factura_Nro),MAX(Oferta_Fecha_Compra),MAX(Oferta_Entregado_Fecha) FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo

*/
CREATE TABLE Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY, --no estoy seguro de si la combinacion de las 3 FK es una PK
	cliente INT REFERENCES Cliente(id),
	oferta VARCHAR(50) REFERENCES Oferta(codigo),
	fecha_Compra DATETIME,
	fecha_Entrega DATETIME,

	--en la tabla maestra hay 3 tipos de filas,
	--unas tienen la factura (indica la compra)
	--otras tienen la entrega (indica entrega)
	--y otros de la compra en si
	)

/*
probando
SELECT (SELECT id FROM Cliente WHERE dni=Cli_Dni),Oferta_Codigo,COUNT(*) FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo
el count da siempre 1 o 3, 1 cuando solo hubo una compra, 3 cuando ademas de la compra hubo una entrega y una factura.

puede verse que solo hay una combinacion de cada cliente con codigo oferta (ningun cliente compro una oferta 2 veces),
lo que es conveniente porque simplifica el query para cargar compras.

El max se encarga de dejar el valor que exista, e ignorar nulls, ya que solo hay dos casos posibles,
o esta el valor unico o hay null. Que sea un max especificamente no significa nada.

Cargo el valor de entrega porque hace mas simple construir la tabla de cupones, despues se lo saco
*/
INSERT INTO Compra_Oferta
SELECT (SELECT id FROM Cliente WHERE dni=Cli_Dni),Oferta_Codigo,MAX(Oferta_Fecha_Compra),MAX(Oferta_Entregado_Fecha) FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL
GROUP BY Cli_Dni,Oferta_Codigo

CREATE TABLE Cupon(
	codigo INT IDENTITY(1000,1) PRIMARY KEY, --podria ser algo mas complejo esto, o podria no serlo
	cliente INT REFERENCES Cliente(id),
	fecha_Consumo DATETIME,
)

INSERT INTO Cupon
SELECT cliente,fecha_Entrega FROM Compra_Oferta
WHERE fecha_Entrega IS NOT null

ALTER TABLE Compra_Oferta DROP COLUMN fecha_Entrega





CREATE TABLE Funcionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
)

CREATE TABLE Rol(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40),
	habilitado bit DEFAULT 1,
)

CREATE TABLE RolxFuncionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	funcionalidad int FOREIGN KEY REFERENCES Funcionalidad(id),
	rol int FOREIGN KEY REFERENCES Rol(id),
)


INSERT INTO Funcionalidad (nombre)
VALUES ('abm rol'), ('abm cliente'),('abm proveedor'),('carga credito')
,('confeccion y publicacion de oferta'),('compra oferta'),('consumo oferta')
,('facturacion a proveedor'),('listado estadistico');
--interpreto que loguearse y registrarse no son funcionalidades valores asignables a un rol
--podria interpretarse que son funcionalidades de un rol 'no logueado', pero tambien podrian tratarse
--aparte.


INSERT INTO Rol (nombre)
VALUES ('cliente'),('proveedor'),('administrador'),('administrador general');

--SELECT * FROM Rol;
--SELECT * FROM Funcionalidad;

INSERT INTO RolxFuncionalidad (rol,funcionalidad) --hay alguna forma mejor de hacer esto? 
VALUES  (1,4),(1,6),(1,7),
		(2,5),
		(3,1),(3,2),(3,3),(3,8),(3,9);
INSERT INTO RolxFuncionalidad (rol,funcionalidad)
(SELECT 4,id FROM Funcionalidad);


UPDATE Cliente SET Saldo = (SELECT SUM(credito) FROM Carga WHERE Cliente.id=cliente)
WHERE EXISTS (SELECT credito FROM Carga WHERE Cliente.id=cliente)
--@TODO restar las compras





/*
Entiendo que un nombre y apellido no son unicos pero se da la casualidad de que
si son unicos en la base de datos de la que se esta migrando:

SELECT c.Cli_Nombre FROM Cliente c JOIN Cliente b 
ON c.Cli_Nombre=b.Cli_Nombre AND c.Cli_Apellido=b.Cli_Apellido AND c.id!=b.id;

da vacio.
Esto es conveniente porque permite generar usuarios sin numeros ni cosas raras.
Si hubieran repeticiones se habria agregado un numero a cada usuario, probablemente concatenado su id

con proveedores pasa algo parecido. RS es unico solo dentro de un pais, por lo que podria darse que
haya 2 sociedades con el mismo nombre pero no se dio
*/

CREATE TABLE Usuario(
	id INT IDENTITY(1,1) PRIMARY KEY, -- estario bueno que la PK sea (cliente,proveedor), pero sql no se banca que parte de una pk sea null
	nombre VARCHAR(128) NOT NULL UNIQUE, --este podria ser la PK, pero *creo* que es mas lento
	contraseña BINARY(32) NOT NULL,
	rol INT FOREIGN KEY REFERENCES Rol(id),
	fallosLogin INT DEFAULT 0,
	habilitado BIT DEFAULT 1,
	cliente INT FOREIGN KEY REFERENCES Cliente(id),
	proveedor INT FOREIGN KEY REFERENCES Proveedor(id),
	--se podrian guardar la FK del cliente y del proveedor en un mismo campo y discriminar por el rol
	--si es que eso existe en sql ni idea
)

--tabla temporal donde se guardan las contraseÃ±as autogeneradas para los usuarios, para entregarselas
--a estos. La idea es que cambien esta contraseÃ±a provisional por una de verdad.
--Sin hacer esta tabla se le estaria asignando contraseÃ±as aleatorias a todos los usuarios, que no conoce nadie
CREATE TABLE contraseñasMigracion(
	id INT PRIMARY KEY FOREIGN KEY REFERENCES Usuario(id),
	contraseñaDesnuda VARCHAR(128)
)

DECLARE @nombre VARCHAR(128)
DECLARE @apellido VARCHAR(128)
DECLARE @id INT
DECLARE @rol INT
DECLARE @contraseñaDesnuda VARCHAR(64)
SET @rol = (SELECT id FROM Rol WHERE nombre='Cliente');

DECLARE cur CURSOR FOR (SELECT nombre,apellido,id FROM Cliente);
OPEN cur  
FETCH NEXT FROM cur INTO @nombre,@apellido,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(CONCAT(@nombre,' ',@apellido),HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,@id,null)
	
	INSERT INTO contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @nombre,@apellido,@id
END 
CLOSE cur  
DEALLOCATE cur 

DECLARE @rs VARCHAR(100)
SET @rol = (SELECT id FROM Rol WHERE nombre='Proveedor');

DECLARE cur CURSOR FOR (SELECT RS,id FROM Proveedor);
OPEN cur  
FETCH NEXT FROM cur INTO @RS,@id
WHILE @@FETCH_STATUS = 0  
BEGIN  
	
	SET @contraseñaDesnuda = CONVERT(varchar(64),ABS(CHECKSUM(NewId())));

	INSERT INTO Usuario (nombre,contraseña,rol,cliente,proveedor) VALUES
	(@RS,HASHBYTES('SHA2_256',@contraseñaDesnuda),@rol,null,@id)
	
	INSERT INTO contraseñasMigracion VALUES (@@IDENTITY ,@contraseñaDesnuda)
	FETCH NEXT FROM cur INTO @RS,@id
END 
CLOSE cur  
DEALLOCATE cur ;


--@TODO habria que meter todo en el esquema gd_esquema?

-- Funciones
-- Dado el precio de ahora y el precio de antes. Calcula el descuento.
-- Devuelve valor entre 0 y 1. Usar FORMAT(func(), 'p') para imprimir lindo

GO -- los go estos estan para solucionar un error raro ('CREATE FUNCTION' must be the first statement in a query batch)
CREATE FUNCTION descuento(@precio_venta NUMERIC(18,2), @precio_original NUMERIC(18,2))
RETURNS NUMERIC(18, 2)
AS
BEGIN
	RETURN (@precio_original - @precio_venta)
			/
			NULLIF(@precio_venta, 0)
END
GO