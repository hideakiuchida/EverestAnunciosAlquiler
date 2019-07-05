CREATE PROCEDURE [dbo].[CrearImagen]
	@IdAnuncio int,
	@Descripcion varchar(100),
	@ImagenUrl varchar(500),
	@IdPublico varchar(100),
	@FechaCreacion datetime
AS
BEGIN
	INSERT INTO [dbo].[Imagen]
           ([IdAnuncio]
           ,[Descripcion]
           ,[ImagenUrl]
           ,[IdPublico]
           ,[FechaCreacion])
     VALUES
           (@IdAnuncio
           ,@Descripcion
           ,@ImagenUrl
           ,@IdPublico
           ,@FechaCreacion);
	SELECT SCOPE_IDENTITY();
END
