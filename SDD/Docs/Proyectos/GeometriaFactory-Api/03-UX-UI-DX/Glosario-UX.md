# Glosario de la sección 03 — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Glosario-UX.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3, §5 y §6; `02-Especificacion-Funcional/Especificacion-Funcional.md` §3, §4 y §11; §6 de CU-01 a CU-12; los glosarios de las secciones 03 de `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Error](#31-error)
  - [3.2 Consumidor](#32-consumidor)
  - [3.3 Tres casos que no son polisemia y no se corrigen](#33-tres-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidos **punto de acceso**, **código de respuesta**, **código del contrato**, **las dos traducciones**, **la guardia de admisión**, **la ruta propuesta**, **la señal que no es un fallo** y **el hueco del conjunto cerrado**, y las tres polisemias de «acceso», «código» y «punto».

Ninguna entrada de §2 pisa a ninguna de esas fuentes. Lo único que se acuña acá es el vocabulario de **quien interviene sobre este proyecto de código** y del **recorrido de implementación, de consumo y de despliegue**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales—, **«acceso» a secas designa el valor firmado**, **«código» a secas no se escribe**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la pieza de datos | El conjunto de lo que este proyecto de código existe hacia afuera. **Son sus dieciséis puntos de acceso y nada más**: no lo referencia nadie por compilación | Los tres | «La superficie» cuando el proyecto de código está nombrado |
| **Consumidor de la superficie** | El rol de intervención de quien escribe el cliente que consume estos puntos de acceso. **Existe acá y no en las capas de adentro**, y es a quien le habla el catálogo entero | `DX-Developer-Experience.md`, `DX-Error-Messages.md` | «El consumidor» cuando el rol ya está nombrado. Acá lo encarna quien escribe el cliente tipado de la pieza pública. Ver §3.2 |
| Implementador de la superficie | El rol de intervención de quien agrega o cambia un punto de acceso | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió | `DX-Developer-Experience.md` | — |
| Operador del despliegue | El rol de intervención de quien arranca el contenedor del servicio **a mano**. Acá lo encarna el docente | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «El operador» cuando el rol ya está nombrado |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md` | El término de la variante DX |
| **Entrada del catálogo** | Cada una de las **18** situaciones en las que esta superficie responde con un fallo: **16** códigos del contrato con destino más **2** respuestas sin código | `DX-Error-Messages.md` | **No se dice «error»** para este referente: ver §3.1 |
| **Familia empobrecida** | Cada uno de los **tres** conjuntos de respuestas que dicen **menos de lo que el servicio sabe**, a propósito: credenciales inválidas, recurso que no se ve y correo ya registrado | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | «Respuesta empobrecida». **No se dice «respuesta vaga»**: la vaguedad sería un defecto y esto es una decisión |
| **Qué hace el consumidor** | La cuarta columna del catálogo, y la que decide si sirve: una de **cuatro** acciones —corregir y reintentar, derivar, mostrar, o pasar a estado degradado— | `DX-Error-Messages.md` | «Acción del consumidor». Es el equivalente del diagnóstico accionable de las capas de adentro |
| **Lo que no falla** | Cada uno de los **tres** defectos que dejan el sistema funcionando y equivocado: el punto fuera de la guardia, la respuesta que distingue lo ajeno de lo inexistente y el texto normalizado en el borde. Tienen métrica propia, con objetivo **cero** | `Guia-Onboarding-Developer.md` §6, `DX-Developer-Experience.md` §1.4 | «Las tres cosas que no fallan». **No se dice «bug»** |
| Detección por recuento | La forma de encontrar el defecto que no falla cuando lo que está mal es una **ausencia**: se cuentan los puntos guardados contra los puntos que exigen acceso | `Guia-Onboarding-Developer.md`, `DX-Developer-Experience.md` | — |
| Detección por comparación | La forma de encontrarlo cuando lo que está mal es una **diferencia** entre dos respuestas que deberían ser idénticas | `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, **la colección de peticiones corriendo entera contra el servicio real**, sin pantalla, sin circuito y sin visor | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio de código hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber corrido la colección y saber por qué los ocho escenarios responden con éxito | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis, con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md` | «Modo Diátaxis» |

## 3. Términos con más de un referente

Los dos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, igual que en las capas hermanas, **y acá tiene un referente que ninguna de ellas tiene**: la clase de respuesta.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada una de las 18 situaciones en las que esta superficie responde con un fallo | **«entrada del catálogo»**, o **«respuesta de fallo»**. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 usan varios referentes en la misma sección |
| El defecto que impide interpretar el texto del alumno, que es **una de las dos especies de observación** | **«error de validación», siempre completo.** Es entidad del dominio, es un **resultado**, viaja en una respuesta exitosa y **no está en el catálogo** | `DX-Error-Messages.md` §1.2 pone los dos referentes en la misma tabla |
| La clase de fallo que el código de respuesta expresa | **«código de respuesta»**, o su número. **No se dice «error 500»**, se dice «respuesta `500`» | `Guia-Onboarding-Developer.md` §3.5 y §4 |
| Una falla no declarada del proyecto de código | **«defecto».** No es una entrada del catálogo: su lugar es una prueba que falla | `Guia-Onboarding-Developer.md` §6 |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «entrada del catálogo», «respuesta de fallo», «error de validación», «código de respuesta» y «defecto».

Y la distinción que completa el cuadro, heredada de las capas de adentro y todavía más importante acá: **el error de validación es lo que el alumno tiene que ver, y viaja en una respuesta exitosa.** Ninguna de las otras formas lo es.

### 3.2 Consumidor

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El rol de intervención de quien escribe el cliente de esta superficie | **«consumidor de la superficie»**, o «el consumidor» cuando el rol ya está nombrado en la misma sección | `DX-Developer-Experience.md` §1.1 y `DX-Error-Messages.md` §1.3 usan los dos referentes en secciones vecinas |
| El código que invoca una biblioteca, que es como lo usan las capas de adentro | **No se nombra «consumidor» a secas** en esta sección. Cuando hace falta, se dice **«la composición de raíz»**, que es el término de aquellas capas | Los dos aparecerían en las mismas tablas de trazabilidad |

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **Los nombres de los códigos del contrato son identificadores literales** y no se califican, no se traducen y no se acentúan. La misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **Los números de código de respuesta se escriben como números** y no se traducen a prosa. «Responde `404`» es la forma; «responde no encontrado» describe la obligación, no la respuesta, y sólo se usa citando la regla que la exige.
- **«Guardia»** tiene un solo referente en esta sección —la comprobación de acceso, papel y marca— y no colisiona con nada. Aparece en singular porque **es una sola**, aunque gobierne once puntos.

## 4. Términos referenciados y no redefinidos

### 4.1 Del glosario raíz de 00

Trabajo; Pieza; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno**: punto de acceso, superficie HTTP, código de respuesta, código del contrato, las dos traducciones, guardia de admisión, papel exigido, composición de raíz, arranque detenido, ruta propuesta, colección de peticiones, señal que no es un fallo y hueco del conjunto cerrado; más las tres polisemias de «acceso», «código» y «punto».

### 4.3 De los glosarios de los proyectos de código vecinos

| Término | Qué designa, en una línea | Dónde está declarado |
| --- | --- | --- |
| Papel, Estado de cuenta, Credencial derivada, Marca de cambio de contraseña pendiente | El vocabulario de la cuenta | `GeometriaFactory-Domain` |
| Desenlace, Terminalidad, Alcance del administrador | Las nociones que gobiernan el cierre del circuito | `GeometriaFactory-Domain` |
| Puerto, Verificación de pertenencia, Verificación de facultad, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación. **Las dos verificaciones no se hacen acá** | `GeometriaFactory-Application` |
| Contrato de uso, Tipo de transferencia, Conjunto cerrado de códigos, Señal declarada | El vocabulario de lo que cruza la frontera | `GeometriaFactory-Contracts` |
| Adaptador, Almacén, Transformación de esquema, Terminación degradada, Arranque detenido | El vocabulario de la capa que toca el mundo | `GeometriaFactory-Infrastructure` |
| Trampa del formato, Lectura tolerante, Operador estricto, Contraseña provisoria, Clave de firma | El vocabulario del dato del alumno y de los secretos | `GeometriaFactory-Infrastructure` |
| **Atajo prohibido**, **Falla hacia el lado seguro**, **Defecto que no falla** | Los términos con los que la sección 03 de la capa que toca el mundo nombra la misma familia de defectos que acá se llama **«lo que no falla»** | `GeometriaFactory-Infrastructure` 03 |

**Sobre el último grupo, una precisión para que no se lea como una polisemia nueva.** Aquella sección y ésta nombran **la misma clase de defecto** —el que se rompe produciendo algo válido— y la nombran distinto porque **el sujeto es distinto**: allá son **atajos** que alguien toma deliberadamente cuando el mundo no responde, y acá son en dos de los tres casos **descuidos** que nadie decide, como olvidarse de poner un punto nuevo bajo la guardia. Se referencia el término de aquella sección y no se lo redefine.

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **este proyecto de código sí es una unidad de entrega**, y es una de las dos del producto.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara los **dieciocho** términos que esta categoría acuña —entre ellos el **consumidor de la superficie**, que las capas hermanas declaran no aplicable, la **familia empobrecida**, **qué hace el consumidor**, **lo que no falla** y sus dos formas de detección, por recuento y por comparación—; los **dos** términos con más de un referente con su forma obligatoria y su evidencia de colisión —«error», que acá tiene un referente que las capas hermanas no tienen, y «consumidor»—; los tres casos que deliberadamente no se corrigen; y los términos referenciados del glosario raíz, del glosario funcional de 02 y de los glosarios de los proyectos de código vecinos, con la precisión de por qué **«lo que no falla» y «atajo prohibido» nombran la misma clase de defecto con sujetos distintos**. |
