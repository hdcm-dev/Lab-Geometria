# RC-06004 — La familia plana o volumétrica no se persiste

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RC-06004-La-Familia-No-Se-Persiste.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §17.1.P.11 · GeometriaFactory-Domain punto 4 («la familia plana/volumétrica **no se persiste**: se deriva de `Tipo` por tabla de consulta»), §20.E-7; `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Glosario-Funcional.md` (entrada «familia plana o volumétrica»)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

La **familia** de una pieza —plana o volumétrica— **no se guarda**. Se deriva de su `Tipo` por tabla de consulta, cada vez que hace falta.

## 2. Justificación

Es una decisión pre-tomada del producto y su motivo es de integridad, no de espacio: la familia **no es un dato independiente**, es una función del tipo. Guardarla crearía un segundo lugar donde la misma verdad puede estar escrita, y por lo tanto un lugar donde puede contradecir al primero.

El dominio ya declara como rechazo la familia que contradice al tipo. Si la familia no se guarda, esa contradicción **no puede nacer del almacén**: sólo puede llegar de una reconstrucción mal formada, que es donde el rechazo tiene sentido.

## 3. Ámbito de aplicación

- Alcanza a las **piezas** del conjunto raíz.
- Alcanza también a los **componentes**, que son siempre figuras planas por definición: tapa, cara, base, lateral o lado.
- La tabla de consulta que traduce tipo a familia **vive en el código, no en el almacén**: no hay una tabla de tipos guardada que haya que mantener al día.
- El escenario **E-7** es el que ejercita el mapeo completo: tres tipos volumétricos —`Cilindro`, `Cubo`, `Ortoedro`— y tres planos —`Rectangulo`, `Cuadrado`, `Circulo`—, estos últimos **como piezas del conjunto raíz** y no como componentes.

## 4. Consecuencia si se viola

Persistir la familia no produce rechazo: produce la posibilidad de que una fila diga «plana» sobre una pieza cuyo tipo es volumétrico, y a partir de ahí cada consulta devuelve una respuesta distinta según de qué campo la lea.

## 5. CU afectados

- [`CU-06001`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) — Interpretar y reconstruir: es donde el tipo se lee.
- [`CU-06003`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar: es donde la familia **no** se escribe.

## 6. Pruebas que la verifican

Por inspección del esquema: **ninguna columna del almacén guarda la familia**. Y por comportamiento, `CU-06001` CA-06 sobre el escenario **E-7**, que reconstruye las seis piezas de los seis tipos, tres de cada familia, sin que la familia haya viajado en el dato.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
