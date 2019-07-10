CREATE PROCEDURE [dbo].[CrearUbicacion]
	@IdAnuncio int,
	@Direccion varchar(500),
	@Latitud decimal(10,6),
	@Longitud decimal(10,6)
AS
BEGIN
	INSERT INTO [dbo].[Ubicacion]
           ([IdAnuncio]
           ,[Direccion]
           ,[Latitud]
           ,[Longitud])
     VALUES
           (@IdAnuncio
           ,@Direccion
           ,@Latitud
           ,@Longitud);
	SELECT SCOPE_IDENTITY();
END
