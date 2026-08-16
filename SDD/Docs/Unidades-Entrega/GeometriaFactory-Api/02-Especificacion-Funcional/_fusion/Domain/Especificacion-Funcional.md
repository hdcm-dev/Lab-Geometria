# Especificación funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Especificacion-Funcional.md
**Versión:** 1.10
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §1, §3, §9; `00-Contexto/Alcance-Producto.md` §4.1, §5, §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4 y §5.3, y las necesidades NB-00001 a NB-00006 y NB-00009; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14** §17.1 íntegro —en particular §17.1.P.2 con los **nueve** invariantes, INV-08 adoptado e INV-09 nuevo—, §4 (capacidades **F-26**, F-03 y **F-04** precisada), §4.1 (las **dieciséis** reglas, con **RN-02016** nueva del intake 1.13), §7 (CL-7 reescrito), §9 (X-2 retirada) y §4.2 (modelo de estados del trabajo), §13 y §14 (composición), §6, §7, §12, §20 y §21
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Alcance funcional de este proyecto de código](#1-alcance-funcional-de-este-proyecto-de-código)
- [2. Documentos de esta categoría](#2-documentos-de-esta-categoría)
- [3. Catálogo de casos de uso](#3-catálogo-de-casos-de-uso)
- [4. Catálogo de reglas de negocio](#4-catálogo-de-reglas-de-negocio)
- [5. Matriz NB → CU → RN → US](#5-matriz-nb--cu--rn--us)
  - [5.1 Matriz](#51-matriz)
  - [5.2 Cobertura bidireccional](#52-cobertura-bidireccional)
  - [5.3 Historias de usuario previstas](#53-historias-de-usuario-previstas)
- [6. Criterio de recorte aplicado](#6-criterio-de-recorte-aplicado)
- [7. Omisiones declaradas](#7-omisiones-declaradas)
- [8. Numeración y nombres de archivo](#8-numeración-y-nombres-de-archivo)
- [9. Puntos abiertos](#9-puntos-abiertos)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Alcance funcional de este proyecto de código

`GeometriaFactory-Domain` contiene las entidades e invariantes del dominio y es el centro de la regla de dependencias: no depende de nada y lo consumen `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure` por referencia de proyecto de código (PRODUCT-INTAKE §13 y §17.1.P.1).

Por eso esta especificación tiene una forma particular y deliberada, que es la de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los trece casos de uso es el código que consume la biblioteca. El alumno y el administrador aparecen como **sujetos de las reglas** que el dominio hace cumplir, nunca como actores.

Lo que no está acá, y dónde está: la interpretación del texto del alumno, el cálculo de los valores derivados, la persistencia, las consultas y los listados, la verificación de la unicidad del correo sobre el conjunto de alumnos, la derivación de la contraseña y la emisión del acceso pertenecen a `GeometriaFactory-Application` y a `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, a `GeometriaFactory-Contracts`; el dibujo, a `GeometriaFactory-Visor`. La tabla completa de fronteras está en [`Definicion-Modelo-De-Dominio.md`](../../Definicion-Modelo-De-Dominio.md) §7.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Modelo-De-Dominio.md`](../../Definicion-Modelo-De-Dominio.md) | Documento de concepto central: las cinco entidades, los nueve invariantes vigentes y las tres máquinas de estado |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Trece casos de uso, uno por archivo |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | Dieciséis reglas de negocio, una por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

## 3. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-02001 | [`CU-02001` · Registrar el alta de un alumno](../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) | Constituir un alumno con cuenta `Pendiente`, sin credencial derivada y con correo único | Propuesto |
| CU-02002 | [`CU-02002` · Gobernar el ciclo de vida de la cuenta del alumno](../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) | Habilitar, bloquear, rehabilitar y dar de baja | Propuesto |
| CU-02003 | [`CU-02003` · Fijar y reemplazar la credencial derivada](../../Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) | Fijar la credencial en el acto de habilitación y reemplazarla después, que es el camino del primer ingreso y el del cambio posterior a un reseteo | Propuesto |
| CU-02004 | [`CU-02004` · Evaluar la admisibilidad de la cuenta](../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | Responder si la cuenta admite acceso y con qué motivo si no lo admite (INV-06) | Propuesto |
| CU-02005 | [`CU-02005` · Crear y reeditar un trabajo](../../Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md) | Constituir el trabajo con dueño, identidad propia y texto original íntegro | Propuesto |
| CU-02006 | [`CU-02006` · Reconstruir el conjunto de piezas del trabajo](../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) | Incorporar piezas y componentes con identidad posicional y valores separados | Propuesto |
| CU-02007 | [`CU-02007` · Registrar las observaciones del trabajo](../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) | Incorporar advertencias y errores de validación bien formados | Propuesto |
| CU-02008 | [`CU-02008` · Gobernar el estado del trabajo en el envío](../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) | Resolver entre `Borrador` y `Pendiente` en la única acción de guardado | Propuesto |
| CU-02009 | [`CU-02009` · Resolver el acceso de un alumno a un trabajo](../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) | Pertenencia del trabajo y acotación de lo que el alumno opera al borrador | Propuesto |
| CU-02010 | [`CU-02010` · Resolver el desenlace del trabajo](../../Casos-De-Uso/CU-02010-Resolver-El-Desenlace-Del-Trabajo.md) | Aprobar o rechazar desde `Pendiente`, con comentario opcional y terminalidad | Propuesto |
| CU-02011 | [`CU-02011` · Resolver el alcance del administrador sobre un trabajo](../../Casos-De-Uso/CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) | Qué trabajos ve el administrador y cuáles puede eliminar | Propuesto |
| CU-02012 | [`CU-02012` · Configurar la cuenta de administrador en el primer arranque](../../Casos-De-Uso/CU-02012-Configurar-La-Cuenta-De-Administrador.md) | Constituir la única cuenta de administrador, `Habilitado` y con credencial, mientras no exista ninguna | Propuesto |
| CU-02013 | [`CU-02013` · Resetear la contraseña de una cuenta de alumno](../../Casos-De-Uso/CU-02013-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) | Fijar una contraseña provisoria conservando la cuenta y todos sus trabajos, y poner la marca de cambio de contraseña pendiente (RN-02012, INV-09) | Propuesto |

Trece casos de uso, sobre un mínimo de cinco para el tipo `library`.

**El cambio forzado de contraseña no tiene caso de uso propio, y es una decisión declarada.** La capacidad F-26 tiene dos mitades: el reseteo, que es un acto nuevo del administrador sobre una cuenta ajena, y el cambio obligatorio, que es el acto de la propia cuenta que levanta la marca. La primera es CU-02013. La segunda **es el reemplazo de credencial que CU-02003 ya declaraba**: mismo sujeto, misma precondición de credencial vigente verificada, mismo efecto sobre el atributo. Lo único nuevo es que, cuando la marca está puesta, el reemplazo además la levanta, y eso es un flujo alternativo de CU-02003 y no un contrato distinto. La guarda que impide todo lo demás mientras la marca está puesta vive en **CU-02004**, que es donde ya vive INV-06. Emitir un caso de uso para el cambio forzado habría declarado dos veces la misma superficie, que es exactamente lo que el criterio de fusión de §6 evita.

**Los dos caminos de alta de cuenta son CU-02001 y CU-02012**, y no se fusionan: el auto-registro del alumno nace con la cuenta `Pendiente` y espera habilitación; la configuración del administrador nace `Habilitado`, porque es la cuenta que habilita a las demás y ninguna anterior podría habilitarla a ella.

## 4. Catálogo de reglas de negocio

Las **dieciséis** reglas del producto, con el invariante que expresa a cada una como condición permanente sobre los datos. La correspondencia es de PRODUCT-INTAKE §17.1.P.2: **los invariantes no son reglas distintas, son las mismas vistas desde el dominio.**

| RN | Enunciado en una línea | Invariante | CU afectados | Estado |
| --- | --- | --- | --- | --- |
| RN-02001 | [`RN-02001` · Administrador único y papeles fijos](../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | INV-05 | CU-02012, CU-02002, CU-02001, CU-02004 | Propuesto |
| RN-02002 | [`RN-02002` · El correo del alumno es único](../../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | INV-01 | CU-02001, CU-02012 | Propuesto |
| RN-02003 | [`RN-02003` · Un alumno sólo ve y opera sus propios trabajos](../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | INV-02 | CU-02009 | Propuesto |
| RN-02004 | [`RN-02004` · El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve](../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | INV-03 | CU-02005, CU-02008, CU-02009, CU-02011 | Propuesto |
| RN-02005 | [`RN-02005` · Un trabajo no pasa a estado `Pendiente` con errores de validación](../../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | INV-04 | CU-02007, CU-02008, CU-02010 | Propuesto |
| RN-02006 | [`RN-02006` · Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso](../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | INV-06 | CU-02002, CU-02003, CU-02004 | Propuesto |
| RN-02007 | [`RN-02007` · La baja arrastra los trabajos y exige confirmación escrita](../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | — | CU-02002 | Propuesto |
| RN-02008 | [`RN-02008` · El texto original del alumno se conserva íntegro](../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | — | CU-02005, CU-02006, CU-02007 | Propuesto |
| RN-02009 | [`RN-02009` · Toda observación de error indica la posición de la pieza y el campo](../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | — | CU-02006, CU-02007 | Propuesto |
| RN-02010 | [`RN-02010` · El desenlace es exclusivo del administrador y es terminal](../../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | INV-07 | CU-02005, CU-02006, CU-02008, CU-02010 | Propuesto |
| RN-02011 | [`RN-02011` · El administrador no ve los trabajos en borrador](../../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | — | CU-02010, CU-02011 | Propuesto |
| RN-02012 | [`RN-02012` · El reseteo de contraseña conserva la cuenta y sus trabajos](../../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | INV-09 | CU-02013, CU-02002 | Propuesto |
| RN-02013 | [`RN-02013` · Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte](../../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | INV-09 | CU-02004, CU-02003, CU-02013 | Propuesto |
| RN-02014 | [`RN-02014` · La contraseña provisoria la produce el sistema, no la escribe el administrador](../../Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | — | CU-02013, CU-02003 | Propuesto |
| RN-02015 | [`RN-02015` · Resetear no exige que la cuenta esté habilitada](../../Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | — | CU-02013, CU-02002, CU-02004 | Propuesto |
| RN-02016 | [`RN-02016` · Habilitar una cuenta produce su contraseña provisoria](../../Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | INV-09 | CU-02002, CU-02003, CU-02004, CU-02013 | Propuesto |

Las **seis** filas sin invariante asociado —sobre dieciséis— lo están por un motivo declarado: RN-02007, RN-02008, RN-02009 y **RN-02014** describen comportamientos y no condiciones permanentes sobre el estado; **RN-02015** enuncia la **ausencia** de una precondición, que tampoco es una condición sobre los datos; y RN-02011 es una regla de alcance de consulta (PRODUCT-INTAKE §17.1.P.2, cuya prosa enumera esas seis y agrega a RN-02012). **RN-02012, RN-02013 y RN-02016 comparten invariante**, INV-09, y no es un descuido de la tabla: las dos primeras son las dos mitades de la misma condición —qué conserva el reseteo y qué no puede la cuenta hasta cambiar la provisoria—, y **RN-02016 no agrega una mitad nueva sino un segundo origen** de la misma marca. **Diez reglas con invariante y seis sin él.** **La fuente de esa lectura es la columna «regla de negocio que sostiene» de INV-09 en `PRODUCT-INTAKE` §17.1.P.2, que dice «RN-02012, RN-02013», y no su prosa**, que en esa misma sección enumera a RN-02012 entre las reglas **sin** invariante asociado y remata «RN-02013 sí lo tiene, y es INV-09». El intake es ambiguo en este punto y esta categoría lo declara en lugar de taparlo: se adopta la lectura de la columna, con el fundamento que `Definicion-Modelo-De-Dominio.md` §4.3 desarrolla —RN-02012 sin INV-09 no tendría cómo impedir que la provisoria sirviera indefinidamente—, y **no se afirma que la prosa del intake lo declare**, porque dice lo contrario. Consolidar una de las dos formas es del Product Owner sobre su propio documento.

Los nueve invariantes no llevan archivo propio: son propiedades permanentes del modelo y viven enunciados en [`Definicion-Modelo-De-Dominio.md`](../../Definicion-Modelo-De-Dominio.md) §4.1. **INV-08, que esta categoría había propuesto como candidato, está adoptado** por `PRODUCT-INTAKE` §17.1.P.2 y se cuenta entre los vigentes; §4.2 conserva el registro de su recorrido. **INV-09 es nuevo del intake 1.7** y es el que sostiene a las **tres** reglas de la contraseña provisoria. **RN-02014 y RN-02015, que el intake 1.10 suma sobre la misma capacidad, no traen invariante y no lo necesitan**: la primera describe cómo se produce un valor que a este proyecto de código le llega ya derivado, y la segunda declara que una precondición no existe. **RN-02016, que el intake 1.13 suma, sí trae invariante y es INV-09**, porque enuncia una condición sobre los datos —ninguna cuenta de alumno `Habilitado` sin credencial, y ninguna habilitación sin marca— y no un comportamiento.

**Desfase sobre la letra de INV-09, declarado y hoy cerrado.** El enunciado que `PRODUCT-INTAKE` §17.1.P.2 le daba a INV-09 dijo, hasta la versión **1.13**, que la marca «la pone **únicamente** el reseteo del administrador». Esa frase era de la **1.7** y la contradecía la propia 1.13, cuya §4.1 declara en RN-02016 que habilitar deja la cuenta con cambio de contraseña pendiente y cita a INV-09 al hacerlo. Esta categoría adoptó la decisión —**dos orígenes de la marca**— y no la letra que la fuente todavía no había actualizado, y elevó la consolidación al Product Owner sobre su propio documento. **El intake `1.14`, del 2026-08-09, la consolidó**: reescribió el enunciado de INV-09 —«la marca la ponen **únicamente** las dos operaciones que producen una contraseña provisoria: el **reseteo** (RN-02014) y la **habilitación** (RN-02016)»— y lo registró en la fila 1.14 de su control de cambios, corrección **(a)**. **Desde la 1.14 la letra de la fuente coincide con la decisión que esta categoría venía sosteniendo**, y no queda desfase que declarar. La misma traza está en `Definicion-Modelo-De-Dominio.md` §4.1.

## 5. Matriz NB → CU → RN → US

### 5.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| NB-00001 · Control de admisión y de bajas del laboratorio | CU-02012, CU-02001, CU-02002, CU-02004, CU-02013 | RN-02001, RN-02002, RN-02006, RN-02007, RN-02012, RN-02015, RN-02016 | US-02001, US-02002, US-02004, US-02005, US-02008, US-02024, US-02025, US-02026 |
| NB-00002 · Identidad propia del alumno sin canal de correo | CU-02001, CU-02002, CU-02003, CU-02004, CU-02013 | RN-02001, RN-02002, RN-02006, RN-02012, RN-02013, RN-02014, RN-02016 | US-02001, US-02003, US-02006, US-02007, US-02008, US-02026, US-02027 |
| NB-00003 · Trabajo con dueño, estado y persistencia | CU-02005, CU-02008, CU-02009 | RN-02003, RN-02004, RN-02005, RN-02008 | US-02009, US-02010, US-02015, US-02016, US-02018, US-02019 |
| NB-00004 · Interpretación fiel del dato del alumno | CU-02005, CU-02006, CU-02007, CU-02008 | RN-02005, RN-02008, RN-02009 | US-02010, US-02011, US-02012, US-02014, US-02015, US-02016 |
| NB-00005 · Visibilidad del error de cálculo | CU-02007, CU-02008 | RN-02005 | US-02013, US-02015 |
| NB-00006 · Visualización del trabajo dentro del producto | CU-02006 (parcial: identidad posicional) | RN-02009 | US-02011 |
| NB-00007 · Revisión de la comisión desde un solo lugar | CU-02011 (parcial: alcance de la vista) | RN-02011 | US-02022 |
| NB-00008 · Alcance del laboratorio desde el aula | — | — | — |
| NB-00009 · Desenlace explícito de la entrega | CU-02010, CU-02011 | RN-02004, RN-02010, RN-02011 | US-02020, US-02021, US-02022, US-02023 |

### 5.2 Cobertura bidireccional

**De CU a NB.** Los trece casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-02001 | NB-00002, NB-00001 |
| CU-02002 | NB-00001 |
| CU-02003 | NB-00002 |
| CU-02004 | NB-00001, NB-00002 |
| CU-02005 | NB-00003, NB-00004 |
| CU-02006 | NB-00004, NB-00006 |
| CU-02007 | NB-00005, NB-00004 |
| CU-02008 | NB-00003, NB-00004, NB-00005 |
| CU-02009 | NB-00003 |
| CU-02010 | NB-00009, NB-00003 |
| CU-02011 | NB-00009, NB-00007 |
| CU-02012 | NB-00001 |
| CU-02013 | NB-00001, NB-00002 |

**De NB a CU.** Ocho de las nueve necesidades reciben al menos un caso de uso en este proyecto de código. La restante **no la toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-00008 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: mediciones de viabilidad, despliegue y estado degradado. Este proyecto de código no atiende peticiones ni abre conexiones (PRODUCT-INTAKE §17.1.P.10) | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; 09-Devops |

Dos necesidades quedan cubiertas **parcialmente**, y conviene que se lea así:

- **NB-00006.** Lo que este proyecto de código aporta es la identidad posicional de la pieza, que es lo que después permite seleccionarla y resaltarla y lo que sostiene una disposición determinista. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-00007.** Lo que aporta es el **predicado** que decide si un trabajo entra en el alcance del administrador, que es lo que excluye los borradores del listado. La consulta que lo aplica sobre el conjunto, la agrupación y el filtro por alumno viven en `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Web`: el dominio no ejecuta consultas.

### 5.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas. Es el mismo mecanismo con el que `01-Necesidades-Negocio` previó las CU.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-02001 | Constituir un alumno con cuenta `Pendiente` y sin credencial | CU-02001 |
| US-02002 | Rechazar el alta con datos obligatorios ausentes | CU-02001 |
| US-02003 | Exigir la unicidad del correo verificada en el alta | CU-02001 |
| US-02004 | Habilitar, bloquear y rehabilitar una cuenta | CU-02002 |
| US-02005 | Dar de baja una cuenta arrastrando sus trabajos en cualquier estado | CU-02002 |
| US-02006 | Fijar la credencial derivada provisoria en el acto de habilitación | CU-02003, CU-02002 |
| US-02007 | Reemplazar la credencial derivada exigiendo la vigente | CU-02003 |
| US-02008 | Evaluar la admisibilidad de la cuenta y devolver su motivo | CU-02004 |
| US-02009 | Constituir un trabajo con dueño, identidad propia y texto original | CU-02005 |
| US-02010 | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | CU-02005 |
| US-02011 | Reconstruir el conjunto de piezas con identidad posicional | CU-02006 |
| US-02012 | Derivar la familia plana o volumétrica desde el tipo | CU-02006 |
| US-02013 | Registrar advertencias con el valor declarado y el derivado | CU-02007 |
| US-02014 | Registrar errores de validación con posición de pieza y campo | CU-02007 |
| US-02015 | Enviar un trabajo que verifica y pasa a estado `Pendiente` | CU-02008 |
| US-02016 | Enviar un trabajo que no verifica y queda en `Borrador` con sus errores | CU-02008 |
| US-02017 | Rechazar toda transición desde un estado terminal | CU-02008 |
| US-02018 | Resolver la pertenencia de un trabajo a su dueño | CU-02009 |
| US-02019 | Acotar al estado `Borrador` lo que el alumno reedita y elimina | CU-02009 |
| US-02020 | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | CU-02010 |
| US-02021 | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-02010 |
| US-02022 | Excluir los trabajos en `Borrador` del alcance del administrador | CU-02011 |
| US-02023 | Eliminar por el administrador en los tres estados que ve | CU-02011 |
| US-02024 | Configurar la cuenta de administrador en el primer arranque, habilitada y con credencial | CU-02012 |
| US-02025 | Rechazar la configuración de un segundo administrador | CU-02012 |
| US-02026 | Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos | CU-02013 |
| US-02027 | Exigir el cambio de la contraseña provisoria antes de toda otra capacidad, y levantar la marca al cambiarla | CU-02004, CU-02003 |

## 6. Criterio de recorte aplicado

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron **trece**: once tras la absorción del circuito de revisión, más **CU-02012**, que la corrección del P0 emitió para la capacidad **F-01**, que hasta entonces no tenía caso de uso propio y sobrevivía como flujo alternativo de CU-02001, más **CU-02013**, que `PRODUCT-INTAKE` 1.7 hizo necesario al incorporar la capacidad **F-26**. El alcance del producto había crecido antes: `PRODUCT-INTAKE` 1.3 incorporó el circuito de revisión, `01-Necesidades-Negocio` 1.1 emitió **NB-00009** y pasó de 22 a 27 los casos de uso previstos a nivel producto. La guía de la regla —«library con menos de diez»— es orientativa y la propia regla declara que el techo lo fija la cobertura de las NB; se documenta acá el apartamiento con su causa.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta —habilitar, bloquear, rehabilitar y dar de baja— quedaron en un solo caso de uso, CU-02002, porque `NB-00001` §5 las trata como un único conjunto de cobertura. **El cambio forzado de contraseña se fusionó con el reemplazo de CU-02003** por el mismo criterio: comparten sujeto, precondición y efecto, y lo único propio del cambio forzado —que levanta la marca— es un flujo alternativo y no un contrato distinto (§3). Aprobar y rechazar quedaron en CU-02010 porque son el mismo acto con dos desenlaces, comparten precondición, comentario y terminalidad. El alcance del administrador y su eliminación quedaron en CU-02011 porque las dos responden la misma pregunta: qué trabajos entran en su flujo de trabajo.
- **Particiones.** La reconstrucción de las piezas (CU-02006) se separó del registro de observaciones (CU-02007) porque trazan a necesidades distintas con métricas distintas, que es la misma partición que `01-Necesidades-Negocio` §3.2 justifica entre NB-00004 y NB-00005. **El desenlace se separó del envío** —CU-02010 frente a CU-02008— por los mismos tres criterios con los que 01 partió NB-00009 de NB-00007: sujetos distintos, el alumno que envía y el administrador que decide; reglas distintas, RN-02005 frente a RN-02010; y momentos distintos del ciclo de vida. **El reseteo se separó del ciclo de vida de la cuenta** —CU-02013 frente a CU-02002— por tres motivos que ninguna fusión salvaría: no es una transición de la máquina de estados de cuenta, porque el estado no cambia; **no dispara RN-02007**, que es la regla que gobierna la única operación destructiva de CU-02002; y su efecto propio es poner una marca que ninguna de las cuatro operaciones toca (RN-02012, `Definicion-Modelo-De-Dominio.md` §5.1 y §5.3). Absorberlo en CU-02002 habría puesto en el mismo contrato la operación que **elimina** todos los trabajos del alumno y la que los **conserva**, que es exactamente la confusión que F-26 viene a cerrar. **El alcance del administrador se separó del acceso del alumno** —CU-02011 frente a CU-02009— porque las reglas que los gobiernan son opuestas: al alumno lo acota la pertenencia y el borrador, y al administrador lo acota exactamente lo contrario, todo menos el borrador. **Los dos caminos de alta se separaron** —CU-02012 frente a CU-02001— porque difieren en todo lo que un caso de uso declara: el estado inicial de la cuenta, si la credencial se aporta o se fija después, la ventana en que el alta procede y los códigos de rechazo. Resolverlos en un solo documento fue el origen del P0: el flujo alternativo del administrador atravesaba el paso que fija el estado en `Pendiente`.
- **Lo que no se convirtió en caso de uso.** Todo lo que exige conocer el conjunto de entidades —unicidad efectiva del correo, listados, agrupaciones, filtros— no está acá: el dominio verifica lo que puede verificar sobre una entidad y declara el predicado que las consultas aplican.

## 7. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos/Modelo-Conceptual.md` | **Omitido** | La regla de la categoría lo omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false. El intake declara «no aplica» en §17.1.P.4: el dominio no conoce el motor de persistencia. El vocabulario, la semántica y los elementos del concepto viven en `Definicion-Modelo-De-Dominio.md`, que es el documento de concepto central de este proyecto de código |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitido** | Dependen del modelo conceptual, que está omitido, y la regla las omite para `library`. Las restricciones de integridad del dominio están declaradas como los nueve invariantes de `Definicion-Modelo-De-Dominio.md` §4 y como las **dieciséis** reglas de `Reglas-De-Negocio/` |
| `Casos-De-Uso/_legacy/` y `Reglas-De-Negocio/_legacy/` | Existen, con el estado 1.0 archivado | Contienen las copias de la emisión del 2026-08-08 con sufijo de versión, archivadas por el orquestador al publicarse esta revisión. No se editan |

## 8. Numeración y nombres de archivo

Tres aclaraciones que evitan una lectura equivocada de la trazabilidad:

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** `01-Necesidades-Negocio` §5.3 previó veintisiete casos de uso a nivel producto; esta categoría se emite por proyecto de código, de modo que `CU-02001` de `GeometriaFactory-Domain` no es el mismo artefacto que el `CU-02001` que previó el catálogo de necesidades. La correspondencia entre unos y otros es la matriz de §5.1, que traza por necesidad de negocio y no por número.
2. **Los identificadores `RN-XX` conservan la numeración del intake** y la serie es **contigua de RN-02001 a RN-02016**, sin huecos. Creció en cuatro tramos: `PRODUCT-INTAKE` 1.3 §4.1 transcribió las nueve de la fuente funcional y sumó RN-02010 y RN-02011 del circuito de revisión —con lo que la nota de no contigüidad que esta sección arrastraba por RN-02002 y RN-02006 quedó sin objeto y se retiró—; 1.7 sumó **RN-02012** y **RN-02013** con la capacidad F-26; **1.10** sumó **RN-02014** y **RN-02015**, las dos decisiones del Product Owner sobre esa misma capacidad; y **1.13** sumó **RN-02016**, la decisión sobre la identificación de la cuenta en el primer ingreso. **Cada regla tiene archivo propio en `Reglas-De-Negocio/`**, y son **dieciséis** archivos.
3. **Dos nombres de archivo conservan un slug que ya no describe del todo su enunciado**, y es deliberado: `RN-02004-Eliminacion-Acotada-Al-Borrador.md`, cuyo enunciado se amplió al borrado del administrador, y `RN-02005-Finalizacion-Sin-Errores-De-Validacion.md`, cuyo corte se adelantó del cierre al envío. Los casos de uso de `GeometriaFactory-Contracts` ya citan los dos por esa ruta, y renombrarlos rompería sus enlaces sin agregar información. Cada uno declara la decisión en su control de cambios.

## 9. Puntos abiertos

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Nombres de tipos y de espacios de nombres | Declarados abiertos por el intake (§17.1.P.11) y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | 05-Arquitectura-Tecnica y la codificación de la etapa `a` |
| Criterio de comparación de dos correos | La unicidad del correo (RN-02002, INV-01) exige decidir si dos correos se comparan tal cual o normalizados. El dominio conserva el dato como lo recibe y no toma la decisión | 05-Arquitectura-Tecnica, junto con la capa que ejerce la verificación |
| **Alcance efectivo de INV-09 fuera de la admisibilidad** | INV-09 enuncia que la cuenta con la marca puesta **no ejerce ninguna capacidad del sistema** salvo cambiar su propia contraseña. El dominio no tiene una puerta única por la que pasen todas las capacidades, de modo que esta categoría concentra la guarda en CU-02004 —la evaluación de admisibilidad—, con el fundamento de que ninguna capacidad se ejerce sin admisión resuelta (`Definicion-Modelo-De-Dominio.md` §4.1). **Es una decisión derivada, no una transcripción**: si la capa que expone habilitara alguna vez un camino que no pase por la admisibilidad, la marca tendría que volver a comprobarse ahí. No es bloqueante y no afecta a ningún criterio de aceptación de esta categoría | 05-Arquitectura-Tecnica y la categoría 02 de `GeometriaFactory-Api`, al fijar por dónde entra cada petición |

**El punto abierto de los sellos de tiempo del trabajo quedó resuelto, y la propuesta de esta categoría era la que el Product Owner adoptó.** `PRODUCT-INTAKE` **§17.3.P.4** lleva la «Ampliación del 2026-08-09: sellos de tiempo del trabajo» rotulada **[DECISIÓN del Product Owner]**: `TRABAJO` suma **fecha de creación** y **fecha de última modificación**, distintas de la `Fecha` que el alumno declara —que sigue siendo un dato que él escribe—, y **las produce el consumidor a través del puerto de reloj** para que sean verificables en prueba. Es exactamente lo que esta categoría había propuesto al elevar el punto: los dos atributos aportados por el consumidor, como la fecha de alta del alumno. `Definicion-Modelo-De-Dominio.md` §2.2 los declara desde su versión 1.6. **Matiz que corresponde declarar**: la decisión vive en §17.3, que es la sección técnica de `GeometriaFactory-Infrastructure`, y no en §17.1; lo que baja a la entidad de dominio son los dos atributos y su origen —el consumidor—, y no el mecanismo de reloj, que es del puerto y de la capa que lo consume. **Ningún caso de uso, regla ni invariante de esta categoría cambia** por la incorporación.

**El punto abierto de cómo se identifica la cuenta que establece su contraseña quedó cerrado por el Product Owner, y no era de esta categoría sino de `GeometriaFactory-Api`.** `PRODUCT-INTAKE` **1.13** §4.1 incorpora **RN-02016**: habilitar produce la provisoria, la cuenta queda marcada y el alumno la cambia por el camino de RN-02013. Para esta categoría el efecto es que **la fijación de la credencial deja de ser un acto del alumno anónimo** y pasa a ejercerse dentro de la habilitación de CU-02002, y que dos condiciones se retiran por imposibilidad de su causa: el motivo `CREDENCIAL_NO_ESTABLECIDA` de CU-02004 y el rechazo `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` de CU-02013. **Las 43 condiciones del proyecto de código pasan a 42**: entra `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-02002 y salen las dos anteriores.

**El punto abierto de cómo llega la cuenta con la contraseña reseteada al cambio de contraseña quedó resuelto, y la lectura de esta categoría era la correcta.** `PRODUCT-INTAKE` **1.8** precisa RN-02013: la cuenta con contraseña provisoria **se autentica pero no obtiene sesión de trabajo** —el sistema reconoce la credencial y la deriva al cambio—, con el fundamento de que emitir sesión a una cuenta que por INV-09 no ejerce ninguna capacidad es contradictorio y de que es el paralelo exacto del primer ingreso con contraseña no fijada. Es lo que esta categoría modelaba **sin ingreso** desde su versión 1.4: CU-02004 devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` y el reemplazo de CU-02003 procede igual, porque exige la credencial vigente verificada y no una sesión previa. **Ningún caso de uso, motivo ni criterio de aceptación cambia** por la precisión.

**El punto abierto de la adopción de INV-08 quedó resuelto.** `PRODUCT-INTAKE` §17.1.P.2 lo incorpora rotulado «adoptado», con la evidencia de las dos puertas como fundamento, y desde esa incorporación se cuenta entre los invariantes vigentes (§4). El registro del recorrido queda en `Definicion-Modelo-De-Dominio.md` §4.2.

Las dos ambigüedades que esta categoría había elevado en su emisión anterior —los enunciados de INV-01 e INV-03, y los de RN-02002 y RN-02006— **están resueltas** en `PRODUCT-INTAKE` 1.3 §4.1 y §17.1.P.2, y ninguno de los enunciados fue inventado por esta categoría.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.10 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-08 | Emisión inicial. Índice maestro de nueve casos de uso y siete reglas de negocio, con la matriz NB → CU → RN → US, la verificación bidireccional de cobertura, las dos necesidades de negocio que este proyecto de código no toca con su justificación, el criterio de recorte con sus fusiones y particiones, las omisiones del modelo conceptual y de las reglas conceptuales con su motivo, la aclaración de las dos numeraciones y los cuatro puntos abiertos. |
| 1.1 | 2026-08-09 | Absorbe el circuito de revisión de `PRODUCT-INTAKE` 1.3, la necesidad **NB-00009** que `01-Necesidades-Negocio` 1.1 emitió y la resolución de las dos ambigüedades que esta categoría había elevado. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). El catálogo de casos de uso pasa de nueve a **once**, con CU-02010 desenlace y CU-02011 alcance del administrador, y CU-02008 se acota al envío. El de reglas pasa de siete a **once y contiguas**, con RN-02002, RN-02006, RN-02010 y RN-02011, y suma la columna del invariante que expresa a cada una, con las cuatro que no tienen ninguno y su motivo. **§5.1 suma NB-00009 y la cobertura parcial de NB-00007**, y las US previstas pasan de 17 a 23. **§6** justifica el apartamiento de la guía de «menos de diez» por el crecimiento del alcance, y declara las dos particiones nuevas. **§8** retira la nota de no contigüidad, que quedó sin objeto, y declara los dos nombres de archivo que se conservan por estabilidad de citación. **§9** deja dos puntos abiertos, ninguno bloqueante, y registra que las dos ambigüedades anteriores están resueltas. |
| 1.2 | 2026-08-09 | **Corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`, primer P0 del producto. La capacidad **F-01** —configurar la cuenta de administrador en el primer arranque— no tenía caso de uso propio y sobrevivía como flujo alternativo de CU-02001, que fija el estado inicial `Pendiente` para toda cuenta: el administrador nacía `Pendiente`, no obtenía acceso por INV-06 y ninguna otra cuenta podía habilitarlo, de modo que la instancia quedaba inutilizable en el primer arranque. Se emite **CU-02012** y el catálogo pasa a **doce casos de uso**; §1 y §3 declaran los dos caminos de alta; §4 reasigna RN-02001 y RN-02002 y remite al invariante candidato INV-08; §5.1 suma CU-02012 a NB-00001 y las US previstas pasan de 23 a **25**; §5.2 suma su fila de cobertura; §6 declara la partición de los dos caminos con su fundamento y actualiza el recuento; y §9 suma como punto abierto la adopción de INV-08, que **no viene del intake**. |
| 1.3 | 2026-08-09 | Correcciones de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`. **H-01**: el catálogo de §4 refleja que RN-02001 pasa a proteger la unicidad del administrador contra **las cuatro operaciones** y no sólo contra la baja, con el código único que las cubre; el fundamento es la capacidad F-03 del intake, que ya las declara sobre cuentas de alumno. **H-03**: §9 declara como punto abierto las fechas de creación y de última modificación del trabajo, que el modelo no enuncia y que la capa de casos de uso supone, con la propuesta de esta categoría y sin rellenarlas. **H-01 / INV-08**: §9 amplía el enunciado del invariante candidato al ciclo de vida completo y suma la evidencia de que la familia ya se abrió dos veces. |
| 1.4 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**, que incorpora la capacidad **F-26** —reseteo de contraseña por el administrador—, las reglas **RN-02012** y **RN-02013**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. El catálogo de casos de uso pasa de doce a **trece** con **CU-02013**, el reseteo; §3 declara por qué el **cambio forzado de contraseña no lleva caso de uso propio** y entra como flujo alternativo de CU-02003 con su guarda en CU-02004. El catálogo de reglas pasa de once a **trece y contiguas**, con **RN-02012** y **RN-02013**, las dos con INV-09, y §4 explica por qué un invariante sostiene dos reglas. Los invariantes pasan de siete a **nueve vigentes**: entra INV-09, y entra **INV-08**, que esta categoría había propuesto como candidato y que el intake rotula «adoptado». **§5.1** suma CU-02013 y las dos reglas nuevas a NB-00001 y NB-00002; **§5.2** suma la fila de cobertura de CU-02013; **§5.3** pasa de 25 a **27** historias previstas, con US-02026 y US-02027. **§6** actualiza el recuento, declara la fusión del cambio forzado con CU-02003 y la **partición del reseteo frente a CU-02002**, con el fundamento de que no es una transición de estado de cuenta y de que no dispara RN-02007. **§9** retira el punto abierto de la adopción de INV-08, que quedó resuelto, y suma dos nuevos: el alcance efectivo de INV-09 fuera de la admisibilidad, y la lectura de esta categoría sobre el «ingresa» de RN-02013, que se modela **sin ingreso** por paralelismo con el primer ingreso efectivo y que el Product Owner puede precisar. |
| 1.5 | 2026-08-09 | **Absorbe la precisión de `PRODUCT-INTAKE` 1.8 §4.1 sobre RN-02013**, que la propagación de esta categoría disparó. §9 **cierra el punto abierto «cómo llega la cuenta reseteada al cambio de contraseña»**: el intake resuelve que la cuenta **se autentica y no obtiene sesión de trabajo**, que es exactamente el modelo sin ingreso que la versión 1.4 había adoptado como lectura derivada. La constancia pasa de la tabla a la prosa que sigue, junto a la de INV-08, para que ningún lector automatizado la cuente como punto vivo. **Ningún caso de uso, regla, invariante, motivo ni criterio de aceptación de esta categoría cambia**: sube minor porque cierra un punto abierto y no porque agregue nada. |
| 1.6 | 2026-08-09 | **Cierra el hallazgo `F26-09`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **§8** afirmaba que **el intake declara en §17.1.P.2 que RN-02012 y RN-02013 comparten INV-09**, y esa sección declara lo contrario en su prosa: enumera a RN-02012 entre las reglas **sin** invariante asociado y cierra con «RN-02013 sí lo tiene, y es INV-09». Lo que sí sostiene la lectura de esta categoría es la **columna «regla de negocio que sostiene» de la fila INV-09**, que dice «RN-02012, RN-02013». La afirmación se corrige: se declara de dónde viene la lectura, se declara que el intake es **internamente ambiguo** en este punto y que consolidarlo es del Product Owner, y se deja de atribuirle a la sección algo que su prosa niega. Es la misma calificación que `Definicion-Modelo-De-Dominio.md` §4.3 ya hacía correctamente. Ninguna regla, ningún invariante y ninguna condición del catálogo cambia. |
| 1.7 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10** y cierra dos hallazgos del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. **(a) Las reglas del producto pasan de trece a quince.** El intake 1.10 §4.1 incorpora **RN-02014** —la provisoria la produce el sistema, no es adivinable y no se repite entre cuentas ni entre reseteos— y **RN-02015** —resetear no exige cuenta habilitada: procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, y sigue sin admitirse sobre la cuenta de administrador por INV-08—, que son las dos decisiones del Product Owner que la propagación anterior había distribuido sin fuente. **§4** suma las dos filas, las dos **sin invariante**, y la nota de las reglas sin invariante pasa de cuatro a **seis** con el motivo de cada una; **§5.1** suma RN-02015 a NB-00001 y RN-02014 a NB-00002; **§7** actualiza el recuento de las restricciones de integridad. **Los trece casos de uso, los nueve invariantes y las 43 condiciones no cambian**: las dos reglas ya estaban modeladas dentro de `CU-02013` y esta emisión les da el identificador que la fuente les dio. **(b) `F26-20`**: **§8** punto 2 declaraba la serie de reglas «**contigua de RN-02001 a RN-02011**» cuando ya llegaba a RN-02013; pasa a declararla contigua **de RN-02001 a RN-02016**, con los tres tramos en que creció y con la constancia de que las quince tienen archivo propio. **(c) `F26-17`**: **§9** listaba como punto abierto pendiente de declaración del Product Owner las **fechas de creación y de última modificación del trabajo**, que `PRODUCT-INTAKE` **§17.3.P.4** ya declara rotuladas **[DECISIÓN del Product Owner]**. La fila sale de la tabla y pasa a la prosa de puntos resueltos, junto a las de INV-08 y RN-02013, con el matiz de que la decisión vive en §17.3 y no en §17.1 y de qué parte de ella baja a la entidad; `Definicion-Modelo-De-Dominio.md` §2.2 los declara desde su versión 1.5. **(d) `F26-28`**: las filas de este control de cambios estaban fuera de orden cronológico (1.0, 1.1, 1.3, 1.2, 1.4, 1.5, 1.6) y se reordenan por versión, **sin tocar el texto de ninguna**. Sube minor: suma dos reglas y cierra un punto abierto sin alterar ningún contrato de uso. |
| 1.8 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13**, que incorpora la regla **RN-02016** —habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo, y la deja con cambio de contraseña pendiente— y precisa la capacidad **F-04**. **§4**: el catálogo de reglas pasa de quince a **dieciséis y contiguas**, con **RN-02016** asociada a **INV-09**; el reparto pasa a **diez con invariante y seis sin él**, y la nota del invariante compartido pasa de dos reglas a **tres**, con el fundamento de que RN-02016 agrega un segundo origen y no una mitad nueva. Entra además la **constancia del desfase** entre la letra de INV-09 en §17.1.P.2, que sigue diciendo «únicamente el reseteo», y lo que RN-02016 decide en §4.1 de la misma versión: esta categoría adopta la decisión y no la letra, con el mismo criterio con el que ya resolvía la ambigüedad entre la columna y la prosa de esa sección. **§3** reescribe la línea de CU-02003, cuya fijación deja de ser el primer ingreso anónimo. **§5.1** suma RN-02016 a NB-00001 y a NB-00002, y CU-02002 a NB-00002. **§5.3** reescribe US-02006. **§7 y §8** actualizan los recuentos de reglas y de archivos a dieciséis, y la serie a **RN-02001 a RN-02016** en cuatro tramos. **§9** registra el cierre del punto abierto de la identificación en el primer ingreso —que era de `GeometriaFactory-Api` y no de esta categoría— y su efecto acá: la fijación cambia de sujeto, se retiran dos condiciones cuya causa dejó de ser posible y entra una nueva, con lo que **las condiciones del proyecto de código pasan de 43 a 42**. **Los trece casos de uso y los nueve invariantes no cambian.** Sube minor. |
| 1.9 | 2026-08-10 | **Cierra los hallazgos `C-02` (P0) y `C-03` (P1) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** **`C-02`**: la fila de `Reglas-De-Negocio/` en **§2** declaraba «**Quince** reglas de negocio, una por archivo» sobre un directorio que tiene **dieciséis** archivos, `RN-02001` a `RN-02016`, serie contigua —contados uno por uno—; pasa a **dieciséis**. Es un inventario de archivos y era la única fila de esa tabla desactualizada: «trece casos de uso» y «nueve invariantes» se recontaron y cierran. **`C-03`**: el párrafo «Desfase declarado sobre la letra de INV-09» de **§4** afirmaba que el intake «dice, todavía en la versión 1.13», que la marca la pone únicamente el reseteo; la **1.14** reescribió ese enunciado y la afirmación quedó falsa sobre la fuente. Se reemplaza por la traza: qué decía la fuente, en qué versión, cuándo se consolidó y que desde la 1.14 letra y decisión coinciden. **Ningún caso de uso, ninguna regla, ningún invariante y ninguna fila de la matriz cambia.** Sube minor. |
