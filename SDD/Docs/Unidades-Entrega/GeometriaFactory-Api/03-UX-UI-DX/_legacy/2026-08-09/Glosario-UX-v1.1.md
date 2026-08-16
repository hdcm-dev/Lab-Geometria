# Glosario de la sección 03 — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Glosario-UX.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9, §9.1, §9.2 y §9.3 (glosario raíz de la cadena); `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` §2.5, §4 y §7; §6 de CU-01 a CU-12; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.2, §12.1, §17.1.P.1, §17.1.P.3, §17.1.P.5; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Error](#31-error)
  - [3.2 Mensaje](#32-mensaje)
  - [3.3 Dos casos que no son polisemia y no se corrigen](#33-dos-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Los términos del dominio ya están declarados aguas arriba y se **referencian** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- `02-Especificacion-Funcional/Glosario-Funcional.md` declara lo que la categoría 02 acuña para este proyecto de código.

Ninguna entrada de §2 pisa a ninguna de las dos fuentes. La regla de no duplicación es explícita: si un término ya está declarado con la misma semántica, se referencia; el vocabulario de la superficie pública y del recorrido de integración es lo único que se acuña acá.

Rigen sin excepción las tres resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado**, «pieza» en su referente del dominio va desnuda y en su referente de artefacto desplegable va calificada, y **la palabra «proyecto» a secas no se usa**.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública del dominio | El conjunto de lo que un consumidor invoca de este proyecto de código: la construcción y la transición de entidades, con sus guardas. **No es una API de servicio**: no expone protocolo, no cruza frontera de proceso y no se publica en ningún feed | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Superficie pública» a secas cuando el proyecto de código está nombrado en la misma oración |
| Guarda | Verificación que una entidad del dominio hace sobre sí misma y que la lleva a **negarse a entrar en un estado prohibido**. Es la unidad de la superficie pública: lo que se documenta, lo que se prueba y lo que produce un código de condición | Los tres | — |
| Condición de error del dominio | Cada una de las 40 situaciones catalogadas en las que una guarda se niega. Se identifica por un **código**, no por un texto. No es una observación y no es un comentario: ver §3.1 | Los tres | «Código de condición», «código de rechazo» en los artefactos de 02. Se dice «condición de error» cuando se habla de la situación y «código» cuando se habla del identificador |
| Rechazo | Forma de terminación en la que el dominio se niega a la operación: no construye la entidad, o la deja exactamente como estaba, y no queda efecto parcial ni estado intermedio | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Terminación controlada» |
| Motivo de resultado | Forma de terminación en la que la operación es una consulta que **siempre devuelve un resultado**, y el código es la razón por la que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Motivo» cuando el resultado ya está nombrado en la misma oración |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: **qué hacer del lado del consumidor**. Existe en esa forma porque el dominio no consulta, no reintenta y no corrige el dato | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los cuatro grupos en que el catálogo ordena las condiciones —entrada inválida, recurso ausente, conflicto de estado, conflicto de facultad—, más las dos declaradas vacías con su motivo | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Taxonomía» para el conjunto |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo: mantenedor, integrador de capa u operador. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX; en la variante UX/UI el término equivalente es «audiencia» |
| Mantenedor del dominio | El rol de intervención principal acá: quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Integrador de capa | El rol de intervención que escribe `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure` contra esta superficie pública. **No hay integradores externos**: los dos consumidores son proyectos de código del mismo producto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Precisa a «consumidor de la biblioteca», que `Glosario-Funcional.md` §2 ya declara, agregando quién lo escribe |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido de aprendizaje —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable**: algo que se ejecuta o se responde, no una lectura declarada como hecha | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible de pasos que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido**: ningún paso asume herramientas en el host | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, la batería de pruebas de dominio en verde en menos de 10 segundos. Es el hito que cierra el tramo de 5 minutos y el que mide el TTFS | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber visto una guarda negándose y saber ubicar la regla o el invariante que la sostiene | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis —tutorial, how-to, reference, explanation— con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

## 3. Términos con más de un referente

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ningún término se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección y la que hay que dejar imposible de confundir: los tres referentes conviven en las mismas secciones de `DX-Error-Messages.md` y de `Guia-Onboarding-Developer.md`.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La guarda que impide una operación ilegítima del consumidor. No se guarda en ninguna parte y no sobrevive a la invocación | **«condición de error del dominio»**, o «código» cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de los tres referentes en la misma sección: catalogan condiciones, y sus enunciados nombran observaciones de especie error de validación |
| El defecto que impide interpretar el texto del alumno como figuras, que es **una de las dos especies de observación** y que impide que el trabajo pase a estado `Pendiente` | **«error de validación», siempre completo.** Es entidad del dominio, no guarda | Declarado en `Vision-Producto.md` §9.1. `ERROR_SIN_UBICACION` y `ADVERTENCIA_SIN_LOS_DOS_VALORES` son condiciones **sobre** observaciones, y los dos sentidos aparecen en la misma fila del catálogo |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.2 declara vacía la categoría «error interno» por este motivo |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las tres formas son «condición de error del dominio», «error de validación» y «defecto».

Y la distinción que completa el cuadro, aunque no sea polisemia de la palabra: **ninguno de los tres es el comentario** del administrador, que lo escribe una persona, hay a lo sumo uno por trabajo y no es una calificación (`Vision-Producto.md` §9.1).

### 3.2 Mensaje

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El enunciado canónico en lenguaje plano de una condición de error, que este catálogo declara como columna y que el dominio **no produce** | **«mensaje del catálogo»** cuando el otro referente está en la misma sección; «mensaje» a secas dentro de la columna de la tabla, donde el encabezado ya fija el referente | `DX-Error-Messages.md` §1.3 y §5 hablan de los dos referentes en la misma sección: la columna del catálogo y lo que la capa que expone compone |
| El texto que una persona lee, compuesto por la capa que expone y traducido a respuesta de protocolo | **«mensaje al usuario»** o «texto que compone el consumidor» | `DX-Error-Messages.md` §5 declara que la composición y la traducción no viven acá |

### 3.3 Dos casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica y que su §10 declara defecto del informe y no del documento auditado.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1 y en `Glosario-Funcional.md` §3.4. Lo que sí colisiona es «error», y está resuelto en §3.1.
- **Los nombres de los códigos son identificadores literales del contrato** y no se califican. `CUENTA_PENDIENTE` se escribe así aunque `Pendiente` lleve forma calificada obligatoria en prosa; calificar un identificador sería el falso positivo que `Glosario-Funcional.md` §3.3 ya declaró como excepción. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados y a las filas de las tablas de transición cuyo encabezado ya fija el referente.

## 4. Términos referenciados y no redefinidos

Los siguientes términos aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo, Pieza, Componente, Observación, Advertencia, Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, Aprobar / Rechazar, Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio, Actividad 1, `Describir()`, Tapa, Rectángulo desarrollado, Coma final, Fallo silencioso | `Vision-Producto.md` §9.1 |
| Punto de control, Hito interno / hito demostrable | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Etapa, Puerta técnica, Capacidad | `Vision-Producto.md` §9.2 |
| Alumno, Papel, Estado de cuenta, Credencial derivada | `Glosario-Funcional.md` §2 |
| **Camino de alta**, con sus dos referentes: el auto-registro del alumno, que nace `Pendiente` y sin credencial, y la configuración del administrador, que nace `Habilitado` y con credencial | `Glosario-Funcional.md` §2 |
| Admisibilidad de la cuenta, Baja de la cuenta | `Glosario-Funcional.md` §2 |
| Texto original, Posición de pieza, Familia plana o volumétrica | `Glosario-Funcional.md` §2 |
| Especie de observación, Desenlace, Alcance del administrador | `Glosario-Funcional.md` §2 |
| Consumidor de la biblioteca, Sujeto de la regla | `Glosario-Funcional.md` §2 |
| Invariante, en el sentido de condición que no puede romperse nunca | `Definicion-Modelo-De-Dominio.md` §4 |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Declara los dieciséis términos que esta categoría acuña para la superficie pública y el recorrido de integración; los dos términos con más de un referente dentro de la sección —«error», con sus tres referentes y la prohibición de la forma desnuda, y «mensaje», con sus dos—, cada uno con su evidencia de colisión verificada por sección; los dos casos que deliberadamente no se corrigen, la superordinación de «observación» y los identificadores literales de los códigos; y los términos referenciados del glosario raíz y del glosario funcional de 02, que no se redefinen. |
| 1.1 | 2026-08-09 | Alineación con la **corrección del P0** que reporta `B-02-03-GeometriaFactory-Application-r1.md`. §4 **referencia** el término **«camino de alta»**, que `Glosario-Funcional.md` §2 acuñó al separarse los dos caminos, y no lo redefine. La entrada «condición de error del dominio» de §2 actualiza su recuento de 37 a 40 situaciones catalogadas. Ningún término de §2 se agrega ni se quita: el vocabulario nuevo es del dominio y pertenece a 02. |
