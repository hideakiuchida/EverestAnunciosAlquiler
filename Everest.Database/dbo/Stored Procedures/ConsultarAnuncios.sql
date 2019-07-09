CREATE PROCEDURE [dbo].[ConsultarAnuncios]
	@IdUsuario int = NULL
AS
BEGIN
	SELECT [IdAnuncio]
	  ,[IdUsuario]
      ,[AdmiteMascota]
      ,[IdTipoPropiedad]
      ,[Precio]
      ,[MaximaCantidadPersonas]
      ,[TieneSeguridadPrivada]
      ,[Activo]
      ,[FechaCreacion]
	FROM [dbo].[Anuncio]
	WHERE (@IdUsuario IS NULL OR [IdUsuario] = @IdUsuario);
END
