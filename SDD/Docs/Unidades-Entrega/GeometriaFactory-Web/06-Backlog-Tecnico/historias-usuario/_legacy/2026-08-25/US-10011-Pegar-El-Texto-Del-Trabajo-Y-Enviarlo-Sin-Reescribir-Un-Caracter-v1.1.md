> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10011-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10011-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md`](../../US-10011-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10011 — Pegar el texto del trabajo y enviarlo sin que se reescriba un carácter

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10011-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10005 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Envio-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **cargar mi trabajo con su nombre, su fecha, su descripción y el texto que produjo mi programa, pegándolo tal cual**, para **que mi trabajo quede guardado con mi nombre y no se pierda al cerrar la página**.

## 2. Contexto

`NB-00003` pide trabajo con dueño, estado y persistencia; `F-06` del intake §4 lo declara `Must Have` y `RN-10008` fija que el texto original se conserva íntegro. El caso de uso es [`CU-10005`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10005-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md). `RT-08` de `02` §6 lo declara: el texto se envía **carácter por carácter** y no se reescribe en ningún punto del recorrido.

## 3. Criterios de aceptación

- Given un texto pegado con comas finales y claves sinónimas tal como lo emite el programa del alumno, When se lo envía, Then el texto que sale de esta pieza es **idéntico carácter por carácter** al que la persona pegó.
- Given ese mismo texto, When se lo vuelve a mostrar en cualquier superficie, Then **no se reescribe**: ni al enviarlo, ni al mostrarlo, ni al pasarlo a la fachada del visor.
- Given el envío, When se cuenta desde dónde sale la solicitud, Then sale del **servidor de esta pieza**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00004 |
| CU cubiertos | CU-10005 |
| Restricciones transversales que la alcanzan | RT-01, RT-06, RT-08 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | El texto se conserva en `GeometriaFactory-Infrastructure` y **el borde del proceso es el primer lugar donde puede alterarse**, según `GeometriaFactory-Api` |
| BT derivadas | BT-10008, BT-10011, BT-10012 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, y la prueba byte a byte de la batería de integración |

## 5. Prioridad y estimación

`Must` por derivar de `F-06`, `Must Have`, y porque el criterio de transición `f` → `g` exige que el texto original se conserve íntegro y **nunca se reescriba**.

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

**El límite de tamaño del cuerpo lo fija `GeometriaFactory-Api` y su forma de rechazo no es negociable**: rechaza y **nunca trunca**, porque truncar rompería `RN-10008` en silencio, con el trabajo guardado y el texto mutilado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
