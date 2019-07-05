CREATE PROCEDURE [dbo].[ConsultarAnuncio]
	@Id int
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
	WHERE [IdAnuncio] = @Id;
END
