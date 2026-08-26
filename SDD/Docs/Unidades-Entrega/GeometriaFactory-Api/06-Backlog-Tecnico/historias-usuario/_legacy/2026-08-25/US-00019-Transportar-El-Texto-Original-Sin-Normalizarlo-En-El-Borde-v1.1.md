> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md`](../../US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-00019 — Transportar el texto original **sin normalizarlo en el borde**

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00004 Gestión del trabajo
**Etapa del producto:** `e`
**Punto de acceso:** `A-10` y `A-11`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que el texto del alumno cruce la frontera del proceso sin recodificarse, sin normalizarse y sin truncarse**, para **que lo que se guarda sea exactamente lo que su programa produjo**.

## 2. Contexto

`RN-00008` conserva el texto íntegro, y `02` §6 declara el tramo propio de esta capa: **el borde del proceso es el primer lugar donde el texto puede alterarse** —por codificación, por normalización o por recorte de tamaño—. `05` §9 declara el riesgo con impacto alto: truncar **rompe `RN-00008` en silencio**, el trabajo se guarda con el texto mutilado y el alumno lo descubre al ver el dibujo. El contrato de uso es [`CU-00026`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md).

## 3. Criterios de aceptación

- Given el texto del escenario `E-1`, When se lo envía y se lo guarda, Then la comparación **byte a byte** entre lo enviado y lo guardado da **0** caracteres de diferencia.
- Given un cuerpo por encima del límite configurado, When se lo envía, Then **se rechaza y no se trunca**: **0** truncamientos silenciosos.
- Given el texto, When se inspecciona el tramo de transporte, Then **no se recodifica, no se recortan espacios, no se normalizan saltos de línea y no se reescriben separadores**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-00006 |
| RN que ejerce | **RN-00008**, en el punto donde esta capa puede romperla |
| Componente de `05` §3.1 | Superficie de trabajos |
| ¿Decide qué se dice? | **No.** El texto viaja como cadena |
| Familia empobrecida | **No** |
| BT derivadas | BT-00008, BT-00009, BT-00018, BT-00024 |
| Tests previstos en 08 | Prueba byte a byte con `E-1`, y prueba de cuerpo excedido que comprueba **rechazo y no truncamiento** |

## 5. Prioridad y estimación

`Must` por `RN-00008`, y porque el criterio de transición `f` → `g` exige que el texto original **se conserve íntegro y nunca se reescriba**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El valor del límite sigue abierto y la forma de rechazo no.** `GeometriaFactory-Infrastructure` decidió que el motor **no impone límite propio** y **exigió por escrito que el borde rechace y no trunque**; esta categoría fija la forma y deja el número en la etapa `a`, calibrado sobre el texto más grande que la fuente documenta. Es `PA-06` de [`../Product-Backlog.md`](../../../Product-Backlog.md) §6, con BT-00009.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
