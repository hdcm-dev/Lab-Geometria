# Product Backlog — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + API Product Owner (AG-06)
**Tipo de proyecto de código (D8):** `rest-api`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.3 §3 (las **cinco** responsabilidades más la demostración), §4 (lo que decide y lo que sólo transporta), §5 (los **doce** casos de uso), §6 (las **dieciséis** reglas y dónde se ejerce cada una), §7.1 (matriz NB → CU → RN → US), §7.3 (las **treinta** historias previstas), §7.4 (los **once** casos de uso de la capa de aplicación orquestados) y §11 (los **once** puntos abiertos); [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) (los **quince** puntos de acceso y los **diez** códigos de respuesta); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1 (los **ocho** componentes), §3.4 (los quince puntos contra la guardia), §8 (los **diecisiete** NFR), §9 (los **nueve** riesgos) y §11 (sus **diez** filas: nueve abiertas y una resuelta); [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4, §13, §14, §15, §16.1, §17.5, §18 y §20; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §3, §4 y §5
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa ser el proyecto de código principal para este backlog](#11-qué-significa-ser-el-proyecto-de-código-principal-para-este-backlog)
  - [1.2 Qué es una historia en la frontera del proceso](#12-qué-es-una-historia-en-la-frontera-del-proceso)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-02 · Identidad del administrador y sesión](#32-ep-02--identidad-del-administrador-y-sesión)
  - [3.3 EP-03 · Ciclo de vida de la cuenta de alumno](#33-ep-03--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.4 EP-04 · Gestión del trabajo](#34-ep-04--gestión-del-trabajo)
  - [3.5 EP-05 · Interpretación y verificación del dato del alumno](#35-ep-05--interpretación-y-verificación-del-dato-del-alumno)
  - [3.6 EP-06 · Desenlace de la entrega](#36-ep-06--desenlace-de-la-entrega)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **doce** contratos de uso de `GeometriaFactory-Api`, el **proyecto de código principal** del producto: los **quince** puntos de acceso de su superficie, la guardia que los admite, las **dos** traducciones, la composición de raíz, el arranque y la colección de peticiones reproducible.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **seis** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una.

### 1.1 Qué significa ser el proyecto de código principal para este backlog

`02` §1 lo declara: es el **nivel 3**, el último del orden topológico, y **el único de los siete que ensambla a los demás**. Cuatro consecuencias operativas:

1. **Ninguna historia se puede cerrar antes que las tres capas que ensambla.** Dentro de cada etapa, el trabajo de este proyecto de código va **último**: un caso de uso que no exista en la capa de aplicación es un punto de acceso que acá no se puede exponer.
2. **Su verificación está invertida a propósito.** La pirámide declarada es **60 % de integración y 40 % unitarias**, porque **lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo** (`PRODUCT-INTAKE` §17.5.P.6).
3. **Es la única puerta.** Un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio: **todo lo que este proyecto de código no exponga, no existe para nadie de afuera**.
4. **Es donde dos reglas de negocio se rompen hacia afuera sin que ninguna capa de adentro se entere**: `RN-03`, eligiendo un código de respuesta que confirma la existencia de un recurso ajeno, y `RN-13`, dejando un punto de acceso fuera de la guardia. Por eso dos tareas técnicas de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) son inspecciones **en las dos direcciones** y no funcionalidades.

### 1.2 Qué es una historia en la frontera del proceso

- **El rol de las treinta historias es el mismo**: el **código de `GeometriaFactory-Web`, servidor a servidor**. El alumno y el administrador aparecen como sujetos de las reglas y nunca como actores, porque **el navegador nunca alcanza esta superficie** (`RA-01`).
- **Ninguna historia decide qué se dice.** `02` §4 lo enuncia en una línea: **esta capa decide cómo se dice, y no decide qué se dice**. Una historia que decidiera un estado, una admisibilidad o qué campos cruzan la frontera estaría mal ubicada.
- **Ninguna historia acuña un código del contrato.** Los códigos son los **quince vivos** del conjunto cerrado de `GeometriaFactory-Contracts` —sobre **dieciocho** identificadores emitidos, tres retirados y **ninguno reciclado**—, y esta capa **no agrega, no renombra y no traduce a texto** ninguno.
- **Tres ausencias son declaradas y no olvidos**: **no hay intercambio de origen cruzado**, **no hay canal bidireccional** y **no hay ningún punto de acceso pensado para que lo invoque un navegador**. Las tres salen de `RA-01`.
- **Y hay una historia que no implementa nada: demuestra.** US-30 es la colección de peticiones reproducible, que `PRODUCT-INTAKE` §16.1 declara como la forma de demostración de este tipo de proyecto de código.

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-01 | Esqueleto ambulante y verificación de viabilidad | `a` | La composición de raíz conecta los cuatro puertos, el arranque prepara el almacén en dos fases y el punto de salud responde sin exigir acceso. **`PT-04` se mide acá**, y se verifica que **la sesión interactiva del front no llega hasta acá** | US-26, US-27, US-28, US-29 | BT-01 a BT-06 |
| EP-02 | Identidad del administrador y sesión | `c` | El canje de credenciales, la guardia de admisión sobre los once puntos que exigen acceso, los puntos de alta y de credencial propia, y las **dos** traducciones con su tabla única | US-01, US-02, US-03, US-04, US-05, US-08, US-10, US-24, US-25 | BT-07 a BT-16 |
| EP-03 | Ciclo de vida de la cuenta de alumno | `d` | El gobierno de la comisión, el reseteo que devuelve la provisoria **una sola vez y no la registra**, y la guardia del cambio de contraseña pendiente sobre todos los puntos salvo uno | US-06, US-07, US-09, US-11, US-12, US-13, US-14, US-15, US-16 | BT-11, BT-12, BT-17 |
| EP-04 | Gestión del trabajo | `e` | Los cinco puntos sobre trabajos: el texto que **no se normaliza en el borde**, la eliminación con sus dos alcances, el listado sin parámetro para pedir borradores y el detalle | US-19, US-20, US-21, US-22 | BT-18, BT-23, BT-24 |
| EP-05 | Interpretación y verificación del dato del alumno | `f` | El envío y el reenvío, que **responden con éxito** transportando el estado que la interpretación decidió | US-17, US-18 | BT-18, BT-22 |
| EP-06 | Desenlace de la entrega | `h` | El punto de desenlace con su terminalidad, y la colección de peticiones reproducible, que incluye la aprobación y el rechazo | US-23, US-30 | BT-19, BT-20, BT-21 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La `b` construye la cáscara del front y no agrega ningún punto de acceso. La `g` integra la visualización y el árbol, y **todo lo que esa etapa necesita de esta superficie ya está expuesto en la `e`**: el punto de detalle devuelve piezas, componentes y texto original desde entonces, y el dibujo ocurre del otro lado de la frontera, en el navegador. Agregar una épica en `g` habría creado trabajo que no existe.

## 3. Historias por épica

Las **treinta** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó —`US-01` a `US-30`, sin huecos—, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-26](historias-usuario/US-26-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion.md) | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | Must | Sin fijar (§4.1) | Propuesta | CU-10 | EP-01 |
| [US-27](historias-usuario/US-27-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md) | Aplicar las transformaciones de esquema al arrancar, sobre almacén inexistente | Must | Sin fijar (§4.1) | Propuesta | CU-11 | EP-01 |
| [US-28](historias-usuario/US-28-Detener-El-Arranque-En-Lugar-De-Atender-Sobre-Un-Almacen-Dudoso.md) | Detener el arranque en lugar de atender peticiones sobre un almacén en el que no se puede confiar | Must | Sin fijar (§4.1) | Propuesta | CU-11 | EP-01 |
| [US-29](historias-usuario/US-29-Responder-Por-El-Estado-Del-Servicio-Sin-Exigir-Acceso.md) | Responder por el estado del servicio en un punto que no exige acceso | Must | Sin fijar (§4.1) | Propuesta | CU-11 | EP-01 |

### 3.2 EP-02 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-01](historias-usuario/US-01-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) | Canjear correo y contraseña por un acceso firmado con sus cuatro reclamos | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-02 |
| [US-02](historias-usuario/US-02-Responder-Credenciales-Invalidas-Sin-Declarar-Que-Campo-Fallo.md) | Responder credenciales inválidas **sin declarar cuál de los dos campos falló** | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-02 |
| [US-03](historias-usuario/US-03-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md) | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-02 |
| [US-04](historias-usuario/US-04-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md) | Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-02 |
| [US-05](historias-usuario/US-05-Exigir-El-Papel-Declarado-Por-Cada-Punto-De-Acceso.md) | Exigir el papel declarado por cada punto de acceso | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-02 |
| [US-08](historias-usuario/US-08-Configurar-La-Cuenta-De-Administrador-Solo-Mientras-No-Exista-Ninguna.md) | Configurar la cuenta de administrador sólo mientras no exista ninguna | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-02 |
| [US-10](historias-usuario/US-10-Cambiar-La-Contrasena-Propia-Exigiendo-La-Vigente.md) | Cambiar la contraseña propia exigiendo la vigente | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-02 |
| [US-24](historias-usuario/US-24-Traducir-Cada-Codigo-Del-Contrato-Al-Codigo-De-Respuesta.md) | Traducir cada código del contrato al código de respuesta que le corresponde | Must | Sin fijar (§4.1) | Propuesta | CU-09 | EP-02 |
| [US-25](historias-usuario/US-25-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md) | Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor | Must | Sin fijar (§4.1) | Propuesta | CU-09 | EP-02 |

### 3.3 EP-03 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06](historias-usuario/US-06-Aplicar-La-Guardia-Del-Cambio-Pendiente-A-Todos-Los-Puntos-Salvo-Uno.md) | Aplicar la guardia del cambio de contraseña pendiente a todos los puntos salvo uno | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-07](historias-usuario/US-07-Registrar-Una-Cuenta-De-Alumno-Sin-Campo-De-Contrasena.md) | Registrar una cuenta de alumno sin campo de contraseña | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-03 |
| [US-09](historias-usuario/US-09-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md) | Cambiar la contraseña propia con la provisoria como vigente | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-03 |
| [US-11](historias-usuario/US-11-Listar-Las-Cuentas-De-La-Comision-Con-Su-Situacion-Y-Su-Marca.md) | Listar las cuentas de la comisión con su situación y su marca | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-03 |
| [US-12](historias-usuario/US-12-Cambiar-La-Situacion-De-Una-Cuenta-Con-Verificacion-De-Papel.md) | Cambiar la situación de una cuenta con verificación de papel | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-03 |
| [US-13](historias-usuario/US-13-Dar-De-Baja-Transportando-El-Correo-Escrito-Como-Confirmacion.md) | Dar de baja una cuenta transportando el correo escrito como confirmación | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-03 |
| [US-14](historias-usuario/US-14-Resetear-La-Contrasena-Y-Devolver-La-Provisoria-Una-Sola-Vez.md) | Resetear la contraseña de un alumno y devolver la provisoria **una sola vez** | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-03 |
| [US-15](historias-usuario/US-15-No-Exigir-Ni-Comprobar-La-Situacion-De-La-Cuenta-Al-Resetear.md) | No exigir ni comprobar la situación de la cuenta al resetear | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-03 |
| [US-16](historias-usuario/US-16-No-Registrar-La-Provisoria-En-Ninguna-Traza.md) | No registrar la provisoria en ninguna traza | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-03 |

### 3.4 EP-04 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-19](historias-usuario/US-19-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md) | Transportar el texto original **sin normalizarlo en el borde** | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-04 |
| [US-20](historias-usuario/US-20-Eliminar-Un-Trabajo-Con-Los-Dos-Alcances-Forzando-La-Peticion.md) | Eliminar un trabajo con los dos alcances, verificado **forzando la petición** | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-04 |
| [US-21](historias-usuario/US-21-Listar-Trabajos-Sin-Parametro-Para-Pedir-Borradores-Ajenos.md) | Listar trabajos con el alcance ya decidido y sin parámetro para pedir borradores ajenos | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-04 |
| [US-22](historias-usuario/US-22-Devolver-El-Detalle-Con-Piezas-Componentes-Observaciones-Y-Comentario.md) | Devolver el detalle con piezas, componentes, observaciones y comentario | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-04 |

### 3.5 EP-05 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-17](historias-usuario/US-17-Enviar-Un-Trabajo-Nuevo-Y-Recibir-El-Estado-Que-La-Interpretacion-Decidio.md) | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-05 |
| [US-18](historias-usuario/US-18-Reenviar-Un-Trabajo-En-Borrador-Con-El-Texto-Que-La-Persona-Volvio-A-Pegar.md) | Reenviar un trabajo en `Borrador` con el texto que la persona volvió a pegar | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-05 |

### 3.6 EP-06 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-23](historias-usuario/US-23-Aprobar-O-Rechazar-Un-Trabajo-En-Estado-Pendiente.md) | Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-06 |
| [US-30](historias-usuario/US-30-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible.md) | Ejercitar la superficie con una colección reproducible en cinco pasos o menos | **Should** | Sin fijar (§4.1) | Propuesta | CU-12 | EP-06 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 29 | 96,7 % | Sin fijar (§4.1) |
| Should | 1 | 3,3 % | Sin fijar (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **30** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 30 de 30 |
| Historias cerradas | 0 de 30 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **30 de 30**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Puntos de acceso que las historias ponen en pie | **15 de 15**, con el reparto de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §4. `A-04` está **retirado y no se recicla** |
| Puntos bajo la guardia | 11 de 15; los otros cuatro son ausencias declaradas y contables |
| Tareas técnicas declaradas | 26 |
| Tareas técnicas cerradas | 0 de 26 |
| Etapas del producto que este proyecto de código toca | 6 de las 8 comprometidas: `a`, `c`, `d`, `e`, `f` y `h` |
| Deuda declarada en el backlog | **8** tareas técnicas que cierran o elevan un punto abierto: BT-05, BT-07, BT-09, BT-10, BT-15, BT-21, BT-25 y BT-26. Son ocho sobre los diez puntos abiertos de §6: `PA-01` no se convierte en trabajo, y `PA-03` y `PA-04` comparten una sola tarea, BT-15 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1).

### 4.1 Por qué la unidad de estimación queda abierta

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por los mismos tres motivos que los proyectos de código ya emitidos: sin plazo calendario y avance por etapas cerradas; la **etapa** como unidad de planificación; y `equipo_n = 1`.

**Y hay un motivo propio, que en este proyecto de código es el más pesado del producto**: de los **diecisiete** requerimientos no funcionales de `05` §8, **cinco vienen rotulados [ASUNCIÓN]** desde el intake y siguen pendientes de confirmación —latencia, caudal, arranque en frío, cobertura y **la forma misma de la pirámide de pruebas**—. Es la mayor concentración de valores sin confirmar de los siete proyectos de código. Un backlog que usa cinco números vigentes sin respaldo, y que además inventara puntos de historia, tendría seis.

En consecuencia la columna `Estimación` dice **«Sin fijar»** en las treinta historias y en las veintiséis tareas técnicas, y la decisión queda como `PA-01` de §6.

### 4.2 Por qué la distribución MoSCoW es la que es

**29 `Must` y 1 `Should`**:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** Todas las capacidades que bajan a esta superficie son `Must Have`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no bajan acá.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue real de la fase `i…`, `F-15` a `F-17` son de esa misma fase y `F-18` a `F-20` están fuera del alcance de la primera versión. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Esta capa no dibuja y F-13 no baja acá con ninguna de las dos prioridades, pero contarla entre las de prioridad menor sería una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-30**, la colección de peticiones reproducible. Y lo es porque **su origen no es una capacidad de `PRODUCT-INTAKE` §4 sino la estrategia de demostración de §16.1 y §18**: es un artefacto que vive en el árbol de muestras del repositorio y **no implementa nada, demuestra**. `02` §7.2 lo declara con todas las letras y por eso `CU-12` **no traza a ninguna necesidad de negocio**. El producto funciona sin ella; lo que se pierde es la forma de demostración que el tipo de proyecto de código tiene declarada.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.**

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

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-10` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-12` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11), `US-16` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-23` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-30` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | **Cada vez que la etapa agrega un punto de acceso**, con la lista de los quince y la guardia sobre la mesa. Es la sesión que mitiga el defecto característico de esta capa, que es **de omisión** |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra el **punto de acceso** de `Definicion-Superficie-HTTP.md` §3 que la realiza, contra el componente de `05` §3.1 que la aloja y contra la tabla de traducción de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 |
| Entrada obligatoria a la sesión | Los **quince** puntos de acceso con su columna de guardia, los **quince** códigos vivos del contrato con su destino, y las **tres** familias deliberadamente empobrecidas |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Dos reglas propias de este refinamiento.** La primera: **todo punto de acceso nuevo se compara contra la lista de la guardia antes de escribirse**, porque `05` §9 declara con probabilidad **alta** que un punto quede fuera de ella y **nada falle**; los defectos de omisión no se ven leyendo el punto nuevo. La segunda: **toda respuesta de fallo se compara contra su vecina**, porque las **tres** familias deliberadamente empobrecidas —credenciales inválidas, recurso que no se ve y correo ya registrado— **dicen menos de lo que el servicio sabe, y en las tres es la decisión y no el defecto**.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c` |
| PA-02 | **Las rutas y los verbos definitivos** de los quince puntos de acceso (`05` §11 `PA-01`). Las **dos** únicas cosas que una fuente declara son el punto de canje, con su ruta, y la **existencia** de un punto de salud, cuya ruta la fuente no da; las quince filas son **propuesta derivada rotulada fila por fila**. Convertido en trabajo como BT-07 | El equipo en el punto de control de la etapa `a` | Etapa `a` |
| PA-03 | **Qué código del contrato recibe una operación de administrador pedida por quien no lo es**, fuera del desenlace (`05` §11 `PA-02`). El conjunto cerrado tiene **un solo** código de facultad y su enunciado está acotado al desenlace. Esta categoría **no inventa un código**: usa el genérico y **declara el hueco**. Convertido en trabajo como BT-15 | El Product Owner, y `GeometriaFactory-Contracts` si decide ampliar su conjunto cerrado | Sin fecha comprometida |
| PA-04 | **Qué código del contrato recibe un envío o una reedición forzados fuera de `Borrador`** (`05` §11 `PA-03`). Misma situación y misma salida que `PA-03`. Convertido en trabajo como BT-15 | El Product Owner, y `GeometriaFactory-Contracts` | Sin fecha comprometida |
| PA-05 | **La vigencia exacta del acceso firmado** (`05` §11 `PA-04`). El intake declara «corta» y sin acceso de refresco, y **no fija número**; la ADR correspondiente fija el **criterio** y toma el número de configuración. Convertido en trabajo como BT-10 | El equipo en la etapa `a`, y el Product Owner si quisiera fijarlo | Etapa `a` |
| PA-06 | **El valor del límite de tamaño del cuerpo de una petición** (`05` §11 `PA-05`). La **forma** ya está decidida y no se reabre —un solo límite para todo el producto, tomado de configuración, que **rechaza y nunca trunca**—; lo que falta es el número. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá**. Convertido en trabajo como BT-09 | El equipo en la etapa `a`, y el Product Owner si quisiera un valor propio | Etapa `a` |
| PA-07 | **El alcance de la colección de peticiones** (`05` §11 `PA-06`). La fuente lo declara en **dos lugares con alcances distintos** —§16.1 con los **ocho** escenarios y §18 `S-2` con **dos**—, los dos textos están al día y **la fuente no declara cuál manda**. La categoría 02 adopta **los ocho** con su fundamento y esta categoría **hereda esa lectura y no la reabre**. Convertido en trabajo como BT-21 | El Product Owner, para declarar cuál alcance rige y alinear los dos lugares | Sin fecha comprometida |
| PA-08 | **Los nombres de tipos y de espacios de nombres, y las versiones exactas de los paquetes** (`05` §11 `PA-07`). Convertido en trabajo como BT-05 | El equipo en la etapa `a` | Etapa `a` |
| PA-09 | **La construcción de la imagen en destino desde el repositorio** (`05` §11 `PA-08`), que el intake rotula **[A VERIFICAR]** y exige **probar una vez antes de depender del mecanismo**. **No es una asunción de esta categoría.** Convertido en trabajo como BT-26 | `09-Devops`, midiendo | Antes de la etapa de despliegue real |
| PA-10 | **Los cinco valores rotulados [ASUNCIÓN]** de `05` §8 —latencia, caudal, arranque en frío, cobertura y forma de la pirámide—, pendientes en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-09`). Convertido en trabajo como BT-25 | El Product Owner sobre su propio documento | Antes de fijar la puerta de cobertura en 09 |

**`PA-10` de la categoría 05 no figura acá porque está resuelto**: los recuentos congelados del catálogo de condiciones de la categoría 03 quedaron corregidos en su versión 1.3 y coinciden punto por punto con lo que la categoría 05 publica. Este backlog usa los números vigentes y **no reabre** el punto.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Api`, proyecto de código principal del producto. Declara **seis** épicas como partición de las etapas que este proyecto de código toca, con el nombre de épica candidata que el roadmap §3 ya había declarado, y las dos etapas que no producen épica —`b` y `g`— con su motivo, incluido el de la `g`, cuyo aporte ya está expuesto en la `e`. Confirma y redacta las **treinta** historias que la categoría 02 previó, cada una en su archivo bajo `historias-usuario/`. Declara qué es una historia en la frontera del proceso, que **ninguna decide qué se dice**, que las **tres ausencias** de `RA-01` son declaradas y no olvidos, y que **dos reglas de negocio se rompen desde acá sin que ninguna capa de adentro se entere**. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que **cinco de los diecisiete requerimientos no funcionales ya vienen rotulados como asunción**, la mayor concentración del producto, y **US-30 como única `Should`** por ser la única historia que no implementa nada sino que demuestra. Deja **diez** puntos abiertos, ocho de ellos convertidos en tareas técnicas. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra el hallazgo `D-06-03`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Ninguna historia de este backlog cambia de prioridad**: `F-13` es de la visualización y no baja a este proyecto de código, de modo que el reparto MoSCoW de §4 no se toca. **§4.2**: la enumeración de capacidades de prioridad menor del punto 2 pasa de **ocho a siete** y deja de incluir a `F-13`. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», que declara lo que hasta ahora estaba implícito —el recuento de los siete proyectos de código, contado de nuevo sobre las fichas y los índices inline: **175 `Must`, 4 `Should` y 1 `Could`** sobre 180— y lo explica sin forzar ninguna redistribución: como las **diecinueve** capacidades del tramo comprometido son hoy todas `Must Have`, toda historia no-`Must` tiene que venir o de una capacidad de la fase `i…` o de una decisión propia de las categorías 02 o 05, y se enumeran las cinco una por una. Se declara además la consecuencia: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog** y la reemplaza íntegramente el orden de etapas. Sube minor. |
