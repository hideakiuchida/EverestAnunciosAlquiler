MERGE INTO [dbo].[TipoPropiedad] AS Target
USING (
		VALUES (1, N'Habitación'), (2, N'Casa'), (3, N'Casa en Condominio'), (4, N'Casa en Residencial'), (5, N'Apartamento Vertical'), (6, N'Apartamento Horizontal')
	) AS Source (IdTipoPropiedad, Nombre)
ON Target.[IdTipoPropiedad] = Source.[IdTipoPropiedad]
WHEN MATCHED THEN
 UPDATE SET Nombre = Source.Nombre
WHEN NOT MATCHED BY TARGET THEN
 INSERT (IdTipoPropiedad, Nombre)
 VALUES (IdTipoPropiedad, Nombre);


