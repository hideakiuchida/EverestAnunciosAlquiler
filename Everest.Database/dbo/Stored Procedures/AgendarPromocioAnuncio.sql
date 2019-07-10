CREATE PROCEDURE [dbo].[AgendarPromocioAnuncio]
	@IdAnuncio int,
	@IdUsuario int,
	@Agendado bit
AS
BEGIN
	DECLARE @Success BIT = 0;

	UPDATE [dbo].[PromocionAnuncio]
    SET [IdUsuario] = @IdUsuario
      ,[Agendado] = @Agendado
	WHERE [IdAnuncio] = @IdAnuncio;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
