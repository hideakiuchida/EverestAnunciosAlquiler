CREATE PROCEDURE [dbo].[EliminarAnuncio]
	@Id int
AS
BEGIN
	DECLARE @Success BIT;
	DELETE FROM [dbo].[Anuncio] WHERE [IdAnuncio] = @Id;
	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
