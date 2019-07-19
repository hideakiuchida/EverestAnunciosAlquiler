CREATE PROCEDURE [dbo].[CrearPromocionAnuncio]
	@IdAnuncio int,
	@FechaCreacion datetime
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].[PromocionAnuncio] WHERE [IdAnuncio] = @IdAnuncio)
	BEGIN 
		SELECT 0;
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[PromocionAnuncio]
			   ([IdAnuncio]
			   ,[FechaCreacion])
		 VALUES
			   (@IdAnuncio
			   ,@FechaCreacion);
		SELECT SCOPE_IDENTITY();
	END
END
