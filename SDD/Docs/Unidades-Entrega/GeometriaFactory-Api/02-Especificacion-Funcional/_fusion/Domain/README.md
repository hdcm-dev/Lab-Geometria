# 02 · Especificación funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.8
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md` §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los trece casos de uso](#2-los-trece-casos-de-uso)
- [3. Las dieciséis reglas de negocio](#3-las-dieciséis-reglas-de-negocio)
- [4. Orden de lectura sugerido](#4-orden-de-lectura-sugerido)
- [5. Artefactos omitidos y su motivo](#5-artefactos-omitidos-y-su-motivo)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogos, matriz NB → CU → RN → US, criterio de recorte, omisiones y puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Modelo-De-Dominio.md`](../../Definicion-Modelo-De-Dominio.md) | Documento de concepto central: cinco entidades, nueve invariantes vigentes, tres máquinas de estado y fronteras del dominio | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Trece casos de uso, uno por archivo | Propuesto |
| `Reglas-De-Negocio/` | Dieciséis reglas de negocio, una por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

Las carpetas `_legacy/` de `Casos-De-Uso/` y de `Reglas-De-Negocio/` conservan las copias de la emisión del 2026-08-08 con su sufijo de versión. No se editan.

## 2. Los trece casos de uso

Todos describen un **contrato de uso de la superficie pública**. El actor primario es siempre el proyecto de código que consume la biblioteca; el alumno y el administrador son sujetos de las reglas, no actores.

