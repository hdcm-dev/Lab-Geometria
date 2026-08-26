> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06003-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06003-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md`](../../US-06003-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06003 — Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06003-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **recibir las piezas del texto con su posición y sus componentes, y que la posición de las que no se reconstruyeron quede reservada**, para **que el índice siga siendo la identidad de la pieza y el dibujo y el árbol puedan señalarse entre sí**.

## 2. Contexto

`NB-00004` pide interpretación fiel y `NB-00006` recibe de acá **la identidad posicional de la pieza**, que es el dato con el que después se dibuja y se arma el árbol (`02` §7.2). El contrato de uso es [`CU-06001`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md), y `RC-06002` declara la identidad posicional.

## 3. Criterios de aceptación

- Given un texto con varias figuras, When se lo interpreta, Then cada pieza viene con su **posición** y con sus componentes.
- Given una figura que no se pudo reconstruir, When se devuelve el conjunto, Then **su posición queda reservada** y el conjunto **no se renumera**.
- Given el escenario `E-7`, When se lo interpreta, Then se reconstruyen los **seis** tipos que el producto sabe dibujar, tres volumétricos y tres planos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00006 (parcial) |
| CU cubiertos | CU-06001 |
| RN que ejerce | RN-06009, con tramo principal acá |
| Componente de `05` §3.1 | Motor de interpretación de figuras |
| Reglas conceptuales de modelo | `RC-06002`, `RC-06004` —la familia no se persiste— |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06016, BT-06018, BT-06024 |
| Tests previstos en 08 | Escenario `E-7` como cobertura adicional declarada de los seis tipos |

## 5. Prioridad y estimación

`Must` porque la identidad posicional es lo que hace posible la sincronización árbol ⇄ escena de la etapa `g`, y porque sin componentes no hay valor derivado que verificar en US-06005.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Hasta dónde llega el conjunto de tipos reconstruibles sigue abierto.** Los **seis** que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar, y **ninguna fuente enumera las clases de la actividad**; un tipo fuera del conjunto produce error de validación, que es correcto **pero puede no ser lo deseado**. Es `PA-04` de [`../Product-Backlog.md`](../../../Product-Backlog.md) §6, elevado con BT-06024.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
