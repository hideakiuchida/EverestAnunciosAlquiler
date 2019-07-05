CREATE PROCEDURE [dbo].[ConsultarAnuncioDetallePorAnuncio]
	@Id int
AS
BEGIN
	SELECT [IdAnuncioDetalle]
      ,[IdAnuncio]
      ,[Metros2]
      ,[CantidadHabitaciones]
      ,[CantidadBaños]
      ,[CantidadParqueos]
      ,[Plantas]
	FROM [dbo].[AnuncioDetalle]
	WHERE [IdAnuncio] = @Id;
END
