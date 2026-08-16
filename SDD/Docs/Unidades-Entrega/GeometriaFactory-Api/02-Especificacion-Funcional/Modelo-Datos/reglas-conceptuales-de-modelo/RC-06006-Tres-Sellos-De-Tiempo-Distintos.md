# RC-06006 — Los tres tiempos del trabajo son distintos y no se confunden

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-06006-Tres-Sellos-De-Tiempo-Distintos.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §17.3.P.4, «Ampliación del 2026-08-09: sellos de tiempo del trabajo» **[DECISIÓN del Product Owner]**, y «El JSON del alumno no lleva fechas y no se le agrega ninguna» **[DECISIÓN 2026-08-09]**; §17.2.P.11 punto 3 (el reloj es un puerto); `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Especificacion-Funcional.md` §9, que registra el cierre de este punto
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

El trabajo guarda **tres tiempos distintos**, y no se confunden entre sí:

| Tiempo | Quién lo produce | Qué significa |
| --- | --- | --- |
| `Fecha` | **El alumno**, escribiéndola en el formulario | Un dato del trabajo, como su nombre y su descripción |
| Fecha de creación | El sistema, a través del puerto de reloj | Cuándo se constituyó el trabajo |
| Fecha de última modificación | El sistema, a través del puerto de reloj | Cuándo se lo tocó por última vez |

Y una prohibición: **al texto del alumno no se le agrega ninguna fecha.** Los tres tiempos viven en la fila del trabajo, nunca dentro del texto conservado.

## 2. Justificación

Los dos sellos que produce el sistema entraron por decisión del Product Owner, y **se producen a través del puerto de reloj para que sean verificables en prueba**: sin puerto, un criterio de aceptación que exige un sello concreto no se puede escribir sin trucos.

La distinción frente a la `Fecha` del alumno no es formal. Confundirlas produce dos daños opuestos: si el sistema pisara la `Fecha` con el momento del guardado, le estaría corrigiendo un dato al alumno; y si la `Fecha` del alumno se usara como sello de modificación, el orden real de las entregas dejaría de ser reconstruible, porque el alumno puede escribir la fecha que quiera.

La prohibición sobre el texto es la contracara de la conservación íntegra: el texto que el programa del alumno produce contiene únicamente figuras con sus dimensiones y sus valores calculados, y el producto no lo modifica.

## 3. Ámbito de aplicación

- Alcanza a la entidad **trabajo**.
- **La cuenta guarda su fecha de alta**, que el consumidor aporta, y **no** una fecha de última modificación: el modelo del dominio no la declara, y las cuatro operaciones del administrador sobre una cuenta no registran ningún sello.
- **El sello de desenlace** —cuándo el administrador aprobó o rechazó— acompaña al comentario, según `RC-06007`.
- La **zona horaria y la precisión** de los sellos no están declaradas por ninguna fuente y quedan como punto abierto para `05-Arquitectura-Tecnica`.

## 4. Consecuencia si se viola

No hay rechazo del almacén: escribir el sello equivocado en la columna equivocada es un defecto que sólo se nota cuando alguien ordena por fecha y el resultado no se parece a lo que pasó.

## 5. CU afectados

- [`CU-06009`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) — Proveer el sello: es de donde salen los dos que produce el sistema.
- [`CU-06003`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar: es donde los tres se escriben en columnas distintas.

## 6. Pruebas que la verifican

`CU-06009` CA-02, que fija el reloj en un momento concreto y exige que el sello registrado sea exactamente ése. Del lado del almacén, `CU-06003` CA-01, que compara el texto materializado con el recuperado: **si se le hubiera agregado una fecha, los dos textos no serían idénticos**.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
