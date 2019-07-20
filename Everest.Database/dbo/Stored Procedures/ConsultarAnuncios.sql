CREATE PROCEDURE [dbo].[ConsultarAnuncios]
	@IdUsuario VARCHAR(100) = NULL
AS
BEGIN
	SELECT [A].[IdAnuncio]
	  ,[A].[IdUsuario]
      ,[A].[AdmiteMascota]
      ,[A].[IdTipoPropiedad]
      ,[A].[Precio]
      ,[A].[MaximaCantidadPersonas]
      ,[A].[TieneSeguridadPrivada]
      ,[A].[Activo]
      ,[A].[FechaCreacion]
	FROM [dbo].[Anuncio] [A]
	INNER JOIN [dbo].[Usuario] [U] ON [U].[IdUsuario] = [A].[IdUsuario]
	WHERE (@IdUsuario IS NULL OR [U].[Identifier] = @IdUsuario);
END
