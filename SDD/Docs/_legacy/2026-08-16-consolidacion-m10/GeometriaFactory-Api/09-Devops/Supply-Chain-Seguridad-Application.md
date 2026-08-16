# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §9; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.0 §3; [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §14, §17.1.P.1 · GeometriaFactory-Application, §17.1.P.5 · GeometriaFactory-Application y §17.1.P.8 · GeometriaFactory-Application
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. La autorización como preocupación de cadena de suministro](#7-la-autorización-como-preocupación-de-cadena-de-suministro)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

## 1. Inventario de componentes

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Ninguna.** La única dependencia core es `GeometriaFactory.Domain`, que es del mismo producto y a su vez no tiene ninguna | Intake §17.1.P.1 · GeometriaFactory-Application y §17.1.P.1 · GeometriaFactory-Domain |
| Referencias a otros proyectos de código del producto | Exactamente **1** | `QG-05`, con `TC-04027` |
| Referencias a bibliotecas de persistencia, transporte, serialización o marco web | **0** | `QG-05`, bloqueante |
| Artefacto publicado | **Ninguno**: `redistribuible` es false | Intake §13; `05` §5 |

**Decisión: el inventario se emite en la unidad desplegable que embebe este ensamblado**, no acá. Lo que este proyecto de código aporta a ese inventario es **una fila con un solo hijo, que a su vez no tiene ninguno**, y un gate que lo sostiene: `QG-05`, con umbral **1 y 0**.

**La ausencia de marco web merece un párrafo, porque es la que más se rompe sola.** La tentación característica de una capa de casos de uso es tomar prestado un tipo del marco web —un resultado, un tipo de acción, una excepción de protocolo— para no escribir el propio. `QG-05` lo prohíbe con umbral **0**, y el efecto sobre la cadena de suministro es que **este ensamblado no arrastra ninguna dependencia transitiva al proceso que lo carga**: todo lo que entra a la imagen del backend por esta vía es código del propio producto.

## 2. Firma del artefacto

**No se firma acá.** No hay canal por el que un integrador reciba este ensamblado: sus consumidores lo obtienen por referencia de proyecto y lo embeben en su propio artefacto. La firma tiene sujeto en **lo que sale del repositorio** —la imagen del backend y la publicación del front— y esa decisión pertenece a las categorías 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

**Lo que sí rige acá es la integridad del origen**: etiqueta por etapa cerrada y reversión apoyada en ella ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 y §6, [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7).

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero. **La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia de los dos artefactos que se despliegan.

## 4. Análisis de dependencias

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Referencias a otros proyectos de código del producto | Exactamente **1** | `QG-05`, con `TC-04027` y la revisión del pull request | **Bloqueante** |
| Referencias a bibliotecas de persistencia, transporte, serialización o marco web | **0** | El mismo gate | **Bloqueante** |
| Pruebas de esta capa que abren el almacén real | **0** | `QG-04`, con `TC-04026` | **Bloqueante** |
| Actualización automática de dependencias | **No aplica**: no hay dependencias externas que actualizar | — | — |

**Sin dependencias externas, el análisis de composición no tiene sujeto y lo que corresponde verificar es que ese cero se sostenga.** Las tres primeras filas son esa verificación, y las tres ya bloquean desde la Fase E: esta categoría no agrega ninguna comprobación nueva, las ubica en el stage donde corren. `QG-05` corre en `build`, que es el más barato, y es **la propiedad que sostiene a `QG-04`**: sin biblioteca de persistencia declarada, una prueba de esta capa no tiene con qué abrir un almacén.

**La regla de anclaje de versiones del producto rige igual**: el intake, en el encabezado de su Parte C, declara que toda versión de paquete se fija explícitamente y que un cambio de versión mayor se documenta, **nunca como efecto colateral de una actualización**. Acá alcanza al ejecutor de pruebas y al recolector de cobertura, que son herramientas del proyecto de pruebas y no dependencias del ensamblado.

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**», y no «sin errores» | Intake §17.1.P.8 · GeometriaFactory-Application, por remisión a §17.1.P.8 · GeometriaFactory-Domain; `QG-01` |
| Estático de estructura | **Existe, bloquea, y es la verificación característica de este proyecto de código**: `QG-05` sobre el archivo de proyecto, `QG-06` sobre el catálogo de las **36** condiciones en las dos direcciones, `QG-08` sobre los **once** orquestadores y `QG-09` sobre la proyección de listado | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Dinámico | **No aplica acá, y tiene sujeto en otro proyecto de código**: este ensamblado no expone ninguna superficie de red. La que un análisis dinámico ejercitaría es la HTTP, que expone `GeometriaFactory-Api` | Intake §17.1.P.3 · GeometriaFactory-Application: «no aplica hacia afuera del proceso» |
| Detección de secretos en las confirmaciones | **Recomendada a nivel producto**: este proyecto de código no maneja secretos, pero comparte repositorio con los que sí | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la plataforma de ejecución | Decisión de plataforma del producto, no parche de este proyecto de código. A diferencia de `GeometriaFactory-Contracts`, **este ensamblado se carga en un solo proceso**, de modo que una bajada de versión del front no lo alcanza | El Product Owner, con constancia en el punto de control |
| Vulnerabilidad sobre una dependencia de este proyecto de código | **No tiene sujeto**: no hay dependencias externas. Si alguna vez la hubiera, el primer problema es `QG-05` | — |
| Vulnerabilidad sobre la herramienta de pruebas o de cobertura | Se ancla la versión corregida por la regla de anclaje del intake, y se registra en el punto de control de la etapa. **No alcanza al ensamblado que se despliega**: son herramientas del proyecto de pruebas | El equipo, con constancia |
| Vulnerabilidad sobre la unidad desplegable que lo embebe | Es de la categoría 09 de `GeometriaFactory-Api`. Este ensamblado sólo tiene que poder reconstruirse desde su etiqueta, y puede | Categoría 09 de `GeometriaFactory-Api` |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso, que impide que una vulnerabilidad conocida quede sin tratar en silencio.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

## 7. La autorización como preocupación de cadena de suministro

Esta sección existe porque en este proyecto de código la cadena de suministro clásica —dependencias, inventario, firma— **no es donde está el riesgo**, y decirlo sin ofrecer dónde sí está dejaría el documento vacío.

El riesgo real de esta capa es **que una comprobación de autorización deje de ejercerse en un camino nuevo**, y no llega por una dependencia: llega por un caso de uso que alguien agrega. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §2 lo pone como eje de la prioridad del proyecto de código citando su caso más agudo: `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad **sin resolver antes la marca de cambio de contraseña pendiente**. Sus tres propiedades, desde el punto de vista de la seguridad de la construcción:

| Propiedad | Por qué importa acá |
| --- | --- |
| **Entra de a un caso de uso por vez, y compila** | Ninguna herramienta de análisis de composición lo detectaría: no es una dependencia, es un orquestador nuevo que no llamó a una comprobación |
| **Se verifica con un recuento, no con un juicio** | `QG-07` mide **4 de 4** comprobaciones con prueba de su negativa **sin base de datos**, y **1** sola prueba de que la cuarta corta antes que las otras tres |
| **Su verificación no necesita ambiente** | Es la consecuencia útil de que la capa se pruebe entera con dobles: la comprobación más sensible del proyecto de código se puede ejercer en el stage `test`, sin levantar nada |

**La conclusión operativa para el pipeline** es que la comprobación de seguridad más valiosa de este proyecto de código corre **en cada pull request que agrega o cambia un caso de uso**, y se cierra **al cerrar la etapa** con `QG-07` sobre la matriz entera. Es la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 materializa como trigger propio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría. Declara que no se emite inventario propio ni se firma, con el fundamento de que el ensamblado no sale del repositorio, y que su aporte al inventario del producto es **una fila con un solo hijo que a su vez no tiene ninguno**. Explica por qué la ausencia de marco web es la que evita dependencias transitivas hacia el proceso que lo carga. Fija como objetivo el **primer nivel** de integridad de la construcción con su brecha declarada. Declara la política ante vulnerabilidades **sin plazos en horas ni días**, con la precisión de que este ensamblado se carga en un solo proceso. Cierra con la sección propia: **la autorización es acá la preocupación de cadena de suministro que importa**, entra de a un caso de uso, compila, se mide con un recuento y no necesita ambiente para verificarse. |
