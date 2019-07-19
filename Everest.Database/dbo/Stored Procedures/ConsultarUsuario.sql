CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@Identifier VARCHAR
AS
BEGIN
	SELECT [IdUsuario]
		, [Identifier]
      ,[Nombre]
      ,[Apellido]
      ,[IdRol]
    FROM [dbo].[Usuario]
	WHERE [Identifier] = @Identifier;
END

