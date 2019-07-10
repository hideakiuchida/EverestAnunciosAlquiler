CREATE PROCEDURE [dbo].[ConsultarAnuncioMasAntiguo]

AS
BEGIN
	SELECT TOP 1 [IdAnuncio]
	  ,[IdUsuario]
      ,[AdmiteMascota]
      ,[IdTipoPropiedad]
      ,[Precio]
      ,[MaximaCantidadPersonas]
      ,[TieneSeguridadPrivada]
      ,[Activo]
      ,[FechaCreacion]
	FROM [dbo].[Anuncio]
	ORDER BY [FechaCreacion] DESC;
END
