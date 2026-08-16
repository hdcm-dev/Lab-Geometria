# Product Backlog — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §3 (los **cuatro** puertos), §4 (las **cuatro** comprobaciones transversales), §5 (los **once** casos de uso), §6 (las **dieciséis** reglas y dónde se ejerce cada una), §7.1 (matriz NB → CU → RN → US), §7.3 (las **treinta y dos** historias previstas) y §11 (los puntos abiertos); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **ocho** componentes), §8 (los **nueve** NFR), §9 (los **seis** riesgos) y §11 (los **seis** puntos abiertos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4 (capacidades y su prioridad), §4.1 (las dieciséis reglas), §15 (las **ocho** etapas comprometidas `a` a `h`, las reglas de delivery y las puertas técnicas) y §17.2 (P.1 a P.12); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §3, §4 y §5; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §3 (orden topológico)
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 1 para este backlog](#11-qué-significa-nivel-topológico-1-para-este-backlog)
  - [1.2 Qué es una historia en una capa de casos de uso](#12-qué-es-una-historia-en-una-capa-de-casos-de-uso)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-04001 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-04002 · Identidad del administrador y sesión](#32-ep-02--identidad-del-administrador-y-sesión)
  - [3.3 EP-04003 · Ciclo de vida de la cuenta de alumno](#33-ep-03--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.4 EP-04004 · Gestión del trabajo](#34-ep-04--gestión-del-trabajo)
  - [3.5 EP-04005 · Interpretación y verificación del dato del alumno](#35-ep-05--interpretación-y-verificación-del-dato-del-alumno)
  - [3.6 EP-04006 · Desenlace de la entrega](#36-ep-06--desenlace-de-la-entrega)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **once** contratos de uso de `GeometriaFactory-Application`, la capa que contiene los casos de uso del producto y los **cuatro** puertos que la infraestructura implementa. Su propósito es que en cualquier momento se pueda responder qué parte de la orquestación ya está construida y de qué etapa del producto depende esa parte.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido del producto —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance que el intake declara, **8 de 8 etapas** (§22, asunción `A-2`). Una historia de este backlog está en el MVP si la etapa que la contiene está entre esas ocho; ninguna otra prueba de pertenencia se aplica. **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **seis** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una. Formalizarlas es lo que ese documento pide de la categoría 06.

### 1.1 Qué significa nivel topológico 1 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Application` en el **nivel 1**, con una sola dependencia saliente: `GeometriaFactory-Domain`. Tres consecuencias operativas:

1. **Ninguna historia de este backlog se puede cerrar antes que la guarda de dominio que invoca.** Dentro de cada etapa, el trabajo de `GeometriaFactory-Domain` va primero: una guarda que allá no exista es una guarda que acá no se puede invocar.
2. **Su trabajo condiciona el de los niveles 2 y 3.** `GeometriaFactory-Infrastructure` implementa los cuatro puertos que esta capa declara y `GeometriaFactory-Api` los conecta; un puerto que acá no esté declarado no se puede implementar ni conectar.
3. **Ninguna historia espera a la infraestructura para poder verificarse.** El estilo de la capa está elegido precisamente para que un caso de uso entero se pueda ejercer con dobles de los cuatro puertos, **sin base de datos y sin frontera de proceso** (`05` §8, NFR de cero pruebas que tocan la base real). Eso hace que las historias de este backlog sean verificables dentro de su etapa aunque el adaptador correspondiente todavía no exista.

### 1.2 Qué es una historia en una capa de casos de uso

`GeometriaFactory-Application` no tiene pantallas y no atiende peticiones. En consecuencia:

- **El rol de las treinta y dos historias es el mismo**: el **código consumidor de la biblioteca**, que en el producto es `GeometriaFactory-Api` a través de su composición de raíz. El alumno y el administrador aparecen como **sujetos de las reglas** y nunca como actores, tal como lo declara [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §1.
- **Buena parte del valor de esta capa es una negativa bien dada.** Diez de las treinta y dos historias entregan un rechazo con su motivo, no un efecto: es lo que hace que el consumidor pueda distinguir por qué no pudo hacer algo. Sus criterios se expresan sobre el **motivo emitido** y sobre el estado que **no** cambió.
- **Ninguna historia acuña un código de condición.** Las condiciones son **36**, su fuente es [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), y `05` §8 exige la cobertura del catálogo **en las dos direcciones**: cien por ciento alcanzadas por una prueba y cero emitidas fuera del catálogo.
- **Ninguna historia enuncia una regla ni un invariante.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; acá se citan por identificador y se declara qué tramo ejerce esta capa, que es lo que `02` §6 y `05` §10.2 ya repartieron.

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-04001 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, compila con una sola dependencia saliente y sus decisiones abiertas de nombre —incluido el del cuarto puerto— quedan cerradas en el punto de control | Ninguna: la etapa `a` no tiene capacidad funcional asociada (`Roadmap-Producto.md` §2.1) | BT-04001 a BT-04006 |
| EP-04002 | Identidad del administrador y sesión | `c` | El segundo camino de alta, la consulta de admisibilidad con su motivo y el reemplazo de la credencial por la propia cuenta | US-04003, US-04007, US-04009, US-04028 | BT-04007, BT-04008, BT-04010, BT-04012, BT-04014 |
| EP-04003 | Ciclo de vida de la cuenta de alumno | `d` | Auto-registro, las cuatro operaciones de admisión, la credencial provisoria, el reseteo y la marca de cambio pendiente con su comprobación transversal | US-04001, US-04002, US-04004, US-04005, US-04006, US-04008, US-04029, US-04030, US-04031, US-04032 | BT-04010, BT-04011, BT-04012, BT-04013, BT-04014, BT-04021 |
| EP-04004 | Gestión del trabajo | `e` | El trabajo se constituye y se reedita con su texto íntegro, y las dos consultas quedan resueltas con su predicado de alcance ya aplicado | US-04010, US-04011, US-04012, US-04017, US-04019, US-04020, US-04021, US-04022, US-04026 | BT-04009, BT-04015, BT-04016 |
| EP-04005 | Interpretación y verificación del dato del alumno | `f` | El envío interpreta por el puerto y deja que el dominio resuelva el estado, con la terminación controlada cuando la interpretación no está disponible | US-04013, US-04014, US-04015, US-04016 | BT-04015, BT-04019 |
| EP-04006 | Desenlace de la entrega | `h` | Aprobar y rechazar desde el estado `Pendiente`, la eliminación por el administrador y la lectura del desenlace por el alumno | US-04018, US-04023, US-04024, US-04025, US-04027 | BT-04015, BT-04017 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La etapa `b` construye la cáscara del front y la `g` la visualización y el árbol; ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización. Lo que esta capa aporta a la visualización —la entrega de las piezas con su identidad posicional y sus componentes en el detalle— se construye en la etapa `e` con US-04019, porque es la forma del resultado de la consulta y no el dibujo (`02` §7.2, cobertura parcial de NB-00006).

## 3. Historias por épica

Las **treinta y dos** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**, que es lo que esa sección declara que le corresponde. Ninguna se agrega, ninguna se retira y ninguna se renumera. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-04001 · Esqueleto ambulante y verificación de viabilidad

Sin historias. La etapa `a` es un hito interno sin capacidad funcional asociada, y todo su trabajo en este proyecto de código es técnico: vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-04001 a BT-04006. Declararlo acá evita que se lea como un hueco de cobertura.

### 3.2 EP-04002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04003](historias-usuario/US-04003-Configurar-La-Cuenta-De-Administrador-Con-Su-Ventana-De-Alta.md) | Configurar la cuenta de administrador con su ventana de alta | Must | Sin fijar (§4.1) | Propuesta | CU-04010 | EP-04002 |
| [US-04007](historias-usuario/US-04007-Devolver-El-Motivo-De-Una-Cuenta-Que-No-Admite-Ingreso.md) | Devolver el motivo de una cuenta que no admite ingreso | Must | Sin fijar (§4.1) | Propuesta | CU-04003 | EP-04002 |
| [US-04009](historias-usuario/US-04009-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md) | Reemplazar la credencial derivada exigiendo la verificación de la vigente | Must | Sin fijar (§4.1) | Propuesta | CU-04003 | EP-04002 |
| [US-04028](historias-usuario/US-04028-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md) | Rechazar la configuración de un segundo administrador | Must | Sin fijar (§4.1) | Propuesta | CU-04010 | EP-04002 |

### 3.3 EP-04003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04001](historias-usuario/US-04001-Constituir-Una-Cuenta-De-Alumno-Pendiente-Y-Sin-Credencial.md) | Constituir una cuenta de alumno en estado `Pendiente` y sin credencial | Must | Sin fijar (§4.1) | Propuesta | CU-04001 | EP-04003 |
| [US-04002](historias-usuario/US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado.md) | Rechazar el alta con un correo ya registrado | Must | Sin fijar (§4.1) | Propuesta | CU-04001 | EP-04003 |
| [US-04004](historias-usuario/US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md) | Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad | Must | Sin fijar (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04005](historias-usuario/US-04005-Dar-De-Baja-Exigiendo-El-Correo-Escrito-Como-Confirmacion.md) | Dar de baja una cuenta exigiendo el correo escrito como confirmación | Must | Sin fijar (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04006](historias-usuario/US-04006-Arrastrar-En-La-Baja-Todos-Los-Trabajos-De-La-Cuenta.md) | Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado | Must | Sin fijar (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04008](historias-usuario/US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md) | Fijar la credencial derivada provisoria dentro de la habilitación | Must | Sin fijar (§4.1) | Propuesta | CU-04003, CU-04002 | EP-04003 |
| [US-04029](historias-usuario/US-04029-Resetear-La-Contrasena-De-Un-Alumno-Con-Verificacion-De-Facultad.md) | Resetear la contraseña de un alumno fijando una provisoria, con verificación de facultad | Must | Sin fijar (§4.1) | Propuesta | CU-04011 | EP-04003 |
| [US-04030](historias-usuario/US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad.md) | Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad | Must | Sin fijar (§4.1) | Propuesta | CU-04011, y la comprobación transversal de `02` §4 | EP-04003 |
| [US-04031](historias-usuario/US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo.md) | Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo | Must | Sin fijar (§4.1) | Propuesta | CU-04011 | EP-04003 |
| [US-04032](historias-usuario/US-04032-Levantar-La-Marca-Con-El-Cambio-Hecho-Por-La-Propia-Cuenta.md) | Levantar la marca con el cambio efectivo hecho por la propia cuenta, y sólo con él | Must | Sin fijar (§4.1) | Propuesta | CU-04003 | EP-04003 |

### 3.4 EP-04004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04010](historias-usuario/US-04010-Cargar-Un-Trabajo-Con-Dueno-Identificador-Propio-Y-Sello-Del-Reloj.md) | Cargar un trabajo con dueño, identificador propio y sello tomado del reloj | Must | Sin fijar (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04011](historias-usuario/US-04011-Conservar-El-Texto-Original-Integro-Al-Cargar-Y-Al-Reeditar.md) | Conservar el texto original íntegro al cargar y al reeditar | Must | Sin fijar (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04012](historias-usuario/US-04012-Reeditar-Solo-Un-Trabajo-Propio-En-Borrador.md) | Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior | Must | Sin fijar (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04017](historias-usuario/US-04017-Listar-Los-Trabajos-Propios-Con-Los-Cuatro-Estados-Distinguibles.md) | Listar los trabajos propios con los cuatro estados distinguibles | Must | Sin fijar (§4.1) | Propuesta | CU-04006 | EP-04004 |
| [US-04019](historias-usuario/US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md) | Devolver el detalle con piezas y componentes, y el listado sin componentes | Must | Sin fijar (§4.1) | Propuesta | CU-04006 | EP-04004 |
| [US-04020](historias-usuario/US-04020-Listar-Los-Trabajos-De-La-Comision-Excluyendo-Los-Borradores.md) | Listar los trabajos de la comisión excluyendo los borradores | Must | Sin fijar (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04021](historias-usuario/US-04021-Filtrar-El-Listado-De-La-Comision-Por-Alumno.md) | Filtrar el listado de la comisión por alumno, con el recorte vigente | Must | Sin fijar (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04022](historias-usuario/US-04022-Abrir-El-Detalle-De-Un-Trabajo-De-La-Comision.md) | Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno | Must | Sin fijar (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04026](historias-usuario/US-04026-Eliminar-Un-Trabajo-Propio-Solo-En-Borrador.md) | Eliminar un trabajo propio sólo en `Borrador` | Must | Sin fijar (§4.1) | Propuesta | CU-04009 | EP-04004 |

### 3.5 EP-04005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04013](historias-usuario/US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md) | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | Must | Sin fijar (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04014](historias-usuario/US-04014-Enviar-Un-Trabajo-Con-Errores-Y-Que-Quede-En-Borrador.md) | Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación | Must | Sin fijar (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04015](historias-usuario/US-04015-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos.md) | Interpretar el texto por el puerto de validación, sin tocar la base de datos | Must | Sin fijar (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04016](historias-usuario/US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible.md) | Terminar de forma controlada cuando la interpretación no está disponible | **Should** | Sin fijar (§4.1) | Propuesta | CU-04005 | EP-04005 |

### 3.6 EP-04006 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04018](historias-usuario/US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md) | Ver el desenlace y el comentario del trabajo propio | Must | Sin fijar (§4.1) | Propuesta | CU-04006 | EP-04006 |
| [US-04023](historias-usuario/US-04023-Aprobar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md) | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04024](historias-usuario/US-04024-Rechazar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md) | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04025](historias-usuario/US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal.md) | Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal | Must | Sin fijar (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04027](historias-usuario/US-04027-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md) | Eliminar por el administrador en los tres estados que ve | Must | Sin fijar (§4.1) | Propuesta | CU-04009 | EP-04006 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 31 | 96,9 % | Sin fijar (§4.1) |
| Should | 1 | 3,1 % | Sin fijar (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **32** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 32 de 32 |
| Historias cerradas | 0 de 32 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **32 de 32**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 21 |
| Tareas técnicas cerradas | 0 de 21 |
| Etapas del producto que este proyecto de código toca | **6** de las 8 comprometidas: `a`, `c`, `d`, `e`, `f` y `h`. La `a` es una de ellas aunque no produzca historias: su trabajo es íntegramente técnico |
| Deuda declarada en el backlog | 5 tareas técnicas que cierran o elevan un punto abierto: BT-04002, BT-04003, BT-04018, BT-04020 y BT-04021 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance del producto se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1); esta tabla mide sólo el estado de este backlog.

### 4.1 Por qué la unidad de estimación queda abierta

La regla de la categoría exige declarar una técnica de estimación y mantenerla. **Este backlog no la fija, y lo declara en lugar de inventarla**, con el mismo fundamento que los tres proyectos de código de nivel 0 ya emitidos:

1. El intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas** (`Roadmap-Producto.md` §1.1, que lo cita de `PRODUCT-INTAKE` §10).
2. **No hay iteraciones**: la unidad de planificación es la **etapa**, no el sprint (`Roadmap-Producto.md` §1.2), de modo que no hay historial del que derivar una velocidad.
3. **El equipo es de una sola persona** (`PRODUCT-INTAKE` §2, `equipo_n = 1`).

En consecuencia, la columna `Estimación` dice **«Sin fijar»** en las treinta y dos historias y en las veintiuna tareas técnicas, y la decisión de si alguna vez se estima queda como punto abierto `PA-01` de §6. Lo que sí se declara y ordena es la **etapa** de cada ítem.

**Hay un motivo propio de este proyecto de código**, y conviene decirlo: el único NFR de tiempo que lo alcanza —los **500 ms** del caso de uso más pesado— viene **rotulado como asunción** desde el intake y sigue pendiente de confirmación del Product Owner (`05` §8 y §11 `PA-05`). Un backlog que usa como vigente un número que su propia fuente no confirmó, y que además inventara puntos de historia, tendría dos números sin respaldo en lugar de uno.

### 4.2 Por qué la distribución MoSCoW es la que es

**31 `Must` y 1 `Should`**, y el motivo es del alcance del producto y no de una falta de priorización:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** `PRODUCT-INTAKE` §4 declara como `Must Have` todas las capacidades que bajan a esta capa: `F-01` a `F-12`, `F-22`, `F-23`, `F-24` y `F-26`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no tocan este proyecto de código.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue; `F-15` a `F-17` son de la fase `i…`; `F-18` a `F-20` están fuera del alcance de la primera versión. Ninguna de esas siete baja a un caso de uso de esta capa. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. No cambia nada del trabajo de esta capa —F-13 es de la visualización y de la presentación, que esta capa no toca—, pero sí cambia el enunciado, porque contar a una `Must Have` entre las de prioridad menor es una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-04016**, y lo es porque **su origen no es una capacidad sino una decisión de esta arquitectura**: `05` §4 declara que «la indisponibilidad de un puerto es una condición y no una excepción que escapa». Ninguna capacidad de §4 del intake la pide. El producto funciona sin ella —el caso de uso de envío terminaría con una excepción del consumidor— y lo que se pierde es que el texto original quede intacto y el motivo sea legible. Diferible, y no gratis.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.** Si una etapa aprieta, lo que se difiere no es una historia `Should` sino una etapa entera, y las etapas son secuenciales y con punto de control bloqueante (`Roadmap-Producto.md` §4 y §5.1).

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

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-04010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-04012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11), `US-04016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-04023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-04030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa y antes de escribir la primera línea de código. La cadencia por sprint de la regla no aplica: no hay sprints, la unidad es la etapa (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Al cerrar la etapa, sobre las historias de la siguiente, dentro de la preparación del punto de control |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su caso de uso de 02, contra el componente de `05` §3.1 que la sostiene y contra las **cuatro** comprobaciones de `02` §4. **Sin estimación relativa**, por §4.1 |
| Entrada obligatoria a la sesión | Las **cuatro** comprobaciones transversales con su orden fijo, los **cuatro** puertos y las condiciones del catálogo de 03 que la etapa produce |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega una operación que lee o escribe algo, la sesión pregunta **si la cuarta comprobación la alcanza**. `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca de cambio de contraseña pendiente, y `Domain ADR-04005` §6 ya declaró que el dominio **no puede impedirlo**. Es una dependencia de disciplina que cae acá, y el refinamiento es donde se ejerce.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1. Queda por decidir si se adopta alguna al cerrarse las primeras etapas, cuando ya haya historia real, o si el producto se planifica siempre por etapa | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c`, primera etapa con carga funcional de este proyecto de código |
| PA-02 | **El identificador del cuarto puerto**, el de repositorio de cuentas. `05` §11 `PA-01` confirma que el puerto existe, declara que su ausencia en el intake es una omisión de **nombre** y no de alcance, y ata el nombre al **punto de control de la etapa `a`**. Este backlog **no lo fija**: lo convierte en trabajo como parte de BT-04002, con esa caja temporal | El equipo en el punto de control de la etapa `a` | Etapa `a` |
| PA-03 | **Los nombres definitivos de tipos y de espacios de nombres** (`05` §11 `PA-02`). Convertido en trabajo como BT-04002 | El Product Owner y el equipo en el punto de control de la etapa `a` | Etapa `a` |
| PA-04 | **La herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación (`05` §11 `PA-06`). Convertido en trabajo como BT-04003 | El equipo en la etapa `a` | Etapa `a` |
| PA-05 | **Los dos valores rotulados [ASUNCIÓN]** de `05` §8 —los 500 ms del caso de uso más pesado y la cobertura mínima—, pendientes de confirmación en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-05`). Convertido en trabajo como BT-04018 | El Product Owner sobre su propio documento | Antes de fijar la puerta de cobertura en 09 |
| PA-06 | **Los sellos de alta, de modificación y de desenlace**: el intake los sostiene como verificables en prueba y el modelo del dominio **no los declara como atributos** (`05` §11 `PA-04`). Este backlog **no lo resuelve**: lo eleva como BT-04020 | El Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos a su modelo | Sin fecha comprometida |
| PA-07 | **El criterio de comparación de dos correos** (`05` §11 `PA-03`). **No es de este proyecto de código decidirlo**: `05` lo derivó a la categoría 05 de `GeometriaFactory-Infrastructure`, que es la que materializa el índice. Convertido en trabajo como BT-04021, que **acompaña** la decisión y no la toma | La categoría 05 de `GeometriaFactory-Infrastructure` | Antes de cerrar la etapa `d` |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Application`. Declara las **seis** épicas como partición de las etapas del producto que este proyecto de código toca, con el nombre de épica candidata que el roadmap §3 ya había declarado para cada una, y las dos etapas que no producen épica —`b` y `g`— con su motivo. Confirma y redacta las **treinta y dos** historias que la categoría 02 previó, cada una en su archivo bajo `historias-usuario/` por superar el umbral de veinte. Declara qué es una historia en una capa de casos de uso y por qué diez de ellas entregan una negativa con su motivo. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que el único NFR de tiempo que alcanza a esta capa ya viene rotulado como asunción sin confirmar. Declara la distribución MoSCoW de 31 `Must` sobre 32 con su fundamento, y **US-04016 como única `Should`** por derivar de una decisión de arquitectura y no de una capacidad. Fija el refinamiento por etapa con la regla propia de preguntar por la cuarta comprobación, y deja **siete** puntos abiertos, cinco de ellos convertidos en tareas técnicas. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra el hallazgo `D-06-03`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Ninguna historia de este backlog cambia de prioridad**: `F-13` es de la visualización y no baja a este proyecto de código, de modo que el reparto MoSCoW de §4 no se toca. **§4.2**: la enumeración de capacidades de prioridad menor del punto 2 pasa de **ocho a siete** y deja de incluir a `F-13`. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», que declara lo que hasta ahora estaba implícito —el recuento de los siete proyectos de código, contado de nuevo sobre las fichas y los índices inline: **175 `Must`, 4 `Should` y 1 `Could`** sobre 180— y lo explica sin forzar ninguna redistribución: como las **diecinueve** capacidades del tramo comprometido son hoy todas `Must Have`, toda historia no-`Must` tiene que venir o de una capacidad de la fase `i…` o de una decisión propia de las categorías 02 o 05, y se enumeran las cinco una por una. Se declara además la consecuencia: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog** y la reemplaza íntegramente el orden de etapas. Sube minor. |
