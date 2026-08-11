# RN-05 — Un trabajo no pasa a estado `Pendiente` con errores de validación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-05-Finalizacion-Sin-Errores-De-Validacion.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-05, con el corte adelantado del cierre al envío), §4 (F-10 y F-22), §4.2 (modelo de estados del trabajo), §17.1.P.2 (INV-04), §7 (CL-3 y CL-4), §17.2.P.11 punto 2, §21, §20.E-1, §20.E-2, §20.E-5, §20.E-6; [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5; [`NB-05`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) §5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Enunciado de la regla](#1-enunciado-de-la-regla)
- [2. Justificación](#2-justificación)
- [3. Ámbito de aplicación](#3-ámbito-de-aplicación)
- [4. Consecuencia si se viola](#4-consecuencia-si-se-viola)
- [5. CU afectados](#5-cu-afectados)
- [6. Pruebas que la verifican](#6-pruebas-que-la-verifican)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Enunciado de la regla

Un trabajo **no pasa a estado `Pendiente` con errores de interpretación** de su texto: queda en `Borrador` con sus errores localizados. Las advertencias **sí** permiten el paso.

## 2. Justificación

Es el límite declarado entre lo que todavía no verifica y lo que ya es una entrega (PRODUCT-INTAKE §4.1). El corte **se adelantó del cierre al envío** con el modelo de estados vigente: al unificarse guardar y enviar en una sola acción, `Borrador` pasó a significar exactamente «el texto no verificó», de modo que la comprobación tiene que ocurrir en el envío y no al final.

La contracara es igual de deliberada: una discrepancia entre el valor declarado y el derivado **no bloquea**, porque bloquear dejaría fuera de la entrega justamente el caso que más interesa observar y el alumno lo viviría como un rechazo del producto en lugar de como información sobre su código (§7, CL-4, y `NB-05` §1).

El invariante que la expresa como condición permanente es **INV-04**: un trabajo `Finalizado` tiene el texto interpretado sin errores, y puede tener advertencias. Se sostiene por consecuencia: si ningún trabajo llega a estado `Pendiente` con errores y `Finalizado` sólo se alcanza desde `Pendiente`, entonces ningún trabajo `Finalizado` los tiene.

## 3. Ámbito de aplicación

- Se evalúa en el envío, que es la transición de `Borrador` a `Pendiente` y la única acción de guardado del alumno.
- No se evalúa en la creación ni en la reedición del trabajo: las dos proceden con el texto en el estado en que esté.
- No se evalúa en el desenlace: un trabajo en estado `Pendiente` ya cumplió la regla, y por eso el administrador nunca aprueba un trabajo con errores de interpretación.
- No se evalúa sobre las advertencias, en ningún momento.

## 4. Consecuencia si se viola

El envío **no falla**: su resultado declarado es que el trabajo queda en `Borrador`, con sus observaciones de especie error de validación y su ubicación por posición de figura y campo.

**No hay ninguna otra forma de llegar a estado `Pendiente`**, y por eso esta regla no acuña un código de rechazo propio: el envío es la única transición hacia ese estado y la decide el dominio, de modo que no existe una operación de «forzar el paso» que se pueda rechazar. Las solicitudes que sí se rechazan son las que caen fuera del envío, y sus códigos los declara [CU-08](../Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md) §6: `ENVIO_FUERA_DE_BORRADOR` cuando el trabajo no está en `Borrador`, y `TRANSICION_DESDE_ESTADO_TERMINAL` cuando ya alcanzó un estado de cierre.

En sentido inverso, impedir el paso a estado `Pendiente` de un trabajo cuyas observaciones son todas advertencias también viola esta regla: el carácter no bloqueante de la advertencia es parte del enunciado, no una tolerancia.

## 5. CU afectados

- [CU-08](../Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md) — Gobernar el estado del trabajo en el envío.
- [CU-07](../Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md) — Registrar las observaciones del trabajo, que es donde la especie queda fijada.
- [CU-10](../Casos-De-Uso/CU-10-Resolver-El-Desenlace-Del-Trabajo.md) — Resolver el desenlace del trabajo, que hereda la garantía sobre los trabajos en estado `Pendiente`.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08, con los escenarios del intake como entrada: E-1 y E-2 pasan a estado `Pendiente` con advertencias; E-5 queda en `Borrador` con su error de tipo desconocido; E-4 pasa con 0 observaciones; E-6 pasa porque la dimensión en 0.00 no produce error de interpretación. Los criterios de éxito de negocio son de `NB-04` §5 —0 trabajos que pasan a estado `Pendiente` con errores sin resolver— y de `NB-05` §5 —0 trabajos impedidos por tener advertencias—.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el enunciado completo de `PRODUCT-INTAKE` 1.3 §4.1 y el modelo de estados de §4.2. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **El momento de evaluación se adelanta del cierre al envío**: la regla deja de decir «no se finaliza con errores» y pasa a decir «no pasa a estado `Pendiente` con errores», con el trabajo quedando en `Borrador` en lugar de rechazarse la operación. Se declara **INV-04** como el invariante que la expresa, con la derivación por la que se sostiene, corrigiendo la atribución anterior que lo daba como el invariante del texto íntegro. §5 suma CU-10. **El nombre del archivo se conserva** aunque el enunciado cambió de momento: otras categorías ya lo citan por esta ruta y renombrarlo rompería sus enlaces. **Corrección de la ronda r1 del audit, hallazgo P2-02**: §4 citaba `TRANSICION_DE_TRABAJO_NO_ADMITIDA`, un código que ningún caso de uso declara y que esta regla no debió acuñar. Se retira sin reemplazo y §4 declara por qué no corresponde ninguno —el envío es la única transición hacia estado `Pendiente` y la decide el dominio, así que no hay operación de «forzar el paso» que rechazar— y remite a los dos códigos que CU-08 §6 sí declara para las solicitudes fuera del envío. |
