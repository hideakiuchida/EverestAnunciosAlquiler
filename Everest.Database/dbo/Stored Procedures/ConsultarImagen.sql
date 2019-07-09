CREATE PROCEDURE [dbo].[ConsultarImagen]
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
  WHERE [IdImagen] = @Id;
END
