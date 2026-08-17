# US-06001 — Leer el texto real del alumno con tolerancia a comas finales y a las claves sinónimas

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06001-Leer-El-Texto-Real-Con-Tolerancia-A-Comas-Finales-Y-Claves-Sinonimas.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **interpretar el texto tal como lo emite el programa del alumno, con sus comas finales, sus claves sinónimas y sus dos formas de nombrar las caras**, para **que el alumno no tenga que cambiar una coma de lo que su programa ya produce**.

## 2. Contexto

`NB-00004` pide interpretación fiel del dato del alumno; el intake §3 declara como diferenciador que el texto se acepte **tal como lo emite su programa**, y que hoy **ningún ortoedro generado por la aplicación se dibuja** porque el visor previo exige una clave que el programa no emite. El contrato de uso es [`CU-06001`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md), y las **cuatro trampas** `T1` a `T4` están declaradas en `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Infrastructure punto 1.

## 3. Criterios de aceptación

- Given el texto del escenario `E-2` con la clave sinónima del ortoedro y con comas finales, When se lo interpreta, Then se lee sin error: las dos claves son sinónimas y la coma final no rompe la lectura.
- Given un texto con caras nombradas en cualquiera de sus **dos** formas admitidas, When se lo interpreta, Then las dos se aceptan.
- Given un texto con valores calculados erróneos, When se lo interpreta, Then **no se rechaza ni se corrige: se señala**, que es la cuarta trampa y el mayor valor didáctico del producto.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004 |
| CU cubiertos | CU-06001 |
| RN que ejerce | RN-06008, RN-06009 —las dos con **tramo principal acá**—, RN-06005 en su parte de insumo |
| Componente de `05` §3.1 | Motor de interpretación de figuras |
| Reglas conceptuales de modelo | `RC-06001` en cuanto no devuelve el texto corregido |
| ¿Toma alguna decisión de negocio? | **No.** Produce el insumo; el estado lo resuelve el dominio |
| ¿Toca el almacén? | **No.** Su prueba es unitaria y sin base |
| BT derivadas | BT-06016, BT-06018, BT-06020 |
| Tests previstos en 08 | Casos 1, 2, 3 y 4 de la batería, con los escenarios `E-2`, `E-3` y `E-4` del intake §20 |

## 5. Prioridad y estimación

`Must` por derivar de `F-09`, `Must Have`, y porque el criterio de transición `f` → `g` exige que **el texto tal como lo emite el programa del alumno se interprete, con sus particularidades de formato incluidas**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Las cuatro trampas se escriben antes de leer texto y no después de que algo falle.** Es la mitigación del riesgo de negocio que la fuente declara con probabilidad **alta**: que el validador se escriba sin leer el análisis y no sirva para el dato que existe.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
