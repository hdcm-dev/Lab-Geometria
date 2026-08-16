# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §8; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Estrategia-Calidad.md) 1.0 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10 (normativa y presupuesto), §13, §17.1.P.1 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Domain y §17.1.P.8 · GeometriaFactory-Domain
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. Qué de esta política es propio y qué es del producto](#7-qué-de-esta-política-es-propio-y-qué-es-del-producto)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto —ni el intake, ni las categorías 02 a 08 de este proyecto de código— declara política de cadena de suministro. `Rules-Devops.md` §2.1 la exige para los ocho tipos D8, de modo que **todo lo que este documento decide es una decisión de esta categoría y va declarada como tal**. No se le atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta: la convención del corpus es nombrar las herramientas por su función, y la elección concreta pertenece al punto de control de la etapa `a`.

## 1. Inventario de componentes

**Este proyecto de código no emite inventario propio, y el motivo es que no tiene componentes que inventariar.**

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Ninguna**. Es biblioteca de clases **sin dependencias core**: no referencia persistencia, ni marco web, ni bibliotecas de serialización | Intake §17.1.P.1 · GeometriaFactory-Domain |
| Referencias salientes admitidas | **0** a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | `05` §8, tercera fila; `QG-04` |
| Artefacto publicado | **Ninguno**: `redistribuible` es false y no se publica en ningún feed | Intake §13 y §17.1.P.7 · GeometriaFactory-Domain |

**Decisión: el inventario de componentes se emite en la unidad desplegable que embebe a esta biblioteca**, no acá. El inventario que le sirve a alguien es el de lo que sale del repositorio —la imagen del backend y la publicación del front—, y ahí es donde este proyecto de código aparece como un componente más, con su versión calculada según [`Estrategia-Versionado.md`](Estrategia-Versionado.md).

**Lo que sí aporta este proyecto de código al inventario del producto es una propiedad verificable y bloqueante: su fila del inventario no tiene hijos.** `QG-04` lo hace cumplir en cada pull request. Un inventario con una dependencia nueva colgando de esta biblioteca es, antes que un hallazgo de cadena de suministro, un incumplimiento del gate que justifica el estilo entero del proyecto de código.

## 2. Firma del artefacto

**No se firma, y no es una omisión.** Se firma para que un integrador pueda verificar autoría e integridad de algo que recibió por un canal. Acá no hay canal ni integrador: el artefacto no sale del repositorio, y sus **dos** consumidores lo obtienen por referencia de proyecto dentro de la misma construcción (intake §13, columna de dependencias).

**Dónde sí corresponde firmar, y por qué no se decide acá:** la imagen del backend y la publicación del front son lo que cruza hacia un destino. La política de firma de esas dos pertenece a la categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`, y este documento **no la escribe por ellas**.

**Lo que sí rige acá es la integridad del origen**: toda etapa cerrada lleva etiqueta ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6, objetivo **100 %**), y la reversión se apoya en esa etiqueta ([`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7). Es lo que permite reconstruir exactamente el estado de cualquier demostración ya aprobada.

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero del marco de niveles de integridad de la construcción, y se declara con su brecha abierta en lugar de darlo por alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| La construcción es **automatizada y reproducible por guion**, no artesanal | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, y todo corre dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| Se emite **procedencia** del artefacto: qué se construyó, desde qué estado del repositorio y con qué entradas | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha, no se declara el nivel alcanzado |

**Por qué no se fija un nivel más alto.** Los niveles superiores exigen infraestructura de construcción con garantías propias —aislamiento del ejecutor, procedencia inalterable— que no tiene sujeto en un producto que el intake §10 declara **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero. Declarar un nivel que nadie va a poder acreditar sería peor que declarar el que se puede sostener con su brecha a la vista.

**La elevación queda como punto abierto** y es de nivel producto: sólo tiene sentido resolverla junto con la procedencia de la imagen del backend, que es el artefacto que efectivamente se despliega.

## 4. Análisis de dependencias

**Sin dependencias no hay análisis de composición que hacer, y lo que reemplaza al análisis es la verificación de que ese cero se sostiene.**

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Referencias salientes del archivo de proyecto | **0** a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | `QG-04`, con `TC-02024` y la revisión del pull request | **Bloqueante** |
| Actualización automática de dependencias | **No aplica**: no hay dependencias que actualizar | — | — |

