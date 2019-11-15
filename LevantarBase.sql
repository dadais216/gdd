

CREATE TABLE Cliente(
	id INT IDENTITY(1,1) PRIMARY KEY,
	Cli_Dni NUMERIC(18,0) NOT NULL,
	Cli_Nombre NVARCHAR(255) NOT NULL,
	Cli_Apellido NVARCHAR(255) NOT NULL,
	Cli_Direccion NVARCHAR(255) NOT NULL,
	Cli_Telefono NUMERIC(18,0) NOT NULL, /*UNIQUE?*/
	Cli_Mail NVARCHAR(255) NOT NULL, /*UNIQUE hay dos minas que comparte mail, no sé si considerarlo un problema*/
	Cli_Fecha_Nac DATETIME, /*deberia ser NOT NULL pero no solucione el tema de la conversion*/
	Cli_Ciudad NVARCHAR(255) NOT NULL,
	Saldo DECIMAL(32,2) NOT NULL DEFAULT 0,
	UNIQUE(Cli_Nombre,Cli_Apellido,Cli_Dni,Cli_Direccion,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac,Cli_Ciudad)
	--deberia ser todo NOT NULL?
)
INSERT INTO Cliente (Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad)
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Direccion, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac, Cli_Ciudad
FROM gd_esquema.Maestra 

CREATE TABLE Carga(
	id INT IDENTITY(1,1) PRIMARY KEY,
	Cli_id INT REFERENCES Cliente(id),
	Carga_Credito NUMERIC(18,2), --seria redundante ponerle NOT NULL a estos?
	Carga_Fecha DATETIME,
	Tipo_Pago_Desc NVARCHAR(100), -- ni idea de que es esto pero es algo de carga. Podria ser un enum
	)

INSERT INTO Carga
SELECT (SELECT id FROM Cliente WHERE Cli_Dni=M.Cli_Dni),Carga_Credito,Carga_Fecha,Tipo_Pago_Desc
FROM gd_esquema.Maestra AS M
WHERE Carga_Credito IS NOT NULL

CREATE TABLE Proveedor(
	Provee_RS NVARCHAR(100) PRIMARY KEY, --creo que usar esto como primary key es lento, no?
	Provee_Drom NVARCHAR(100),
	Provee_Ciudad NVARCHAR(255), --100 para el domicilio y 255 para ciudad??
	Provee_Telefono NUMERIC(18,0),
	Provee_CUIT NVARCHAR(100),
	)

INSERT INTO Proveedor
SELECT DISTINCT Provee_RS,Provee_Dom,Provee_Ciudad,Provee_Telefono,Provee_CUIT
FROM gd_esquema.Maestra
WHERE Provee_RS IS NOT NULL

CREATE TABLE Oferta(
	Oferta_Codigo NVARCHAR(50) PRIMARY KEY, --hay ofertas iguales con codigos distintos, sospechoso
	Oferta_Descripcion NVARCHAR(255),
	Oferta_Cantidad NUMERIC(18,0), --es el stock
	Oferta_Fecha DATETIME NOT NULL,
	Oferta_Fecha_Venc DATETIME NOT NULL,
	Oferta_Precio NUMERIC(18,2) NOT NULL,
	Oferta_Precio_Ficticio NUMERIC(18,2) NOT NULL,
	Provee_RS NVARCHAR(100) REFERENCES Proveedor(Provee_RS)
	)
INSERT INTO Oferta
SELECT DISTINCT Oferta_Codigo, Oferta_Descripcion, Oferta_Cantidad, Oferta_Fecha, Oferta_Fecha_Venc, Oferta_Precio, Oferta_Precio_Ficticio, Provee_RS
FROM gd_esquema.Maestra
WHERE Oferta_Codigo IS NOT NULL 

CREATE TABLE Factura(
	Factura_Nro NUMERIC(18,0) PRIMARY KEY,
	Factura_Fecha DATETIME,
	Provee_RS NVARCHAR(100) REFERENCES Proveedor(Provee_RS)
	)
INSERT INTO Factura
SELECT DISTINCT Factura_Nro,Factura_Fecha,Provee_RS FROM gd_esquema.Maestra WHERE Factura_Nro IS NOT NULL

CREATE TABLE Compra_Oferta(
	id INT IDENTITY(1,1) PRIMARY KEY, --no estoy seguro de si la combinacion de las 3 FK es una PK
	Cli_id INT REFERENCES Cliente(id),
	Oferta_Codigo NVARCHAR(50) REFERENCES Oferta(Oferta_Codigo),
	Factura_Nro NUMERIC(18,0) REFERENCES Factura(Factura_Nro),
	Oferta_Fecha_Compra DATETIME,
	Oferta_Entregado_Fecha DATETIME,

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



/*SELECT CONCAT(Cli_Nombre,id) FROM Cliente;
*/


UPDATE Cliente SET Saldo = (SELECT SUM(Carga_Credito) FROM Carga WHERE Cliente.id=Cli_id)
WHERE EXISTS (SELECT Carga_Credito FROM Carga WHERE Cliente.id=Cli_id)
--@TODO restar las compras




--@TODO habria que meter todo en el esquema gd_esquema?
