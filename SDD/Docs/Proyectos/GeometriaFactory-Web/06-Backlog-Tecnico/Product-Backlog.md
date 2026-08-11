# Product Backlog — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master (AG-06)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §2 (las tres reglas de arquitectura vistas desde acá), §3 (los **diez** casos de uso), §3.2 (la numeración local y las **treinta** historias previstas), §4 (matriz NB → CU → RN → US), §6 (las **trece** restricciones transversales) y §7 (consumo del contrato de fachada); [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md), [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) y los **once** wireframes de [`../03-UX-UI-DX/`](../03-UX-UI-DX/); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **ocho** componentes en tres capas), §3.4 (las **once** superficies), §8 (los **catorce** NFR), §9 (los **siete** riesgos) y §11 (los **siete** puntos abiertos); [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.19** §4 (con `F-13` ya `Must Have`), §15, §16.1, §17.6 y **§17.7 P.8** (lo que `PT-02` mide antes de comprometer la etapa `g`); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §3, §4 y §5
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Web

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa ser la pieza pública para este backlog](#11-qué-significa-ser-la-pieza-pública-para-este-backlog)
  - [1.2 Qué es una historia en la única superficie con actores humanos](#12-qué-es-una-historia-en-la-única-superficie-con-actores-humanos)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-02 · Navegación y sistema visual](#32-ep-02--navegación-y-sistema-visual)
  - [3.3 EP-03 · Identidad del administrador y sesión](#33-ep-03--identidad-del-administrador-y-sesión)
  - [3.4 EP-04 · Ciclo de vida de la cuenta de alumno](#34-ep-04--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.5 EP-05 · Gestión del trabajo](#35-ep-05--gestión-del-trabajo)
  - [3.6 EP-06 · Interpretación y verificación del dato del alumno](#36-ep-06--interpretación-y-verificación-del-dato-del-alumno)
  - [3.7 EP-07 · Visualización del trabajo](#37-ep-07--visualización-del-trabajo)
  - [3.8 EP-08 · Desenlace de la entrega](#38-ep-08--desenlace-de-la-entrega)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **diez** casos de uso de `GeometriaFactory-Web` y las **once** superficies que la categoría 03 diseñó y validó contra una maqueta aprobada. Es el backlog del único proyecto de código del producto cuyos casos de uso tienen **actores humanos**, y del único que puede violar las tres reglas de arquitectura del producto.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **ocho** épicas de §2 son las ocho etapas del roadmap, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una. **Este es el único de los siete proyectos de código que toca las ocho**, y la causa es directa: todo lo que la persona hace, lo hace acá.

### 1.1 Qué significa ser la pieza pública para este backlog

`02` §1 declara a este proyecto de código como **el único punto de contacto del navegador** y una de las dos unidades de entrega del producto. Cuatro consecuencias operativas:

1. **Ninguna historia se cierra sin que exista del otro lado el punto de acceso que consume.** Este proyecto de código habla con `GeometriaFactory-Api` **en tiempo de ejecución**, servidor a servidor; no depende de él por compilación, pero sí depende de que el punto exista para poder demostrar la etapa.
2. **Su verificación no es una batería de cobertura, es un guion.** `PRODUCT-INTAKE` §17.6.P.6 declara que este proyecto de código **no tiene proyecto de pruebas propio** y que su verificación es el guion de demostración de cada etapa, acumulativo por la regla de no regresión.
3. **Su categoría 03 ya está emitida y validada contra una maqueta aprobada**, con **once** superficies, **tres** representaciones reutilizadas y una línea de base visual. Este backlog **no rediseña nada**: convierte en historias lo que esa categoría ya fijó.
4. **Es el lugar donde las tres reglas de arquitectura del producto se pueden violar** (`05` §1). Por eso tres de las tareas técnicas de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) son puertas de conteo con umbral cero y no funcionalidades.

### 1.2 Qué es una historia en la única superficie con actores humanos

- **Los roles son dos, y son personas**: el **alumno** de la comisión y el **docente en su papel de administrador**. Es la diferencia con los cuatro proyectos de código de nivel 0 y 1, cuyo actor es el código consumidor.
- **Ninguna historia hace cumplir una regla de negocio, y no es una omisión.** `02` §5 lo declara: la pieza pública **no puede ser la última defensa de ninguna regla, porque el navegador no es confiable**. Ocultar un botón o no armar una ruta **acotan lo que se ofrece**; quien hace cumplir es el servicio de datos. Por eso varias historias verifican la acotación **forzando la solicitud sin pasar por la pantalla**.
- **Ninguna historia rediseña una superficie.** Las **once** superficies, sus estados y sus interacciones están en la categoría 03; lo que las historias declaran es qué puede hacer la persona y con qué criterio se verifica.
- **Ninguna historia acuña un mensaje de error.** Los códigos son los **quince vivos** del conjunto cerrado de `GeometriaFactory-Contracts` —sobre **dieciocho** identificadores emitidos, tres de ellos retirados y ninguno reciclado—, y el traductor de condiciones es el único lugar por el que un mensaje llega a la persona.

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-01 | Esqueleto ambulante y verificación de viabilidad | `a` | El front publicado arranca, consume el punto de salud del servicio de datos y las **cuatro** partes de `PT-01` quedan medidas | Ninguna: la etapa `a` no tiene capacidad funcional asociada | BT-01 a BT-06 |
| EP-02 | Navegación y sistema visual | `b` | Los **dos** shells, el mapa de rutas y las **once** superficies con marcador de posición, sobre la línea de base visual aprobada | Ninguna: la etapa `b` no tiene capacidad funcional asociada | BT-07, BT-08, BT-09, BT-10 |
| EP-03 | Identidad del administrador y sesión | `c` | El aprovisionamiento inicial, el ingreso con la credencial custodiada del lado del servidor, el cambio de contraseña propio y el estado degradado como superficie | US-03, US-04, US-05, US-06, US-08, US-26, US-27 | BT-11, BT-12, BT-13, BT-14, BT-15 |
| EP-04 | Ciclo de vida de la cuenta de alumno | `d` | El registro, el panel de cuentas con sus cinco operaciones, la provisoria comunicada y el confinamiento de la cuenta marcada | US-01, US-02, US-07, US-09, US-10, US-28, US-29, US-30 | BT-07, BT-13, BT-14 |
| EP-05 | Gestión del trabajo | `e` | La carga del trabajo con su texto intacto, el listado propio y el de la comisión | US-11, US-15, US-16, US-22, US-23 | BT-13 |
| EP-06 | Interpretación y verificación del dato del alumno | `f` | La previsualización que dibuja y no verifica, y la presentación de advertencias y errores con su ubicación | US-12, US-13, US-14 | BT-16 |
| EP-07 | Visualización del trabajo | `g` | La vista de trabajo con sus cuatro elementos, el árbol y la sincronización por índice, con el anfitrión del visor gobernando el movimiento | US-18, US-19, US-20, US-21 | BT-16, BT-17, BT-18, BT-23 |
| EP-08 | Desenlace de la entrega | `h` | El desenlace en el listado propio, la resolución con comentario opcional y el retiro por el administrador | US-17, US-24, US-25 | BT-19, BT-20 |

**Las ocho etapas comprometidas producen épica en este proyecto de código, y es el único de los siete del que se puede decir.** Las dos primeras son hitos internos sin capacidad funcional asociada y por eso no tienen historias: todo su trabajo es técnico y vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md).

## 3. Historias por épica

Las **treinta** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.2 previó —eran veintisiete y son treinta desde el `PRODUCT-INTAKE` 1.7— y las que su §4 reparte por necesidad de negocio. Esa sección **describió las tres últimas por contenido y las veintisiete anteriores sólo por identificador**; **esta categoría las numera y las redacta**, que es lo que esa sección deja a la 06, respetando fila por fila el reparto de la matriz de §4. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad

Sin historias. La etapa `a` es un hito interno: su entregable son el esqueleto ejecutable y las mediciones de `PT-01` y `PT-04`. Todo su trabajo en este proyecto de código vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-01 a BT-06.

### 3.2 EP-02 · Navegación y sistema visual

Sin historias. La etapa `b` es un hito interno: su entregable es el mapa de navegación recorrible con pantallas de marcador de posición, sobre el sistema visual adoptado. Su trabajo vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.2 como BT-07 a BT-10. **Declararlo acá evita que se lea como un hueco de cobertura**: las once superficies existen desde esta etapa, aunque todavía no hagan nada.

### 3.3 EP-03 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-03](historias-usuario/US-03-Iniciar-Sesion-Sin-Que-La-Credencial-Llegue-Al-Navegador.md) | Iniciar sesión sin que la credencial llegue al navegador | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-04](historias-usuario/US-04-Informar-El-Motivo-Cuando-La-Cuenta-No-Admite-Ingreso.md) | Informar el motivo cuando la cuenta no admite ingreso | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-05](historias-usuario/US-05-Cerrar-Sesion-Y-Acotar-Las-Rutas-Por-Papel.md) | Cerrar sesión y acotar las rutas por papel | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-06](historias-usuario/US-06-Cambiar-La-Contrasena-Propia-Presentando-La-Vigente.md) | Cambiar la contraseña propia presentando la vigente | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-03 |
| [US-08](historias-usuario/US-08-Configurar-La-Cuenta-De-Administrador-Una-Sola-Vez.md) | Configurar la cuenta de administrador una sola vez en la vida de la instancia | Must | Sin fijar (§4.1) | Propuesta | CU-04 FA-03 | EP-03 |
| [US-26](historias-usuario/US-26-Distinguir-El-Listado-Vacio-Del-Fallo-Por-El-Tipo-Recibido.md) | Distinguir el listado vacío del fallo por el tipo recibido y no por el conteo | Must | Sin fijar (§4.1) | Propuesta | CU-10 | EP-03 |
| [US-27](historias-usuario/US-27-Sostener-La-Reconexion-Y-El-Estado-Degradado-Como-Dos-Tramos.md) | Sostener la reconexión y el estado degradado como dos tramos independientes | Must | Sin fijar (§4.1) | Propuesta | CU-10 | EP-03 |

### 3.4 EP-04 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-01](historias-usuario/US-01-Registrar-La-Cuenta-Sin-Campo-De-Contrasena.md) | Registrar la cuenta con correo, nombre y apellido, sin campo de contraseña | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-04 |
| [US-02](historias-usuario/US-02-Rechazar-El-Registro-Con-Un-Correo-Ya-Usado.md) | Rechazar el registro con un correo ya usado, sin revelar de quién es | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-04 |
| [US-07](historias-usuario/US-07-Recorrer-El-Mismo-Formulario-En-Los-Tres-Cursos-De-La-Credencial.md) | Recorrer el mismo formulario de tres campos en los tres cursos de la credencial | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-09](historias-usuario/US-09-Ver-La-Lista-De-Cuentas-Y-Habilitar-Bloquear-Y-Rehabilitar.md) | Ver la lista de cuentas y habilitar, bloquear y rehabilitar, comunicando la provisoria | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-04 |
| [US-10](historias-usuario/US-10-Dar-De-Baja-Exigiendo-El-Correo-Escrito-Y-Declarando-El-Arrastre.md) | Dar de baja exigiendo el correo escrito y declarando el arrastre antes del intento | Must | Sin fijar (§4.1) | Propuesta | CU-04 FA-02 | EP-04 |
| [US-28](historias-usuario/US-28-Cambiar-La-Contrasena-Obligada-Y-Levantar-La-Marca.md) | Cambiar la contraseña obligada tras un reseteo y levantar la marca | Must | Sin fijar (§4.1) | Propuesta | CU-03 FA-04 | EP-04 |
| [US-29](historias-usuario/US-29-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md) | Confinar la cuenta con cambio pendiente a una sola ruta, sin sesión de trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-02 FA-07, CU-03 FA-05 | EP-04 |
| [US-30](historias-usuario/US-30-Resetear-La-Contrasena-Desde-El-Panel-Declarando-Que-No-Se-Pierde-Nada.md) | Resetear la contraseña desde el panel, declarando que no se pierde ningún trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-04 FA-06 | EP-04 |

### 3.5 EP-05 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-11](historias-usuario/US-11-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md) | Pegar el texto del trabajo y enviarlo sin que se reescriba un carácter | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-05 |
| [US-15](historias-usuario/US-15-Ver-Los-Trabajos-Propios-Con-Sus-Cuatro-Estados.md) | Ver los trabajos propios con sus cuatro estados distinguibles | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-05 |
| [US-16](historias-usuario/US-16-Reeditar-Y-Eliminar-Solo-En-Borrador-Sin-Dibujar-El-Control.md) | Reeditar y eliminar sólo en `Borrador`, sin dibujar el control cuando no corresponde | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-05 |
| [US-22](historias-usuario/US-22-Recorrer-La-Entrega-De-La-Comision-Agrupada-Y-Filtrada.md) | Recorrer la entrega de la comisión agrupada y filtrada por alumno | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-05 |
| [US-23](historias-usuario/US-23-No-Pedir-Los-Borradores-Y-Responder-No-Encontrado.md) | No pedir los trabajos en `Borrador` y responder «no encontrado» al pedirlos por dirección directa | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-05 |

### 3.6 EP-06 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-12](historias-usuario/US-12-Previsualizar-Antes-De-Enviar-Declarando-Que-Dibujar-No-Es-Verificar.md) | Previsualizar el trabajo antes de enviarlo, declarando que dibujar no es verificar | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-06 |
| [US-13](historias-usuario/US-13-Ver-Las-Advertencias-Con-El-Valor-Declarado-Y-El-Derivado.md) | Ver las advertencias con el valor declarado y el derivado, sin bloqueo | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-06 |
| [US-14](historias-usuario/US-14-Ver-Los-Errores-Con-Indice-De-Figura-Y-Campo.md) | Ver los errores con índice de figura y campo, con el trabajo en `Borrador` | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-06 |

### 3.7 EP-07 · Visualización del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-18](historias-usuario/US-18-Abrir-El-Trabajo-Y-Encontrar-Los-Mismos-Cuatro-Elementos.md) | Abrir el trabajo y encontrar los mismos cuatro elementos que ve el administrador | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-07 |
| [US-19](historias-usuario/US-19-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md) | Ver la lista de observaciones con su severidad y su par de valores | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-07 |
| [US-20](historias-usuario/US-20-Explorar-La-Estructura-Del-Texto-Como-Arbol-Colapsable.md) | Explorar la estructura del texto como árbol colapsable | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-07 |
| [US-21](historias-usuario/US-21-Sincronizar-El-Arbol-Y-La-Escena-Por-Indice-De-Pieza.md) | Sincronizar el árbol y la escena por índice de pieza | **Should** | Sin fijar (§4.1) | Propuesta | CU-07 | EP-07 |

### 3.8 EP-08 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-17](historias-usuario/US-17-Ver-El-Desenlace-Del-Trabajo-Propio-En-El-Listado.md) | Ver el desenlace del trabajo propio en el listado, y el comentario al abrirlo | Must | Sin fijar (§4.1) | Propuesta | CU-06, CU-07 | EP-08 |
| [US-24](historias-usuario/US-24-Aprobar-O-Rechazar-Con-Comentario-Opcional.md) | Aprobar o rechazar un trabajo en estado `Pendiente` con comentario opcional | Must | Sin fijar (§4.1) | Propuesta | CU-09 | EP-08 |
| [US-25](historias-usuario/US-25-Eliminar-Cualquier-Trabajo-Que-El-Administrador-Ve.md) | Eliminar cualquier trabajo que el administrador ve, verificado forzando la solicitud | Must | Sin fijar (§4.1) | Propuesta | CU-09 FA-03 | EP-08 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 30 | 100 % | Sin fijar (§4.1) |
| Should | 0 | 0 % | — |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **30** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-11 |
| --- | --- |
| Historias en estado `Propuesta` | 30 de 30 |
| Historias cerradas | 0 de 30 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **30 de 30**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Superficies que las historias ejercen | **11 de 11** de `03`, con el reparto de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §4 |
| Tareas técnicas declaradas | 23 |
| Tareas técnicas cerradas | 0 de 23 |
| Etapas del producto que este proyecto de código toca | **8 de las 8 comprometidas.** Es el único de los siete proyectos de código del que se puede decir |
| Deuda declarada en el backlog | **7** tareas técnicas que cierran o elevan un punto abierto: BT-02, BT-04, BT-10, BT-12, BT-21, BT-22 y BT-23. Son siete y no nueve porque `PA-01` y `PA-02` de §6 **no se convierten en trabajo**: uno es una decisión de método de este backlog y el otro era una tensión de prioridad que sólo el Product Owner podía resolver, **y que resolvió el 2026-08-10**; `PA-02` queda cerrado y su fila se conserva en §6 para no dejar hueco de numeración |

**El porcentaje cerrado no es una medida de avance del producto.** El avance se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1).

### 4.1 Por qué la unidad de estimación queda abierta

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por los mismos tres motivos que los proyectos de código ya emitidos: el intake declara **sin plazo calendario y avance medido por etapas cerradas**; la unidad de planificación es la **etapa** y no el sprint, de modo que no hay historial del que derivar una velocidad; y `equipo_n = 1`.

**Y hay un motivo propio de este proyecto de código, que es el más fuerte de todos los emitidos hasta acá**: `05` §8 declara explícitamente que **no hay umbral numérico de latencia de respuesta y que esa categoría no lo inventa**, porque las tolerancias percibidas de la categoría 03 dicen a partir de cuándo se muestra un indicador y **no cuánto puede tardar el servidor**. Un backlog que se apoya en una arquitectura que se negó a inventar su único número faltante, y que a la vez inventara puntos de historia, sería incoherente consigo mismo.

En consecuencia la columna `Estimación` dice **«Sin fijar»** en las treinta historias y en las veintitrés tareas técnicas, y la decisión queda como `PA-01` de §6.

### 4.2 Por qué la distribución MoSCoW es la que es

**30 `Must` y ninguna no-`Must`**, desde el 2026-08-10:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** `PRODUCT-INTAKE` §4 declara `Must Have` **todas** las capacidades que bajan a esta pieza. Hasta el 2026-08-10 había una excepción, `F-13`, y dejó de haberla: la versión **1.19** de esa fuente la promovió a `Must Have`.
2. **La historia que era `Should` es US-21**, sincronización del árbol y la escena por índice de pieza. Cambió de prioridad **porque cambió la de `F-13`**, su capacidad de origen, no porque este backlog la repriorizara.
3. **Cómo se resolvió la tensión que este backlog había elevado.** US-21 está **dentro de lo que la puerta `PT-02` mide antes de comprometer la etapa `g`** —`PRODUCT-INTAKE` §17.7.P.8 nombra la sincronización por índice entre lo que la puerta verifica— y además es criterio de la transición `g` → `h` del roadmap §5.2, de modo que en la práctica no era diferible aunque su prioridad declarada lo admitiera. Este backlog **no le subió la prioridad**: elevó la tensión como `PA-02`, la misma que `GeometriaFactory-Visor` elevó desde el otro lado de la fachada. **El Product Owner la resolvió el 2026-08-10 promoviendo `F-13`**, con ese mismo fundamento, y `PA-02` queda cerrado en §6.
4. **Este backlog queda en 100 % `Must`, y hay que declararlo.** El criterio de aceptación de `Rules-Backlog-Tecnico.md` §6 pide que la distribución **no sea 100 % `Must`**, y ésta lo es. **Es la consecuencia aritmética de una decisión ajena y no una omisión de priorización**: todas las capacidades que bajan a esta pieza son `Must Have` en la fuente, y esta pieza es además la única de los siete proyectos de código que toca **las ocho** etapas comprometidas, de modo que no hay tramo suyo que quede fuera del compromiso. Inventar una `Should` para cumplir el reparto sería declarar una prioridad falsa. Queda declarado como apartamiento consciente, y la condición para que deje de serlo es que el Product Owner clasifique con prioridad menor alguna capacidad que baje acá.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa**, con la particularidad de que en este proyecto de código las ocho etapas producen trabajo: diferir una etapa difiere una parte de esta pieza sí o sí.

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
| Segunda sesión obligatoria | **Antes de comprometer la etapa `g`**, junto con la lectura de `PT-02` y `PT-03`, que se miden sobre el bundle y sobre una página de esta pieza. Si una puerta no pasa, el refinamiento de la etapa `g` se detiene y **no se arrastra como deuda** |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su caso de uso de 02, contra la **superficie** de 03 que la aloja, contra el componente de `05` §3.1 que la sostiene y contra las **trece** restricciones transversales de `02` §6 |
| Entrada obligatoria a la sesión | Las trece restricciones transversales, las **once** superficies con su mapa de estados, y las filas de [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) que la etapa alcanza |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega interactividad, la sesión pregunta **si esa interactividad puede originar una petición desde el navegador**. `05` §9 declara como riesgo de impacto **muy alto** que aparezca un guion del navegador que llame al servicio de datos, «siempre por una comodidad de interfaz», y la categoría 03 ya fijó la regla de diseño de que **ninguna validación consulta al servidor mientras se escribe**.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c` |
| PA-02 | ~~**La tensión entre la prioridad declarada de `F-13` y la puerta `PT-02`**: US-21 está dentro de lo que la puerta mide antes de comprometer la etapa `g`, de modo que en la práctica no es diferible. Este backlog **no la repriorizó**; se elevó para que el Product Owner decidiera si `F-13` seguía siendo `Should Have`.~~ **CERRADO el 2026-08-10.** Desenlace: el Product Owner **promovió `F-13` a `Must Have`** en `PRODUCT-INTAKE` **1.19** §4, con el fundamento que este backlog había elevado —§17.7 P.8 incluye la sincronización por índice entre lo que `PT-02` mide antes de comprometer la etapa `g`, y una puerta que no pasa detiene la planificación—. US-21 pasa a `Must` en consecuencia, y §4.2 recoge el desenlace. `GeometriaFactory-Visor` había elevado la misma tensión desde el otro lado de la fachada, como `PA-06` de su backlog, y queda cerrada por la misma decisión. **La fila se conserva para no dejar hueco de numeración** | Cerrado por el Product Owner sobre `PRODUCT-INTAKE` §4 | Cerrado el 2026-08-10 |
| PA-03 | **La versión exacta de la biblioteca de componentes de interfaz**, que la fuente deja **[A VERIFICAR]** (`05` §11 `PA-01`). Convertido en trabajo como BT-02 | El equipo, al crear el andamiaje | Etapa `a` |
| PA-04 | **La versión de plataforma que soporta el hosting**, **[A VERIFICAR]** en la fuente (`05` §11 `PA-02`). Es `PT-01.a`, y si no pasa la salida es **bajar la versión objetivo del front y no la del backend**. Convertido en trabajo como BT-04 | La medición de `PT-01.a` | Etapa `a` |
| PA-05 | **El formato de intercambio y su configuración** (`05` §11 `PA-03`). **No es de este proyecto de código decidirlo**: `05` declaró que la decisión pertenece a la categoría 05 de `GeometriaFactory-Api`, que es el productor, y que esta pieza la **adopta**. Convertido en trabajo como BT-12, que adopta y no decide | La categoría 05 de `GeometriaFactory-Api`, con esta pieza como consumidor | Etapa `a` |
| PA-06 | **El umbral numérico de tiempo de respuesta** (`05` §11 `PA-04`). Ninguna fuente lo declara y `05` §8 **se niega explícitamente a inventarlo**. Convertido en trabajo como BT-21 | El Product Owner, o la categoría 08 al fijar su guion de medición, después de `PT-01` | Después de la etapa `a` |
| PA-07 | **El punto de quiebre principal y la proporción de la escena**, los dos rotulados **[ASUNCIÓN]** por la categoría 03 (`05` §11 `PA-05`). Convertido en trabajo como BT-10 | El Product Owner sobre la línea de base visual | Antes de cerrar la etapa `g` |
| PA-08 | **El volumen de la comisión**, **[A VERIFICAR]** (`05` §11 `PA-06`): el diseño de los dos listados supone decenas y no cientos, y por eso **no incorpora paginación**. Convertido en trabajo como BT-22 | El Product Owner | Antes de comprometer la etapa `e` |
| PA-09 | Si el **bundle generado se versiona en el repositorio o se ignora** (`05` §11 `PA-07`). Alcanza a esta pieza porque el bundle vive en su directorio de recursos estáticos. Convertido en trabajo como BT-23 | La categoría 09 | Al emitirse 09 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Web`. Declara **ocho** épicas, una por cada etapa comprometida del producto, con el nombre de épica candidata que el roadmap §3 ya había declarado, y con la constancia de que **este es el único de los siete proyectos de código que toca las ocho**. Numera y redacta las **treinta** historias que la categoría 02 previó —veintisiete por identificador en su matriz de §4 y las tres últimas por contenido en su §3.2—, respetando fila por fila el reparto por necesidad de negocio, cada una en su archivo bajo `historias-usuario/` por superar el umbral de veinte. Declara qué es una historia en la única superficie con actores humanos y por qué **ninguna hace cumplir una regla de negocio**. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que la categoría 05 ya se negó a inventar el único umbral que le faltaba. Eleva como `PA-02` la tensión entre la prioridad `Should Have` de `F-13` y la puerta `PT-02`, **sin reprioritizar**, y deja **nueve** puntos abiertos, siete de ellos convertidos en tareas técnicas. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra el hallazgo `D-06-03`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Cabecera**: la trazabilidad upstream pasa al intake **1.19** y suma §17.7 P.8, que es la fuente del fundamento. **§3 y ficha `US-21`**: la historia pasa de `Should` a **`Must`**, porque cambió la prioridad de su capacidad de origen y no porque este backlog repriorizara. **§4**: el reparto pasa de 29/1 a **30 `Must` sobre 30**. **§4.2**: se reescriben los tres puntos —la excepción `F-13` desapareció, la tensión elevada tiene desenlace— y entra un cuarto punto que declara el **100 % `Must`** como apartamiento consciente del criterio de aceptación de `Rules-Backlog-Tecnico.md` §6, con su motivo y su condición de revisión, en lugar de inventar una `Should` falsa para cumplir el reparto. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», con el recuento de los siete proyectos de código contado de nuevo —**175 `Must`, 4 `Should`, 1 `Could`** sobre 180— y con la explicación de por qué la regularidad no es una cuota: las diecinueve capacidades del tramo comprometido son hoy todas `Must Have`, de modo que toda no-`Must` viene o de una capacidad de la fase `i…` o de una decisión de las categorías 02 o 05. **§4 y §6**: `PA-02` queda **cerrado** con su desenlace y su fecha, conservando la fila para no dejar hueco de numeración, y la nota de deuda declarada lo recoge. Sube minor. |
