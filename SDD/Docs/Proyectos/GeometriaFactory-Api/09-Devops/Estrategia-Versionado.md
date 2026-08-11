# Estrategia de versionado — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) 1.0 §2, §7 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) 1.0; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §11; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3 y §1.4; [`../../GeometriaFactory-Contracts/09-Devops/Estrategia-Versionado.md`](../../GeometriaFactory-Contracts/09-Devops/Estrategia-Versionado.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §15, §17.4.P.3, §17.5.P.3 y §17.5.P.7
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md)

---

## Tabla de contenido

- [1. Versionado semántico, y qué reemplaza al versionado de rutas](#1-versionado-semántico-y-qué-reemplaza-al-versionado-de-rutas)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Modelo de ramas](#4-modelo-de-ramas)
- [5. Canales](#5-canales)
- [6. Política de cambios incompatibles](#6-política-de-cambios-incompatibles)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Versionado semántico, y qué reemplaza al versionado de rutas

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.5.P.7 lo declara **sin excepciones**, junto con las convenciones de mensaje de confirmación, una rama y un pull request por etapa, y **una etiqueta por cada etapa cerrada y fusionada, para poder volver a cualquier demostración**. Declara además que **el registro de cambios se actualiza en la rama de la etapa, no después de la fusión**.

**Y declara una ausencia con su sustituto, que es lo que ordena este documento.** El intake §17.5.P.3 dice que **no hay versionado de rutas porque no hay clientes de terceros**, y [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 declara qué lo reemplaza, en cinco reglas que esta categoría transcribe y no reescribe:

1. **Una sola versión de la superficie vive a la vez**: sin prefijo de versión en las rutas, sin convivencia de dos formas de un punto y sin deprecación gradual.
2. **Todo cambio del ensamblado de contratos obliga al despliegue conjunto** de esta unidad y de la pública.
3. **Tres clases de cambio no las detecta la compilación, y cada una tiene su mecanismo**: la **configuración de intercambio**, declarada una sola vez para los dos extremos; el **esquema del almacén**, verificado al arrancar con su linaje, que detiene el arranque si no cierra; y **las rutas**, que sólo el consumidor conoce y que la batería de integración ejerce contra el servicio real.
4. **Cada etapa cerrada y fusionada recibe una etiqueta**, y la reversión es volver a la etiqueta anterior y reconstruir.
5. **La colección de peticiones reproducible es parte del contrato hacia afuera**, y cuando la superficie cambia, la colección cambia con ella.

**La tercera regla es la que esta categoría tiene que hacer operativa**, porque las tres clases que la compilación no detecta son las que un pipeline puede dejar pasar:

| Clase que la compilación no detecta | Dónde se la atrapa en la canalización | Umbral |
| --- | --- | --- |
| **Configuración de intercambio** divergente entre los dos extremos | `QG-10`, en el stage `build` | **1** sola configuración declarada en el producto |
| **Esquema del almacén** que no cierra | El stage `verificar-transformaciones` de `GeometriaFactory-Infrastructure`, y después el arranque en dos fases, que **detiene el arranque** si la preparación no se completó | 0 pasos manuales; el servicio **no escucha** si no cerró |
| **Rutas** que cambian sin que el consumidor se entere | La batería de integración, que ejerce el servicio real por su protocolo | La batería entera en verde (`QG-02`) |

**La segunda fila tiene una propiedad que las otras dos no tienen**: su falla **no se puede ignorar en ejecución**. [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2 declara que **no hay modo de sólo lectura ni arranque parcial**, con el fundamento de que un servicio que atiende sobre un almacén en el que no se puede confiar es peor que uno que no arranca: «el segundo se nota en el despliegue, el primero se nota cuando alguien busca su trabajo y no está».

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisiones propias de este proyecto de código, y las dos salen de `ADR-08` §7.** Primera: **todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa**, de modo que el mensaje que lo introduce no puede quedar aislado en una rama que se fusione sola. Segunda: **la colección de peticiones se actualiza en la misma intervención en que cambia la superficie**; una confirmación que agrega un punto de acceso y no toca la colección deja la demostración de la etapa fallando, que es la señal correcta.

**Y una tercera que esta categoría agrega, derivada de `QG-05`**: agregar un punto de acceso **es siempre un cambio que hay que declarar**, aunque sea aditivo, porque cambia el recuento de la guardia de admisión. No sube mayor por sí solo; lo que exige es que el pull request diga **de qué lado de la guardia queda**, y `TC-07` lo verifica en las dos direcciones.

## 3. Herramienta de cálculo de la versión

**Se declara por su función, y esta categoría no la elige**: ninguna fuente la nombra, y `PA-07` de `05` §11 deja los nombres definitivos y **las versiones exactas de los paquetes** anclados en la etapa `a`.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | **Las tres clases de §1 que la compilación no detecta.** Ninguna herramienta de comparación de superficie vería una configuración de intercambio divergente, un esquema que no cierra ni una ruta que sólo el consumidor conoce |

## 4. Modelo de ramas

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**; y sin OK explícito no se avanza (intake §10, §15 y §17.5.P.7).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, **incluida la batería de integración completa**, que vive acá y que ninguna otra canalización del producto puede correr.
- **Todo pull request que agregue o cambie un punto de acceso reejecuta `TC-07` en las dos direcciones sobre los quince.** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 lo llama **el control que más veces hay que ejercer**.
- **Ninguna etapa se cierra sin etiqueta**, porque la reversión del servidor propio depende de ella: no hay imagen publicada a la que volver.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §5.

## 5. Canales

**No hay canales de publicación**, y hay **un** destino de despliegue.

`Rules-Devops.md` §4.3 pide declarar canales `preview` y `stable`; esa figura pertenece a artefactos que se publican y se consumen por versión. Acá el artefacto **no se publica**: el intake §17.5.P.7 declara la imagen construida **en destino desde el repositorio, sin publicar en un registro**, y `redistribuible` es false.

| Figura del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Canal `preview` | **No existe** | No hay registro ni integrador que consuma un anticipo. Lo que un anticipo compraría —probar antes de que llegue a producción— lo compra la puerta `PT-04`, que ejercita el arranque completo **antes** de que exista la oportunidad de desplegar |
| Canal `stable` | **Se corresponde con el único destino**: el servidor propio | Intake §17.5.P.7 |
| Despliegue **canario** | **No existe.** Sin proxy inverso no hay despliegue con solapamiento, y el almacén tiene **escritor único** | Intake §17.5.P.8 y §17.5.P.12; [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 |
| Sufijos de anticipo `-alpha`, `-beta`, `-rc` | **No se usan** | Las etiquetas del producto son **de etapa cerrada**, no de anticipo (intake §15 y §17.5.P.7) |

## 6. Política de cambios incompatibles

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo lo funda la propia [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2: **no hay a quién darle plazo**, porque el único consumidor es del mismo producto. Lo que rige en su lugar son las convenciones impuestas de `ADR-08` §7 y sus métricas de §8:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| **Ninguna ruta lleva prefijo ni sufijo de versión**, y ningún punto de acceso convive con una forma anterior de sí mismo | Inspección de los **quince** puntos | `ADR-08` §7 y §8, primeras dos métricas |
| **Todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa** | `QG-08` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa**; revisión de cada etapa que toque el ensamblado | Intake §17.4.P.3; `ADR-08` §8, sexta métrica |
| La **colección de peticiones** se actualiza en la misma intervención en que cambia la superficie, **se reproduce en cinco pasos o menos y no inventa datos de prueba** | `QG-15`, con `TC-35`, al cierre de la etapa que la incorpora | `ADR-08` §7 y §8, cuarta y quinta métrica |
| **0** etapas cerradas sin etiqueta | Inspección del historial | `ADR-08` §8, tercera métrica |
| Un punto de acceso nuevo **declara de qué lado de la guardia queda** | `QG-05`, con `TC-07` en las dos direcciones. **Exactamente 4 fuera, ni uno más** | `05` §9, primer riesgo |
| Todo cambio mayor recibe su fila en el registro de cambios del producto, **escrita en la rama de la etapa** | Revisión del pull request, que **es** el punto de control | Intake §17.5.P.7 |

**Las seis métricas de `ADR-08` §8 se adoptan sin agregar ninguna**, y las seis figuran arriba o en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §5.

**Y una ausencia que `ADR-08` §2 sostiene y esta categoría no reabre**: **la pasarela de reenvío del front no se implementa**. El intake la declara **especificada y no implementada**, y su condición de reingreso está escrita: descarga de archivos, carga directa desde el navegador o migración del front a ejecución en el navegador. **Ninguna de las tres está en el tramo comprometido**, y por eso esta canalización no la contempla.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.5.P.7 declara **sin excepciones**, y transcribe las **cinco** reglas con las que `ADR-08` §2 reemplaza al versionado de rutas. Hace operativa la tercera: las **tres** clases de cambio que la compilación no detecta, cada una con dónde se la atrapa en la canalización y con qué umbral, y con la precisión de que la del esquema del almacén **no se puede ignorar en ejecución** porque el arranque en dos fases detiene el servicio. Agrega **tres** precisiones sobre los mensajes de confirmación, una de ellas propia: agregar un punto de acceso exige declarar de qué lado de la guardia queda. Declara la herramienta de cálculo por su función sin elegirla, el modelo de ramas con la batería de integración que **ninguna otra canalización del producto puede correr**, la ausencia de canales y de canario con sus motivos, y la política de cambios incompatibles con **seis** obligaciones, adoptando las seis métricas de `ADR-08` §8 sin agregar ninguna y sosteniendo la ausencia declarada de la pasarela de reenvío. |
