CREATE PROCEDURE [dbo].[ConsultarEvaluacionesPorAnuncio]
	@Id int
AS
BEGIN
	SELECT [IdEvaluacion]
      ,[IdAnuncio]
      ,[Comentario]
      ,[Calificacion]
      ,[FechaCreacion]
  FROM [dbo].[Evaluacion]
  WHERE [IdAnuncio] = @Id;
END
