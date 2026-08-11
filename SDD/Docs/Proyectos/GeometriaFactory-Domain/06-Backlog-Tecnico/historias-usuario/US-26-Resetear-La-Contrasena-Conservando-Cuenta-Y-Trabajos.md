# US-26 — Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** US-26-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el reseteo fije una contraseña provisoria conservando la cuenta, su situación y todos sus trabajos**, para **que el primer olvido de contraseña deje de costarle al alumno toda la cursada**.

## 2. Contexto

La capacidad `F-26` del intake §4 es `Must Have` desde su versión 1.7 y **retira la exclusión `X-2`**: hasta entonces el único camino declarado era dar de baja y volver a dar de alta, y por `RN-07` eso eliminaba todos los trabajos del alumno. `RN-12` declara qué conserva el reseteo y `RN-15` que no exige cuenta habilitada.

## 3. Criterios de aceptación

- Given una cuenta de alumno con trabajos en tres estados distintos y con sus comentarios, When se la resetea, Then conserva su identidad, su situación y **todos** sus trabajos, por `RN-12`.
- Given una cuenta de alumno en `Bloqueado` o en estado `Pendiente`, When se la resetea, Then el reseteo procede y **no le cambia la situación**, por `RN-15`.
- Given la cuenta con papel `Administrador`, When se intenta resetearla, Then se rechaza, por `INV-08`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-13 |
| RN e invariantes que ejerce | RN-12, RN-14, RN-15; INV-08, INV-09 |
| BT derivadas | BT-10, BT-11 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del reseteo sobre cada situación de cuenta admitida y sobre la rechazada, con la conservación de los trabajos verificada. |

## 5. Prioridad y estimación

`Must` porque `PRODUCT-INTAKE` §4 declara `F-26` como `Must Have`, y porque el roadmap §3 declara que ubicarla en la fase `d` la compromete: la transición `d` → `e` incorpora sus criterios y la fase no cierra sin ellos.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**El reseteo se separó del ciclo de vida de la cuenta** por tres motivos declarados: no es una transición de la máquina de estados, no dispara `RN-07` y su efecto propio es poner una marca que ninguna de las cuatro operaciones toca ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6). Que la provisoria no sea adivinable y no se repita es `RN-14`, y su mecanismo es de `GeometriaFactory-Infrastructure`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
