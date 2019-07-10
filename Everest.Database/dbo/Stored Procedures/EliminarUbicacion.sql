﻿CREATE PROCEDURE [dbo].[EliminarUbicacion]
	@IdUbicacion int
AS
BEGIN
	DECLARE @Success BIT = 0;

	DELETE FROM [dbo].[Ubicacion]
	WHERE [IdUbicacion] = @IdUbicacion;

	IF(@@ROWCOUNT > 0)
		SET @Success = 1;

	SELECT @Success;
END
