CREATE TABLE [dbo].[AnuncioDetalle]
(
	[IdAnuncioDetalle] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdAnuncio] INT NOT NULL,
	[Metros2] DECIMAL(10,8) NULL,
	[CantidadHabitaciones] INT NULL,
	[CantidadBaños] INT NULL,
	[CantidadParqueos] INT NULL,
	[Plantas] INT NULL,
    CONSTRAINT [FK_AnuncioDetalle_Anuncio] FOREIGN KEY ([IdAnuncio]) REFERENCES [Anuncio]([IdAnuncio]) ON DELETE CASCADE
)
