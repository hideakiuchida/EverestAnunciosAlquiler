CREATE PROCEDURE [dbo].[ConsultarUbicacionPorAnuncio]
	@Id int
AS
BEGIN
	SELECT [IdUbicacion]
      ,[IdAnuncio]
      ,[Direccion]
      ,[Latitud]
      ,[Longitud]
	FROM [dbo].[Ubicacion]
	WHERE [IdAnuncio] = @Id;
END
