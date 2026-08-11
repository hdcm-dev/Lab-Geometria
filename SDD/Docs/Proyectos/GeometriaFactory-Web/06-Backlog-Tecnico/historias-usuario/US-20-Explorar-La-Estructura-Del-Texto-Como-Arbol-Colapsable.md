# US-20 — Explorar la estructura del texto como árbol colapsable

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-20-Explorar-La-Estructura-Del-Texto-Como-Arbol-Colapsable.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-07 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que abre un trabajo**, quiero **recorrer la estructura del texto como un árbol que puedo plegar y desplegar**, para **entender cómo quedó modelado el trabajo sin leer el texto corrido**.

## 2. Contexto

`F-11` del intake §4 declara `Must Have` la previsualización **y el árbol colapsable**. El caso de uso es [`CU-07`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md). **La presentación del árbol es del anfitrión**: la fachada del visor devuelve la estructura y no dibuja el árbol.

## 3. Criterios de aceptación

- Given un trabajo cargado en la instancia del visor, When se pide la estructura, Then la superficie arma el árbol con el **índice de cada pieza**.
- Given ese árbol, When se lo compara con el texto original, Then **no lo reescribe ni lo normaliza**: el texto es un dato de entrada opaco.
- Given el árbol, When se pliega y se despliega, Then **no hay tráfico de circuito hacia el servidor**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-06 |
| CU cubiertos | CU-07 |
| Restricciones transversales que la alcanzan | RT-04, RT-08, RT-10 |
| Componente de `05` §3.1 | Anfitrión del visor, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La estructura la devuelve la fachada del bundle; **el árbol lo dibuja esta pieza** |
| BT derivadas | BT-16 |
| Tests previstos en 08 | Paso del guion de la etapa `g`, y conteo de tráfico de circuito |

## 5. Prioridad y estimación

`Must` por derivar de `F-11`, `Must Have`, que declara la previsualización **y** el árbol colapsable.

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

**El árbol se porta del visualizador previo**, al que la fuente califica como su mejor recurso didáctico, y la Fase C del visor declaró que **la fachada devuelve la estructura y la presentación es del anfitrión**. El índice que trae esa estructura es el mismo con el que US-21 sincroniza el resaltado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
