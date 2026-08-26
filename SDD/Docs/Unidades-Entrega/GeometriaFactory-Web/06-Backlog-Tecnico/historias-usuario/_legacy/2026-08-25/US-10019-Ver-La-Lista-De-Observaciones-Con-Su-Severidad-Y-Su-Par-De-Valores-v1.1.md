> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10019-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10019-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md`](../../US-10019-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10019 — Ver la lista de observaciones con su severidad y su par de valores

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10019-Ver-La-Lista-De-Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10007 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que abre un trabajo**, quiero **ver sus observaciones con su severidad, su ubicación y el par de valores cuando corresponde**, para **entender qué encontró el sistema sobre ese trabajo sin volver a enviarlo**.

## 2. Contexto

`NB-00005` pide visibilidad del error de cálculo y `RN-10009` exige la ubicación. El caso de uso es [`CU-10007`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md). La **lista de observaciones con el par declarado y derivado** es una de las **tres** representaciones reutilizadas que la categoría 03 declara.

## 3. Criterios de aceptación

- Given un trabajo con advertencias y errores, When se lo abre, Then cada observación aparece con su **severidad**, su **índice de figura y campo** y, en las advertencias, el **par declarado y derivado**.
- Given esa lista, When se busca en ella una pieza que la fachada no dibujó, Then **no está**: las piezas no dibujadas se enumeran aparte y **no son observaciones del trabajo**.
- Given la severidad, When se la comunica, Then usa **al menos dos canales** y nunca sólo color.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00005, NB-00006, NB-00004 |
| CU cubiertos | CU-10007 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Representaciones reutilizadas, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La producción de la observación es de `GeometriaFactory-Infrastructure`; su transporte sin recortes, de `GeometriaFactory-Api` |
| BT derivadas | BT-10009, BT-10013 |
| Tests previstos en 08 | Filas de la matriz de sensado de deriva sobre la lista de observaciones |

## 5. Prioridad y estimación

`Must` por derivar de `F-11` y `F-10`, `Must Have`, y porque es donde `NB-00005` se vuelve visible sobre el trabajo ya entregado.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los diecisiete códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**Que las piezas no dibujadas y las observaciones no se mezclen está declarado en los dos casos de uso que consumen la fachada** (`02` §7, consecuencia 2), precisamente porque son dos cosas que se parecen y tienen orígenes distintos.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
