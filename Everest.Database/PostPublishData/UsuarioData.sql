MERGE INTO [dbo].[Usuario] AS Target
USING (
		VALUES (1, CAST(NEWID() AS VARCHAR(100)), N'Alonso', N'Uchida', N'alonso.uchida@gmail.com', 1), 
		(2, CAST(NEWID() AS VARCHAR(100)), N'Hideaki', N'Uchida', N'hideaki.uchida@gmail.com', 2), 
		(3, CAST(NEWID() AS VARCHAR(100)), N'Javier', N'Nakasone', N'javier.uchida@gmail.com',3)
	) AS Source (IdUsuario, Identifier, Nombre, Apellido, Correo, IdRol)
ON Target.[IdUsuario] = Source.[IdUsuario]
WHEN MATCHED THEN
UPDATE SET Nombre = Source.Nombre,
		Apellido = Source.Apellido,
		IdRol = Source.IdRol
WHEN NOT MATCHED BY TARGET THEN
 INSERT (Nombre, Identifier, Apellido, Correo, IdRol)
 VALUES (Nombre, Identifier, Apellido, Correo, IdRol);
