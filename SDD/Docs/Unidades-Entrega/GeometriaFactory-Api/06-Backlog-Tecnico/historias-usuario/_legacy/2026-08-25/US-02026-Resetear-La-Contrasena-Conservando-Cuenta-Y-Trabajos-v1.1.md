> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md`](../../US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02026 — Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el reseteo fije una contraseña provisoria conservando la cuenta, su situación y todos sus trabajos**, para **que el primer olvido de contraseña deje de costarle al alumno toda la cursada**.

## 2. Contexto

La capacidad `F-26` del intake §4 es `Must Have` desde su versión 1.7 y **retira la exclusión `X-2`**: hasta entonces el único camino declarado era dar de baja y volver a dar de alta, y por `RN-02007` eso eliminaba todos los trabajos del alumno. `RN-02012` declara qué conserva el reseteo y `RN-02015` que no exige cuenta habilitada.

## 3. Criterios de aceptación

- Given una cuenta de alumno con trabajos en tres estados distintos y con sus comentarios, When se la resetea, Then conserva su identidad, su situación y **todos** sus trabajos, por `RN-02012`.
- Given una cuenta de alumno en `Bloqueado` o en estado `Pendiente`, When se la resetea, Then el reseteo procede y **no le cambia la situación**, por `RN-02015`.
- Given la cuenta con papel `Administrador`, When se intenta resetear su contraseña, Then se rechaza, por `INV-08`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-02013 |
| RN e invariantes que ejerce | RN-02012, RN-02014, RN-02015; INV-08, INV-09 |
| BT derivadas | BT-02010, BT-02011 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria del reseteo sobre cada situación de cuenta admitida y sobre la rechazada, con la conservación de los trabajos verificada. |

## 5. Prioridad y estimación

`Must` porque `PRODUCT-INTAKE` §4 declara `F-26` como `Must Have`, y porque el roadmap §3 declara que ubicarla en la fase `d` la compromete: la transición `d` → `e` incorpora sus criterios y la fase no cierra sin ellos.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**El reseteo se separó del ciclo de vida de la cuenta** por tres motivos declarados: no es una transición de la máquina de estados, no dispara `RN-02007` y su efecto propio es poner una marca que ninguna de las cuatro operaciones toca ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6). Que la provisoria no sea adivinable y no se repita es `RN-02014`, y su mecanismo es de `GeometriaFactory-Infrastructure`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». El caso de la **cuenta de administrador** se reescribe como «resetear **la contraseña de** la cuenta de administrador», que sigue sin admitirse (**INV-08**, **RN-02015**): no se cambia el sujeto a «de alumno», que invertiría el sentido de la regla. No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
