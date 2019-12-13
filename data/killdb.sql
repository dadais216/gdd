IF OBJECT_ID('Carga') IS NOT NULL DROP TABLE Carga;
IF OBJECT_ID('Compra_Oferta') IS NOT NULL DROP TABLE Compra_Oferta;
IF OBJECT_ID('contraseñasMigracion') IS NOT NULL DROP TABLE contraseñasMigracion;
IF OBJECT_ID('Cupon') IS NOT NULL DROP TABLE Cupon;
IF OBJECT_ID('Factura') IS NOT NULL DROP TABLE Factura;
IF OBJECT_ID('Oferta') IS NOT NULL DROP TABLE Oferta;
IF OBJECT_ID('RolXFuncionalidad') IS NOT NULL DROP TABLE RolXFuncionalidad;
IF OBJECT_ID('Funcionalidad') IS NOT NULL DROP TABLE Funcionalidad;
IF OBJECT_ID('Usuario') IS NOT NULL DROP TABLE Usuario;
IF OBJECT_ID('Rol') IS NOT NULL DROP TABLE Rol;
IF OBJECT_ID('Tipo_Pago') IS NOT NULL DROP TABLE Tipo_Pago;
IF OBJECT_ID('Cliente') IS NOT NULL DROP TABLE Cliente;
IF OBJECT_ID('Proveedor') IS NOT NULL DROP TABLE Proveedor;

IF OBJECT_ID('descuento') IS NOT NULL DROP FUNCTION descuento;