# Glosario de la sección 03 — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Glosario-UX.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9, §9.1, §9.2 y §9.3 (glosario raíz de la cadena); `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4 y §11; §6 de CU-01 a CU-10; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Glosario-Funcional.md` §2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.2, §12.1, §17.2.P.1, §17.2.P.3, §17.2.P.5, §17.2.P.11; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Error](#31-error)
  - [3.2 Negativa](#32-negativa)
  - [3.3 Tres casos que no son polisemia y no se corrigen](#33-tres-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidos los cuatro puertos, las dos verificaciones, el motivo, el doble y la unidad de trabajo.
- El glosario funcional de `GeometriaFactory-Domain` declara el vocabulario de la capa de la que este proyecto de código depende.

Ninguna entrada de §2 pisa a ninguna de las tres fuentes. La regla de no duplicación es explícita: si un término ya está declarado con la misma semántica, se referencia; lo único que se acuña acá es el vocabulario de la **superficie pública vista por quien interviene** y del **recorrido de integración**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales de los motivos—, «pieza» va desnuda en su referente del dominio y calificada en su referente de artefacto desplegable, **«repositorio» a secas no se escribe**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la capa de aplicación | El conjunto de lo que se invoca y de lo que se implementa contra este proyecto de código. Tiene **dos caras que miran para lados opuestos**: los casos de uso, que un consumidor invoca, y los puertos, que esta capa declara y otra implementa | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Superficie pública» a secas cuando el proyecto de código está nombrado en la misma oración |
| Inversión de dependencias | El rasgo que gobierna la capa: acá se declara **qué** hace falta y otra capa provee el **cómo**. Es lo que permite ejercer un caso de uso entero con dobles, sin base de datos ni frontera de proceso | Los tres | «La dependencia se invierte», que es la forma que usa `Especificacion-Funcional.md` §1 |
| Condición de error del caso de uso | Cada una de las 34 situaciones catalogadas en las que una comprobación se niega. Se identifica por un **motivo**. No es una observación y no es un comentario: ver §3.1 | Los tres | «Condición de error» cuando el proyecto de código está nombrado. El identificador se llama **motivo**, que `Glosario-Funcional.md` §2 ya declara |
| Negativa por pertenencia | La condición que se produce cuando el trabajo pedido no es del alumno solicitante. **Oculta la existencia del recurso**: el trabajo ajeno y el identificador inexistente comparten motivo por diseño | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Nombra el resultado de la «verificación de pertenencia» que `Glosario-Funcional.md` §2 declara |
| Negativa por facultad | La condición que se produce cuando quien pide una operación reservada no tiene el papel `Administrador`. **No oculta nada**: no hay recurso ajeno cuya existencia proteger | Los tres | Nombra el resultado de la «verificación de facultad» de `Glosario-Funcional.md` §2 |
| Negativa por alcance | La condición que se produce cuando el trabajo pedido está en `Borrador` y no forma parte del flujo de trabajo del administrador. **Tampoco oculta la existencia**: expresa que está fuera de su alcance | Los tres | Nombra el resultado de la comprobación de «alcance del administrador» |
| Traducción prohibida | Cada una de las cuatro correspondencias entre motivo y respuesta que ninguna capa aguas abajo puede establecer, empezando por traducir la negativa por pertenencia a «no autorizado». Tiene métrica propia, con objetivo **cero** | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | — |
| Frontera de autorización | El límite declarado entre lo que esta capa hace —decidir quién puede hacer qué sobre qué recurso— y lo que no hace: comparar contraseñas, derivarlas, emitir accesos, sostener sesiones o autenticar la petición. **Esta capa no autentica: autoriza** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «La frontera» cuando ya está nombrada en la misma sección |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: qué hacer al respecto. Acá tiene **dos destinatarios**, el consumidor y el adaptador del puerto, según de dónde nazca la negativa | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los siete grupos en que el catálogo ordena las condiciones: entrada inválida, recurso ausente, conflicto de estado, conflicto de facultad, conflicto de alcance, error transitorio y error interno | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Taxonomía» para el conjunto |
| Forma de terminación | Dimensión ortogonal a la categoría, que dice qué le queda por hacer al consumidor: **negativa sin escritura**, **motivo de resultado** o **terminación degradada** | `DX-Error-Messages.md` | — |
| Terminación degradada | La forma de terminación de una sola condición, `INTERPRETACION_NO_DISPONIBLE`: la operación no se completó por una causa que no depende del pedido, y el caso de uso lo declara en vez de fingir un resultado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Estado degradado», que es la forma que usa CU-05 §6 |
| Condición agregada | Condición de esta capa que **reúne varios rechazos distintos del dominio** bajo un solo motivo, porque ninguno de ellos es un resultado que el alumno deba ver. Son dos: la del conjunto de piezas y la del conjunto de observaciones. El motivo fino queda del lado del dominio | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Rechazo inalcanzable por construcción | Rechazo que el dominio declara y que **esta capa no puede provocar**, porque su propio flujo resuelve antes la condición que lo dispararía. No es una condición del catálogo y su ausencia no es un olvido: está declarado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | Se distingue del **rechazo equivalente**, que sí puede ocurrir pero esta capa nombra con un motivo propio, y del **agregado**, que es la fila anterior |
| Adaptador de puerto | La implementación concreta de un puerto, que vive en `GeometriaFactory-Infrastructure`. Se nombra así cuando hay que decir de qué lado se corrige un defecto | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «La implementación del puerto». No se dice «el puerto» a secas cuando el sujeto es la implementación |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo: mantenedor de la capa, integrador por casos de uso, implementador de puertos u operador. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX; en la variante UX/UI el término equivalente es «audiencia» |
| Integrador por casos de uso | El rol de intervención que escribe `GeometriaFactory-Api` contra los casos de uso de esta capa. **No hay integradores externos** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Precisa al «consumidor de los casos de uso» que `Glosario-Funcional.md` §2 declara, agregando quién lo escribe |
| Implementador de puertos | El rol de intervención que escribe `GeometriaFactory-Infrastructure` contra los puertos que esta capa declara. Es el otro consumidor, y mira la superficie desde el lado opuesto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido de aprendizaje —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable**: algo que se ejecuta o se responde, no una lectura declarada como hecha | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible de pasos que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido**: ningún paso asume herramientas en el host | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, la batería de pruebas de la capa de aplicación en verde **sin haber preparado nada externo**. Es el hito que cierra el tramo de 5 minutos y el que mide el TTFS | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber ejercitado un caso de uso entero con dobles y saber nombrar los cuatro puertos | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis —tutorial, how-to, reference, explanation— con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

## 3. Términos con más de un referente

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ningún término se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, y la misma que el proyecto de código hermano resolvió para su propia categoría 03. Acá los referentes son cuatro, porque esta capa suma uno que el dominio no tenía: el defecto del adaptador de un puerto.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La comprobación que impide una operación ilegítima o imposible. No se guarda en ninguna parte y no sobrevive a la invocación | **«condición de error del caso de uso»**, o **«motivo»** cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de varios referentes en la misma sección: catalogan condiciones, y sus enunciados nombran observaciones de especie error de validación |
| El defecto que impide interpretar el texto del alumno como figuras, que es **una de las dos especies de observación** y que impide que el trabajo pase a estado `Pendiente` | **«error de validación», siempre completo.** Es entidad del dominio, no comprobación | Declarado en `Vision-Producto.md` §9.1. `OBSERVACION_MAL_FORMADA` es una condición **sobre** observaciones, y los dos sentidos aparecen en la misma fila del catálogo |
| Lo que un adaptador de puerto devolvió y el contrato no admite | **«defecto del adaptador»**, y la categoría del catálogo se llama «error interno» sólo como nombre de categoría, donde el encabezado ya fija el referente | `DX-Error-Messages.md` §2.2 y §3.5 hablan de los dos referentes en la misma sección |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.2 lo declara al justificar la categoría de error interno |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «condición de error del caso de uso», «error de validación», «defecto del adaptador» y «defecto».

Y la distinción que completa el cuadro, aunque no sea polisemia de la palabra: **ninguno de los cuatro es el comentario** del administrador, que lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala (`Vision-Producto.md` §9.1).

### 3.2 Negativa

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El resultado de una de las tres comprobaciones de autorización: pertenencia, facultad o alcance | **Siempre calificada**: «negativa por pertenencia», «negativa por facultad», «negativa por alcance». Es el uso que domina la sección | `DX-Error-Messages.md` §2.4 y `DX-Developer-Experience.md` §1.4 usan los dos referentes en la misma sección: enumeran las tres calificadas y hablan además de la negativa como forma de terminación |
| Cualquier terminación en la que el caso de uso no hace lo que se le pidió, incluidas las que nada tienen que ver con autorización | **«negativa sin escritura»** cuando se habla de la forma de terminación, que es donde el término aparece sin calificar por comprobación | `DX-Error-Messages.md` §2.3 declara la forma, y §2.4 declara las tres calificadas |

La forma desnuda «negativa» sólo se admite cuando la calificación aparece en la misma oración y el referente ya quedó fijado.

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1 y en `Glosario-Funcional.md` §3.6. Lo que sí colisiona es «error», y está resuelto en §3.1. **El comentario del administrador no es una observación.**
- **Los nombres de los motivos son identificadores literales del contrato** y no se califican ni se traducen. `CUENTA_PENDIENTE` se escribe así aunque `Pendiente` lleve forma calificada obligatoria en prosa; calificar un identificador sería el falso positivo que `Glosario-Funcional.md` §3.3 ya declaró como excepción. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **«Puerto»** designa acá una sola cosa: el contrato que esta capa declara y otra implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta sección. Los contextos son disjuntos y por eso no se califica; la resolución es idéntica a la que declara `Glosario-Funcional.md` §3.6 y se conserva.

## 4. Términos referenciados y no redefinidos

Los siguientes términos aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

### 4.1 Del glosario raíz de 00

| Término | Dónde está declarado |
| --- | --- |
| Trabajo, Pieza, Componente, Observación, Advertencia, Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, como única acción de guardado; Aprobar / Rechazar; Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio, Actividad 1, Punto de control | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Etapa, Capacidad | `Vision-Producto.md` §9.2 |

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno**, ni siquiera cuando le agrega un nombre para el resultado de una comprobación, como en las tres negativas de §2.

| Término | Qué designa, en una línea |
| --- | --- |
| Puerto | Contrato que esta capa declara y que otra capa implementa |
| Puerto de repositorio de trabajos | Por donde el caso de uso recupera, consulta acotado, materializa y retira |
| Puerto de repositorio de cuentas | Por donde recupera una cuenta, pregunta por un correo y materializa. **Su identificador es punto abierto** |
| Puerto de validación de figuras | Por donde entrega el texto original y recibe piezas y observaciones |
| Puerto de reloj del sistema | Por donde obtiene el sello, para que los sellos de alta, de modificación y de desenlace sean verificables en prueba |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública: `GeometriaFactory-Api` |
| Verificación de pertenencia | La comprobación de que el trabajo pedido es del alumno solicitante |
| Verificación de facultad | La comprobación de que quien pide una operación reservada tiene el papel `Administrador` |
| Alcance de consulta | El recorte que el caso de uso traslada al puerto antes de pedir |
| Unidad de trabajo | El tramo dentro del cual las escrituras de un caso de uso ocurren enteras o no ocurren |
| Motivo | El valor de la enumeración cerrada con la que un caso de uso explica por qué una operación no procede |
| Doble | Implementación de prueba de un puerto, que hace ejercitable un caso de uso entero sin base de datos |
| Camino de alta | Cada una de las dos vías por las que nace una cuenta, con reglas opuestas: el auto-registro del alumno y la configuración del administrador |
| Metadato de orquestación | Dato que esta capa aporta al materializar y que el modelo del dominio no declara como atributo: los sellos de alta, de modificación y de desenlace |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las que no se pudieron reconstruir; es el rango de posiciones válidas del trabajo. `Glosario-Funcional.md` §4.2 lo referencia del modelo del dominio, y esta sección hace lo mismo |
| Repositorio, con sus dos referentes y la forma calificada obligatoria | `Glosario-Funcional.md` §3.1 |
| Trabajo y la forma compuesta «unidad de trabajo» | `Glosario-Funcional.md` §3.5 |
| Rol, con «papel» para el atributo de la cuenta | `Glosario-Funcional.md` §3.4 |

### 4.3 Del glosario funcional de GeometriaFactory-Domain

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Admisibilidad de la cuenta, Baja de la cuenta | Las dos nociones que CU-03 y CU-02 orquestan |
| Texto original, Posición de pieza, Familia plana o volumétrica | El vocabulario del trabajo interpretado |
| Especie de observación, Desenlace, Alcance del administrador | Las tres nociones que gobiernan el cierre del circuito |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Declara los veintitrés términos que esta categoría acuña para la superficie pública de dos caras y el recorrido de integración, entre ellos las tres negativas de autorización, la traducción prohibida, la frontera de autorización, la terminación degradada y el adaptador de puerto; los dos términos con más de un referente dentro de la sección —«error», con sus cuatro referentes y la prohibición de la forma desnuda, y «negativa», con sus dos—, cada uno con su evidencia de colisión verificada por sección; los tres casos que deliberadamente no se corrigen, entre ellos la superordinación de «observación» con la precisión de que el comentario del administrador no lo es, y la de «puerto», cuyos contextos son disjuntos; y los términos referenciados de las tres fuentes aguas arriba —el glosario raíz de 00, el glosario funcional de 02 de este proyecto de código y el de `GeometriaFactory-Domain`—, que no se redefinen. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **Alineación con el 02 corregido**: la cifra de condiciones catalogadas pasa de 27 a **34** y el upstream, de CU-01 a CU-10. §2 da de alta dos términos que la sección nueva de rechazos del dominio acuñó —**«condición agregada»** y **«rechazo inalcanzable por construcción»**, con la distinción frente al rechazo equivalente— y llega a **veinticinco**. §4.2 suma como **referenciados y no redefinidos** los dos términos que la categoría 02 acuñó en su propia corrección, «camino de alta» y «metadato de orquestación», y «cantidad de figuras del conjunto raíz», que 02 referencia del modelo del dominio y esta sección referencia igual. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-16**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. El puntero al puerto de reloj en §4.2 conservaba la nomenclatura anterior a los **sellos** —«por donde obtiene la fecha»— y pasa a la forma vigente del glosario de 02, con los tres sellos nombrados. Es el único cambio: el término no se redefine, sólo se corrige el resumen de una línea del término referenciado. |
