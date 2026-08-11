# US-05 — Dar de baja una cuenta exigiendo el correo escrito como confirmación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-05-Dar-De-Baja-Exigiendo-El-Correo-Escrito-Como-Confirmacion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la baja de una cuenta no proceda sin que el correo escrito coincida con el de la cuenta**, para **que la única operación irreversible del producto no ocurra por un clic distraído**.

## 2. Contexto

`RN-07` exige confirmación escrita y arrastre. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Gobernar-Las-Cuentas-De-La-Comision.md), y `02` §6 declara que **la comparación del correo escrito es de esta capa**. El criterio de transición `d` → `e` del roadmap §5.2 lo exige por escrito.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y un correo escrito que coincide con el de la cuenta, When se pide la baja, Then la baja procede.
- Given un correo escrito que **no** coincide, When se pide la baja, Then se rechaza con su motivo y **no se retira nada**: ni la cuenta ni ninguno de sus trabajos.
- Given un solicitante sin el papel `Administrador`, When pide la baja con el correo correcto, Then se devuelve el motivo de facultad requerida **antes** de tocar el almacén.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-02 |
| RN e invariantes que ejerce | RN-01, RN-07 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas, Guarda de autorización |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | **Facultad**, y **cambio de contraseña pendiente** antes que ella |
| BT derivadas | BT-10, BT-13 |
| Tests previstos en 08 | Prueba de baja con correo que no coincide, comprobando que no hay efecto |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have`, y por ser criterio de la transición `d` → `e`.

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

**El arrastre es US-06 y ocurre en la misma unidad de trabajo que esta baja.** Separarlos en dos historias no los separa en dos transacciones: `05` §4 declara la baja como el caso testigo de «un caso de uso, una unidad de trabajo».

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
