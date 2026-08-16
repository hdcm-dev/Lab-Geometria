# US-04011 — Conservar el texto original íntegro al cargar y al reeditar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04011-Conservar-El-Texto-Original-Integro-Al-Cargar-Y-Al-Reeditar.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el texto que el alumno pegó se entregue tal cual y no se reescriba en ningún camino**, para **que el trabajo guardado sea exactamente el que su programa produjo, incluso cuando la interpretación falla**.

## 2. Contexto

`RN-04008` fija que el texto original se conserva íntegro y nunca se reescribe. El contrato de uso es [`CU-00026`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md), y `05` §10.2 declara el tramo de esta capa: el texto se entrega tal cual **ni cuando la interpretación falla**.

## 3. Criterios de aceptación

- Given un texto con comas finales y claves sinónimas tal como lo emite el programa del alumno, When se lo carga, Then el texto entregado al repositorio es **idéntico carácter por carácter** al recibido.
- Given un envío cuya interpretación produce errores de validación, When se resuelve el caso de uso, Then el texto original **sigue intacto**: no se normaliza, no se recorta y no se sustituye por una versión corregida.
- Given una reedición, When se reemplaza el texto, Then el texto nuevo se conserva íntegro y el anterior se descarta entero, sin mezclas.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-04004 |
| RN e invariantes que ejerce | RN-04008 |
| Componente de `05` §3.1 | Orquestación del trabajo |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04015 |
| Tests previstos en 08 | Comparación del texto entregado contra el recibido, con doble del repositorio |

## 5. Prioridad y estimación

`Must` por `RN-04008`, y porque el criterio de transición `f` → `g` exige que el texto original se conserve íntegro y nunca se reescriba.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**El tramo principal de `RN-04008` no está acá.** `GeometriaFactory-Infrastructure` §6 lo declara suyo —es donde el texto se escribe y por lo tanto donde puede perderse— y `GeometriaFactory-Api` declara que **el borde del proceso es el primer lugar donde el texto puede alterarse**. Lo que esta capa aporta es no reescribirlo en ninguno de sus caminos.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
