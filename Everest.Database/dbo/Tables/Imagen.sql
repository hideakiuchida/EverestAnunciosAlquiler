CREATE TABLE [dbo].[Imagen]
(
	[IdImagen] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdAnuncio] INT NOT NULL,
	[Descripcion] VARCHAR(100) NULL,
	[ImagenUrl] VARCHAR(500) NULL,
	[IdPublico] VARCHAR(100) NULL,
	[FechaCreacion] DATETIME NOT NULL
	CONSTRAINT [FK_Imagen_Anuncio] FOREIGN KEY ([IdAnuncio]) REFERENCES [Anuncio]([IdAnuncio]) ON DELETE CASCADE
)
