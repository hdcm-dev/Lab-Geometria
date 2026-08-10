# CU-10 — Preparar el almacén al arrancar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-10-Preparar-El-Almacen-Al-Arrancar.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); [`NB-08`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §17.3.P.4, §17.3.P.7, §17.3.P.8, §17.3.P.11 punto 3, §17.5.P.4, §17.5.P.8 y §17.5.P.10 (el arranque en frío de CA-07, rotulado asunción allá); el arranque que lo invoca es responsabilidad declarada de `GeometriaFactory-Api`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Dejar el almacén en condiciones de recibir escrituras **antes de que el servicio atienda su primera petición**: crear el archivo si no existe, aplicar las transformaciones de esquema pendientes y dejarlo en el modo de diario declarado.

Es una decisión pre-tomada del producto: **las transformaciones de esquema se aplican al arrancar**, sobre base inexistente o desactualizada, y no por un paso manual de despliegue. El despliegue del backend lo ejecuta el docente a mano, y un paso manual más sería un paso que alguien se olvida.

Lo que este caso de uso **no** hace: no decide **cuándo** arrancar ni **dónde** vive el archivo —la ruta viene de configuración y quien la toma es `GeometriaFactory-Api`—, y no siembra ningún dato. **El primer arranque deja el almacén vacío**: la cuenta de administrador se configura después, por el camino de alta que le corresponde.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor (`GeometriaFactory-Api`, al arrancar) | Primario | Invoca la preparación antes de atender peticiones |
| Almacén de datos | Sistema | El archivo único de SQLite, en la ruta configurada |

## 3. Precondiciones

- La **ruta del archivo** llega de configuración. En producción apunta a un volumen persistente y **nunca al interior de la imagen**.
- No hace falta que el archivo exista: crearlo es parte de este contrato.

## 4. Flujo principal

1. El consumidor invoca la preparación al arrancar.
2. Se resuelve la ruta configurada y, si el archivo no existe, se crea.
3. Se dejan aplicadas **todas** las transformaciones de esquema pendientes, en su orden.
4. Se deja el almacén en modo de diario **WAL**.
5. Se devuelve la preparación como completa, y recién entonces el consumidor atiende peticiones.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El archivo no existe | Se crea y se aplican **todas** las transformaciones desde el origen. Es el caso del primer arranque y **el que la puerta de calidad de la etapa verifica**: las transformaciones se aplican solas sobre una base inexistente | Paso 4 |
| FA-02 | El esquema ya está al día | No se aplica ninguna transformación y la preparación termina igual. **Arrancar dos veces seguidas produce el mismo resultado** | Paso 4 |
| FA-03 | El esquema está desactualizado en una o más transformaciones | Se aplican las pendientes, en orden, y ninguna de las ya aplicadas se vuelve a ejecutar | Paso 4 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `MIGRACION_NO_APLICABLE` | Una transformación de esquema no se puede aplicar sobre el almacén encontrado: el esquema diverge del linaje de transformaciones conocido, o está por delante de él | **Detiene el arranque** y lo declara. No se atiende ninguna petición. **No se aplica un esquema por aproximación y no se descarta el almacén**: los dos caminos perderían datos de alumnos, y el remedio —restaurar el respaldo, o revisar la transformación— es una decisión de la persona que despliega, no del programa |
| `RUTA_DEL_ALMACEN_NO_DISPONIBLE` | La ruta configurada no es alcanzable o no admite escritura: típicamente el volumen persistente no está montado | **Detiene el arranque** y lo declara. **No se cae hacia una ruta alternativa dentro de la imagen**: el servicio arrancaría, aceptaría trabajos y los perdería en el siguiente reemplazo de versión, sin que nadie lo notara hasta entonces |

**Las dos comparten forma de terminación y es propia de este contrato: el arranque se detiene.** Ninguna de las dos deja el servicio en pie sobre un almacén en el que no se puede confiar.

## 7. Postcondiciones

