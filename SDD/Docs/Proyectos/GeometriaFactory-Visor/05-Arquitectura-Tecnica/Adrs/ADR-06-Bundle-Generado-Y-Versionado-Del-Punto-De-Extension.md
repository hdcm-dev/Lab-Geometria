# ADR-06 — El artefacto es un bundle generado, y su versionado es el del punto de extensión

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

`PRODUCT-INTAKE` §17.7.P.7 declara cuatro cosas sobre el artefacto de este proyecto de código: versionado semántico y convenciones de mensaje de confirmación como el resto del producto; **no se publica en ningún repositorio de paquetes**; su artefacto **es un archivo generado**, que si se versiona en el repositorio se versiona como salida reproducible y si se ignora lo genera el guion de construcción antes de publicar; y **nunca se edita a mano**.

Lo que hace especial al versionado acá es que este contrato **es el punto de extensión declarado del producto** (`PRODUCT-INTAKE` §18) y que `tiene_extensibilidad` es true sólo en este proyecto de código. La versión no gobierna a un consumidor externo —no lo hay— sino **la estabilidad de la promesa que hace posible reemplazar el motor de dibujo**.

Hay además una asimetría con los otros proyectos de código del producto: el anfitrión **no compila contra este artefacto**. Lo carga en el navegador e invoca sus funciones por interoperabilidad, de modo que **un cambio incompatible no rompe ninguna compilación**: se manifiesta en tiempo de ejecución.

Motivación upstream: `PRODUCT-INTAKE` §17.7.P.7, §17.7.P.8, §18; §16 (destino del bundle en el directorio de recursos estáticos del anfitrión).

## 2. Decisión

**Versionado semántico sin publicación**, con el artefacto tratado como **salida reproducible** y nunca editado a mano.

Lo que la versión gobierna es la **superficie pública del punto de extensión**: las seis funciones, las siete garantías y los siete códigos de condición. El criterio de §7 declara qué constituye cada clase de cambio, y su rasgo distintivo es que **ninguno de los cambios mayores lo detecta una compilación**: la mitigación es la revisión más el sample S-1, que ejerce el contrato entero.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Versionado semántico sin publicación, artefacto reproducible (**adoptada**) | Da vocabulario para declarar qué rompe al anfitrión y qué no; se apoya en las etiquetas por etapa del producto; el artefacto se puede regenerar desde el fuente | La versión no la verifica ninguna herramienta; y ningún cambio mayor lo detecta una compilación |
| Publicar el bundle en un repositorio público de paquetes | El artefacto sería resoluble por versión | El intake lo descarta explícitamente, `redistribuible` es false y no hay integradores externos: sería un portal para una comunidad que no existe |
| Sin versión propia, sólo la del producto | Cero ceremonia | Perdería la señal de que la superficie del **punto de extensión** cambió, que es exactamente lo que este proyecto de código tiene para declarar |
| Versionar el bundle a mano en el repositorio, editándolo cuando haga falta | Corrección inmediata sin regenerar | Contradice la regla del intake —el artefacto nunca se edita a mano— y rompe la reproducibilidad: el archivo dejaría de corresponder al fuente |

## 5. Consecuencias positivas

1. Un cambio de la superficie del punto de extensión queda **declarado como tal** antes de que lo descubra un anfitrión en ejecución.
2. El artefacto es **regenerable desde el fuente**, de modo que no hay estado que se pueda perder ni divergir.
3. El sample **S-1 funciona como prueba de contrato**: ejerce las seis funciones sin ninguna pieza del backend, y por eso detecta un cambio incompatible sin necesidad de levantar el producto.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que ningún cambio mayor lo detecte una compilación.** Es la asimetría con el resto del producto, y la mitigación es la revisión y el sample S-1.
2. **Se acepta que la versión no la verifique ninguna herramienta.** Es una convención sostenida por disciplina.
3. **Se acepta que agregar una función sea cambio menor y ocurra.** Ocurrió una vez, con la sexta, sin romper a ningún anfitrión escrito contra las cinco anteriores.
4. **Se acepta que la decisión de versionar o ignorar el artefacto en el repositorio quede abierta.** El intake admite las dos formas y la elección pertenece a 09; está registrada como punto abierto PA-05.

## 7. Implementación

Qué constituye cada clase de cambio sobre el punto de extensión:

| Clase | Qué la produce | ¿Lo detecta una compilación? |
| --- | --- | --- |
| **Mayor** | Quitar una función, renombrarla o cambiar qué recibe: rompe al anfitrión y al sample S-1 | No |
| **Mayor** | **Perder cualquiera de las siete garantías**, aunque las seis firmas no se toquen | No |
| **Mayor** | Cambiar la semántica de una entrada ya declarada del resultado de dibujo | No |
| **Menor** | Agregar una función. Así entró `establecerMovimiento`, sin romper a ningún anfitrión escrito contra las cinco anteriores | — |
| **Menor** | Agregar una entrada nueva al resultado de dibujo, conservando la semántica de las declaradas | — |
| **Menor** | Agregar un código de condición, que sólo puede nacer en la categoría 02 | — |
| **Sin efecto de contrato** | Cambiar la forma interna del identificador de instancia, mientras siga siendo opaco y cumpla sus tres propiedades semánticas. Que el anfitrión dependa de su forma es un defecto del anfitrión | — |
| **Parche** | Corregir el interior de la capa 3 sin cambiar la superficie ni las garantías | — |

- Convenciones de mensaje de confirmación y una rama por etapa, como el resto del producto.
- **El artefacto nunca se edita a mano.** Un guion propio genera sólo el bundle para el ciclo corto de trabajo; el guion general lo encadena con el resto de la construcción.
- El bundle se copia al directorio de recursos estáticos del anfitrión como paso final.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Ediciones manuales del artefacto generado | Exactamente **0** | Revisión: el artefacto siempre corresponde al fuente que lo generó |
| Reproducibilidad | Dos construcciones desde el mismo estado producen el mismo artefacto | Comparación de dos construcciones |
| Cambios mayores sin registro | Exactamente **0** | Revisión del pull request de la etapa, que **es** el punto de control |
| Garantías vigentes tras un cambio mayor | **7 de 7** verificadas antes de fusionar | Batería de 08 sobre las siete garantías |
| Recorrido del contrato por el sample S-1 | **6 de 6** funciones, con **0** servicios del backend disponibles | Ejecución del sample, en cinco pasos o menos |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.7.P.7, §17.7.P.8, §16, §16.1 y §18.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §2 y §5.
- [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §5.1 y §7.
- [`../Extensibilidad.md`](../Extensibilidad.md), que desarrolla el punto de extensión y el proceso de crecimiento de la fachada.
- ADR relacionadas: [`ADR-02`](ADR-02-Superficie-De-Seis-Funciones-Planas.md), [`ADR-04`](ADR-04-Motor-De-Dibujo-Empaquetado-Y-Aislado.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el versionado semántico sin publicación con el artefacto como salida reproducible, declara que lo que la versión gobierna es la superficie del punto de extensión, fija el criterio de cambio mayor con la columna que declara que **ninguno lo detecta una compilación**, evalúa cuatro alternativas y fija cinco métricas. |
