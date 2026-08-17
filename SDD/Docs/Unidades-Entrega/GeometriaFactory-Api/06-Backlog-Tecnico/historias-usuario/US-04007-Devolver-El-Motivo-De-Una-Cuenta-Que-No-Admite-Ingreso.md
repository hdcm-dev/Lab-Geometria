# US-04007 — Devolver el motivo de una cuenta que no admite ingreso

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04007-Devolver-El-Motivo-De-Una-Cuenta-Que-No-Admite-Ingreso.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **consultar la admisibilidad de una cuenta y recibir su motivo cuando no la admite**, para **poder decirle a la persona en qué situación está su cuenta en lugar de un rechazo mudo**.

## 2. Contexto

`RN-04006` fija que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso, y `02` §6 asigna a esta capa **la consulta de admisibilidad con su motivo**. El contrato de uso es [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md). `Domain ADR-04005` declara la admisibilidad como **puerta única** de las guardas de acceso.

## 3. Criterios de aceptación

- Given una cuenta `Habilitado` y sin marca de cambio de contraseña pendiente, When se consulta su admisibilidad, Then el resultado es admisible.
- Given una cuenta en estado `Pendiente` y otra en estado `Bloqueado`, When se consulta su admisibilidad, Then el resultado es no admisible y **los dos motivos son distinguibles**: no se colapsan en uno solo.
- Given una cuenta con la marca de cambio de contraseña pendiente puesta, When se consulta su admisibilidad, Then el resultado es **no admisible**, y ése es el camino por el que la cuenta **se autentica y no obtiene sesión de trabajo**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-04003 |
| RN e invariantes que ejerce | RN-04006, RN-04013; INV-06, INV-09 |
| Componente de `05` §3.1 | Orquestación del ingreso y la credencial |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **Ninguna previa**: es la consulta que **produce** el insumo de la cuarta comprobación |
| BT derivadas | BT-04008, BT-04014, BT-04010 |
| Tests previstos en 08 | Prueba de los tres motivos distinguibles, con dobles y sin base de datos |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `d` → `e` exige que una cuenta en estado `Pendiente` reciba un aviso explícito de que todavía no fue habilitada.

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

**Acá no se compara ninguna contraseña.** `02` §1 declara que esta capa autoriza y no autentica: la verificación de la credencial es de `GeometriaFactory-Infrastructure` y la traducción del motivo a respuesta de protocolo es de `GeometriaFactory-Api`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