**El día en que este proyecto de código adquiera una dependencia externa, el gate bloqueante se dispara antes que cualquier análisis de composición.** Es un orden afortunado: la primera pregunta no será «¿esa dependencia tiene vulnerabilidades?» sino «¿por qué el dominio adquirió una dependencia?», que es la que [`../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) obliga a contestar.

**La regla de anclaje de versiones del producto rige igual** aunque hoy no haya nada que anclar: el intake, en el encabezado de su Parte C, declara que toda versión de paquete se fija explícitamente y que un cambio de versión mayor es una decisión que se documenta, **nunca el efecto colateral de una actualización**. Esa regla es la que hace que una actualización automática silenciosa no sea admisible en este producto.

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**, integrado en el stage `build`: `CV-20` declara **0** advertencias nuevas del análisis estático, bloqueante por `CV-13`, que es el gate de construcción sin advertencias | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Criterios-Validacion.md) §5 y §3 |
| Estático de superficie | **Existe y bloquea**: las pruebas de inspección `TC-02023`, `TC-02024`, `TC-02026` y `TC-02027` revisan el proyecto de código sobre sí mismo —catálogo de condiciones, dependencias salientes, invariantes ejercidos y condiciones que no viajan como excepción— | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §1, «prueba de inspección» |
| Dinámico | **No aplica, y se declara en lugar de omitirse** | Un análisis dinámico ejercita una aplicación en ejecución. Este proyecto de código **no atiende peticiones ni abre conexiones** (`05` §8, cierre), de modo que no hay superficie que ejercitar. El análisis dinámico del producto tiene sujeto en `GeometriaFactory-Api`, que es quien expone la superficie HTTP |
| Detección de secretos en las confirmaciones | **Recomendada a nivel producto y no propia**: este proyecto de código no maneja secretos (intake §17.1.P.5 · GeometriaFactory-Domain), pero comparte repositorio con los que sí | Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**No se agrega ninguna herramienta nueva al pipeline.** Todo lo que esta sección declara ya está ejecutándose como gate de la categoría 08; lo que hace este documento es nombrarlo desde la perspectiva de la seguridad de la construcción, que es la que faltaba.

## 6. Política ante vulnerabilidades publicadas

**Sin dependencias externas, la superficie de vulnerabilidad propia de este proyecto de código es la de su plataforma de ejecución.** Esa plataforma la comparten los seis proyectos de código del producto que no son el visor, y su versión objetivo la fija el intake para todo el producto.

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad publicada sobre la plataforma de ejecución | Se trata como **decisión de plataforma del producto**, no como parche de este proyecto de código: la corrección es una actualización de la versión objetivo, que por la regla de anclaje del intake **se documenta y no se aplica como efecto colateral** | El Product Owner, con la constancia en el punto de control de la etapa en curso |
| Vulnerabilidad publicada sobre una dependencia de este proyecto de código | **No tiene sujeto hoy**: no hay dependencias. Si alguna vez la hay, la vulnerabilidad es el segundo problema; el primero es `QG-04` | — |
| Vulnerabilidad que afecta a la unidad desplegable que embebe esta biblioteca | Es de la categoría 09 de esa unidad. Este proyecto de código sólo tiene que poder **reconstruirse desde su etiqueta**, y puede | Categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días, y es deliberado.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y un plazo de remediación en horas sería exactamente el tipo de compromiso calendario que ninguna fuente da. Lo que sí rige es el mecanismo: **el punto de control de la etapa es bloqueante**, de modo que una vulnerabilidad conocida y no tratada llega a la mesa del Product Owner en el cierre de la etapa en curso y no puede quedar en silencio.

**Comunicación a integradores: no aplica.** No hay integradores externos —`redistribuible` es false— y el intake §10 declara que **ninguna normativa de compliance aplica**: es un laboratorio de aula con cuentas creadas para la materia.

## 7. Qué de esta política es propio y qué es del producto

La tabla existe para que la próxima categoría 09 —la de un proyecto de código que sí se despliega— sepa qué le queda por decidir y no lo dé por escrito acá:

| Preocupación | Dónde se decide |
| --- | --- |
| Cero dependencias salientes y su verificación | **Acá**, y ya bloquea: `QG-04` |
| Cero advertencias de construcción y análisis estático | **Acá**, y ya bloquea: `QG-01` con `CV-20` |
| Inventario de componentes de lo que se despliega | Categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` |
| Firma del artefacto desplegado | Las mismas dos |
| Procedencia de la construcción y elevación del nivel de integridad | Nivel producto, junto con la imagen del backend |
| Análisis dinámico de la superficie HTTP | Categoría 09 de `GeometriaFactory-Api` |
| Rotación de secretos del despliegue | Categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api` |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara de entrada que **ninguna fuente del producto declara política de cadena de suministro** y que todo lo que este documento decide es decisión de esta categoría. Declara que no se emite inventario propio ni se firma, con el fundamento de que no hay artefacto que salga del repositorio, y que las dos cosas corresponden a las unidades desplegables. Fija como objetivo el **primer nivel** de integridad de la construcción **con su brecha declarada** —la construcción ya es por guion, la procedencia no se emite— en lugar de declararlo alcanzado. Declara que el análisis de composición **no tiene sujeto** por no haber dependencias y que lo reemplaza el gate bloqueante de cero referencias salientes, que el análisis estático ya existe y bloquea, y que el dinámico no aplica por no haber superficie en ejecución. Declara la política ante vulnerabilidades **sin plazos en horas ni días**, porque el intake declara sin plazo calendario, y cierra con el reparto de qué se decide acá y qué en las categorías 09 de los proyectos de código que se despliegan. |
