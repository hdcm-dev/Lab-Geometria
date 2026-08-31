# Inventario del tramo `R-3` — lo que NO se renombra, y por qué

**Producto:** Fábrica de Geometría
**Documento:** Inventario-Renombre-F03-2026-08-31.md
**Versión:** 1.0
**Fecha:** 2026-08-31
**Autor:** Orquestador SDD
**Nivel:** Producto
**Instrumento:** `Norma-De-Nomenclatura.md` §4.1 —las cinco formas— y `V-4` de §7, que exige que **esta lista se escriba antes de editar**

---

## 1. Por qué existe, y por qué su cifra principal es la que no se tocó

`Norma-De-Nomenclatura.md` §4.1 lo dice en una línea: **«un tramo que renombra *todas las ocurrencias* corrige unas y falsifica otras, y la diferencia no se ve en un recuento»**.

Este documento es ese recuento hecho **antes** de editar, y su resultado corrigió por completo el tamaño que el hallazgo `I-03` le atribuía al tramo.

## 2. La medición

**723 ocurrencias candidatas**, en **133 documentos del corpus vivo** —excluidos `_legacy/` y `Audit/`—, de los **101 códigos** que §6.8 del glosario declara.

| Forma de §4.1 | Ocurrencias | Qué se hace |
| --- | --- | --- |
| **Registro histórico** | **580** | **No se toca.** Filas de control de cambios que nombran un código con la forma de entonces |
| **El glosario hablando de sí mismo** | **138** | **No se toca.** §6.8 es la **fuente** del tramo y no su objeto, y la norma ya lo declaraba |
| **Cita textual** | **3** | **No se toca.** Renombrar dentro de una cita la vuelve falsa |
| **Uso propio** | **1** | **Se renombra.** Es la población real del tramo |

**722 de 723 no se tocan. La población real era UNA.**

## 3. La única ocurrencia de uso propio

`samples/domain/02-intermedio` escribía **a mano** la cadena `TIPO_DE_PIEZA_DESCONOCIDO` en su salida, y su §6 la transcribía. El glosario le fija el nombre `UNKNOWN_PIECE_TYPE`.

**La constante no existe todavía**, y eso no cambia nada: es uno de los ocho de la etapa `f` que el dominio deliberadamente no escribió —«escribir los nueve restantes sería declarar condiciones que ninguna operación de estas etapas puede producir»—. Lo que el glosario fija es **el nombre**, no su existencia.

Corregida el 2026-08-31, en el sample, en su transcripción y en el §6 que la gobierna.

## 4. Una que parece uso propio y no lo es

`ejemplo-01-basico-dominio.md` dice: *«`ADMINISTRATOR_ALREADY_CONFIGURED` y este documento pedía `ADMINISTRADOR_YA_CONFIGURADO`»*. Es **registro histórico en prosa**: describe lo que el documento decía antes de una corrección ya aplicada. Renombrarla haría ilegible la explicación.

**El clasificador automático la contó como uso propio**, porque reconoce el registro histórico por su ubicación —sección de control de cambios o fila con versión y fecha— y ésta es prosa corriente. **Se clasificó a mano**, y queda escrita acá para que la próxima corrida no la vuelva a levantar.

## 5. Dónde está el registro histórico, que es el grueso

| Ocurrencias | Documento |
| --- | --- |
|  17 | `Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md` |
|  12 | `Producto/Contratos-Inter-Unidad/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md` |
|  11 | `Unidades-Entrega/GeometriaFactory-Web/02-Especificacion-Funcional/Casos-De-Uso/CU-10004-Administrar-Las-Cuentas-De-La-Comision.md` |
|   8 | `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` |
|   7 | `Producto/Contratos-Inter-Unidad/CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` |
|   7 | `Producto/Contratos-Inter-Unidad/CU-08002-Contrato-De-Administracion-De-Cuentas.md` |
|   7 | `Producto/Contratos-Inter-Unidad/CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md` |
|   7 | `Producto/Norma-De-Nomenclatura.md` |

**Es el 80 % del tramo**, y es exactamente lo que §4.1 protege: cada una de esas filas declara un cambio con el nombre que el identificador tenía **entonces**, y renombrarlas borraría la trazabilidad que el acta existe para dar.

## 6. Qué queda del tramo `R-3`

**Nada.** Con la ocurrencia de §3 corregida, el corpus vivo no tiene ningún código de condición en forma castellana que sea uso propio.

**Y el hallazgo `I-03` queda cerrado con su tamaño real declarado**, que no era el que se le atribuyó: se estimó en «~90 códigos, 731 ocurrencias, 132 documentos, cuatro o cinco tandas» **contando sin clasificar**, que es el error que §4.1 nombra.

---

## Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-31 | Emisión. Clasificación de las 723 ocurrencias candidatas del tramo `R-3` contra las cinco formas de §4.1, hecha **antes de editar** como `V-4` exige. **722 no se tocan y una se renombró.** Cierra `I-03` con su tamaño real, y deja escrita la ocurrencia que el clasificador automático confunde con uso propio. |
