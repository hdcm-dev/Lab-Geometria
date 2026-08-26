> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10003-Iniciar-Sesion-Sin-Que-La-Credencial-Llegue-Al-Navegador.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10003-Iniciar-Sesion-Sin-Que-La-Credencial-Llegue-Al-Navegador.md`](../../US-10003-Iniciar-Sesion-Sin-Que-La-Credencial-Llegue-Al-Navegador.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10003 — Iniciar sesión sin que la credencial llegue al navegador

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10003-Iniciar-Sesion-Sin-Que-La-Credencial-Llegue-Al-Navegador.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Ingreso`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta**, quiero **entrar al laboratorio escribiendo mi correo y mi contraseña**, para **operar con mi identidad**, y como **producto**, que la credencial de sesión **nunca** llegue al navegador.

## 2. Contexto

`NB-00002` pide identidad propia. El caso de uso es [`CU-10002`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) y la superficie es `Ingreso`. `02` §2 declara esta decisión como **la más consecuente del producto en términos de lo que la persona puede observar**, y `05` §2.1 la registra como alternativa evaluada y descartada del lado contrario.

## 3. Criterios de aceptación

- Given una cuenta habilitada, When la persona escribe su correo y su contraseña y confirma, Then obtiene sesión y llega al shell de trabajo que su papel determina.
- Given esa sesión abierta, When se inspecciona el navegador con las herramientas de desarrollo —almacenamiento, marcas de sesión y contenido servido—, Then las apariciones de la credencial de sesión son exactamente **0**.
- Given el canje de credenciales, When se cuenta desde dónde sale, Then sale del **servidor de esta pieza**: ningún formulario lo envía directo, ni siquiera llevando credenciales en claro.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-10002 |
| Restricciones transversales que la alcanzan | RT-01, RT-02, RT-09 |
| Componente de `05` §3.1 | Sesión y estado del circuito, Cliente tipado, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La verificación de la credencial es de `GeometriaFactory-Infrastructure` y la admisibilidad, del dominio |
| BT derivadas | BT-10011, BT-10014 |
| Tests previstos en 08 | Inspección del navegador en la etapa `c`, que es criterio de aceptación verificable declarado por la fuente |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que **la credencial de sesión no sea observable desde el navegador**.

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

**El navegador conserva sólo una marca de sesión que no transporta la credencial y no es legible por guion** (`05` §7). Guardar la credencial en el navegador sobreviviría al reciclado del proceso del hosting, y por eso `05` §2.1 la evaluó y la descartó explícitamente.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
