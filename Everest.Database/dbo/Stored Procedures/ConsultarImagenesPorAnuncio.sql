CREATE PROCEDURE [dbo].[ConsultarImagenesPorAnuncio]
	@Id int
AS
BEGIN
	SELECT [IdImagen]
      ,[IdAnuncio]
      ,[Descripcion]
      ,[ImagenUrl]
      ,[IdPublico]
      ,[FechaCreacion]
  FROM [dbo].[Imagen]
  WHERE [IdAnuncio] = @Id;
END
