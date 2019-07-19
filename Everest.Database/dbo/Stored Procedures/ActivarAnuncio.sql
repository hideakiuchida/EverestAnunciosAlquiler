CREATE PROCEDURE [dbo].[ActivarAnuncio]
	@IdAnuncio int,
	@Activo bit
AS
BEGIN
	DECLARE @Success BIT = 0;

	UPDATE [dbo].[Anuncio]
	SET [Activo] = @Activo
	WHERE [IdAnuncio] = @IdAnuncio;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
