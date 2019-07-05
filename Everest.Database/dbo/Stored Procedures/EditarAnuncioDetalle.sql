CREATE PROCEDURE [dbo].[EditarAnuncioDetalle]
	@IdAnuncioDetalle int,
	@IdAnuncio int = NULL,
	@Metros2 decimal(10,8) = NULL,
	@CantidadHabitaciones int  = NULL,
	@CantidadBaños int  = NULL,
	@CantidadParqueos int  = NULL,
	@Plantas int  = NULL
AS
BEGIN
	DECLARE @Success BIT;

	UPDATE [dbo].[AnuncioDetalle]
	SET [IdAnuncio] = CASE WHEN @IdAnuncio IS NULL THEN [IdAnuncio] ELSE @IdAnuncio END
		,[Metros2] = CASE WHEN @Metros2 IS NULL THEN [Metros2] ELSE @Metros2 END
		,[CantidadHabitaciones] = CASE WHEN @CantidadHabitaciones IS NULL THEN [CantidadHabitaciones] ELSE @CantidadHabitaciones END
		,[CantidadBaños] = CASE WHEN @CantidadBaños IS NULL THEN [CantidadBaños] ELSE @CantidadBaños END
		,[CantidadParqueos] = CASE WHEN @CantidadParqueos IS NULL THEN [CantidadParqueos] ELSE @CantidadParqueos END
		,[Plantas] = CASE WHEN @Plantas IS NULL THEN [Plantas] ELSE @Plantas END
	WHERE [IdAnuncioDetalle] = @IdAnuncioDetalle;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END

