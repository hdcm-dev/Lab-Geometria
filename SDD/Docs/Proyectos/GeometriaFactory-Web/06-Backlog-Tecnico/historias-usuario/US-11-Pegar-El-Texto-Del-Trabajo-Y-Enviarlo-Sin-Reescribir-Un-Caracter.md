# US-11 — Pegar el texto del trabajo y enviarlo sin que se reescriba un carácter

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-11-Pegar-El-Texto-Del-Trabajo-Y-Enviarlo-Sin-Reescribir-Un-Caracter.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-05 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Envio-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **cargar mi trabajo con su nombre, su fecha, su descripción y el texto que produjo mi programa, pegándolo tal cual**, para **que mi trabajo quede guardado con mi nombre y no se pierda al cerrar la página**.

## 2. Contexto

`NB-03` pide trabajo con dueño, estado y persistencia; `F-06` del intake §4 lo declara `Must Have` y `RN-08` fija que el texto original se conserva íntegro. El caso de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md). `RT-08` de `02` §6 lo declara: el texto se envía **carácter por carácter** y no se reescribe en ningún punto del recorrido.

## 3. Criterios de aceptación

- Given un texto pegado con comas finales y claves sinónimas tal como lo emite el programa del alumno, When se lo envía, Then el texto que sale de esta pieza es **idéntico carácter por carácter** al que la persona pegó.
- Given ese mismo texto, When se lo vuelve a mostrar en cualquier superficie, Then **no se reescribe**: ni al enviarlo, ni al mostrarlo, ni al pasarlo a la fachada del visor.
- Given el envío, When se cuenta desde dónde sale la solicitud, Then sale del **servidor de esta pieza**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-04 |
| CU cubiertos | CU-05 |
| Restricciones transversales que la alcanzan | RT-01, RT-06, RT-08 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | El texto se conserva en `GeometriaFactory-Infrastructure` y **el borde del proceso es el primer lugar donde puede alterarse**, según `GeometriaFactory-Api` |
| BT derivadas | BT-08, BT-11, BT-12 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, y la prueba byte a byte de la batería de integración |

## 5. Prioridad y estimación

`Must` por derivar de `F-06`, `Must Have`, y porque el criterio de transición `f` → `g` exige que el texto original se conserve íntegro y **nunca se reescriba**.

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

**El límite de tamaño del cuerpo lo fija `GeometriaFactory-Api` y su forma de rechazo no es negociable**: rechaza y **nunca trunca**, porque truncar rompería `RN-08` en silencio, con el trabajo guardado y el texto mutilado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
