-- CREATE DATABASE DB_FERREMAX;

-- USE DB_FERREMAX;

/*
CREATE TABLE TB_PRODUCTO (
    Idproducto INT IDENTITY(100, 1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
	Precio_Unitario DECIMAL(10,2) NOT NULL
);

CREATE TABLE TB_FACTURA (
    idFactura INT IDENTITY(2000, 1) PRIMARY KEY,
    Nombre_sucursal varchar(20) NOT NULL,
	Nombre_cliente varchar(255) NOT NULL,
	Numero_factura varchar(10) NOT NULL,
	Fecha DATETIME NOT NULL,
	Subtotal DECIMAL(10,2) NOT NULL,
	IGV DECIMAL(10,2) NOT NULL,
	Total DECIMAL(10,2) NOT NULL
);

CREATE TABLE TB_DETALLE_FACTURA (
    Idfactura int FOREIGN KEY REFERENCES TB_FACTURA(idFactura) NOT NULL,
    Idproducto int FOREIGN KEY REFERENCES TB_PRODUCTO(Idproducto) NOT NULL,
	Cantidad int NOT NULL,
	Precio_unitario DECIMAL(10,2) NOT NULL,
	SubTotal DECIMAL(10,2) NOT NULL,
	CONSTRAINT PK_TB_DETALLE_FACTURA PRIMARY KEY (Idfactura, Idproducto)
);
*/


CREATE PROCEDURE SP_RegistrarFactura @Sucursal varchar(20), @Cliente varchar(255), @Factura varchar(10), @Fecha DATETIME, @Subtotal DECIMAL(10,2), @IGV DECIMAL(10,2), @Total DECIMAL(10,2), @id int out
AS
BEGIN
	INSERT INTO TB_FACTURA (Nombre_sucursal, Nombre_cliente, Numero_factura, Fecha, Subtotal, IGV, Total)
	VALUES (@Sucursal, @Cliente, @Factura, @Fecha, @Subtotal, @IGV, @Total)
	SET @id = SCOPE_IDENTITY()
	return @id
END
GO

DECLARE @newid int;
EXEC SP_RegistrarFactura @Sucursal = 'Bogota', @Cliente = 'WA1 1DP', @Factura = '1', @Fecha =  '27-12-2021', @Subtotal = 1.12, @IGV = 2.12, @Total = 13.12, @id = @newid output;
print @newid






CREATE PROCEDURE SP_RegistrarProducto @Nombre VARCHAR(100), @Precio_unitario DECIMAL(10,2)
AS
BEGIN
	INSERT INTO TB_PRODUCTO (Nombre, Precio_Unitario)
	VALUES (@Nombre, @Precio_unitario)
END
GO

EXEC SP_RegistrarProducto @Nombre = 'zxc', @Precio_unitario = 3.51;







CREATE PROCEDURE SP_RegistrarDetalleFactura @Idfactura int, @Idproducto int, @Cantidad int, @Precio_unitario DECIMAL(10,2), @Subtotal DECIMAL(10,2)
AS
BEGIN
	INSERT INTO TB_DETALLE_FACTURA (Idfactura, Idproducto, Cantidad, Precio_unitario, SubTotal)
	VALUES (@Idfactura, @Idproducto, @Cantidad, @Precio_unitario, @Subtotal)
END
GO

EXEC SP_RegistrarDetalleFactura @Idfactura = 2006, @Idproducto = 101, @Cantidad = 5, @Precio_unitario = 2.51, @Subtotal = 3.72;






CREATE FUNCTION FN_CantProductosXFactura (@idfactura int)
returns table
as
return (SELECT SUM(CANTIDAD) AS CONTEO FROM TB_DETALLE_FACTURA WHERE Idfactura = @idfactura)

select * from FN_CantProductosXFactura (2000)






CREATE PROCEDURE SP_ListarFacturasXFecha @fecha date
as
BEGIN
	SELECT idfactura, Numero_factura, Fecha, fn.CONTEO, Total
	FROM TB_FACTURA f
	cross apply FN_CantProductosXFactura(f.idfactura) fn
	where fecha >= @fecha and fecha < DATEADD(day, 1, @fecha)
END
GO

EXEC SP_ListarFacturasXFecha @fecha = '27-12-2021'







CREATE PROCEDURE SP_ListarProducto
AS
BEGIN
	SELECT * FROM TB_PRODUCTO
END
GO

EXEC SP_ListarProducto






CREATE PROCEDURE SP_ListarLast5Facturas
AS
BEGIN
	SELECT TOP 5 F.*, FN.CONTEO
	FROM TB_FACTURA F
	cross apply FN_CantProductosXFactura(f.idfactura) fn
	ORDER BY FECHA DESC
END
GO


EXEC SP_ListarLast5Facturas



CREATE PROCEDURE SP_ReporteDeVentas @fechaINI DATE, @fechaFIN DATE
AS
BEGIN
	BEGIN
		SELECT TOP 5 * 
		FROM TB_FACTURA
		where fecha >= @fechaINI and fecha < DATEADD(day, 1, @fechaFIN)
		ORDER BY TOTAL DESC
	END

	BEGIN
		SELECT * FROM TB_DETALLE_FACTURA WHERE Idfactura IN 
		(
			SELECT TOP 5 IDFACTURA
			FROM TB_FACTURA
			where fecha >= @fechaINI and fecha < DATEADD(day, 1, @fechaFIN)
			ORDER BY TOTAL DESC
		)
	END
END
GO



EXEC SP_ReporteDeVentas @fechaINI = '27-12-2021', @fechaFIN = '30-12-2021'


