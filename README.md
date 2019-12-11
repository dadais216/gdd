# **Gestión de Datos UTN FRBA - TP 2019 2C**
# **FRBA Ofertas**
 
![Frba Ofertas](/images/utnposta.png)
 
## **FRBA Ofertas**
El presente TP describe una situación de migración de un sistema antiguo a partir de una 
base de datos con una tabla única no normalizada. A partir de la inferencia, consulta, deducción y 
análisis de las funcionalidades a proveer se diseñó el nuevo modelo de datos representado en un DER.
El nuevo sistema será un sistema de ofertas mediante cupones. Un proveedor publica un cupón con cierto
descuento sobre un producto. El cliente puede adquirirlo si tiene saldo en su cuenta y aprovechar un mejor precio.
Por otro lado el usuario administrador será el encargado de emitir las facturas regularmente. Este proceso 
será manual y el administrador podrá seleccionar el período a facturar y el proveedor para el cual emitir la factura.
El alcance del TP no es un sistema listo para salir a producción. No tiene testing unitario ya que no fue
requisito de aprobación. No obstante se hicieron las debidas pruebas funcionales para comprobar que el sistema contempla
correctamente casos de borde emitiendo mensajes de error informativos que sugieren al usuario su buen uso.
En un proyecto de software no académico el testing unitario es indispensable ya que se está lidiando con dinero y
cualquier falta puede ser motivo de sanción y/o pérdida de contrato/s.

## **Diagrama Entidad Relación (DER):**

![DER](/images/derv1.png)

## **Integrantes:**

| Legajo | Apellido | Nombre | Curso |
| -------- | -------- | -------- | -------- |
| 155.559-5 | Ambrosini | Nahuel | K3521 | 
| 156.101-7 | Apellido | Daniel | K3573 | 

## **Componentes del Sistema:**
* [Base de Datos](https://github.com)
* [Aplicación Desktop](https://github.com)
  * [Registro](https://github.com)
  * [Login](https://github.com)
  * [ABM Cliente](https://github.com)
  * [ABM Proveedor](https://github.com)
  * [ABM Rol](https://github.com)
  * [Cargar Credito](https://github.com)
  * [Comprar Oferta](https://github.com)
  * [Publicar Oferta](https://github.com)
  * [Facturación a Proveedor](https://github.com)
  * [Listado Estadístico](https://github.com)


## **Instalación:**

#### 1. Instalar el motor de base de datos Microsoft SQL Server 2012 Express

#### 2. Crear una instancia del motor de base de datos

    ● El nombre de la instancia del motor de base de datos a instalar debe llamarse “SQLSERVER2012”. 
      No utilizar el nombre “Default” para la instancia. Instalar como instancia con nombre (“Named Instance”)
    ● La autenticación debe ser por “Modo Mixto”
       
#### 3. El usuario administrador de la base de datos deberá tener la siguiente configuración:
    ● Username: “sa”
    ● Password: “gestiondedatos”
    
#### 4. Crear un nuevo “Inicio de Sesión”, desde el item “Seguridad” perteneciente al servidor de Base de Datos general. El inicio de sesión debe poseer las siguientes características:

    ● Solapa “General”:
    ● Nombre de inicio de sesión: “gd”
    ● Autenticación de SQL Server
    ● Contraseña: “gd2019”
    ● Base de Datos Predeterminada: GD2C2019.
    ● El resto de los parámetros respetar sus valores default.
    ● Solapa “Funciones del Servidor”:
    ● Seleccionar “sysadmin”
    ● Solapa “Asignación de Usuarios”:
    ● Seleccionar asignar a “GD2C2019”
    ● Para el resto de los parámetros respetar sus valores default.   
    
#### 5. Salir del “Management Studio” como usuario “sa” y volver a ingresar con el nuevo usuario “gd” creado. Es probable que informe que la contraseña ha caducado. Cambiar la contraseña ingresando exactamente la misma que antes: “gd2019”

#### 6. Correr el [EjecutarScriptTablaMaestra.bat](https://github.com) 

    El Script necesita aproximadamente 40 minutos para finalizar su ejecución

#### 7. Correr el [script_creacion_inicial.sql](https://github.com) en el SQL Server

     El Script necesita aproximadamente 1 minuto para finalizar su ejecución
   
#### 8. Instalar el Microsoft Visual C# 2012 Express Edition

#### 9. Importar la [Aplicación Desktop](https://github.com) en el Visual Studio 2012 Express y ejecutarlo

## **Parámetros de Configuración**
Se encuentran en el archivo [App.config](https://github.com/) y definen las partes configurables de la aplicación.

* **server/user/password:** cadena de conexión de la aplicación. Define la ubicación de la base de datos, su usuario y su password.
* **fecha:** la fecha actual del sistema para realizar pruebas a la aplicación.
