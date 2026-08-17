# US-04002 — Rechazar el alta con un correo ya registrado

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **recibir un motivo tipado cuando el correo del alta ya está registrado**, para **poder decirle a la persona que ese correo está ocupado sin revelar nada de la cuenta que lo ocupa**.

## 2. Contexto

`RN-04002` fija que el correo del alumno es único, y `02` §6 declara que **la verificación sobre el conjunto de cuentas es de esta capa, en los dos caminos de alta**. El contrato de uso es [`CU-00021`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md). Sin esta historia, la colisión llegaría al consumidor como una excepción del almacén, que es exactamente lo que [`ADR-04006`](../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) descarta.

## 3. Criterios de aceptación

- Given un correo que el puerto de repositorio de cuentas responde como ya registrado, When se solicita el alta, Then **no se constituye ninguna cuenta** y se devuelve el motivo de correo ocupado, tomado del catálogo cerrado.
- Given ese rechazo, When se inspecciona el motivo emitido, Then **no declara la situación ni el papel** de la cuenta que ocupa el correo.
- Given el mismo rechazo, When se cuenta el efecto sobre el estado, Then **no hay ninguno**: la operación termina de forma controlada y nada queda a medio escribir.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04001 |
| RN e invariantes que ejerce | RN-04002; INV-01 |
| Componente de `05` §3.1 | Orquestación del alta de cuentas |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | Ninguna: el solicitante es anónimo, igual que en US-04001 |
| BT derivadas | BT-04008, BT-04012, BT-04021 |
| Tests previstos en 08 | Prueba unitaria con doble que responde correo ocupado, y prueba de inspección del motivo emitido contra el catálogo |

## 5. Prioridad y estimación

`Must` porque `RN-04002` es una de las dieciséis reglas del producto y porque sin esta negativa el alta duplicaría cuentas, con lo que el ingreso dejaría de ser determinista (`05` §9, último riesgo de `GeometriaFactory-Infrastructure` sobre la misma regla).

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

El **criterio de comparación de dos correos** —tal cual o normalizados— condiciona cuándo se produce esta negativa y **no se decide acá**: `05` §11 `PA-03` lo derivó a la categoría 05 de `GeometriaFactory-Infrastructure`. BT-04021 lo acompaña con su plazo.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
