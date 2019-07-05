CREATE TABLE [dbo].[Anuncio]
(
	[IdAnuncio] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdUsuario] INT NOT NULL,
	[AdmiteMascota] BIT NOT NULL,
	[IdTipoPropiedad] INT NOT NULL,
	[Precio] DECIMAL(10,2) NOT NULL,
	[MaximaCantidadPersonas] INT NOT NULL,
	[TieneSeguridadPrivada] BIT NOT NULL,
	[Activo] BIT NOT NULL,
	[FechaCreacion] DATETIME NOT NULL, 
    CONSTRAINT [FK_Anuncio_TipoPropiedad] FOREIGN KEY ([IdTipoPropiedad]) REFERENCES [TipoPropiedad]([IdTipoPropiedad]),
	CONSTRAINT [FK_Anuncio_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([IdUsuario])
)
