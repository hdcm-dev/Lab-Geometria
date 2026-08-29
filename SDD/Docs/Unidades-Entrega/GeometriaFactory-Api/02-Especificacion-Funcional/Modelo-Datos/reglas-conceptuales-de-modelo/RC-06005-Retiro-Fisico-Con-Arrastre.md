# RC-06005 — El retiro es físico y la baja arrastra todo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RC-06005-Retiro-Fisico-Con-Arrastre.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-02007`](../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md); [`RN-02004`](../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md); [`RN-02012`](../../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) **por contraste**; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4 (F-03, F-24), §4.1 (RN-06004, RN-06007), §7 (CL-6)
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

Un pedido de baja que no declara el arrastre devuelve `PARTIAL_DELETION_NOT_ALLOWED` y **no retira nada** ([`CU-06004`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) §6).

Introducir un borrado lógico no produciría ningún rechazo: produciría un producto en el que la baja no da de baja, y en el que el criterio con el que la regla se verifica pasa sin haber verificado nada.

## 5. CU afectados

- [`CU-06004`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) — Borrado físico y arrastre: es donde se hace cumplir.
- [`CU-06007`](../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) — **por contraste**: el reseteo no pasa por acá.

## 6. Pruebas que la verifican

`CU-06004` CA-01 —un trabajo retirado sin dejar ninguna fila—, CA-02 —la cuenta con tres trabajos en tres estados distintos, ninguno de los cuales sobrevive—, CA-03 —el arrastre no declarado, rechazado sin tocar nada— y CA-05 —la baja interrumpida, que no deja retiro parcial—. Las cuatro son de integración contra el almacén real.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
| 1.2 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../../Producto/Norma-De-Nomenclatura.md`](../../../../../Producto/Norma-De-Nomenclatura.md) §8. **1 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
