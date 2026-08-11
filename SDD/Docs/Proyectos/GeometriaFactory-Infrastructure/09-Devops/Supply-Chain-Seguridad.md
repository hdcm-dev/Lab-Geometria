# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §5, §8, §9 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md); [`../05-Arquitectura-Tecnica/Adrs/ADR-05-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md); [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §14, §17.3.P.1, §17.3.P.3, §17.3.P.5, §17.3.P.8 y §17.3.P.9
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. Las dos bibliotecas sensibles, y qué las hace distintas del resto](#7-las-dos-bibliotecas-sensibles-y-qué-las-hace-distintas-del-resto)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

**Y una diferencia con las otras cuatro bibliotecas del producto.** Aquéllas declararon que su análisis de composición **no tiene sujeto**, porque no tienen dependencias externas. **Éste sí lo tiene**: el intake §17.3.P.1 declara **tres** dependencias core externas y **dos son sensibles**. Es la biblioteca del producto donde este documento más contenido real tiene.

## 1. Inventario de componentes

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias core externas | **Tres**, nombradas por su función: el proveedor de acceso a datos con su motor embebido, la biblioteca de **derivación de clave** y la de **emisión de acceso firmado** | Intake §17.3.P.1 |
| Herramienta de transformaciones | Instalada como **herramienta local del repositorio**, para que su versión quede versionada junto al código | Intake §17.3.P.1 |
| Dependencias del producto | `GeometriaFactory-Application` y `GeometriaFactory-Domain` | Intake §13 |
| Artefacto publicado | **Ninguno**: `redistribuible` es false | Intake §13; `05` §5 |

**Decisión: el inventario se emite en la unidad desplegable que embebe este ensamblado**, no acá; pero **este proyecto de código aporta la mayor parte de las dependencias externas de esa unidad**, y por eso su anclaje es una decisión de cadena de suministro y no de conveniencia.

**Las versiones exactas no figuran en este documento y no es una omisión.** El intake §17.3.P.1 declara que se anclan en la etapa `a` y se registran en ese momento, y la regla de anclaje del encabezado de la Parte C del intake prohíbe que una versión cambie **como efecto colateral de una actualización**. Escribir un número acá lo congelaría antes de que se decida, que es el defecto que este corpus viene corrigiendo en otras tablas.

## 2. Firma del artefacto

**No se firma acá.** No hay canal por el que un integrador reciba este ensamblado: su único consumidor es la composición de raíz de `GeometriaFactory-Api`, y lo embebe en su propio artefacto. La firma tiene sujeto en **lo que sale del repositorio** —la imagen del backend y la publicación del front—, y esa decisión pertenece a las categorías 09 de esas dos unidades.

**Y una distinción que en este proyecto de código hay que hacer explícita, porque las dos cosas se llaman igual.** Acá vive la **emisión de accesos firmados**, que es una **capacidad del producto** —firmar un acceso con una clave simétrica provista desde afuera— y **no es la firma de un artefacto de la cadena de suministro**. Son dos preocupaciones distintas:

| Preocupación | Qué firma | Quién verifica | Dónde vive en este corpus |
| --- | --- | --- | --- |
| Firma **de artefacto** | Un artefacto publicado, para que un integrador compruebe autoría e integridad | Un integrador externo, que acá **no existe** | No aplica en este proyecto de código |
| Firma **de acceso** | El acceso que el producto emite a una persona ya autenticada | El propio servicio, al recibirlo | Intake §17.3.P.5; gate `QG-12` |

**Lo que sí rige acá como integridad del origen**: etiqueta por etapa cerrada, reversión apoyada en ella, y **linaje de transformaciones inmutable** ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4).

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**. **La elevación es de nivel producto.**

**Una precisión propia.** Si alguna vez se emitiera procedencia del artefacto del servidor propio, **la parte que más valor tendría es la de este proyecto de código**: es el que introduce las dependencias externas, y una procedencia sin ellas describiría lo que menos riesgo tiene.

## 4. Análisis de dependencias

