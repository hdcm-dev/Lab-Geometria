# US-04031 — Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el reseteo deje intactos la identidad, el papel, el estado de habilitación y todos los trabajos de la cuenta**, para **que recuperar una contraseña olvidada no le cueste al alumno lo que ya entregó**.

## 2. Contexto

`RN-04012` declara que el reseteo conserva la cuenta y sus trabajos y que **no dispara `RN-04007`**. El contrato de uso es [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md). `05` §10.2 declara el tramo de esta capa: la postcondición que deja todo intacto y la **ausencia deliberada** de todo retiro.

## 3. Criterios de aceptación

- Given un alumno con tres trabajos en tres estados distintos y con sus comentarios, When se resetea su contraseña, Then conserva los tres trabajos con sus estados y sus comentarios.
- Given esa misma cuenta, When se compara su estado de habilitación, su papel y su identidad antes y después, Then **no cambió ninguno de los tres**.
- Given el caso de uso de reseteo, When se inspecciona qué operaciones de retiro invoca, Then son exactamente **0**: el reseteo no pasa por ninguna ruta de retiro.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04011 |
| RN e invariantes que ejerce | RN-04007 por contraste, RN-04012, RN-04015 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Facultad, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04007, BT-04013 |
| Tests previstos en 08 | Prueba con un alumno con trabajos en tres estados, comprobando la conservación completa |

## 5. Prioridad y estimación

`Must` por `RN-04012` y porque el criterio de transición `d` → `e` lo exige verificado **sobre un alumno con trabajos en tres estados distintos y con sus comentarios**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**El contraste con US-04006 es el punto de la historia.** La baja arrastra todos los trabajos; el reseteo no toca ninguno. Que las dos operaciones vivan en el mismo panel del administrador es exactamente el motivo por el que la distinción tiene que ser verificable y no sólo declarada.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
