# US-14 — Ver los errores con índice de figura y campo, con el trabajo en `Borrador`

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-14-Ver-Los-Errores-Con-Indice-De-Figura-Y-Campo.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-06 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Superficie de 03:** `Envio-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **que el sistema me diga en qué figura y en qué campo está el problema**, para **no tener que adivinar dónde falla mi salida**, y que mi trabajo quede en `Borrador` mientras lo corrijo.

## 2. Contexto

`RN-09` exige que los mensajes de error de validación indiquen **índice de figura y campo, nunca un texto genérico**, y `RN-05` fija que un trabajo no pasa a estado `Pendiente` con errores. El caso de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md).

## 3. Criterios de aceptación

- Given un envío con un tipo desconocido, When se muestra el resultado, Then el error aparece con **el índice de la figura y el campo señalado**, y el trabajo queda en `Borrador`.
- Given un envío cuyo texto no verifica, When se lo presenta, Then **no se presenta como un fallo de la petición**: el trabajo se guardó y su estado se decidió; lo que no verifica es el texto.
- Given una observación, When se busca en la lista una pieza no dibujada de la previsualización, Then **no está mezclada**: son cosas distintas y la superficie no las junta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-05, NB-03 |
| CU cubiertos | CU-05 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Representaciones reutilizadas, Traductor de condiciones, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La producción del mensaje ubicado es de `GeometriaFactory-Infrastructure` y su transporte sin recortes, de `GeometriaFactory-Api` |
| BT derivadas | BT-09, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `f`, con los escenarios `E-5` y `E-8` del intake §20 |

## 5. Prioridad y estimación

`Must` por derivar de `F-09` y `F-22`, `Must Have`, y porque el criterio de transición `f` → `g` exige que un tipo desconocido produzca error **con índice de figura y campo** y que el trabajo no pase a estado `Pendiente`.

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

**El escenario `E-8` —dimensión no legible— produce error y no advertencia**, y el intake 1.12 lo fijó con su fundamento: el sistema **no entendió** lo que el alumno escribió, y es el modo de falla **más probable** de los ocho, porque lo produce la configuración regional de la máquina.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
