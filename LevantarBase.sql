

CREATE TABLE Cliente(
	id INT IDENTITY(1,1) PRIMARY KEY,
	dni NUMERIC(18,0) NOT NULL,
	nombre NVARCHAR(255) NOT NULL,
	apellido NVARCHAR(255) NOT NULL,
	direccion NVARCHAR(255) NOT NULL,
	telefono NUMERIC(18,0) NOT NULL, /*UNIQUE?*/
	mail NVARCHAR(255) NOT NULL, /*UNIQUE hay dos minas que comparte mail, no sé si considerarlo un problema*/
	fecha_Nac DATETIME, /*deberia ser NOT NULL pero no solucione el tema de la conversion*/
	ciudad NVARCHAR(255) NOT NULL,
	saldo DECIMAL(32,2) NOT NULL DEFAULT 0,
	UNIQUE(nombre,apellido,dni,direccion,telefono,mail,fecha_Nac,ciudad)
	--deberia ser todo NOT NULL?
)
INSERT INTO Cliente (dni, nombre, apellido, direccion, telefono, mail, fecha_Nac, ciudad)
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad
FROM gd_esquema.Maestra 

CREATE TABLE Carga(
	id INT IDENTITY(1,1) PRIMARY KEY,
	cliente INT REFERENCES Cliente(id),
	credito NUMERIC(18,2), --seria redundante ponerle NOT NULL a estos?
	fecha DATETIME,
	tipo_Pago_Desc NVARCHAR(100), -- ni idea de que es esto pero es algo de carga. Podria ser un enum
	)

INSERT INTO Carga
SELECT (SELECT id FROM Cliente WHERE dni=Cli_Dni),Carga_Credito,Carga_Fecha,Tipo_Pago_Desc
FROM gd_esquema.Maestra AS M
WHERE Carga_Credito IS NOT NULL

CREATE TABLE Proveedor(
	id INT IDENTITY(1,1) PRIMARY KEY,
	RS NVARCHAR(100) UNIQUE, --no uso esto como PK porque es mas lento y solo es unico dentro de un pais
	dom NVARCHAR(255),
	ciudad NVARCHAR(255),
	telefono NUMERIC(18,0),
	CUIT NVARCHAR(16) UNIQUE,
	rubro NVARCHAR(32),
	mail NVARCHAR(255) DEFAULT null,
	codigoPostal INT DEFAULT null, 
	contacto NVARCHAR(255) DEFAULT null,
	--estoy casi seguro de que el rs y el cuit no son unicos globalmente, pero lo pide el enunciado
	--la tabla maestra no tiene mail ni codigoPostal ni contacto, pero se da a entender que son datos que tienen
	--los proveedores y que se van a agregar mas adelante
	)

INSERT INTO Proveedor (RS,dom,ciudad,telefono,CUIT,rubro)
SELECT DISTINCT Provee_RS,Provee_Dom,Provee_Ciudad,Provee_Telefono,Provee_CUIT,Provee_Rubro
FROM gd_esquema.Maestra
WHERE Provee_RS IS NOT NULL

CREATE TABLE Oferta(
	codigo NVARCHAR(50) PRIMARY KEY, --hay ofertas iguales con codigos distintos, sospechoso
	descripcion NVARCHAR(255),
	cantidad NUMERIC(18,0), --es el stock
	fecha DATETIME NOT NULL,
	fecha_Venc DATETIME NOT NULL,
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

CREATE TABLE Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY, --no estoy seguro de si la combinacion de las 3 FK es una PK
	cliente INT REFERENCES Cliente(id),
	oferta NVARCHAR(50) REFERENCES Oferta(codigo),
	factura NUMERIC(18,0) REFERENCES Factura(nro),
	fecha_Compra DATETIME,
	fecha_Entrega DATETIME,

	--en la tabla maestra hay 3 tipos de filas,
	--unas tienen la factura (indica la compra)
	--otras tienen la entrega (indica entrega)
	-- (hay la misma cantidad de estos 2)
	--y otra que no tienen ni la factura ni la entrega. Por ahora las ignoro porque no me dicen nada
	)

/*
SELECT A.Cli_Dni, A.Oferta_Codigo,A.Factura_Nro,A.Oferta_Fecha_Compra,B.Oferta_Entregado_Fecha 
FROM gd_esquema.Maestra A INNER JOIN gd_esquema.Maestra B 
ON A.Oferta_Codigo IS NOT NULL AND
A.Oferta_Fecha_Compra=B.Oferta_Fecha_Compra AND 
A.Cli_Dni=B.Cli_Dni AND
A.Factura_Nro IS NOT NULL AND 
B.Oferta_Entregado_Fecha IS NOT NULL 
--la idea de este query es juntar los registros de factura y entregado
--no es perfecto porque no tengo un identificador que me separe esos dos registros
--osea si un cliente pide la misma oferta el mismo dia van a generarse 2 registros de mas
*/



--no estoy seguro de si resolver esto con un muchos a muchos normalizado es la mejor idea

CREATE TABLE Funcionalidad(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre nvarchar(40),
)

CREATE TABLE Rol(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre nvarchar(40),
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
	nombre VARCHAR(128) NOT NULL UNIQUE, --este podria ser la PK
	contraseña VARCHAR(128) NOT NULL,
	rol INT FOREIGN KEY REFERENCES Rol(id),
	fallosLogin INT DEFAULT 0,
	habilitado BIT DEFAULT 1,
	cliente INT FOREIGN KEY REFERENCES Cliente(id),
	proveedor INT FOREIGN KEY REFERENCES Proveedor(id),
	--se podrian guardar la FK del cliente y del proveedor en un mismo campo y discriminar por el rol
	--si es que eso existe en sql ni idea
)

--@todo contraseña
INSERT INTO Usuario (nombre,contraseña,rol,cliente,proveedor)
SELECT CONCAT(nombre,' ',apellido),'1234',(SELECT id FROM Rol WHERE nombre='Cliente'),id,null
FROM Cliente

INSERT INTO Usuario (nombre,contraseña,rol,cliente,proveedor)
SELECT RS,'1234',(SELECT id FROM Rol WHERE nombre='Proveedor'),null,id
FROM Proveedor

--@TODO habria que meter todo en el esquema gd_esquema?
