/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [idFactura]
      ,[Nombre_sucursal]
      ,[Nombre_cliente]
      ,[Numero_factura]
      ,[Fecha]
      ,[Subtotal]
      ,[IGV]
      ,[Total]
  FROM [DB_FERREMAX].[dbo].[TB_FACTURA]


  select GETDATE()


INSERT INTO TB_FACTURA (Nombre_sucursal, Nombre_cliente, Numero_factura, Fecha, Subtotal, IGV, Total)

VALUES ('ASD', 'ASD', 'AASD', GETDATE(), 2, 2, 1)
SELECT SCOPE_IDENTITY()


