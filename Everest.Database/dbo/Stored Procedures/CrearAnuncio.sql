CREATE PROCEDURE [dbo].[CrearAnuncio]
	@IdUsuario int,
	@AdmiteMascota bit,
	@IdTipoPropiedad int,
	@Precio int,
	@MaximaCantidadPersonas int,
	@TieneSeguridadPrivada int,
	@Activo bit,
	@FechaCreacion datetime
AS
BEGIN
	INSERT INTO [dbo].[Anuncio]
           ([IdUsuario]
           ,[AdmiteMascota]
           ,[IdTipoPropiedad]
           ,[Precio]
           ,[MaximaCantidadPersonas]
           ,[TieneSeguridadPrivada]
           ,[Activo]
           ,[FechaCreacion])
     VALUES
           (@IdUsuario
           ,@AdmiteMascota
           ,@IdTipoPropiedad
           ,@Precio
           ,@MaximaCantidadPersonas
           ,@TieneSeguridadPrivada
           ,@Activo
           ,@FechaCreacion);
	SELECT SCOPE_IDENTITY();
END