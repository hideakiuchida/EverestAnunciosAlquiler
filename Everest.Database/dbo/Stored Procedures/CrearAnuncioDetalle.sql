CREATE PROCEDURE [dbo].[CrearAnuncioDetalle]
	@IdAnuncio int,
	@Metros2 decimal(10,8) = NULL,
	@CantidadHabitaciones int  = NULL,
	@CantidadBaños int  = NULL,
	@CantidadParqueos int,
	@Plantas int
AS
BEGIN
	INSERT INTO [dbo].[AnuncioDetalle]
           ([IdAnuncio]
           ,[Metros2]
           ,[CantidadHabitaciones]
           ,[CantidadBaños]
           ,[CantidadParqueos]
           ,[Plantas])
     VALUES
           (@IdAnuncio
           ,@Metros2
           ,@CantidadHabitaciones
           ,@CantidadBaños
           ,@CantidadParqueos
           ,@Plantas)
	SELECT SCOPE_IDENTITY();
END

