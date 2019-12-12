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
IF OBJECT_ID('tp.Proveedor') IS NOT NULL DROP TABLE tp.Proveedor;

IF OBJECT_ID('tp.descuento') IS NOT NULL DROP FUNCTION tp.descuento;