# Casos de uso absorbidos por la consolidación 8.5

**Fecha:** 2026-08-16
**Motivo:** `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2
**Regla:** `Migracion-Rules.md` §4.3.2 paso 3

Los **32** documentos de esta carpeta **no se borraron**: describían la misma capacidad desde su capa,
y en el modelo de unidad de entrega las capas son internas, de modo que **un caso de uso por capa es
una vista y no un caso de uso**. Su contenido está en el caso de uso consolidado que los reemplaza,
que se cita acá y que también los cita desde su cabecera.

Ruta de los reemplazos:
`../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/`

## Reemplazos

| Caso de uso consolidado | Documentos que absorbe |
| --- | --- |
| [`CU-00021` — Dar de alta una cuenta de alumno](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | `CU-00003` **(A-02)**, `CU-04001`, `CU-02001` |
| [`CU-00022` — Ingresar al laboratorio y sostener la sesión](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | `CU-00003` **(A-05)**, `CU-00001`, `CU-00002`, `CU-04003`, `CU-02003`, `CU-02004` |
| [`CU-00023` — Gobernar las cuentas de la comisión](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | `CU-00004`, `CU-04002`, `CU-02002` |
| [`CU-00024` — Resetear la contraseña de un alumno](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | `CU-00005`, `CU-04011`, `CU-02013` |
| [`CU-00025` — Configurar la cuenta de administrador](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | `CU-00003` **(A-03 y A-17)**, `CU-04010`, `CU-02012` |
| [`CU-00026` — Enviar un trabajo y ver sus observaciones](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | `CU-00006` **(A-10 y A-11)**, `CU-04004`, `CU-04005`, `CU-02005`, `CU-02006`, `CU-02007`, `CU-02008` |
| [`CU-00027` — Eliminar un trabajo](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | `CU-00006` **(A-12)**, `CU-04009` |
| [`CU-00028` — Consultar el listado y el detalle de los trabajos](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | `CU-00007`, `CU-04006`, `CU-04007`, `CU-02009`, `CU-02011` |
| [`CU-00029` — Dar desenlace a la revisión](../../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | `CU-00008`, `CU-04008`, `CU-02010` |

**Dos documentos se reparten en más de un reemplazo, y no es una excepción sino la regla del
reparto.** `CU-00003` cubre cuatro puntos de acceso que pertenecen a tres capacidades, y `CU-00006`
tres puntos que pertenecen a dos: los documentos de la capa `Api` **agrupaban por perfil de
autenticación y por recurso, no por capacidad**. El reparto es de **punto de acceso a capacidad**, y
está justificado en §2.1.1 y §2.1.2 del documento de consolidación.

## Por qué los identificadores nuevos empiezan en `CU-00021`

**Los identificadores absorbidos no se reciclan.** `CU-00003` no significa hoy lo que significaba
ayer, y reutilizarlo para un documento de alcance distinto haría que una cita vieja resolviera en
silencio a otra cosa. Es el mismo criterio con el que el producto retiró `A-04`,
`CONTRATO_CONTRASENA_NO_ESTABLECIDA` y `CREDENCIAL_NO_ESTABLECIDA`, y que `Root-Rules.md` §9 declara
para todo el sistema de identificadores.

## Qué no está acá

Los **dieciséis** documentos de la salida 3 —operaciones internas que ninguna persona ejecuta— no se
archivaron: **se reubicaron**, a `05-Arquitectura-Tecnica`, a `09-Devops`, a `10-Examples` y a
`Producto/Contratos-Inter-Unidad/`, cada uno a la categoría que le corresponde. El detalle está en
§2.2 del documento de consolidación.
