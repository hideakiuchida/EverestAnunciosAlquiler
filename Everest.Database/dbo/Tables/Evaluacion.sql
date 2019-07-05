CREATE TABLE [dbo].[Evaluacion]
(
	[IdEvaluacion] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdAnuncio] INT NOT NULL,
	[Comentario] TEXT NULL,
	[Calificacion] INT NULL,
	[FechaCreacion] DATETIME NOT NULL
	CONSTRAINT [FK_Evaluacion_Anuncio] FOREIGN KEY ([IdAnuncio]) REFERENCES [Anuncio]([IdAnuncio]) ON DELETE CASCADE
)
