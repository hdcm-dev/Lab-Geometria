# US-06006 — Comparar con tolerancia absoluta y **operador estricto**

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06006-Comparar-Con-Tolerancia-Absoluta-Y-Operador-Estricto.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la comparación de valores use tolerancia absoluta de 0.01 con operador estricto**, para **que el escenario semilla del producto dé exactamente las advertencias que su documentación declara**.

## 2. Contexto

El intake §17.1.P.10 · GeometriaFactory-Infrastructure fija la tolerancia y declara explícitamente que **no es una asunción**: sale de que el emisor redondea a dos decimales. Y fija el operador: **se advierte cuando la diferencia absoluta es mayor que 0.01, nunca mayor o igual**, con un fundamento numérico verificable. El contrato de uso es [`CU-06002`](../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md).

## 3. Criterios de aceptación

- Given una diferencia absoluta **mayor** que 0.01, When se compara, Then se emite advertencia.
- Given una diferencia absoluta de **exactamente** 0.01, When se compara, Then **no se emite advertencia**: el operador es estricto.
- Given el escenario semilla `E-1`, When se lo verifica entero, Then produce **exactamente 2** advertencias y no 3, que es el caso testigo del producto.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00005 |
| CU cubiertos | CU-06002 |
| RN que ejerce | RN-06005 en su parte de insumo |
| Componente de `05` §3.1 | Motor de verificación de valores |
| Reglas conceptuales de modelo | `RC-06003` |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06017, BT-06018 |
| Tests previstos en 08 | Caso 9 de la batería, con el escenario `E-1`, que debe dar exactamente 2 advertencias |

## 5. Prioridad y estimación

`Must` porque el criterio de transición `f` → `g` exige que **la comparación de valores use tolerancia absoluta y no igualdad exacta**, y porque con «mayor o igual» **el caso de prueba canónico del producto fallaría**.

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

**El número no se negocia y no se transcribe con margen.** `05` §7 lo declara sin asunción y con su fundamento: en `E-1` el área del cilindro declara un valor y la suma de sus componentes da otro con una diferencia de **exactamente 0.01**; con el operador estricto ese caso **no** produce advertencia.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
