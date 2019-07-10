CREATE PROCEDURE [dbo].[EditarUbicacion]
	@IdUbicacion int,
	@IdAnuncio int = NULL,
	@Direccion varchar(500) = NULL,
	@Latitud decimal(10,6) = NULL,
	@Longitud decimal(10,6) = NULL
AS
BEGIN
	DECLARE @Success BIT = 0;

	UPDATE [dbo].[Ubicacion]
	SET [IdAnuncio] = CASE WHEN @IdAnuncio IS NULL THEN [IdAnuncio] ELSE @IdAnuncio END
		,[Direccion] = CASE WHEN @Direccion IS NULL THEN [Direccion] ELSE @Direccion END
		,[Latitud] = CASE WHEN @Latitud IS NULL THEN [Latitud] ELSE @Latitud END
		,[Longitud] = CASE WHEN @Longitud IS NULL THEN [Longitud] ELSE @Longitud END
	WHERE [IdUbicacion] = @IdUbicacion;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END

