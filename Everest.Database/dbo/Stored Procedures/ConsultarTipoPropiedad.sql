CREATE PROCEDURE [dbo].[ConsultarTipoPropiedad]
	@Id int
AS
BEGIN
	SELECT [IdTipoPropiedad]
			,[Nombre]
	FROM [dbo].[TipoPropiedad]
	WHERE [IdTipoPropiedad] = @Id;
END

