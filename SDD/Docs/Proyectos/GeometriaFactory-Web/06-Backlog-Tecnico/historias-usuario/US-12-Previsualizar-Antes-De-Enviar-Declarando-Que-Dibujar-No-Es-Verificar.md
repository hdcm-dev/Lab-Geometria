# US-12 — Previsualizar el trabajo antes de enviarlo, declarando que dibujar no es verificar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-12-Previsualizar-Antes-De-Enviar-Declarando-Que-Dibujar-No-Es-Verificar.md
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

Como **alumno**, quiero **ver mi trabajo en tres dimensiones antes de enviarlo**, para **darme cuenta de si modelé lo que quería modelar**, sabiendo que **dibujar no es verificar**.

## 2. Contexto

`F-11` del intake §4 declara `Must Have` la previsualización. El caso de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md), y `02` §7 declara que esta superficie consume la fachada del visor con `inicializar`, `cargarJson` y `destruir`. `05` §10.3 declara qué hace esta pieza por `RN-05`: presentar el estado resultante del envío y declarar que **la previsualización dibuja y no verifica**.

## 3. Criterios de aceptación

- Given un texto pegado, When la persona pide la previsualización, Then la escena se dibuja **sin que el trabajo se envíe** y sin que su estado cambie.
- Given una previsualización con piezas que la fachada **no dibujó**, When se las presenta, Then aparecen **enumeradas por su índice** y **no se mezclan con las observaciones del trabajo**: quien decide si el trabajo verifica es el servicio de datos.
- Given el descarte del componente que aloja la instancia, When se navega a otra superficie, Then la instancia **se libera**: no es opcional.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-04, NB-06 |
| CU cubiertos | CU-05 |
| Restricciones transversales que la alcanzan | RT-04, RT-05, RT-10, RT-11 |
| Componente de `05` §3.1 | Anfitrión del visor, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | El dibujo es del bundle de `GeometriaFactory-Visor`, por sus seis funciones; la decisión del estado es del dominio |
| BT derivadas | BT-16, BT-18 |
| Tests previstos en 08 | Paso del guion de la etapa `f`, y el conteo de tráfico de circuito durante la interacción |

## 5. Prioridad y estimación

`Must` por derivar de `F-11`, `Must Have`, y porque sin la previsualización la persona no puede darse cuenta de lo que modeló antes de entregarlo, que es la segunda historia de usuario del intake §5.

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

**Sin capacidad gráfica tridimensional la escena no es soportada y el resto del producto sigue disponible** (`RT-11`). El requisito se declara **por capacidad y no por versión de navegador**, y la fachada informa la ausencia en lugar de fallar en silencio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
