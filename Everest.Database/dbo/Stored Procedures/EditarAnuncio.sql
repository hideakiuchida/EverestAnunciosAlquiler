CREATE PROCEDURE [dbo].[EditarAnuncio]
	@IdAnuncio int,
	@IdUsuario int = NULL,
	@AdmiteMascota bit = NULL,
	@IdTipoPropiedad int = NULL,
	@Precio int = NULL,
	@MaximaCantidadPersonas int = NULL,
	@TieneSeguridadPrivada int = NULL,
	@Activo bit = NULL
AS
BEGIN
	DECLARE @Success BIT;

	UPDATE [dbo].[Anuncio]
	SET [IdUsuario] = CASE WHEN @IdUsuario IS NULL THEN [IdUsuario] ELSE @IdUsuario END
		  ,[AdmiteMascota] = CASE WHEN @AdmiteMascota IS NULL THEN [AdmiteMascota] ELSE @AdmiteMascota END
		  ,[IdTipoPropiedad] = CASE WHEN @IdTipoPropiedad IS NULL THEN [IdTipoPropiedad] ELSE @IdTipoPropiedad END
		  ,[Precio] = CASE WHEN @Precio IS NULL THEN [Precio] ELSE @Precio END
		  ,[MaximaCantidadPersonas] = CASE WHEN @MaximaCantidadPersonas IS NULL THEN [MaximaCantidadPersonas] ELSE @MaximaCantidadPersonas END
		  ,[TieneSeguridadPrivada] = CASE WHEN @TieneSeguridadPrivada IS NULL THEN [TieneSeguridadPrivada] ELSE @TieneSeguridadPrivada END
		  ,[Activo] = CASE WHEN @Activo IS NULL THEN [Activo] ELSE @Activo END
	WHERE [IdAnuncio] = @IdAnuncio;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
