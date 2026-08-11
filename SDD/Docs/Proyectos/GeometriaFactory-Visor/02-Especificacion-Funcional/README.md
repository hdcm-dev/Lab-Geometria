# 02 · Especificación Funcional — GeometriaFactory-Visor

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `Especificacion-Funcional.md` §3 (catálogo de casos de uso) y §5 (matriz de trazabilidad); `../../../00-Contexto/Vision-Producto.md` §3 y §9; `../../../00-Contexto/Alcance-Producto.md` §4.1; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; `../../../01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §5 y §7; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14 (RA-02) y §17.7
**Trazabilidad downstream:** 03-UX-UI-DX (variante DX) de este proyecto de código, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

---

## Tabla de contenido

- [1. Punto de entrada](#1-punto-de-entrada)
- [2. Documentos vigentes](#2-documentos-vigentes)
- [3. Artefactos omitidos, con su motivo](#3-artefactos-omitidos-con-su-motivo)
- [4. Cómo leer esta sección](#4-cómo-leer-esta-sección)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Punto de entrada

Esta sección especifica **qué hace** el archivo de guion del visualizador tridimensional del producto Fábrica de Geometría, y con qué contrato lo hace. El punto de entrada es [`Especificacion-Funcional.md`](Especificacion-Funcional.md), que trae el catálogo de casos de uso y la matriz de trazabilidad.

`GeometriaFactory-Visor` es un proyecto de código de tipo `library`, nivel 0 del orden topológico del producto y **sin dependencias**. Es un **visualizador puro**: no hace red, no persiste nada, no conoce a ninguna persona y no valida ningún trabajo.

## 2. Documentos vigentes

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro, catálogo de casos de uso, matriz NB → CU → RN → US y propiedades transversales con umbral | Propuesto |
| [`Definicion-Contrato-De-Fachada.md`](Definicion-Contrato-De-Fachada.md) | Documento de concepto central: vocabulario, ciclo de vida, garantías, semántica de las **seis** funciones, códigos de condición y compatibilidad de la superficie pública. Acuña la sexta función, `establecerMovimiento` | Propuesto |
| [`Casos-De-Uso/CU-01-Inicializar-Instancia-Del-Visor.md`](Casos-De-Uso/CU-01-Inicializar-Instancia-Del-Visor.md) | Contrato de uso de `inicializar`: crear la instancia sobre un elemento de dibujo y obtener su identificador | Propuesto |
| [`Casos-De-Uso/CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md`](Casos-De-Uso/CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) | Contrato de uso de `cargarJson`: dibujar las piezas por índice y devolver el resultado de dibujo | Propuesto |
| [`Casos-De-Uso/CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md`](Casos-De-Uso/CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md) | Contrato de uso de `seleccionarPieza`: resaltado exclusivo por índice, que sincroniza el árbol con la escena | Propuesto |
| [`Casos-De-Uso/CU-04-Redimensionar-La-Escena.md`](Casos-De-Uso/CU-04-Redimensionar-La-Escena.md) | Contrato de uso de `redimensionar`: ajustar la escena al tamaño vigente del elemento de dibujo | Propuesto |
| [`Casos-De-Uso/CU-05-Destruir-La-Instancia-Y-Liberar-Recursos.md`](Casos-De-Uso/CU-05-Destruir-La-Instancia-Y-Liberar-Recursos.md) | Contrato de uso de `destruir`: liberar geometrías, materiales y contexto gráfico e invalidar el identificador | Propuesto |
| [`Casos-De-Uso/CU-06-Ejercitar-La-Fachada-Sin-Backend.md`](Casos-De-Uso/CU-06-Ejercitar-La-Fachada-Sin-Backend.md) | Caso de uso transversal: recorrer las seis funciones desde una página integradora sin backend. Materializa el sample S-1 | Propuesto |
| [`Casos-De-Uso/CU-07-Gobernar-El-Movimiento-Automatico-De-La-Escena.md`](Casos-De-Uso/CU-07-Gobernar-El-Movimiento-Automatico-De-La-Escena.md) | Contrato de uso de `establecerMovimiento`: prender y apagar los dos movimientos automáticos sobre una instancia viva, sin reconstruirla y sin perder la selección | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, términos polisémicos resueltos y términos referenciados del glosario raíz | Propuesto |

Un solo archivo por nombre lógico y ninguna versión superada: la carpeta `Casos-De-Uso/_legacy/` no existe todavía y no corresponde crearla.

## 3. Artefactos omitidos, con su motivo

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Un visualizador puro no tiene reglas de dominio.** Las reglas que rigen el trabajo del alumno —qué se puede finalizar, qué produce advertencia, quién ve qué— las decide el backend, y este proyecto de código no participa de ninguna de esas decisiones (PRODUCT-INTAKE §14 RA-02, §17.7 P.5 y P.11 punto 4). `Rules-Especificacion-Funcional.md` §2.2 declara las RN como no obligatorias para `library`. Lo que sí existe son **condiciones de contrato**, declaradas en `Definicion-Contrato-De-Fachada.md` §6, que no son invariantes del dominio y no deben escribirse como RN |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitidos por tipo y por flag.** `Rules-Especificacion-Funcional.md` §2.1 y §2.2 los excluyen para `library` sin estado, y el flag `tiene_persistencia` de este proyecto de código es **false**: el archivo de guion no guarda estado entre páginas ni escribe en el almacenamiento del navegador, por prohibición explícita de PRODUCT-INTAKE §17.7 P.4. Sin entidades persistidas no hay modelo conceptual que levantar, y sin modelo no hay reglas conceptuales |

Las dos omisiones son deliberadas y están declaradas también en `Especificacion-Funcional.md` §7. No son artefactos pendientes.

## 4. Cómo leer esta sección

1. Empezar por `Especificacion-Funcional.md` §2, que fija qué es y qué no es este proyecto de código. Es lo que evita el error más frecuente al leerlo: suponer que el archivo de guion participa de decisiones que no toma.
2. Seguir por `Definicion-Contrato-De-Fachada.md`, que declara una sola vez el vocabulario y los códigos que los siete casos de uso referencian.
3. Leer los casos de uso en el orden del ciclo de vida —CU-01, CU-02, CU-03, CU-04, CU-05, y después CU-07, que gobierna el movimiento sobre una instancia ya viva— y cerrar con CU-06, que los recorre juntos sin backend. **CU-07 lleva el número más alto y se lee antes que CU-06**: se emitió más tarde, con la sexta función, y no se renumeró para no romper referencias ya emitidas aguas abajo.
4. Tener a mano `Glosario-Funcional.md` si se entra por una sección suelta: es donde se resuelve a qué apunta la forma desnuda «pieza».

Advertencia para las categorías aguas abajo: los nombres de las cinco primeras funciones son los que declara el intake y no se cambian; el de la **sexta**, `establecerMovimiento`, lo acuñó `Definicion-Contrato-De-Fachada.md` §4.6 por decisión del Product Owner del 2026-08-09 y **el intake ya lo consolidó** en su versión 1.6, de modo que §17.7 P.3 declara las seis; los nombres de funciones internas, de clases y de campos del resultado **no están fijados acá** y se anclan en la etapa que implementa la fachada. La elección del motor de dibujo tridimensional y su versión es decisión de 05-Arquitectura-Tecnica, y la maqueta de este proyecto de código pertenece a su propia fase, posterior a esta.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Índice navegable de los nueve documentos vigentes de la categoría, declaración de las dos omisiones de artefacto con su motivo y guía de lectura para revisores externos. |
| 1.0 | 2026-08-08 | Corrección absorbida del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-10**: la cabecera declaraba como upstream dos carpetas enteras, que no son vínculo verificable; pasa a citar los documentos y las secciones efectivamente consumidos. |
| 1.0 | 2026-08-09 | Absorción de las **dos decisiones del Product Owner** de la **Fase B2**. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). §2 suma **`CU-07`**, el contrato de uso de la sexta función de la fachada, y la sección pasa a tener **diez** documentos vigentes; la descripción del concepto central pasa a **seis** funciones y declara que acuña la sexta. §4 actualiza el orden de lectura —CU-07 se lee antes que CU-06 aunque lleve número más alto, y se declara por qué no se renumera— y la advertencia aguas abajo distingue las cinco funciones que nombra el intake de la sexta, acuñada acá y **pendiente de consolidación en el intake**. Las dos omisiones de artefacto no cambian. |
| 1.1 | 2026-08-09 | **Cierra la parte del hallazgo `F26-11`** que alcanza a este índice, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **§4**: la advertencia aguas abajo declaraba la sexta función «sujeta a la consolidación del intake, que todavía declara cinco», y el intake la consolidó en su versión **1.6**: §17.7 P.3 declara las seis. Se corrige la afirmación. Ningún documento de la sección, ningún caso de uso y ningún orden de lectura cambia. |
