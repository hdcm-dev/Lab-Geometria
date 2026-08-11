# US-04 — Informar el motivo cuando la cuenta no admite ingreso

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-04-Informar-El-Motivo-Cuando-La-Cuenta-No-Admite-Ingreso.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-03 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Ingreso`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta que todavía no fue habilitada o que fue bloqueada**, quiero **que la pantalla me diga en qué situación está mi cuenta**, para **saber si tengo que esperar al docente o hablar con él, en lugar de recibir un rechazo mudo**.

## 2. Contexto

`RN-06` fija que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso. El caso de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md). `05` §10.3 declara qué hace esta pieza por `RN-06`: mostrar el motivo de la situación de la cuenta al intentar ingresar, **sin sesión**.

## 3. Criterios de aceptación

- Given una cuenta en estado `Pendiente`, When la persona intenta ingresar con credenciales correctas, Then la superficie muestra el motivo explícito de que todavía no fue habilitada y **no se abre ninguna sesión**.
- Given una cuenta `Bloqueado`, When intenta ingresar, Then el motivo es distinguible del anterior.
- Given un correo o una contraseña equivocados, When intenta ingresar, Then el mensaje **no declara cuál de los dos campos falló**, y es indistinguible entre los dos casos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01, NB-02 |
| CU cubiertos | CU-02 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La admisibilidad la resuelve el dominio y la traducción a respuesta de protocolo, `GeometriaFactory-Api` |
| BT derivadas | BT-13 |
| Tests previstos en 08 | Comparación de las dos respuestas de credencial inválida, que deben ser indistinguibles |

## 5. Prioridad y estimación

`Must` por `RN-06`, y porque el criterio de transición `d` → `e` exige que un alumno cuya cuenta está en estado `Pendiente` reciba **un aviso explícito** de que todavía no fue habilitada.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los quince códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**Dos mensajes con propósitos opuestos conviven en la misma superficie**: el de situación de cuenta, que **sí** dice qué pasa, y el de credencial inválida, que **deliberadamente dice menos** de lo que el servicio sabe. Confundirlos convertiría la superficie de ingreso en un modo de averiguar qué correos existen.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
