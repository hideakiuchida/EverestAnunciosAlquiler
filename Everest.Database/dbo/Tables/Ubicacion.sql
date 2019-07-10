CREATE TABLE [dbo].[Ubicacion]
(
	[IdUbicacion] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdAnuncio] INT NOT NULL,
	[Direccion] VARCHAR(500) NOT NULL,
	[Latitud] DECIMAL(10,6) NULL,
	[Longitud] DECIMAL(10,6) NULL, 
    CONSTRAINT [FK_Ubicacion_Anuncio] FOREIGN KEY ([IdAnuncio]) REFERENCES [Anuncio]([IdAnuncio]) ON DELETE CASCADE
)
