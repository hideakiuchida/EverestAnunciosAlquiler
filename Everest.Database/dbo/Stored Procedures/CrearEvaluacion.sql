CREATE PROCEDURE [dbo].[CrearEvaluacion]
	@IdAnuncio int,
	@Comentario text,
	@Calificacion int,
	@FechaCreacion datetime
AS
BEGIN
	INSERT INTO [dbo].[Evaluacion]
           ([IdAnuncio]
           ,[Comentario]
           ,[Calificacion]
           ,[FechaCreacion])
     VALUES
           (@IdAnuncio
           ,@Comentario
           ,@Calificacion
           ,@FechaCreacion);
	SELECT SCOPE_IDENTITY();
END
