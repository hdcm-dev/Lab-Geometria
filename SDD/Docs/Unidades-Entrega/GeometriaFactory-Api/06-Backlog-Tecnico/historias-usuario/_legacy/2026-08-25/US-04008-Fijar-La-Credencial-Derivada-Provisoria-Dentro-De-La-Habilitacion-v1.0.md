> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md`](../../US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-04008 — Fijar la credencial derivada provisoria dentro de la habilitación

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que habilitar y rehabilitar una cuenta fijen su credencial derivada provisoria y le pongan la marca de cambio pendiente, en el mismo acto**, para **que el alumno entre con una clave que el administrador le comunica y quede obligado a cambiarla, sin ningún punto que fije una contraseña sin credencial vigente**.

## 2. Contexto

`RN-04016` declara que habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que la del reseteo. El contrato de uso es [`CU-00022`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md), en su fijación, solicitada por [`CU-00023`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md). `05` §10.2 asigna el tramo de `RN-04016` a esta capa en las operaciones de habilitar y rehabilitar.

## 3. Criterios de aceptación

- Given una cuenta que se habilita, When se aplica la transición, Then queda con **credencial derivada provisoria fijada** y con la **marca de cambio de contraseña pendiente puesta**, en el mismo acto.
- Given ese mismo acto, When se inspecciona lo que esta capa manipuló, Then el valor llegó **ya producido y ya derivado**: esta capa **no ve la contraseña en claro** y no la produce.
- Given una rehabilitación sobre una cuenta previamente bloqueada, When se aplica, Then ocurre lo mismo que en la habilitación: una provisoria nueva y la marca puesta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04003, CU-04002 |
| RN e invariantes que ejerce | RN-04014 (exigida por escrito, ejercida en `GeometriaFactory-Infrastructure`), RN-04016; INV-09 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas, Orquestación del ingreso y la credencial |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Facultad, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04007, BT-04013, BT-04014 |
| Tests previstos en 08 | Prueba de que la marca queda puesta y de que ningún valor en claro atraviesa esta capa |

## 5. Prioridad y estimación

`Must` por derivar de `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que **ningún punto de acceso del producto acepte un correo y una contraseña nueva sin credencial**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**`RN-04014` es la única de las dieciséis sin tramo en esta capa** (`02` §6 y `05` §10.2): que la provisoria no sea adivinable y no se repita lo ejerce `GeometriaFactory-Infrastructure`. Lo que esta historia sí hace es **exigirla por escrito**, para que no se pierda al bajar de contrato a implementación.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador, reescrita por `02` 1.5 sobre `RN-04016`. |
