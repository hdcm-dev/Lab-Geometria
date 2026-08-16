# US-04030 — Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que una cuenta con la marca de cambio de contraseña pendiente no pueda ejercer ninguna capacidad salvo cambiar su propia contraseña**, para **que una clave que el administrador conoce no quede sirviendo indefinidamente para operar como el alumno**.

## 2. Contexto

`RN-04013` declara que mientras la provisoria no se cambie la cuenta **se autentica y no obtiene sesión de trabajo**, e `INV-09` lo sostiene. El contrato de uso es [`CU-04011`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04011-Resetear-La-Contrasena-De-Un-Alumno.md) junto con la **cuarta comprobación transversal** de `02` §4. `Domain ADR-04005` §6 punto 1 declaró que el dominio **no puede impedir** que exista un camino que saltee la admisibilidad: esa dependencia de disciplina cae acá.

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When pide **cualquiera** de los once casos de uso salvo el reemplazo de su propia credencial, Then se devuelve el motivo de cambio de contraseña pendiente y **no se lee ni se escribe nada**.
- Given esa misma cuenta y un pedido que además fallaría por pertenencia o por facultad, When se lo procesa, Then el motivo emitido es el de **cambio de contraseña pendiente**: la cuarta comprobación **corta antes** que las otras tres.
- Given una cuenta sin la marca, When pide cualquiera de los once, Then la cuarta comprobación **no cambia nada** y el pedido sigue su curso normal.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-04011, y la comprobación transversal de `02` §4 |
| RN e invariantes que ejerce | RN-04013, RN-04016; INV-09 |
| Componente de `05` §3.1 | Guarda de autorización |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **La cuarta, y es su historia**: es la que la declara y la que la verifica |
| BT derivadas | BT-04008, BT-04010, BT-04011 |
| Tests previstos en 08 | **1** prueba que verifica que la cuarta corta antes que las otras tres, sin base de datos (`05` §8) |

## 5. Prioridad y estimación

`Must` porque `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca, y porque `05` §10.3 llama a esta comprobación **el aporte más consecuente de esta capa** a `INV-09`.

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

**Es una comprobación de esta capa y no una decisión de ruteo del front** (`02` §4, precisión 5): ocultar rutas acota lo que se ofrece y no hace cumplir nada. `GeometriaFactory-Web` declara lo mismo desde su lado —«la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable»— y `GeometriaFactory-Api` garantiza que **ningún punto de acceso quede fuera de la guardia**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
