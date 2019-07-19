﻿CREATE PROCEDURE [dbo].[ConsultarPromocion]
AS
BEGIN
	SELECT TOP 1 [IdPromocionAnuncio]
      ,[IdAnuncio]
      ,[IdUsuario]
      ,[Agendado]
      ,[FechaCreacion]
	FROM [dbo].[PromocionAnuncio]
	WHERE [Agendado] IS NULL
	ORDER BY [FechaCreacion] DESC;
END