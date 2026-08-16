# Extensibilidad — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Extensibilidad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Por qué existe este documento](#1-por-qué-existe-este-documento)
- [2. El punto de extensión del producto](#2-el-punto-de-extensión-del-producto)
- [3. Qué se puede reemplazar y qué no](#3-qué-se-puede-reemplazar-y-qué-no)
- [4. Contrato que un reemplazo tiene que cumplir](#4-contrato-que-un-reemplazo-tiene-que-cumplir)
- [5. Cómo crece la fachada cuando al anfitrión le falta algo](#5-cómo-crece-la-fachada-cuando-al-anfitrión-le-falta-algo)
- [6. Ejemplo de ejercicio del punto de extensión](#6-ejemplo-de-ejercicio-del-punto-de-extensión)
- [7. Lo que este proyecto de código no ofrece como extensión](#7-lo-que-este-proyecto-de-código-no-ofrece-como-extensión)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Por qué existe este documento

`tiene_extensibilidad` es **true** en este proyecto de código, y **sólo en éste** de los siete del producto (`PRODUCT-MANIFEST` §5). La regla de la categoría exige este documento para `library` con puntos de extensión.

Conviene decir de entrada qué clase de extensibilidad es, porque no es la habitual: **no hay complementos que se registren, ni ganchos que un tercero implemente, ni un mecanismo de descubrimiento**. Lo que hay es un contrato angosto y estable —seis funciones— que hace **reemplazable la pieza que está detrás**. La extensión no se agrega desde afuera: se sustituye desde adentro sin que nadie de afuera se entere.

## 2. El punto de extensión del producto

`PRODUCT-INTAKE` §18 declara que **el punto de extensión del producto es el contrato de la fachada del visor**: las seis funciones que §17.7 P.3 enumera desde su versión 1.6. Y declara además que **el sample S-1 lo ejerce entero sin ninguna pieza del backend**, que es exactamente la propiedad que hace reemplazable al motor de dibujo tridimensional.

| Qué es | Detalle |
| --- | --- |
| El punto de extensión | Las **seis** funciones de la fachada: `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento` |
| Qué compra | Que el motor de dibujo tridimensional se pueda reemplazar **sin tocar ninguna página** |
| Por qué funciona | Porque el componente anfitrión no conoce los nombres internos del motor: sólo estas seis funciones (`PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Visor, alternativa descartada 2) |
| Quién lo ejerce | El sample **S-1**, la página integradora sin backend, en cinco pasos o menos |
| ADR que lo gobiernan | [`ADR-12001`](Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md), [`ADR-12002`](Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) y [`ADR-12006`](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) |

## 3. Qué se puede reemplazar y qué no

| Elemento | ¿Reemplazable? | Fundamento |
| --- | --- | --- |
| El **motor de dibujo tridimensional** | **Sí**, y es el propósito del punto de extensión | Vive confinado a la capa 3 y nunca aparece en la superficie pública ([`ADR-12004`](Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md)) |
| El **servicio de dibujo** entero, capa 3 | **Sí** | La capa 2 no contiene lógica de dibujo, de modo que la capa 3 se puede sustituir sin tocarla |
| La **forma interna del identificador** de instancia | **Sí, y no es cambio de contrato** | Es opaco por decisión; que el anfitrión dependa de su forma es un defecto del anfitrión |
| La **disposición** de las piezas en la escena | **No libremente**: cualquier reemplazo tiene que derivarla del índice | Garantía G-6 y [`ADR-12005`](Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) |
| Las **seis funciones** y sus nombres | **No.** Quitar una, renombrarla o cambiar qué recibe es cambio mayor | Rompe al anfitrión y al sample S-1 ([`ADR-12006`](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §7) |
| Las **siete garantías** | **No.** Perder cualquiera es cambio mayor aunque las firmas no se toquen | Son parte del contrato, no detalles de implementación |
| Los **siete códigos de condición** | **No aguas abajo.** Un código nuevo sólo puede nacer en la categoría 02 | Fuente única en §6 del contrato de fachada |

## 4. Contrato que un reemplazo tiene que cumplir

Un reemplazo de la capa 3 —otro motor de dibujo, u otra implementación del servicio— tiene que sostener, sin excepción:

| # | Compromiso | Cómo se verifica |
| --- | --- | --- |
| 1 | Las **seis** funciones, con sus nombres y con lo que cada una recibe y devuelve | Recorrido del sample S-1 |
| 2 | Las **siete** garantías: cero red, cero persistencia, sin configuración propia, aislamiento entre instancias, sin fallo silencioso, determinismo de posición y terminación controlada | Las seis propiedades transversales de la categoría 02, con sus condiciones de medición |
| 3 | Los **siete** códigos de condición, ni uno más ni uno menos, con sus dos cursos de `ELEMENTO_DE_DIBUJO_INVALIDO` | Inspección contra §6 del contrato de fachada |
| 4 | Los **seis** tipos de pieza dibujables, con el cero como dimensión legible | Escenario **E-7**, que cubre los seis tipos |
| 5 | La disposición derivada del índice, con **posición reservada** para las figuras no reconstruidas | Escenario **E-5** y comparación de dos procesados |
| 6 | Los **dos** movimientos automáticos gobernables por separado, con reposición de la orientación de partida al apagar el giro | Los criterios de aceptación de `CU-12007` |
| 7 | Liberación completa en `destruir`, incluido el corte del bucle | **10** recorridos de ida y vuelta con los movimientos prendidos, puerta `PT-02` |
| 8 | Empaquetado sin dependencias traídas de una red externa en tiempo de ejecución | Puerta `PT-03` |

**Los ocho compromisos son verificables sin backend**, que es lo que hace barato evaluar un reemplazo: alcanza con el sample S-1 y los escenarios E-1, E-5 y E-7.

## 5. Cómo crece la fachada cuando al anfitrión le falta algo

Es el proceso que hay que seguir cuando el anfitrión necesita una capacidad que las seis funciones no ofrecen, y está escrito porque **el atajo natural es el que rompe el punto de extensión**: exponerle al anfitrión algo del interior.

| Paso | Qué se hace | Quién |
| --- | --- | --- |
| 1 | Comprobar que la capacidad **no se puede resolver del lado del anfitrión**. Dibujar controles, consultar la preferencia de movimiento reducido del sistema y conservar la elección son del anfitrión, y no entran a la fachada | El equipo |
| 2 | Comprobar que **no cabe como flujo alternativo** de una función existente. Una capacidad cuya precondición es una instancia ya viva y cuyo efecto no es crear ni cargar no cabe en `inicializar` ni en `cargarJson` | El equipo |
| 3 | **Especificar la función nueva en la categoría 02**, en el documento de concepto central, con su firma, qué garantiza, qué no hace y su caso de uso propio | AG-02 |
| 4 | Comprobar si acuña **garantía** o **código** nuevos. Puede no acuñar ninguno de los dos | AG-02 |
| 5 | **Consolidarla en el intake** §17.7 P.3, para que la fuente de las funciones vuelva a ser única | El Product Owner |
| 6 | Declararla **cambio menor** y registrarla | El equipo |

**Ese proceso ya se recorrió entero una vez**, con la sexta función: el Product Owner la decidió el 2026-08-09 al cerrar la validación visual de la Fase B2, la categoría 02 la acuñó en §4.6 de su documento de concepto central con caso de uso propio `CU-12007`, **no acuñó garantía ni código** —la condición que informa ya existía— y el intake la consolidó en su versión 1.6. La superficie pasó de cinco funciones a seis **sin romper a ningún anfitrión escrito contra las cinco anteriores**, que es la definición de cambio menor.

## 6. Ejemplo de ejercicio del punto de extensión

El ejemplo canónico es el sample **S-1**, y no un ejemplo inventado acá: `PRODUCT-INTAKE` §18 lo declara como **el sample que demuestra el punto de extensión principal**, y §16.1 lo describe como una página integradora sin backend —un documento que carga el archivo de guion, un área donde se pega un texto y una superficie de dibujo—.

| Aspecto | Detalle |
| --- | --- |
| Qué ejerce | Las **seis** funciones, en recorrido, sin ninguna pieza del backend |
| Reproducibilidad | **Cinco pasos o menos**, todos dentro del contenedor de desarrollo: generar el bundle, abrir la página, pegar un texto, mirar |
| Material | Escenarios **E-1** —tres piezas, con el ortoedro dibujado— y **E-7** —seis piezas que cubren los seis tipos dibujables— |
| Caso de uso que lo materializa | `CU-12006`, el transversal de la categoría 02 |
| Dónde se desarrolla | La categoría **10-Examples**, que todavía no está emitida para este proyecto de código |

**Por qué el sample es la prueba del punto de extensión y no un agregado de conveniencia**: si las seis funciones se pueden ejercer enteras sin backend, entonces nada del interior depende del resto del producto, y por lo tanto el interior se puede reemplazar. El intake lo declara como propiedad exigida y no opcional.

## 7. Lo que este proyecto de código no ofrece como extensión

Se declara para que ninguna categoría aguas abajo lo busque ni lo invente.

| No existe | Por qué |
| --- | --- |
| Un mecanismo de **registro de complementos** | No hay terceros: `redistribuible` es false y el único consumidor es el anfitrión del mismo producto |
| **Ganchos** o puntos de intercepción dentro de la canalización de dibujo | Expondrían el interior de la capa 3 y volverían irreemplazable al motor, que es lo contrario del punto de extensión |
| **Configuración externa** que altere el comportamiento | Violaría la garantía G-3: la fachada no lee configuración propia |
| Un **catálogo abierto de tipos de pieza** | Los tipos dibujables son **seis** y son un conjunto cerrado. Uno fuera de esos seis no se dibuja y queda enumerado con su condición |
| Un **portal de developers** | `tiene_portal_developers` es false. Un portal para una comunidad que no existe sería documentación sin lector |

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| ADR que justifica el punto de extensión | [`ADR-12001`](Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md), y su política de crecimiento en [`ADR-12002`](Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) y [`ADR-12006`](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) |
| Contrato que lo define | [`Contratos-Abstractions.md`](Contratos-Abstractions.md) y [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) |
| CU que lo ejercen | `CU-12001` a `CU-12007`; el recorrido completo es `CU-12006` |
| Ejemplo de extensión | Sample **S-1**, que la categoría 10-Examples desarrollará |
| Tests previstos en 08 | Los ocho compromisos de §4, todos verificables sin backend |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara qué clase de extensibilidad es ésta —contrato angosto que hace reemplazable la pieza de atrás, y no registro de complementos—, qué se puede reemplazar y qué no, los ocho compromisos que un reemplazo tiene que cumplir con su forma de verificación sin backend, el proceso de seis pasos por el que la fachada crece con el precedente de la sexta función recorrido entero, el sample S-1 como ejemplo canónico y las cinco cosas que este proyecto de código **no** ofrece como extensión. |
