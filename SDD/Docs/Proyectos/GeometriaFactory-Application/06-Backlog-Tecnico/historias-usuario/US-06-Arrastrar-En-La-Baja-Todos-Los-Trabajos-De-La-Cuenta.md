# US-06 — Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-06-Arrastrar-En-La-Baja-Todos-Los-Trabajos-De-La-Cuenta.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la baja de una cuenta retire todos sus trabajos, en cualquier estado, dentro de la misma unidad de trabajo**, para **que no queden trabajos huérfanos de un alumno que ya no existe**.

## 2. Contexto

`RN-07` declara que la baja física elimina la cuenta y **todos sus trabajos**. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Gobernar-Las-Cuentas-De-La-Comision.md). [`ADR-05`](../../05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) declara esta baja como **el caso testigo** del alcance transaccional de la capa.

## 3. Criterios de aceptación

- Given una cuenta con trabajos en varios estados y la confirmación escrita correcta, When se ejecuta la baja, Then la cuenta y **todos** sus trabajos quedan retirados, **en la misma unidad de trabajo**.
- Given esa misma baja interrumpida a mitad de camino, When se inspecciona el resultado, Then **no queda ningún retiro parcial**: o se retiró todo, o no se retiró nada.
- Given una cuenta sin trabajos, When se ejecuta la baja, Then la baja procede igual y el arrastre no produce ningún efecto adicional.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-02 |
| RN e invariantes que ejerce | RN-04, RN-07 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Facultad, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-09, BT-13 |
| Tests previstos en 08 | Prueba del arrastre como testigo de la unidad de trabajo, con dobles |

## 5. Prioridad y estimación

`Must` por `RN-07` y porque `05` §8 fija el NFR de **0** casos de uso que repartan su efecto entre dos unidades de trabajo, con esta baja como testigo.

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

**El reseteo no dispara esta historia, y es la distinción que el producto vino a cerrar.** `RN-12` declara que resetear conserva la cuenta y todos sus trabajos; hasta que existió el reseteo, la única salida documentada ante un olvido de contraseña era esta baja, que cuesta todos los trabajos del alumno.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
