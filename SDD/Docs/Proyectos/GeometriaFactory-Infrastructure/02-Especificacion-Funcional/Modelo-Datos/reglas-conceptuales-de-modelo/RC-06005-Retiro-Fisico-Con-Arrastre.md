# RC-06005 — El retiro es físico y la baja arrastra todo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-06005-Retiro-Fisico-Con-Arrastre.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-02007`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md); [`RN-02004`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md); [`RN-02012`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) **por contraste**; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-03, F-24), §4.1 (RN-06004, RN-06007), §7 (CL-6)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

El retiro es **físico**: no queda fila marcada, no hay borrado lógico y no hay papelera. La baja de una cuenta retira la cuenta **y todos sus trabajos**, en cualquiera de los cuatro estados, **en la misma unidad de trabajo**: todo o nada.

## 2. Justificación

El enunciado de la baja es de negocio y viene con su criterio de verificación puesto: se verifica que **no quede ningún trabajo del alumno dado de baja**. Un borrado lógico volvería ese criterio incomprobable —las filas seguirían ahí— y convertiría la única operación destructiva del producto en algo que parece destructivo y no lo es.

El «todo o nada» no es una preferencia transaccional: una baja a medias deja **trabajos sin dueño**, que es la forma más silenciosa de romper el modelo, porque nada falla y el listado del administrador sigue mostrándolos.

## 3. Ámbito de aplicación

- Alcanza al retiro de **un trabajo** —con sus piezas, sus componentes, sus observaciones y su comentario— y a la **baja de una cuenta** con el arrastre de todos sus trabajos.
- **No distingue estados**: `Finalizado` y `Rechazado` son terminales para las transiciones, no para el retiro.
- **No alcanza al reseteo de contraseña**, y el contraste es deliberado: el reseteo **conserva** la cuenta y todos sus trabajos con sus estados y comentarios, y **no dispara** la baja. Es exactamente la confusión que la capacidad de reseteo vino a cerrar, y el modelo de datos no la reintroduce.
- La **confirmación escrita del correo** no vive acá: es de la capa de aplicación y llega resuelta.

## 4. Consecuencia si se viola

Un pedido de baja que no declara el arrastre devuelve `RETIRO_PARCIAL_NO_ADMITIDO` y **no retira nada** ([`CU-06004`](../../Casos-De-Uso/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) §6).

Introducir un borrado lógico no produciría ningún rechazo: produciría un producto en el que la baja no da de baja, y en el que el criterio con el que la regla se verifica pasa sin haber verificado nada.

## 5. CU afectados

- [`CU-06004`](../../Casos-De-Uso/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) — Borrado físico y arrastre: es donde se hace cumplir.
- [`CU-06007`](../../Casos-De-Uso/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) — **por contraste**: el reseteo no pasa por acá.

## 6. Pruebas que la verifican

`CU-06004` CA-01 —un trabajo retirado sin dejar ninguna fila—, CA-02 —la cuenta con tres trabajos en tres estados distintos, ninguno de los cuales sobrevive—, CA-03 —el arrastre no declarado, rechazado sin tocar nada— y CA-05 —la baja interrumpida, que no deja retiro parcial—. Las cuatro son de integración contra el almacén real.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
