MERGE INTO [dbo].[Rol] AS Target
USING (
		VALUES (1, N'Propietario'), (2, N'Inquilino'), (3, N'Administrador')
	) AS Source (IdRol, Descripcion)
ON Target.[IdRol] = Source.IdRol
WHEN MATCHED THEN
 UPDATE SET Descripcion = Source.Descripcion
WHEN NOT MATCHED BY TARGET THEN
 INSERT (IdRol, Descripcion)
 VALUES (IdRol, Descripcion);