| CU | Título | NB que implementa |
| --- | --- | --- |
| CU-02001 | [Registrar el alta de un alumno](../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) | NB-00002, NB-00001 |
| CU-02002 | [Gobernar el ciclo de vida de la cuenta del alumno](../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) | NB-00001 |
| CU-02003 | [Fijar y reemplazar la credencial derivada](../../Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) | NB-00002 |
| CU-02004 | [Evaluar la admisibilidad de la cuenta](../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | NB-00001, NB-00002 |
| CU-02005 | [Crear y reeditar un trabajo](../../Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md) | NB-00003, NB-00004 |
| CU-02006 | [Reconstruir el conjunto de piezas del trabajo](../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) | NB-00004, NB-00006 |
| CU-02007 | [Registrar las observaciones del trabajo](../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) | NB-00005, NB-00004 |
| CU-02008 | [Gobernar el estado del trabajo en el envío](../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) | NB-00003, NB-00004, NB-00005 |
| CU-02009 | [Resolver el acceso de un alumno a un trabajo](../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) | NB-00003 |
| CU-02010 | [Resolver el desenlace del trabajo](../../Casos-De-Uso/CU-02010-Resolver-El-Desenlace-Del-Trabajo.md) | NB-00009, NB-00003 |
| CU-02011 | [Resolver el alcance del administrador sobre un trabajo](../../Casos-De-Uso/CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) | NB-00009, NB-00007 |
| CU-02012 | [Configurar la cuenta de administrador en el primer arranque](../../Casos-De-Uso/CU-02012-Configurar-La-Cuenta-De-Administrador.md) | NB-00001 |
| CU-02013 | [Resetear la contraseña de una cuenta de alumno](../../Casos-De-Uso/CU-02013-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) | NB-00001, NB-00002 |

## 3. Las dieciséis reglas de negocio

La serie es **contigua de RN-02001 a RN-02016**. La columna del invariante es la correspondencia que declara `PRODUCT-INTAKE` §17.1.P.2: los invariantes no son reglas distintas, son las mismas vistas desde el dominio.

| RN | Título | Invariante que la expresa |
| --- | --- | --- |
| RN-02001 | [Administrador único y papeles fijos](../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | INV-05 |
| RN-02002 | [El correo del alumno es único](../../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | INV-01 |
| RN-02003 | [Un alumno sólo ve y opera sus propios trabajos](../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | INV-02 |
| RN-02004 | [El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve](../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | INV-03 |
| RN-02005 | [Un trabajo no pasa a estado `Pendiente` con errores de validación](../../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | INV-04 |
| RN-02006 | [Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso](../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | INV-06 |
| RN-02007 | [La baja arrastra los trabajos y exige confirmación escrita](../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | — |
| RN-02008 | [El texto original del alumno se conserva íntegro](../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | — |
| RN-02009 | [Toda observación de error indica la posición de la pieza y el campo](../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | — |
| RN-02010 | [El desenlace es exclusivo del administrador y es terminal](../../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | INV-07 |
| RN-02011 | [El administrador no ve los trabajos en borrador](../../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | — |
| RN-02012 | [El reseteo de contraseña conserva la cuenta y sus trabajos](../../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | INV-09 |
| RN-02013 | [Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte](../../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | INV-09 |
| RN-02014 | [La contraseña provisoria la produce el sistema, no la escribe el administrador](../../Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | — |
| RN-02015 | [Resetear no exige que la cuenta esté habilitada](../../Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | — |
| RN-02016 | [Habilitar una cuenta produce su contraseña provisoria](../../Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | INV-09 |

Las seis filas con guion —sobre dieciséis— lo están por un motivo declarado en `PRODUCT-INTAKE` §17.1.P.2: RN-02007, RN-02008, RN-02009, **RN-02014** y **RN-02015** describen comportamientos —o, en el caso de RN-02015, la ausencia de una precondición— y no condiciones permanentes, y RN-02011 es una regla de alcance de consulta. **RN-02012, RN-02013 y RN-02016 comparten INV-09**: las dos primeras son las dos mitades de la misma condición, y **RN-02016 agrega un segundo origen** de la marca que las dos gobiernan —la habilitación, junto al reseteo—. Esa lectura la sostiene la **columna «regla de negocio que sostiene» de la fila INV-09** de esa sección del intake, que dice «RN-02012, RN-02013», y **no su prosa**, que enumera a RN-02012 entre las reglas sin invariante. La ambigüedad es del intake, está declarada en `Especificacion-Funcional.md` §8 y su fundamento está en `Definicion-Modelo-De-Dominio.md` §4.3.

**Dos nombres de archivo conservan un slug que ya no describe del todo su enunciado** —`RN-02004-Eliminacion-Acotada-Al-Borrador.md` y `RN-02005-Finalizacion-Sin-Errores-De-Validacion.md`—, porque otras categorías ya los citan por esa ruta. La decisión está declarada en `Especificacion-Funcional.md` §8.

## 4. Orden de lectura sugerido

1. **`Especificacion-Funcional.md`** — primero siempre: da el alcance, la matriz y el criterio de recorte.
2. **`Definicion-Modelo-De-Dominio.md`** — las entidades, los nueve invariantes vigentes y las tres máquinas de estado, incluida la de la marca de cambio de contraseña pendiente en §5.3. Los trece casos de uso se leen sobre él, y en particular §5.2, que es donde vive el ciclo de vida completo del trabajo.
3. **CU-02012, CU-02001 a CU-02004 y CU-02013** — el ciclo de vida de la cuenta: primero la configuración del administrador, que es la que arranca la instancia, y después el auto-registro del alumno, sus transiciones, su credencial y su admisibilidad. **Son dos caminos de alta con estado inicial distinto** y conviene leerlos en ese orden. **CU-02013 va último de este bloque**: el reseteo se entiende sobre la credencial de CU-02003 y sobre la admisibilidad de CU-02004, que es donde se ejerce la guarda que pone.
4. **CU-02005 a CU-02008** — el ciclo de vida del trabajo hasta el envío: constitución, interpretación, observaciones y estado.
5. **CU-02010** — el desenlace, que cierra el recorrido del trabajo.
6. **CU-02009 y CU-02011** — el par simétrico de alcance: qué puede el alumno sobre un trabajo y qué puede el administrador.
7. **`Reglas-De-Negocio/`** — se leen sueltas, en cualquier orden: son invariantes atemporales y cada una declara los casos de uso que alcanza.
8. **`Glosario-Funcional.md`** — conviene tenerlo a mano desde el principio si el lector viene de otra categoría, sobre todo por la forma calificada obligatoria de `Pendiente`.

## 5. Artefactos omitidos y su motivo

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Modelo-Datos/Modelo-Conceptual.md` | La regla de la categoría lo omite para el tipo `library` y el flag `tiene_persistencia` de este proyecto de código es false. El intake declara «no aplica» en §17.1.P.4: el dominio no conoce el motor de persistencia, que materializa `GeometriaFactory-Infrastructure`. El concepto central se documenta en `Definicion-Modelo-De-Dominio.md`, que **no** es un modelo de persistencia |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | La regla las omite para `library` y dependen del modelo conceptual, que está omitido. Las restricciones de integridad del dominio están declaradas como los nueve invariantes y como las **dieciséis** reglas de negocio |

## 6. Notas de uso de esta sección

- **Autoridad.** Nada se origina acá. Toda regla, todo invariante y todo valor numérico traza a su sección del intake o de las categorías 00 y 01. Lo que el intake no declara, no se inventa: los puntos abiertos están listados en `Especificacion-Funcional.md` §9.
- **Ubicación de responsabilidades.** Un enunciado de esta categoría que mencione persistencia, protocolo de transporte, serialización, emisión de acceso o ejecución de consultas está mal ubicado: esas responsabilidades pertenecen a otros proyectos de código y su tabla está en `Definicion-Modelo-De-Dominio.md` §7.
- **Decisiones de otras categorías.** Los nombres de tipos y de espacios de nombres son de 05 y de la codificación; el backlog es de 06; las pruebas, de 08. Esta categoría las referencia y no las toma.
- **Vocabulario.** Los términos del dominio no se redefinen: `Vision-Producto.md` §9 es el glosario raíz, y de ahí vienen los cuatro estados del trabajo, «enviar», «aprobar / rechazar» y «comentario». La palabra «proyecto» a secas no se usa, y `Pendiente` va siempre calificado.
- **Nombres de archivo.** Ningún archivo vivo lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera, y el sufijo queda reservado a las copias de `_legacy/`.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera los nueve casos de uso con la necesidad de negocio que implementan, las siete reglas de negocio con su invariante, el orden de lectura, y registra la omisión del modelo conceptual y de las reglas conceptuales con su motivo. |
| 1.1 | 2026-08-09 | Absorbe el circuito de revisión de `PRODUCT-INTAKE` 1.3 y la resolución de las dos ambigüedades que esta categoría había elevado. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Los casos de uso pasan de nueve a **once** y las reglas de siete a **once contiguas**, con su invariante y con las cuatro que no tienen ninguno. **Corrige la atribución de INV-04**, que la versión anterior daba como el invariante de RN-02008. El orden de lectura incorpora el desenlace y el par simétrico de alcance; §1 registra las carpetas `_legacy/`; §3 declara los dos nombres de archivo que se conservan por estabilidad de citación; y §6 remite al glosario raíz por los términos nuevos y por la forma calificada de `Pendiente`. |
| 1.2 | 2026-08-09 | **Corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`: se emite **CU-02012**, el caso de uso de la capacidad F-01 que faltaba, y el catálogo pasa a **doce**. La cuenta del administrador nace `Habilitado` y con credencial, y la del alumno sigue naciendo `Pendiente`: son dos caminos de alta y la versión anterior los resolvía con un solo estado inicial, con lo que la instancia quedaba inutilizable en el primer arranque. §2 suma CU-02012, §3 mantiene las once reglas con RN-02001 y RN-02002 reasignadas, y §4 reordena el recorrido de lectura del ciclo de vida de la cuenta. |
| 1.3 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-07**: §4 conservaba un «once casos de uso» en prosa viva, que la emisión de CU-02012 dejó desactualizado. |
| 1.4 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**, que incorpora la capacidad **F-26** —reseteo de contraseña por el administrador—, las reglas **RN-02012** y **RN-02013** y el invariante **INV-09**. §2 pasa de doce a **trece casos de uso** con **CU-02013**; §3 pasa de once a **trece reglas contiguas**, con las dos nuevas compartiendo INV-09; §1, §4 y §5 actualizan los recuentos de invariantes —de siete a **nueve vigentes**, con INV-08 ya adoptado por el intake— y de máquinas de estado, que pasan a **tres** con la de la marca de cambio de contraseña pendiente. §4 ubica CU-02013 al final del bloque del ciclo de vida de la cuenta, con su motivo. |
| 1.5 | 2026-08-09 | **Cierra el hallazgo `F26-09`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **§3** afirmaba que el intake §17.1.P.2 declara que RN-02012 y RN-02013 comparten INV-09, cuando la prosa de esa sección enumera a RN-02012 entre las reglas **sin** invariante asociado; lo que sostiene la lectura es la columna «regla de negocio que sostiene» de la fila INV-09. Se corrige la atribución, se declara que la ambigüedad es del intake y se remite a `Especificacion-Funcional.md` §8 y a `Definicion-Modelo-De-Dominio.md` §4.3, que ya la calificaba correctamente. Ningún documento de la sección, ninguna regla y ningún invariante cambia. |
| 1.6 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10**, que incorpora a §4.1 las reglas **RN-02014** —la contraseña provisoria la produce el sistema, no es adivinable y no se repite— y **RN-02015** —resetear no exige cuenta habilitada—, con lo que el catálogo del producto pasa de trece a **quince reglas contiguas**. **§1** y **§3** suman las dos filas nuevas, las dos **sin invariante asociado** por la prosa de `PRODUCT-INTAKE` §17.1.P.2, y la nota de §3 pasa de cuatro a **seis** filas con guion con el motivo de cada una. **§5** actualiza el recuento de las restricciones de integridad. **Los trece casos de uso no cambian**: las dos reglas nuevas no abren contrato de uso, porque ya estaban modeladas dentro de `CU-02013`. Cierra además el hallazgo `F26-28` del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 en su parte de este archivo: **las filas de este control de cambios estaban fuera de orden cronológico** y se reordenan por versión, **sin tocar el texto de ninguna**. |
| 1.7 | 2026-08-10 | Absorbe el `PRODUCT-INTAKE` **1.13**, que incorpora a §4.1 la regla **RN-02016** —habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo, y la deja con cambio de contraseña pendiente— y precisa la capacidad **F-04**. **§3** pasa de quince a **dieciséis reglas contiguas**, con RN-02016 asociada a **INV-09**, y la nota del invariante compartido pasa de dos reglas a **tres**, con el fundamento de que RN-02016 aporta un segundo origen de la marca y no una mitad nueva de la condición. **§5** actualiza el recuento de las restricciones de integridad a dieciséis. **Los trece casos de uso no cambian de número**: RN-02016 no abre contrato de uso, se materializa en `CU-02002` y retira una condición de `CU-02004` y otra de `CU-02013`. Sube minor. |
| 1.8 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** La fila de `Reglas-De-Negocio/` en el inventario de **§1** declaraba «**Quince** reglas de negocio, una por archivo» sobre un directorio que tiene **dieciséis** archivos, `RN-02001` a `RN-02016`, serie contigua y sin huecos —contados uno por uno—: es la fila que la versión 1.7 no actualizó cuando llevó §3 y §5 a dieciséis. Pasa a **dieciséis**. Las otras dos filas de recuento de ese mismo inventario se recontaron sobre los archivos y **cierran**: trece casos de uso y nueve invariantes vigentes. **Ningún caso de uso, ninguna regla y ningún documento de la sección cambia.** Sube minor. |
