CREATE PROCEDURE [dbo].[EliminarImagen]
	@Id int
AS
BEGIN
	DECLARE @Success BIT;
	DELETE FROM [dbo].[Imagen] WHERE [IdImagen] = @Id;
	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
