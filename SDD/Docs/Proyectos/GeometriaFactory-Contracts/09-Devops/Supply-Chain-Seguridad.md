# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5 y §8; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.1 §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §14, §17.4.P.1, §17.4.P.5 y §17.4.P.8
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. La superficie de exposición como preocupación de cadena de suministro](#7-la-superficie-de-exposición-como-preocupación-de-cadena-de-suministro)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

## 1. Inventario de componentes

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Ninguna**. Biblioteca de tipos de datos **sin dependencias**: no referencia el dominio | Intake §17.4.P.1 |
| Bibliotecas de serialización declaradas | **0**. Declararlas rompería las cero dependencias | `CV-22`, bloqueante |
| Referencias hacia `GeometriaFactory-Domain` | **0**, puerta bloqueante de construcción | Intake §17.4.P.8; `QG-02` |
| Artefacto publicado | **Ninguno**: `redistribuible` es false | Intake §13 y §17.4.P.7 |

**Decisión: el inventario se emite en las dos unidades desplegables que embeben este ensamblado**, no acá. Lo que este proyecto de código aporta a ese inventario es una fila **sin hijos**, y tres gates que lo sostienen: `QG-02`, `CV-22` y `CV-23` —el grafo entre las **ocho** familias de tipos es acíclico, con una única arista adicional que conserva su motivo declarado—.

**La ausencia de biblioteca de serialización merece un párrafo, porque es contraintuitiva.** Un ensamblado de tipos de transferencia es exactamente el lugar donde uno esperaría encontrar una: la tentación es anotar los tipos para controlar cómo se serializan. `CV-22` lo prohíbe, y la decisión de dónde vive esa configuración ya está tomada aguas arriba: [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §6 declara **una sola configuración de intercambio en todo el producto**, decidida en `GeometriaFactory-Api`. Desde la cadena de suministro, el efecto es que este ensamblado no arrastra ninguna dependencia transitiva a los dos procesos que lo cargan.

## 2. Firma del artefacto

**No se firma acá.** No hay canal por el que un integrador reciba este ensamblado: sus dos consumidores lo obtienen por referencia de proyecto y lo embeben en su propio artefacto. La firma tiene sujeto en **lo que sale del repositorio** —la imagen del backend y la publicación del front— y esa decisión pertenece a la categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

**Lo que sí rige acá es la integridad del origen**: etiqueta por etapa cerrada y reversión apoyada en ella ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4, [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7).

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** `scripts/build.sh` es el mismo guion en la máquina de quien construye y en el pipeline, dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero. **La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia de los dos artefactos que se despliegan.

## 4. Análisis de dependencias

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Referencias hacia `GeometriaFactory-Domain` | **0** | `QG-02`, con `TC-08020` y la revisión del pull request | **Bloqueante** |
| Bibliotecas de serialización declaradas | **0** | `CV-22` | **Bloqueante** |
| Ciclos entre las **ocho** familias de tipos | **0**, con **1** arista adicional —reseteo hacia cuentas— que conserva su motivo declarado | `CV-23` | **Bloqueante** |
| Actualización automática de dependencias | **No aplica**: no hay dependencias que actualizar | — | — |

**Sin dependencias externas, el análisis de composición no tiene sujeto y lo que corresponde verificar es que ese cero se sostenga.** Las tres primeras filas son esa verificación, y las tres ya bloquean desde la Fase E: esta categoría no agrega ninguna comprobación nueva, las ubica en el stage donde corren.

**La regla de anclaje de versiones del producto rige igual**: el intake, en el encabezado de su Parte C, declara que toda versión de paquete se fija explícitamente y que un cambio de versión mayor se documenta, **nunca como efecto colateral de una actualización**.

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «sin advertencias» y no «sin errores», y su incumplimiento es la entrada de diagnóstico `DXC-09` | Intake §17.4.P.8; `QG-01`; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §3 |
| Estático de superficie | **Existe, bloquea y es la verificación principal de este proyecto de código**: cinco de los nueve gates se comprueban leyendo la superficie pública, escritos como recuentos | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 |
| Dinámico | **No aplica acá, y tiene sujeto en otro proyecto de código**: este ensamblado no ejecuta nada. La superficie que un análisis dinámico ejercitaría es la HTTP, que expone `GeometriaFactory-Api` | `05` §8, cierre |
| Detección de secretos en las confirmaciones | **Recomendada a nivel producto**: este proyecto de código no maneja secretos, pero comparte repositorio con los que sí | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la plataforma de ejecución | Decisión de plataforma del producto, no parche de este proyecto de código. Con una precisión propia: **este ensamblado se carga en los dos procesos**, de modo que su versión objetivo tiene que seguir siendo cargable por los dos, incluso si el front baja la suya por la puerta `PT-01.a` | El Product Owner, con constancia en el punto de control |
| Vulnerabilidad sobre una dependencia de este proyecto de código | **No tiene sujeto**: no hay dependencias. Si alguna vez la hay, el primer problema es `QG-02` o `CV-22` | — |
| Vulnerabilidad sobre una unidad desplegable que lo embebe | Es de la categoría 09 de esa unidad. Este ensamblado sólo tiene que poder reconstruirse desde su etiqueta, y puede | Categorías 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` |
| Corrección que exige un cambio incompatible del contrato | Se aplica la política de §6 de [`Estrategia-Versionado.md`](Estrategia-Versionado.md): se declara incompatible y **las dos unidades se despliegan juntas** | El mismo |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso, que impide que una vulnerabilidad conocida quede sin tratar en silencio.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

## 7. La superficie de exposición como preocupación de cadena de suministro

Esta sección existe porque en este proyecto de código la cadena de suministro clásica —dependencias, inventario, firma— **no es donde está el riesgo**, y decirlo sin ofrecer dónde sí está dejaría el documento vacío.

El riesgo real de este ensamblado es **lo que deja cruzar la frontera**, y tiene nombre propio en el corpus: `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 —una dirección de servicio interno, una ruta del almacén o un secreto que cruzan dentro de un mensaje—, con `RA-03` del intake §14 como regla de nivel producto. Sus tres propiedades, desde el punto de vista de la seguridad de la construcción:

| Propiedad | Por qué importa acá |
| --- | --- |
| **Entra de a un campo por vez, y compila** | Ninguna herramienta de análisis de composición lo detectaría: no es una dependencia, es un campo nuevo en un tipo propio |
| **Se verifica con un recuento, no con un juicio** | `QG-03` mide **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza |
| **No admite excepción** | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §6 lo declara como su única situación sin salida admitida |

**La conclusión operativa para el pipeline** es que la comprobación de seguridad más valiosa de este proyecto de código corre **en cada pull request que agrega o cambia un campo**, y no en un stage periódico de análisis. Es la cadencia que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara, y [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 la materializa como trigger.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría. Declara que no se emite inventario propio ni se firma, con el fundamento de que el ensamblado no sale del repositorio, y que su aporte al inventario del producto es **una fila sin hijos** sostenida por tres gates. Explica por qué un ensamblado de tipos de transferencia **no declara biblioteca de serialización** y qué efecto tiene sobre las dependencias transitivas de los dos procesos. Fija como objetivo el **primer nivel** de integridad de la construcción con su brecha declarada. Declara la política ante vulnerabilidades **sin plazos en horas ni días**, con la precisión de que este ensamblado se carga en los dos procesos. Cierra con la sección propia: **la superficie de exposición es acá la preocupación de cadena de suministro que importa**, entra de a un campo, compila, se mide con un recuento y no admite excepción. |
