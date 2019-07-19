CREATE PROCEDURE [dbo].[ConsultarAnuncioMasAntiguo]

AS
BEGIN
	SELECT TOP 1 [A].[IdAnuncio]
	  ,[A].[IdUsuario]
      ,[A].[AdmiteMascota]
      ,[A].[IdTipoPropiedad]
      ,[A].[Precio]
      ,[A].[MaximaCantidadPersonas]
      ,[A].[TieneSeguridadPrivada]
      ,[A].[Activo]
      ,[A].[FechaCreacion]
	FROM [dbo].[Anuncio] [A] 
	ORDER BY [A].[FechaCreacion];
END
