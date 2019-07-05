CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@Id int
AS
BEGIN
	SELECT [IdUsuario]
      ,[Nombre]
      ,[Apellido]
      ,[IdRol]
    FROM [dbo].[Usuario]
	WHERE [IdUsuario] = @Id;
END

