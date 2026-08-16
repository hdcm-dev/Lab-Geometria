# Product Backlog — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.9 §3 (los **trece** casos de uso), §4 (las **dieciséis** reglas), §5.1 (matriz NB → CU → RN → US) y §5.3 (las **veintisiete** historias previstas); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los cinco componentes), §8 (los seis NFR) y §11 (los cuatro puntos abiertos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4 (las **veintiséis** capacidades y su prioridad), §4.1 (las dieciséis reglas), §15 (las **ocho** etapas comprometidas `a` a `h`, las reglas de delivery y las puertas técnicas) y §17.1 (P.1 a P.12); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §3, §4 y §5; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3 (orden topológico)
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 0 para este backlog](#11-qué-significa-nivel-topológico-0-para-este-backlog)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-02001 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-02002 · Identidad del administrador y sesión](#32-ep-02--identidad-del-administrador-y-sesión)
  - [3.3 EP-02003 · Ciclo de vida de la cuenta de alumno](#33-ep-03--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.4 EP-02004 · Gestión del trabajo](#34-ep-04--gestión-del-trabajo)
  - [3.5 EP-02005 · Interpretación y verificación del dato del alumno](#35-ep-05--interpretación-y-verificación-del-dato-del-alumno)
  - [3.6 EP-02006 · Desenlace de la entrega](#36-ep-06--desenlace-de-la-entrega)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **trece** contratos de uso y las **dieciséis** reglas que `GeometriaFactory-Domain` declara, sin agregar alcance y sin reordenar el plan de etapas. Su propósito es que, en cualquier momento, se pueda responder qué parte del dominio ya está construida y de qué etapa del producto depende esa parte.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido del producto —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance que el intake declara, **8 de 8 etapas** (§22, asunción `A-2`). Una historia de este backlog está en el MVP si la etapa que la contiene está entre esas ocho; ninguna otra prueba de pertenencia se aplica.

**Este backlog no reordena las etapas ni las renombra.** Las seis épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una. Formalizarlas es lo que ese documento pide de la categoría 06; inventar una agrupación distinta habría creado una segunda fuente de verdad sobre el orden de construcción.

### 1.1 Qué significa nivel topológico 0 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Domain` en el **nivel 0** del orden topológico de construcción, junto con `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`. Tres consecuencias operativas, y ninguna de ellas es una licencia para adelantar alcance:

1. **Ninguna historia ni ninguna tarea de este backlog espera a otro proyecto de código.** El proyecto de código no referencia a ninguno (`05` §2 propiedad 1), de modo que su trabajo puede empezar apenas la etapa `a` deja el esqueleto en pie.
2. **Su trabajo condiciona el de los niveles 1 a 3.** `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure` compilan contra esta biblioteca, de modo que una guarda que acá no exista es una guarda que allá no se puede invocar. Dentro de cada etapa, lo de este backlog va primero.
3. **El orden topológico no cambia el orden de las etapas.** Las etapas son estrictamente secuenciales y sin paralelismo (`Roadmap-Producto.md` §4). Que este proyecto de código pueda arrancar primero significa que arranca primero **dentro** de la etapa vigente, no que pueda construir la etapa `e` mientras la `c` sigue abierta.

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-02001 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, compila sin dependencias salientes y sus decisiones abiertas de nombre y de herramienta quedan cerradas en el punto de control | Ninguna: la etapa `a` no tiene capacidad funcional asociada (`Roadmap-Producto.md` §2.1) | BT-02001 a BT-02005 |
| EP-02002 | Identidad del administrador y sesión | `c` | La cuenta de administrador se constituye en el primer arranque y la admisibilidad y el cambio de credencial quedan resueltos como contrato de uso | US-02007, US-02008, US-02024, US-02025 | BT-02006, BT-02007, BT-02010, BT-02011 |
| EP-02003 | Ciclo de vida de la cuenta de alumno | `d` | Alta, ciclo de vida, credencial provisoria, reseteo y marca de cambio pendiente | US-02001 a US-02006, US-02026, US-02027 | BT-02009, BT-02010, BT-02011, BT-02016 |
| EP-02004 | Gestión del trabajo | `e` | El trabajo se constituye con dueño e identidad propia, y quedan resueltos el acceso del alumno y el alcance del administrador | US-02009, US-02010, US-02018, US-02019, US-02022 | BT-02006, BT-02012 |
| EP-02005 | Interpretación y verificación del dato del alumno | `f` | El conjunto de piezas y las observaciones se adoptan, y el envío resuelve entre `Borrador` y estado `Pendiente` | US-02011 a US-02017 | BT-02008, BT-02012, BT-02013 |
| EP-02006 | Desenlace de la entrega | `h` | Aprobar y rechazar desde el estado `Pendiente`, con terminalidad, y la eliminación por el administrador | US-02020, US-02021, US-02023 | BT-02012, BT-02014 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La etapa `b` construye la cáscara del front y la `g` la visualización y el árbol; ninguna de las dos toca entidades, invariantes ni transiciones. Lo que este proyecto de código aporta a la visualización —la identidad posicional de la pieza— se construye en la etapa `f`, con US-02011, porque es parte de la adopción del conjunto de piezas y no del dibujo (`02` §5.2, cobertura parcial de NB-00006).

## 3. Historias por épica

Las **veintisiete** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**, que es lo que esa sección declara que le corresponde. Ninguna historia se agrega, ninguna se retira y ninguna se renumera. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-02001 · Esqueleto ambulante y verificación de viabilidad

Sin historias. La etapa `a` es un hito interno sin capacidad funcional asociada, y todo su trabajo en este proyecto de código es técnico: vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-02001 a BT-02005. Declararlo acá evita que se lea como un hueco de cobertura.

### 3.2 EP-02002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02007](historias-usuario/US-02007-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md) | Reemplazar la credencial derivada exigiendo la vigente | Must | Sin fijar (§4.1) | Propuesta | CU-02003 | EP-02002 |
| [US-02008](historias-usuario/US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | Evaluar la admisibilidad de la cuenta y devolver su motivo | Must | Sin fijar (§4.1) | Propuesta | CU-02004 | EP-02002 |
| [US-02024](historias-usuario/US-02024-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | Configurar la cuenta de administrador en el primer arranque, habilitada y con credencial | Must | Sin fijar (§4.1) | Propuesta | CU-02012 | EP-02002 |
| [US-02025](historias-usuario/US-02025-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md) | Rechazar la configuración de un segundo administrador | Must | Sin fijar (§4.1) | Propuesta | CU-02012 | EP-02002 |

### 3.3 EP-02003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02001](historias-usuario/US-02001-Constituir-Un-Alumno-Con-Cuenta-Pendiente-Y-Sin-Credencial.md) | Constituir un alumno con cuenta `Pendiente` y sin credencial | Must | Sin fijar (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02002](historias-usuario/US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md) | Rechazar el alta con datos obligatorios ausentes | Must | Sin fijar (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02003](historias-usuario/US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md) | Exigir la unicidad del correo verificada en el alta | Must | Sin fijar (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02004](historias-usuario/US-02004-Habilitar-Bloquear-Y-Rehabilitar-Una-Cuenta.md) | Habilitar, bloquear y rehabilitar una cuenta | Must | Sin fijar (§4.1) | Propuesta | CU-02002 | EP-02003 |
| [US-02005](historias-usuario/US-02005-Dar-De-Baja-Una-Cuenta-Arrastrando-Sus-Trabajos.md) | Dar de baja una cuenta arrastrando sus trabajos en cualquier estado | Must | Sin fijar (§4.1) | Propuesta | CU-02002 | EP-02003 |
| [US-02006](historias-usuario/US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion.md) | Fijar la credencial derivada provisoria en el acto de habilitación | Must | Sin fijar (§4.1) | Propuesta | CU-02003, CU-02002 | EP-02003 |
| [US-02026](historias-usuario/US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md) | Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos | Must | Sin fijar (§4.1) | Propuesta | CU-02013 | EP-02003 |
| [US-02027](historias-usuario/US-02027-Exigir-El-Cambio-De-La-Provisoria-Antes-De-Toda-Otra-Capacidad.md) | Exigir el cambio de la contraseña provisoria antes de toda otra capacidad, y levantar la marca al cambiarla | Must | Sin fijar (§4.1) | Propuesta | CU-02004, CU-02003 | EP-02003 |

### 3.4 EP-02004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02009](historias-usuario/US-02009-Constituir-Un-Trabajo-Con-Dueno-Identidad-Y-Texto-Original.md) | Constituir un trabajo con dueño, identidad propia y texto original | Must | Sin fijar (§4.1) | Propuesta | CU-02005 | EP-02004 |
| [US-02010](historias-usuario/US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md) | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | Must | Sin fijar (§4.1) | Propuesta | CU-02005 | EP-02004 |
| [US-02018](historias-usuario/US-02018-Resolver-La-Pertenencia-De-Un-Trabajo-A-Su-Dueno.md) | Resolver la pertenencia de un trabajo a su dueño | Must | Sin fijar (§4.1) | Propuesta | CU-02009 | EP-02004 |
| [US-02019](historias-usuario/US-02019-Acotar-Al-Borrador-Lo-Que-El-Alumno-Reedita-Y-Elimina.md) | Acotar al estado `Borrador` lo que el alumno reedita y elimina | Must | Sin fijar (§4.1) | Propuesta | CU-02009 | EP-02004 |
| [US-02022](historias-usuario/US-02022-Excluir-Los-Borradores-Del-Alcance-Del-Administrador.md) | Excluir los trabajos en `Borrador` del alcance del administrador | Must | Sin fijar (§4.1) | Propuesta | CU-02011 | EP-02004 |

### 3.5 EP-02005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02011](historias-usuario/US-02011-Reconstruir-El-Conjunto-De-Piezas-Con-Identidad-Posicional.md) | Reconstruir el conjunto de piezas con identidad posicional | Must | Sin fijar (§4.1) | Propuesta | CU-02006 | EP-02005 |
| [US-02012](historias-usuario/US-02012-Derivar-La-Familia-Plana-O-Volumetrica-Desde-El-Tipo.md) | Derivar la familia plana o volumétrica desde el tipo | Should | Sin fijar (§4.1) | Propuesta | CU-02006 | EP-02005 |
| [US-02013](historias-usuario/US-02013-Registrar-Advertencias-Con-El-Valor-Declarado-Y-El-Derivado.md) | Registrar advertencias con el valor declarado y el derivado | Must | Sin fijar (§4.1) | Propuesta | CU-02007 | EP-02005 |
| [US-02014](historias-usuario/US-02014-Registrar-Errores-De-Validacion-Con-Posicion-De-Pieza-Y-Campo.md) | Registrar errores de validación con posición de pieza y campo | Must | Sin fijar (§4.1) | Propuesta | CU-02007 | EP-02005 |
| [US-02015](historias-usuario/US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md) | Enviar un trabajo que verifica y pasa a estado `Pendiente` | Must | Sin fijar (§4.1) | Propuesta | CU-02008 | EP-02005 |
| [US-02016](historias-usuario/US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador.md) | Enviar un trabajo que no verifica y queda en `Borrador` con sus errores | Must | Sin fijar (§4.1) | Propuesta | CU-02008 | EP-02005 |
| [US-02017](historias-usuario/US-02017-Rechazar-Toda-Transicion-Desde-Un-Estado-Terminal.md) | Rechazar toda transición desde un estado terminal | Must | Sin fijar (§4.1) | Propuesta | CU-02008 | EP-02005 |

### 3.6 EP-02006 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02020](historias-usuario/US-02020-Aprobar-Un-Trabajo-En-Estado-Pendiente.md) | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-02010 | EP-02006 |
| [US-02021](historias-usuario/US-02021-Rechazar-Un-Trabajo-En-Estado-Pendiente.md) | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-02010 | EP-02006 |
| [US-02023](historias-usuario/US-02023-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md) | Eliminar por el administrador en los tres estados que ve | Must | Sin fijar (§4.1) | Propuesta | CU-02011 | EP-02006 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 26 | 96,3 % | Sin fijar (§4.1) |
| Should | 1 | 3,7 % | Sin fijar (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **27** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 27 de 27 |
| Historias cerradas | 0 de 27 |
| Porcentaje cerrado | 0 % |
| Tareas técnicas declaradas | 16 |
| Tareas técnicas cerradas | 0 de 16 |
| Etapas del producto que este proyecto de código toca | 6 de las 8 comprometidas (`a`, `c`, `d`, `e`, `f`, `h`) |
| Deuda declarada en el backlog | 4 tareas técnicas que cierran un punto abierto: BT-02002, BT-02003, BT-02015 y BT-02016 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance del producto se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1); esta tabla mide sólo el estado de este backlog.

### 4.1 Por qué la unidad de estimación queda abierta

La regla de la categoría exige declarar una técnica de estimación y mantenerla. **Este backlog no la fija, y lo declara en lugar de inventarla.**

El intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas** (`Roadmap-Producto.md` §1.1, que lo cita de `PRODUCT-INTAKE` §10). No hay historial de iteraciones cerradas del que derivar una velocidad, no hay iteraciones —la unidad de planificación es la **etapa**, no el sprint (`Roadmap-Producto.md` §1.2)— y el equipo es de **una sola persona** (`PRODUCT-INTAKE` §2, `equipo_n = 1`). Poner puntos de historia o tallas acá produciría números que ninguna fuente sostiene y que la categoría 07 tomaría como capacidad.

En consecuencia: la columna `Estimación` dice **«Sin fijar»** en las veintisiete historias y en las dieciséis tareas técnicas, y la decisión de si alguna vez se estima queda como punto abierto `PA-01` de §6. Lo que sí se declara y se usa para ordenar es la **etapa** de cada ítem, que es la unidad que el producto sí tiene.

### 4.2 Por qué la distribución MoSCoW es la que es

La regla de la categoría marca como anti-patrón que todo sea `Must`. Este backlog queda en **26 `Must` sobre 27**, y el motivo es del alcance del producto y no de una falta de priorización:

1. **La prioridad la declara el Product Owner en el intake, y esta categoría no reprioriza** (`Rules-Plan-Sprint.md` §1.3 declara esa división de titularidad para AG-06). `PRODUCT-INTAKE` §4 declara **diecinueve** de sus **veintiséis** capacidades como `Must Have` —**dieciocho** hasta el 2026-08-10, y `F-13` desde que la versión **1.19** de esa fuente la promovió—.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no tocan este proyecto de código.** Son **siete** desde el 2026-08-10, y no ocho: F-14 es del despliegue, F-15 a F-17 son de etapa `i…` y F-18 a F-20 están fuera del alcance de la primera versión. Ninguna de esas siete baja a entidades ni a invariantes del dominio. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Para este proyecto de código el cambio es sólo de exactitud del enunciado —F-13 es de la visualización y nunca bajó acá—, pero el enunciado hay que corregirlo igual, porque contar a una `Must Have` entre las capacidades de prioridad menor es una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-02012**, y lo es porque su origen no es una capacidad sino una decisión técnica pre-tomada del intake (§17.1.P.11 punto 4, la familia plana o volumétrica derivada del tipo por tabla de consulta). El dominio funciona sin ella; lo que se pierde es una derivación de conveniencia.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.** Si una etapa aprieta, lo que se difiere no es una historia `Should` sino una etapa entera, y las etapas son secuenciales y con punto de control bloqueante (`Roadmap-Producto.md` §4 y §5.1). El ejercicio de recorte existe, pero su unidad es la etapa.

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-02010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-02012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11), `US-02016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-02023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-00030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa y antes de escribir la primera línea de código. La cadencia por sprint de la regla no aplica: no hay sprints, la unidad es la etapa (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Al cerrar la etapa, sobre las historias de la siguiente, dentro de la preparación del punto de control |
| Responsable | La única persona del equipo, con el papel de AG-06. Con `equipo_n = 1` no hay dos papeles que negociar, y por eso el filtro real de calidad es la Definition of Ready y no el acuerdo entre personas |
| Formato | Revisión de la historia contra su caso de uso de 02 y contra el componente de `05` §3.1 que la sostiene. **Sin estimación relativa**, por §4.1 |
| Entrada obligatoria a la sesión | Los puntos abiertos de `05` §11 que la etapa cierra, y las condiciones de error del catálogo de 03 que la etapa produce |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación.** Ninguna fuente da base para puntos de historia ni para tallas, por lo declarado en §4.1. Queda por decidir si se adopta alguna al cerrarse las primeras etapas, cuando ya haya historia real, o si el producto se planifica siempre por etapa | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c`, primera etapa con carga funcional de este proyecto de código |
| PA-02 | **Los nombres de tipos y de espacios de nombres**, que el intake deja abiertos y ata al punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11; `05` §11 PA-01). Este backlog no los resuelve: los convierte en trabajo, BT-02002 | El Product Owner en el punto de control de la etapa `a` | Etapa `a` |
| PA-03 | **La herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación (`PRODUCT-INTAKE` §17.1.P.7; `05` §11 PA-04). Convertido en trabajo como BT-02003 | El equipo en la etapa `a` | Etapa `a` |
| PA-04 | **Los dos valores rotulados [ASUNCIÓN]** de `05` §8 —tiempo de la batería de pruebas y cobertura mínima—, pendientes de confirmación en `PRODUCT-INTAKE` §22 (asunción `A-3` para la cobertura y `A-5` para el tiempo). Convertido en trabajo como BT-02015 | El Product Owner sobre su propio documento | Antes de fijar la puerta de cobertura en 09 |
| PA-05 | **La ambigüedad del intake sobre RN-02012 e INV-09** (`05` §11 PA-03, `02` §4). Este backlog hereda la lectura de 02 y no la resuelve; ninguna historia depende de cuál lectura rija | El Product Owner sobre `PRODUCT-INTAKE` §17.1.P.2 | Sin fecha comprometida |
| PA-06 | **El criterio de comparación de dos correos** (`02` §9). Convertido en trabajo como BT-02016 | `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación | Antes de cerrar la etapa `d` |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Domain`. Declara las **seis** épicas como partición de las etapas del producto que este proyecto de código toca, con el nombre de épica candidata que el roadmap ya había declarado para cada una, y las dos etapas que no producen épica con su motivo. Confirma y redacta las **veintisiete** historias que la categoría 02 previó, con el mismo identificador y el mismo contenido, cada una en su archivo bajo `historias-usuario/` por superar el umbral de veinte. Declara la unidad de estimación como **punto abierto** en lugar de inventarla, con el fundamento de que el intake no fija plazo calendario y de que la unidad de planificación del producto es la etapa. Declara la distribución MoSCoW de 26 `Must` sobre 27 con su fundamento y con el recorte por etapa como reemplazo del recorte por prioridad. Fija el refinamiento por etapa y deja seis puntos abiertos, cuatro de ellos convertidos en tareas técnicas del backlog técnico. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra el hallazgo `D-06-03`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Ninguna historia de este backlog cambia de prioridad**: `F-13` es de la visualización y no baja a este proyecto de código, de modo que el reparto MoSCoW de §4 no se toca. **§4.2**: la enumeración de capacidades de prioridad menor del punto 2 pasa de **ocho a siete** y deja de incluir a `F-13`, porque contar a una `Must Have` entre las de prioridad menor es una afirmación falsa sobre la fuente; el punto 1 pasa de **dieciocho a diecinueve** capacidades `Must Have`. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», que declara lo que hasta ahora estaba implícito —el recuento de los siete proyectos de código, contado de nuevo sobre las fichas y los índices inline: **175 `Must`, 4 `Should` y 1 `Could`** sobre 180— y lo explica sin forzar ninguna redistribución: como las **diecinueve** capacidades del tramo comprometido son hoy todas `Must Have`, toda historia no-`Must` tiene que venir o de una capacidad de la fase `i…` o de una decisión propia de las categorías 02 o 05, y se enumeran las cinco una por una. Se declara además la consecuencia: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog** y la reemplaza íntegramente el orden de etapas. Sube minor. |