**Acá el análisis de composición tiene sujeto real**, a diferencia de las otras cuatro bibliotecas del producto.

| Comprobación | Umbral o criterio | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Peticiones de red originadas por los **dos motores** | Exactamente **0** | `QG-08`, con `TC-14`, inspección de dependencias de los dos motores | **Bloqueante** |
| Anclaje explícito de las **tres** dependencias core y de la herramienta de transformaciones | Toda versión **fijada explícitamente**, nunca cambiada como efecto colateral | Revisión del archivo de proyecto y del archivo de herramientas, en la etapa `a` y en cada cambio | Bloqueante como regla del intake, encabezado de la Parte C |
| Elección y anclaje de la **función de derivación de clave** | El intake declara dos opciones y **no elige**. La forma y el criterio los fija `ADR-04`; la elección concreta es de la etapa `a` | Punto de control de la etapa `a`. Registrado como `PD-03` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 | Bloqueante como tarea de la etapa `a` |
| Actualización automática de dependencias | **No se declara ninguna.** Contradiría la regla de anclaje | — | — |

**La primera fila es un gate de composición escrito como recuento, y conviene ver por qué está donde está.** El intake §17.3.P.3 declara que **el validador de figuras no hace red**: recibe texto y devuelve observaciones. `QG-08` no verifica esa intención en el código propio, sino **en las dependencias de los dos motores**: una biblioteca que hiciera una petición por dentro rompería la propiedad sin que ninguna línea del proyecto de código la mencione. Es exactamente el modo de falla que un análisis de composición existe para encontrar, y acá está escrito como **0**.

**La regla de anclaje de versiones no es una preferencia de esta categoría**: el intake, en el encabezado de su Parte C, la declara para los seis proyectos de código de la plataforma, y agrega que **un cambio de versión mayor es una decisión que se documenta**. En este proyecto de código alcanza a tres dependencias externas, a la herramienta de transformaciones y al motor de almacenamiento embebido (intake §17.3.P.9).

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**», que es la formulación de `QG-01`. El intake §17.3.P.8 la declara como «build en 0 sin advertencias» | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3, `QG-01`; intake §17.3.P.8 |
| Estático de estructura | **Existe y bloquea**: `QG-10` sobre las proyecciones de listado, `QG-11` sobre el texto original conservado, `QG-12` sobre la emisión de accesos y `QG-13` sobre el catálogo de las **17** condiciones en las dos direcciones | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Dinámico sobre superficie de red | **No aplica acá**: este ensamblado **no expone endpoints** y sus dos motores no hacen red. La superficie que un análisis dinámico ejercitaría es la HTTP, que expone `GeometriaFactory-Api` | Intake §17.3.P.3 |
| **Dinámico sobre almacenamiento** | **Existe, y es propio de este proyecto de código**: el stage `verificar-transformaciones` ejercita el arranque **sobre un almacén inexistente** y comprueba que el esquema queda completo sin paso manual | `QG-04`; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con el sujeto más sensible del producto**: este proyecto de código es el que trabaja con la clave de firma, aunque no la custodie | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la **biblioteca de derivación de clave** | Se ancla la versión corregida. **Si la corrección cambia los parámetros de derivación**, las contraseñas ya guardadas siguen verificándose porque `ADR-04` exige que los parámetros viajen **junto al valor derivado, sin valor por defecto silencioso** | El equipo, con constancia en el punto de control |
| Vulnerabilidad sobre la **biblioteca de emisión de acceso firmado** | Se ancla la versión corregida y **se despliega la unidad del servidor propio**. Los accesos vigentes caducan solos: el intake §17.5.P.5 declara vigencia **corta** y **sin acceso de refresco** | El mismo, y el Product Owner que ejecuta el despliegue |
| Vulnerabilidad sobre el **proveedor de acceso a datos o su motor embebido** | Se ancla la versión corregida y se ejercita el stage `verificar-transformaciones` **antes** de construir la imagen: un cambio de motor puede alterar cómo se aplica el linaje | El equipo |
| Vulnerabilidad sobre la plataforma de ejecución | Decisión de plataforma del producto. **Este ensamblado no llega al front**, de modo que una bajada de versión del front no lo alcanza | El Product Owner |
| Vulnerabilidad sobre la unidad desplegable que lo embebe | Es de la categoría 09 de `GeometriaFactory-Api` | Esa categoría |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

