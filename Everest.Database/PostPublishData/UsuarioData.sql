MERGE INTO [dbo].[Usuario] AS Target
USING (
		VALUES (1, N'Alonso', N'Uchida', N'alonso.uchida@gmail.com', 1), (2, N'Hideaki', N'Uchida', N'hideaki.uchida@gmail.com', 2), (3, N'Javier', N'Nakasone', N'javier.uchida@gmail.com',3)
	) AS Source (IdUsuario, Nombre, Apellido, Correo, IdRol)
ON Target.[IdUsuario] = Source.[IdUsuario]
WHEN MATCHED THEN
UPDATE SET Nombre = Source.Nombre,
		Apellido = Source.Apellido,
		IdRol = Source.IdRol
WHEN NOT MATCHED BY TARGET THEN
 INSERT (Nombre, Apellido, Correo, IdRol)
 VALUES (Nombre, Apellido, Correo, IdRol);
