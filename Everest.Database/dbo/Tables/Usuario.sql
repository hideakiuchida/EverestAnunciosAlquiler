﻿CREATE TABLE [dbo].[Usuario]
(
	[IdUsuario]  INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Identifier] VARCHAR(100) NOT NULL,
	[Nombre] VARCHAR(100) NOT NULL,
	[Apellido] VARCHAR(100) NOT NULL,
	[Correo] VARCHAR(200) NOT NULL UNIQUE, 
	[IdRol] INT NOT NULL, 
    CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY ([IdRol]) REFERENCES [Rol]([IdRol])
)
