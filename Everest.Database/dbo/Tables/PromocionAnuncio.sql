CREATE TABLE [dbo].[PromocionAnuncio]
(
	[IdPromocionAnuncio] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[IdAnuncio] INT NOT NULL,
	[IdUsuario] INT NULL,
	[Agendado] BIT NULL,
	[FechaCreacion] DATETIME NOT NULL,
	CONSTRAINT [FK_PromocionAnuncio_Anuncio] FOREIGN KEY ([IdAnuncio]) REFERENCES [Anuncio]([IdAnuncio]) ON DELETE CASCADE,
	CONSTRAINT [FK_PromocionAnuncio_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([IdUsuario]) ON DELETE CASCADE
)