**Y un riesgo aceptado por escrito que esta categoría no reabre**: el intake declara que **las credenciales viajan en claro en el tramo entre el front y el servicio de datos** si ese salto es HTTP plano, con el túnel saliente como salida **documentada y no adoptada**. Alcanza a lo que este proyecto de código recibe, pero la decisión es del Product Owner y está registrada aguas arriba.

## 7. Las dos bibliotecas sensibles, y qué las hace distintas del resto

Esta sección existe porque en este proyecto de código **la cadena de suministro sí es donde está buena parte del riesgo**, y conviene separar qué protege cada mecanismo.

| Dependencia, por su función | Qué pasaría si estuviera comprometida | Qué la protege hoy |
| --- | --- | --- |
| **Derivación de clave** | Las contraseñas guardadas dejarían de estar protegidas, **sin ninguna señal visible**: el producto seguiría funcionando igual | El anclaje explícito de versión, los parámetros versionados junto al valor derivado ([`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)) y la revisión de todo cambio de versión mayor como decisión documentada |
| **Emisión de acceso firmado** | Se podrían emitir accesos válidos sin la clave, o filtrarse la clave. **Es la capacidad más sensible del producto** | El mismo anclaje, más `QG-12` —**0** emisiones sin clave de firma y **0** claves generadas al vuelo— y la clave viviendo **fuera del repositorio y fuera de la imagen** |

**Las dos comparten una propiedad que las distingue de cualquier otra dependencia del producto**: su compromiso **no produce ningún síntoma**. Un motor de dibujo comprometido se nota; una derivación de clave debilitada no. De ahí que el único mecanismo disponible sea **saber exactamente qué versión se está usando**, que es lo que la regla de anclaje del intake compra, y **no dejar que cambie sola**.

**La contribución de este proyecto de código a la seguridad del producto tiene además una parte que no es una dependencia**, y es la contraseña provisoria: `QG-09` mide **0** provisorias repetidas y **0** derivables del nombre, del correo ni de la fecha, y [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) fija la longitud y el alfabeto que lo sostienen. **No se verifica contra un registro de provisorias anteriores**, porque conservarlas exigiría guardar contraseñas en claro; la sostiene la impredecibilidad, y así lo declara `PA-06` de `05` §11, que esta categoría **hereda y no reabre**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría, con la diferencia de que **acá el análisis de composición sí tiene sujeto**: **tres** dependencias core externas, **dos** de ellas sensibles. Declara que el inventario se emite en la unidad desplegable pero que **este proyecto de código aporta la mayor parte de sus dependencias externas**, y que las versiones exactas **no se escriben acá** porque se anclan en la etapa `a`. Separa explícitamente la **firma de artefacto**, que no aplica, de la **firma de acceso**, que es una capacidad del producto. Fija como objetivo el **primer nivel** de integridad de la construcción con su brecha. Declara que `QG-08` es un gate de composición escrito como recuento y por qué mide **las dependencias de los dos motores** y no el código propio, y que el stage de verificación de transformaciones es **análisis dinámico sobre almacenamiento**. Cierra con la sección propia: **las dos bibliotecas sensibles comparten que su compromiso no produce ningún síntoma**, y por eso el único mecanismo disponible es el anclaje explícito de versión. |
| 1.1 | 2026-08-11 | **Corrección de atribución de cita, del mismo tipo que el `H-02` de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`, en una ocurrencia que el informe no listó.** La fila `Estático` de la tabla de análisis atribuía al intake §17.3.P.8 la formulación «en 0 y sin advertencias», que es la de `QG-01` de `08` §3; el intake §17.3.P.8 dice «build en 0 sin advertencias». Se separan las dos citas con su fuente propia. Trazabilidad upstream del intake a **1.22**, cuyas §17.3.x no cambiaron. |
