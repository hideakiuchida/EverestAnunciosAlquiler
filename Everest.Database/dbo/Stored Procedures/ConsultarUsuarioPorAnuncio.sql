CREATE PROCEDURE [dbo].[ConsultarUsuarioPorAnuncio]
	@Id int
AS
BEGIN
	SELECT [U].[IdUsuario]
      ,[U].[Nombre]
      ,[U].[Apellido]
      ,[U].[IdRol]
    FROM [dbo].[Usuario] [U] 
	INNER JOIN [dbo].[Anuncio] [A] ON [U].[IdUsuario] = [A].[IdUsuario]
	WHERE [A].[IdAnuncio] = @Id;
END

