# Inventario del renumerado `R-4` — las referencias que quedaron sin bloque

**Producto:** Fábrica de Geometría
**Documento:** Inventario-Renumerado-R-4-2026-08-29.md
**Versión:** 1.0
**Fecha:** 2026-08-29
**Autor:** Orquestador SDD
**Nivel:** Producto
**Instrumento:** tramo `R-4`, decidido por el Product Owner el 2026-08-29 al retirar el [`ADR-14005`](../Producto/Adrs/ADR-14005-Familias-Acunadas-Por-El-Destino-Con-Ambito-De-Proyecto.md)

---

## 1. Qué es esta lista, y por qué existir es su propósito

**Son las 278 referencias a `QG` y `CV` que el renumerado NO pudo mover, y dejarlas así fue la
decisión y no el resto.**

El tramo `R-4` renumeró **507 ocurrencias** al mapa de bloques del destino —`00` Api, `02` Domain,
`04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor—, deduciendo el bloque de
la línea o de la sección que contiene cada cita. **Estas 278 no lo declaran en ninguna de las dos**, y
deducirlo de la prosa habría sido inventarlo.

**Conservan la forma `QG-NN` / `CV-NN`, que después del renumerado ya no existe.** Ése es el punto:

> Antes eran **278 ambigüedades invisibles** — `QG-03` se leía bien y significaba tres cosas distintas
> según el proyecto de código.
> Ahora son **278 referencias rotas**, y una referencia que no resuelve **la levanta la compuerta
> mecánica** de `Master-Prompt.md` §10.0.

**Convertir lo invisible en detectable es el resultado buscado**, y no un efecto colateral que haya que
disculpar.

## 2. El ítem diferido, con sus cuatro campos

> **1 · Qué falta:** asignarle su proyecto de código a cada una de las 278 referencias, y renumerarla.
> **2 · Por qué no se puede hoy:** su bloque **no está en el texto**. Para cada una hay que decidir a
> qué proyecto de código se refería quien la escribió, y eso es interpretación y no evidencia. Es el
> mismo motivo con el que la mesa se negó a reconstruir el mapeo de las 40 historias pronosticadas.
> **3 · Quién lo cierra:** las categorías **08** y **09** de las dos unidades de entrega, que son las
> que acuñaron las dos familias.
> **4 · En qué evento se cierra:** la **Fase J**, en su revisión de huecos y contradicciones entre
> documentos. Cada corrida de la compuerta mecánica las va a listar hasta entonces, que es lo que
> impide que se olviden.

## 3. El reparto

| Documento | Referencias | Identificadores |
|---|---|---|
| [`Pipeline-CI-CD.md`](../Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md) | 58 | `QG-05`×8, `QG-01`×7, `QG-06`×6, `QG-04`×6, `QG-07`×5, `QG-02`×4, y 11 más |
| [`Definition-Of-Done.md`](../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Definition-Of-Done.md) | 47 | `CV-11`×3, `CV-10`×3, `CV-31`×2, `CV-16`×2, `CV-27`×2, `CV-35`×2, y 22 más |
| [`Casos-Prueba-Referenciales.md`](../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) | 31 | `QG-08`×4, `QG-04`×4, `QG-09`×3, `QG-06`×3, `QG-10`×3, `QG-11`×3, y 6 más |
| [`Pipeline-CI-CD.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md) | 31 | `QG-01`×6, `QG-04`×5, `QG-02`×4, `QG-03`×4, `QG-05`×2, `QG-06`×2, y 5 más |
| [`Definition-Of-Done.md`](../Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Definition-Of-Done.md) | 23 | `CV-23`×2, `CV-13`×2, `CV-18`×2, `CV-31`×2, `CV-27`×2, `CV-29`×2, y 11 más |
| [`Casos-Prueba-Referenciales.md`](../Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) | 12 | `QG-04`×3, `QG-05`×2, `QG-06`×2, `QG-08`×2, `QG-07`, `QG-09`, y 1 más |
| [`Supply-Chain-Seguridad.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Supply-Chain-Seguridad.md) | 10 | `QG-01`×2, `QG-06`, `QG-08`, `QG-09`, `QG-05`, `QG-07`, y 3 más |
| [`Estrategia-Calidad.md`](../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Calidad.md) | 8 | `QG-03`×2, `QG-07`×2, `QG-10`, `QG-05`, `QG-06`, `QG-14` |
| [`Supply-Chain-Seguridad.md`](../Unidades-Entrega/GeometriaFactory-Api/09-Devops/Supply-Chain-Seguridad.md) | 8 | `QG-05`, `QG-06`, `QG-08`, `QG-10`, `QG-01`, `QG-02`, y 2 más |
| [`Plan-Etapa-A.md`](../Producto/Plan-Etapa-A.md) | 7 | `QG-02`×3, `QG-10`×2, `QG-11`, `QG-01` |
| [`ADR-14005-Familias-Acunadas-Por-El-Destino-Con-Ambito-De-Proyecto.md`](../Producto/Adrs/ADR-14005-Familias-Acunadas-Por-El-Destino-Con-Ambito-De-Proyecto.md) | 6 | `QG-03`×2, `QG-05`, `QG-14`, `QG-01`, `CV-02` |
| [`Guia-Publicacion-Bundle-Visor.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Bundle-Visor.md) | 6 | `QG-09`×2, `QG-04`, `QG-05`, `QG-06`, `CV-30` |
| [`Guia-Publicacion-Front-Ftp.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) | 5 | `QG-02`×2, `QG-03`×2, `QG-04` |
| [`README.md`](../Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/README.md) | 4 | `CV-01`, `CV-35`, `QG-11`, `QG-04` |
| [`Matriz-Cobertura-Pruebas.md`](../Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md) | 3 | `CV-20`×2, `CV-23` |
| [`Entornos-Deploy.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/Entornos-Deploy.md) | 3 | `QG-01`, `QG-02`, `QG-09` |
| [`Norma-De-Nomenclatura.md`](../Producto/Norma-De-Nomenclatura.md) | 2 | `QG-11`, `QG-10` |
| [`Matriz-Cobertura-Pruebas.md`](../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Cobertura-Pruebas.md) | 2 | `QG-08`, `QG-11` |
| [`README.md`](../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/README.md) | 2 | `CV-01`, `CV-40` |
| [`Guia-Publicacion-Image-Docker.md`](../Unidades-Entrega/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md) | 2 | `QG-15`×2 |
| [`Estrategia-Calidad.md`](../Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Estrategia-Calidad.md) | 2 | `QG-04`×2 |
| [`Handoff-Checkout.md`](../Handoff-Checkout.md) | 1 | `QG-14` |
| [`Pipeline-Producto.md`](../Producto/Pipeline-Producto.md) | 1 | `QG-01` |
| [`Entornos-Deploy.md`](../Unidades-Entrega/GeometriaFactory-Api/09-Devops/Entornos-Deploy.md) | 1 | `QG-08` |
| [`README.md`](../Unidades-Entrega/GeometriaFactory-Api/09-Devops/README.md) | 1 | `QG-10` |
| [`ejemplo-03-avanzado-dominio.md`](../Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-dominio.md) | 1 | `QG-04` |
| [`README.md`](../Unidades-Entrega/GeometriaFactory-Web/09-Devops/README.md) | 1 | `QG-11` |

**Total: 278 referencias en 27 documentos.** 197 de `QG` y 81 de `CV`.

**Los cuatro documentos de arriba concentran más de la mitad**, y no es casualidad: son los que
enumeran las puertas y los criterios **sin decir de qué proyecto de código**, porque cuando se
escribieron el ámbito no estaba declarado y no hacía falta decirlo.

## 4. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-29 | Emisión inicial, junto con el tramo `R-4`. Inventaría las **278** referencias a `QG` y `CV` cuyo bloque **no se pudo deducir de la línea ni de la sección**, y que por eso conservan la forma que el renumerado retiró. **Existir es su propósito**: las convierte de ambigüedad invisible en referencia rota y detectable. | Orquestador SDD |
