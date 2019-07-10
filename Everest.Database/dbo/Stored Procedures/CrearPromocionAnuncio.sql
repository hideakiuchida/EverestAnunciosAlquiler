CREATE PROCEDURE [dbo].[CrearPromocionAnuncio]
	@IdAnuncio int,
	@FechaCreacion datetime
AS
BEGIN
	INSERT INTO [dbo].[PromocionAnuncio]
           ([IdAnuncio]
           ,[FechaCreacion])
     VALUES
           (@IdAnuncio
           ,@FechaCreacion);
	SELECT SCOPE_IDENTITY();
END
