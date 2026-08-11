# US-13 — Ver las advertencias con el valor declarado y el derivado, sin bloqueo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-13-Ver-Las-Advertencias-Con-El-Valor-Declarado-Y-El-Derivado.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-06 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Superficie de 03:** `Envio-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **ver la advertencia de que mi cubo declara un área y la geometría dice otra, con los dos valores a la vista**, para **descubrir el error de fórmula sobre mi propio trabajo**, y que la advertencia **no me impida entregar**.

## 2. Contexto

`NB-05` pide visibilidad del error de cálculo, y el intake §3 lo declara **el mayor valor didáctico del servicio**: el alumno ve sobre su propio trabajo que su cubo declara 36.00 donde la geometría dice 54.00. `RN-05` fija que las advertencias **sí** permiten el paso a estado `Pendiente`. El caso de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md).

## 3. Criterios de aceptación

- Given un envío que produce advertencias, When se muestra el resultado, Then cada advertencia aparece con **el valor declarado y el derivado**, y el trabajo queda en estado `Pendiente`.
- Given esa presentación, When se la mira, Then **la advertencia no se presenta como un bloqueo** ni como un fallo del envío.
- Given una advertencia, When se compara con una observación de error, Then la severidad **se comunica por al menos dos canales y nunca sólo por color**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-05, NB-04, NB-03 |
| CU cubiertos | CU-05 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Representaciones reutilizadas —lista de observaciones—, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | El recálculo y la tolerancia son de `GeometriaFactory-Infrastructure`; la decisión del estado, del dominio |
| BT derivadas | BT-09, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `f`, con los escenarios `E-3` y `E-4` del intake §20 |

## 5. Prioridad y estimación

`Must` por derivar de `F-10`, `Must Have`, y porque el criterio de transición `f` → `g` exige que un cubo del primer ejemplo produzca advertencia de área **con los dos valores expresados** y que el trabajo pase a estado `Pendiente` igual.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los quince códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**No se inventan datos de prueba**: el material son los escenarios del intake §20, y el mismo cubo del segundo ejemplo **no** produce ninguna advertencia, que es la otra mitad del criterio de transición.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
