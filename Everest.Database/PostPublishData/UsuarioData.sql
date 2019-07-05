MERGE INTO [dbo].[Usuario] AS Target
USING (
		VALUES (1, N'Alonso', N'Uchida', 1), (2, N'Hideaki', N'Uchida', 2), (3, N'Javier', N'Nakasone', 3)
	) AS Source (IdUsuario, Nombre, Apellido, IdRol)
ON Target.[IdUsuario] = Source.[IdUsuario]
WHEN MATCHED THEN
UPDATE SET Nombre = Source.Nombre,
		Apellido = Source.Apellido,
		IdRol = Source.IdRol
WHEN NOT MATCHED BY TARGET THEN
 INSERT (Nombre, Apellido, IdRol)
 VALUES (Nombre, Apellido, IdRol);
