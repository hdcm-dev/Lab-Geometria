# 02 · Especificación Funcional — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §3 (catálogo), §4 (matriz), §6 (restricciones transversales) y §9 (omisiones); `../../../00-Contexto/Vision-Producto.md` §3 y §9; `../../../00-Contexto/Alcance-Producto.md` §4.1 y §5; `../../../01-Necesidades-Negocio/Necesidades-Negocio.md` §2 y §5.3; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §13, §14 (RA-01 a RA-03) y §17.6
**Trazabilidad downstream:** `../03-UX-UI-DX/` de este proyecto de código, que es el downstream más directo; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Punto de entrada](#1-punto-de-entrada)
- [2. Documentos vigentes](#2-documentos-vigentes)
- [3. Artefactos omitidos, con su motivo](#3-artefactos-omitidos-con-su-motivo)
- [4. Cómo leer esta sección](#4-cómo-leer-esta-sección)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Punto de entrada

Esta sección especifica **qué hace la persona** en la pieza pública del producto Fábrica de Geometría, y **qué ve**. El punto de entrada es [`Especificacion-Funcional.md`](Especificacion-Funcional.md), con el catálogo de los diez casos de uso, la matriz NB→CU→RN→US y las **trece** restricciones transversales.

`GeometriaFactory-Web` es de tipo `web-monolith`, nivel 1 del orden topológico, y es **el único punto de contacto del navegador**. Es el primer proyecto de código del producto cuyos casos de uso tienen actores humanos: el alumno y el docente en su papel de administrador.

## 2. Documentos vigentes

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro, catálogo, criterio de recorte, matriz NB→CU→RN→US, cobertura inversa, restricciones transversales y consumo del contrato de fachada | Propuesto |
| [`Casos-De-Uso/CU-10001-Registrar-La-Cuenta-De-Alumno.md`](Casos-De-Uso/CU-10001-Registrar-La-Cuenta-De-Alumno.md) | El alumno se da de alta sin elegir contraseña y sin que se envíe correo | Propuesto |
| [`Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md`](Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) | Sesión con la credencial custodiada del lado del servidor y rutas protegidas por papel | Propuesto |
| [`Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md`](Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) | Primer ingreso y cambio de contraseña, **los dos exigiendo la vigente**: en el primer ingreso la vigente es la provisoria que produjo la habilitación | Propuesto |
| [`Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md`](Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md) | Las **cinco** operaciones sobre una cuenta —incluido el **reseteo de contraseña**—, con la confirmación escrita de la baja | Propuesto |
| [`Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md`](Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) | La acción única de guardado y el estado que la interpretación decide | Propuesto |
| [`Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md`](Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) | Los cuatro estados a la vista y las acciones acotadas al borrador | Propuesto |
| [`Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md`](Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) | La vista de trabajo con sus cuatro partes y el ciclo de vida completo de la fachada | Propuesto |
| [`Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md`](Casos-De-Uso/CU-10008-Recorrer-La-Entrega-De-La-Comision.md) | El listado del administrador, agrupado y filtrado, sin los borradores | Propuesto |
| [`Casos-De-Uso/CU-10009-Resolver-Un-Trabajo-Con-Comentario-Opcional.md`](Casos-De-Uso/CU-10009-Resolver-Un-Trabajo-Con-Comentario-Opcional.md) | Aprobar o rechazar con comentario opcional, y retirar cualquier trabajo que ve | Propuesto |
| [`Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md`](Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md) | Caso de uso transversal: los dos tramos que se cortan y cómo se avisa cada uno | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario acuñado, términos con más de un referente y términos referenciados | Propuesto |

Un solo archivo por nombre lógico y ninguna versión superada: la carpeta `Casos-De-Uso/_legacy/` no existe todavía y no corresponde crearla.

## 3. Artefactos omitidos, con su motivo

La tabla maestra de `Rules-Especificacion-Funcional.md` §2.1 tiene ocho filas. Se emiten cuatro artefactos y **no se emiten cuatro**, agrupados en tres filas porque el modelo conceptual y sus reglas conceptuales se omiten por el mismo motivo.

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Las dieciséis reglas viven en `GeometriaFactory-Domain`** —RN-10012 y RN-10013 entraron con el `PRODUCT-INTAKE` 1.7, RN-10014 y RN-10015 con el 1.10 y **RN-10016** con el 1.13, y las cinco **ya tienen archivo allá**, de modo que se enlazan como las otras once—, que es donde se hacen cumplir. Acá se **referencian** por identificador, con enlace relativo, en la fila «Reglas de negocio aplicables» del §9 de cada caso de uso y en la tercera columna de la matriz. El motivo no es formal: **la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable.** Ocultar un botón o no armar una ruta acota lo que se ofrece y no hace cumplir nada; por eso tres criterios de aceptación de esta sección verifican la acotación forzando la solicitud sin pasar por la pantalla. Redactar acá una `RN-XX` habría producido una segunda copia de una invariante que ya tiene dueño, con el riesgo de que las dos divergieran |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md`, que son **dos** de las ocho filas | **Omitidos como decisión técnica declarada, no por no-aplicabilidad.** `Rules-Especificacion-Funcional.md` §2.1 y §2.2 los marcan **obligatorios** para `web-monolith`, y aun así no se emiten: el flag `tiene_persistencia` de este proyecto de código es **false y eso es deliberado**. `PRODUCT-INTAKE` §17.6 P.4 lo declara: «el front no guarda estado propio: es exactamente el problema que la topología evita». Lo único que vive del lado de esta pieza es el estado del circuito, en memoria del servidor del hosting, donde reside la credencial de sesión; no es persistencia y no tiene entidades que modelar. La decisión es de las que dejan huella: **corresponde una ADR en 05-Arquitectura-Tecnica** que registre por qué un `web-monolith` de este producto no tiene modelo conceptual de datos, con su alternativa descartada —replicar o cachear datos en la pieza pública— y su consecuencia aceptada: cuando la pieza de datos no responde, no hay nada que mostrar y se declara el estado degradado |
| `Definicion-<Concepto-Central>.md` | **El concepto central del producto ya está documentado aguas arriba.** El modelo de dominio vive en [`Definicion-Modelo-De-Dominio.md`](../../GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) de `GeometriaFactory-Domain`, y el contrato de la superficie que esta pieza invoca para dibujar vive en [`Definicion-Contrato-De-Fachada.md`](Definicion-Contrato-De-Fachada.md) de `GeometriaFactory-Visor`. Esta pieza no tiene un concepto técnico central propio y separable: lo que la caracteriza son las tres reglas de arquitectura de nivel producto, cuyo enunciado vive en `PRODUCT-INTAKE` §14 y cuya aplicación acá está declarada en `Especificacion-Funcional.md` §2 y §6. Un documento de concepto propio duplicaría lo de aguas arriba |

Las tres omisiones son deliberadas y están declaradas también en `Especificacion-Funcional.md` §9. No son artefactos pendientes.

## 4. Cómo leer esta sección

1. Empezar por `Especificacion-Funcional.md` §2, que fija las tres reglas de arquitectura que gobiernan todo lo demás, y §6, con las **trece** restricciones transversales y su punto de verificación. Es lo que evita el error de lectura más frecuente: suponer que un componente de esta pieza puede hablar con la pieza de datos desde el navegador, o hacer cumplir una regla por su cuenta.
2. Seguir por los casos de uso en el orden en que la persona los recorre: CU-10001 a CU-10004 para el circuito de admisión, CU-10005 a CU-10007 para el trabajo, CU-10008 y CU-10009 para la revisión, y CU-10010 al final, que es transversal a los nueve anteriores.
3. Leer `Especificacion-Funcional.md` §7 antes de cualquier decisión sobre el visualizador: declara una sola vez qué función de la fachada consume cada caso de uso, y las dos consecuencias que no hay que perder.
4. Tener a mano `Glosario-Funcional.md` si se entra por una sección suelta: es donde se resuelven las formas desnudas de «vista», «pieza» y `Pendiente`.

Advertencias para las categorías aguas abajo:

- **03-UX-UI-DX es el downstream más directo de esta sección**, y de él depende la fase de maqueta. Acá se declara **qué hace la persona y qué ve**, nunca cómo se ve: no hay maquetas, ni proporciones, ni tipografía, ni sistema visual. Lo único que esta categoría fija sobre la disposición es la de la vista de trabajo, y no porque la decida acá sino porque **viene decidida aguas arriba y probada en el aula**: datos y texto a la izquierda; elemento de dibujo arriba y árbol abajo, a la derecha. No se reinventa.
- La arquitectura y los registros de decisión son de 05, incluida la ADR que el punto 3 de §3 deja pendiente. Las pruebas son de 08.
- Los nombres de las **seis** funciones de la fachada —las cinco originales y `establecerMovimiento`, que el intake incorporó en su versión 1.6— son los que declara el intake y no se cambian; los nombres internos del bundle no están fijados y no se usan acá, por la regla de aislamiento.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice navegable de los doce documentos vigentes de la categoría, declaración de las cuatro omisiones de artefacto agrupadas en tres motivos —con la del modelo conceptual declarada como decisión técnica y con la ADR que le corresponde en 05— y guía de lectura para revisores externos, con la advertencia de frontera hacia 03-UX-UI-DX. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**. Las restricciones transversales pasan de once a **trece** —RT-12, el confinamiento de la cuenta reseteada, y RT-13, la frontera del movimiento automático—; CU-10004 pasa de cuatro a **cinco** operaciones sobre la cuenta; las reglas de negocio pasan a **trece**, con dos todavía sin archivo aguas arriba; y la fachada del visualizador pasa de cinco a **seis** funciones. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8 y con las categorías vecinas.** §4 dejaba escrito que RN-10012 y RN-10013 «todavía no tienen archivo» en `GeometriaFactory-Domain`: **lo tienen**, y la fila pasa a declarar que se enlazan como las otras once. El recuento de trece reglas y de trece restricciones transversales **no cambia**; lo que cambia en `Especificacion-Funcional.md` 1.2 es el enunciado de RT-12, que ahora declara que la cuenta reseteada llega al cambio **sin sesión de trabajo**, según la precisión de RN-10013 en el intake 1.8. |
| 1.3 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10**: las reglas del producto pasan de trece a **quince** con **RN-10014** —la provisoria la produce el sistema— y **RN-10015** —resetear no exige cuenta habilitada—, y las cuatro que entraron después de la emisión inicial tienen archivo en `GeometriaFactory-Domain`, de modo que la nota de omisión de `Reglas-De-Negocio/` actualiza su recuento. **Ningún documento de esta sección, ningún caso de uso y ninguna restricción transversal cambia.** Sube minor. |
| 1.4 | 2026-08-10 | Alineación con `PRODUCT-INTAKE` **1.13** §4.1 (**RN-10016**) y la precisión de **F-04**, que `CU-10002` 1.3, `CU-10003` 1.4 y `CU-10004` 1.6 absorben: habilitar una cuenta produce su contraseña provisoria y **no queda ninguna ruta que fije una contraseña sin credencial vigente**. §1 reescribe la línea de `CU-10003`. Ningún artefacto se agrega ni se omite y el orden de lectura no cambia. (Analista Funcional senior (AG-02)). |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** La fila de `Reglas-De-Negocio/` de **§4** declaraba «Las **quince** reglas viven en `GeometriaFactory-Domain`» y enumeraba las cuatro entradas tardías; el conjunto tiene **dieciséis** reglas, `RN-10001` a `RN-10016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. Pasa a **dieciséis**, con **RN-10016** sumada a la enumeración de las entradas tardías —entró con el intake 1.13— y con «las otras once» recontado y sin cambio. **Ningún caso de uso, ninguna restricción transversal y ninguna fila de la matriz cambia**: acá las reglas se referencian y no se redactan. Sube minor. |