- **Éxito:** el archivo existe en la ruta configurada, su esquema está al día, el modo de diario es WAL y el servicio puede atender peticiones.
- **Éxito en el primer arranque:** además, el almacén está **vacío**: ninguna cuenta, ningún trabajo.
- **Fallo:** el servicio **no atiende ninguna petición**, y el almacén queda como estaba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una ruta configurada donde no existe ningún archivo | Se prepara el almacén | El archivo se crea, **todas** las transformaciones se aplican solas y el esquema queda completo. Es el criterio de aceptación de la etapa que declara el intake |
| CA-02 | Un almacén con el esquema al día | Se prepara dos veces seguidas | El resultado es el mismo las dos veces y **no se aplica ninguna transformación** en la segunda |
| CA-03 | Un almacén preparado | Se consulta su modo de diario | Es **WAL** |
| CA-04 | Un almacén cuyo esquema no corresponde al linaje de transformaciones conocido | Se prepara | Devuelve `MIGRACION_NO_APLICABLE`, **el arranque se detiene** y **el almacén no se modifica ni se descarta** |
| CA-05 | Una ruta configurada que no admite escritura | Se prepara | Devuelve `RUTA_DEL_ALMACEN_NO_DISPONIBLE`, el arranque se detiene y **no se crea ningún archivo en ninguna otra ruta** |
| CA-06 | Un almacén recién preparado en su primer arranque | Se consultan las cuentas | **No hay ninguna**, y en particular no hay ninguna cuenta de administrador sembrada |
| CA-07 | El arranque completo desde una base inexistente | Se cronometra | Aplica las transformaciones y el servicio responde su comprobación de salud en **menos de 30 segundos**. El valor está rotulado como asunción aguas arriba y se usa como vigente |
| CA-08 | Un almacén preparado y el guion de reinicio del repositorio | Se ejecuta el guion y se prepara de nuevo | El almacén queda en el **estado de primer arranque**, que es el camino de vuelta declarado del producto |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-08 en cuanto un arranque que se detiene es preferible a un servicio que pierde datos en silencio |
| Reglas de negocio aplicables | Ninguna directa. **Sostiene a todas indirectamente**: sin esquema al día no hay dónde hacer cumplir ninguna |
| Reglas conceptuales de modelo | Las siete de [`Modelo-Datos/`](../Modelo-Datos/Modelo-Conceptual.md), que este contrato deja materializadas |
| Consumidor | `GeometriaFactory-Api`, en su arranque |
| Historias de usuario a generar en 06 | US-24, US-25 |
| Componentes esperados en 05 | Transformaciones de esquema versionadas y el paso de preparación invocado desde el arranque |
| Tests previstos en 08 | La verificación de que las transformaciones **se aplican solas sobre una base inexistente**, que el intake declara puerta de calidad bloqueante de la construcción, más el arranque repetido de CA-02 |

## 10. Notas y supuestos

- **Cada transformación de esquema se versiona con el código de su etapa, y las ya fusionadas no se editan.** Editar una fusionada rompería el linaje contra el que `MIGRACION_NO_APLICABLE` compara, y el defecto aparecería recién en el despliegue de destino.
- **El motor es un archivo único y no un servicio cliente-servidor.** Es coherente con un despliegue domiciliario de un contenedor, y es lo que hace que preparar el almacén sea parte del arranque y no una operación aparte.
- **El respaldo es la copia del archivo con WAL activo**, y su frecuencia queda a definir por el docente. Este contrato no lo ejecuta y no lo programa: lo hace posible al fijar el modo de diario.
- **La detención del arranque no es una elección conservadora, es la única segura.** Las dos condiciones de §6 tienen en común que la alternativa deja el servicio en pie sobre datos que no son los que cree tener. El intake declara que el servidor domiciliario no tiene alta disponibilidad y que su caída se responde con estado degradado en la pieza pública: una detención declarada entra en ese camino, un almacén equivocado no.
- **Este contrato no siembra la cuenta de administrador.** El primer arranque deja el almacén vacío y la ventana de alta del administrador se abre precisamente porque no existe ninguna.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |

## 17. Compatibilidad de la superficie pública

Agregar una transformación de esquema es compatible: se aplica sola sobre el almacén encontrado. **Editar una transformación ya fusionada, descartar el almacén ante un esquema divergente, caer hacia una ruta alternativa cuando la configurada no responde, o atender peticiones sin haber completado la preparación son cambios incompatibles** y suben versión mayor: los cuatro pueden perder datos de alumnos y tres de ellos lo hacen sin aviso.
