# Ejemplo 02 — La colección de peticiones reproducible: los ocho escenarios contra la superficie ensamblada

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** `/samples/api/02-intermedio/`
**Trazabilidad upstream:** [`CU-00012`](CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md) 1.2 entero, y `CU-00003` a `CU-00008` de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/); [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, los **quince** puntos de acceso; [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md); [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) `TC-00035`; `PRODUCT-INTAKE` 1.23 §16.1, §18 **S-2**, §20 los **ocho** escenarios, y §21
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-00002` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

**Es la muestra `S-2` del producto.** Recorrer la superficie ensamblada en el orden en que el producto se usa, con **los ocho escenarios del `PRODUCT-INTAKE` §20 como cuerpo**, y comparar cada respuesta con la que su propia fuente declara esperada. Al terminar, quien lo ejecuta sabe **qué observaciones produce cada escenario y en qué estado queda cada trabajo**, sin ninguna pantalla de por medio, y ha visto rechazar contra la superficie —y no contra un control que no se dibuja— los caminos que las reglas prohíben.

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01: ya se sabe por qué un envío que no verifica responde con éxito. Agrega los **seis** casos de uso de capacidad que aquél no tocaba, los **ocho** escenarios como cuerpo y **13 de los 15** puntos de acceso.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico-api.md) §3: entorno de desarrollo contenido, almacén reiniciado, servicio levantado, dirección tomada del entorno y un cliente de peticiones nombrado por su función.

**Un prerequisito de datos, no de herramientas.** Los ocho cuerpos se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, incluidas las **dos comas finales** y la clave `"Tapas"` de `E-2`. **No se inventa ningún texto de prueba**, y no es una preferencia de esta categoría: es una regla de delivery del producto (`PRODUCT-INTAKE` §15), medida como NFR con umbral **0** y verificada por `TC-00035`.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio: `bash scripts/run-api.sh`.
4. Ejecutar la colección: `bash samples/api/02-intermedio/run.sh`.
5. Comparar la salida con §6.

**El flujo principal de `CU-00012` §4 son tres pasos** —reinicio, ejecución del servicio y colección—; acá son cinco porque se cuentan también la apertura del repositorio y la comparación. Los cinco están **dentro** del entorno contenido: **cero** pasos en el host, que es `CA-01` de `CU-00012` §8.

## 5. Estructura del código

```
samples/api/02-intermedio/
├── README.md                        # Copia corta de §1, §3 y §4 de este documento
├── run.sh                           # Punto de entrada único: ejecuta la colección entera
├── coleccion/
│   ├── 1-administrador              # A-03, y el segundo intento que no procede
│   ├── 2-cuenta-de-alumno           # A-02, A-01, A-06, A-07, A-05
│   ├── 3-canje-de-los-dos-papeles   # A-01
│   ├── 4-envio-de-los-ocho          # A-10, un trabajo por escenario
│   ├── 5-listado-y-detalle          # A-13, A-14, con los dos papeles
│   ├── 6-desenlace                  # A-15, uno aprobado y uno rechazado
│   ├── 7-caminos-prohibidos         # A-12, A-15, A-11, forzados contra la superficie
│   └── 8-reseteo-y-confinamiento    # A-09, A-05, A-13
├── cuerpos/
│   ├── E1.txt  E2.txt  E3.txt  E4.txt   # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── E5.txt  E6.txt  E7.txt  E8.txt   #   E2.txt NO es JSON estrictamente válido: es su gracia
└── esperado/
    └── salida.txt                   # Snapshot de la salida de §6
```

**El orden de los ocho guiones es el de `CU-00012` §4 y no se elige acá.** Es el orden en que el producto se usa, y por eso la colección puede recorrerlo entero sin volver atrás.

**El guion 7 es el que justifica que esta colección exista y no sea un recorrido feliz.** El intake declara **bloqueante** que la eliminación de un trabajo que no está en `Borrador` o que no pertenece al solicitante se verifique **forzando la petición contra esta superficie**, y una colección de peticiones es exactamente el instrumento para eso: que un control no se dibuje en una pantalla no prueba nada.

**Los archivos de cuerpo llevan extensión `.txt` y no `.json`, a propósito.** El de `E-2` **no es JSON estrictamente válido**, y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo: se perdería la tolerancia `T2` que ese escenario existe para ejercitar, y `CA-02` de `CU-00012` §8 —**0 textos modificados, incluidas las comas finales**— dejaría de cumplirse en silencio.

## 6. Qué esperar

```
[1] Configurar administrador: 201 | Segundo intento: 409 CONTRATO_ADMINISTRADOR_YA_CONFIGURADO
[2] Registrar alumno: 201 | Habilitar: 200 con provisoria | Cambiar contrasena: 200
[3] Canje como alumno: 200 | Canje como administrador: 200
[4] E-1: 201 estado=Pendiente | piezas=3 observaciones=2
[4] E-2: 201 estado=Pendiente | piezas=1 advertencia de volumen declarado=343.00 derivado=1029.00
[4] E-3: 201 estado=Pendiente | advertencia de area declarado=36.00 derivado=54.00
[4] E-4: 201 estado=Pendiente | observaciones=0
[4] E-5: 201 estado=Borrador | error indice-figura=1 campo=Tipo
[4] E-6: 201 estado=Pendiente | la figura se interpreta y no se descarta
[4] E-7: 201 estado=Pendiente | piezas=6
[4] E-8: 201 estado=Borrador | error localizado por indice de figura y campo
[4] Envios: 8 | Respuestas de exito: 8 | Pendiente: 6 | Borrador: 2
[5] Listado del alumno: 200 con 8 | Listado del administrador: 200 con 6 | borradores visibles: 0
[5] Detalle con los dos papeles: 200 | componentes presentes: si
[6] Aprobar con comentario: 200 estado=Aprobado | Rechazar sin comentario: 200 estado=Rechazado
[7] Eliminar un trabajo propio fuera de Borrador: 409 | Eliminar un trabajo ajeno: 404
[7] Desenlace pedido por un alumno: 403 | Reenvio sobre un trabajo que no esta en Borrador: 409
[7] Caminos prohibidos forzados: 4 | Rechazados por el servicio: 4 | Rechazados por una pantalla: 0
[8] Reseteo: 200 con provisoria | Listado pedido por el alumno: 403 CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO
[8] Cambio de contrasena: 200 | La misma peticion de listado: 200
[cobertura] Puntos de acceso ejercitados: 13 de 15
[datos] Cuerpos inventados: 0 | Cuerpos modificados: 0
Pasos de la coleccion: 3 | Peticiones: 34 | Diferencias contra lo esperado: 0
```

**La línea `[4] Envios: 8 | Respuestas de exito: 8 | Pendiente: 6 | Borrador: 2` es la propiedad más importante que esta colección demuestra.** Los ocho responden con éxito y ninguno con fallo: **el estado del trabajo no es el código de respuesta**. Dos quedan en `Borrador` y los ocho se guardaron. Es `CA-03` de `CU-00012` §8.

**El contraste `E-3` contra `E-4` es el criterio negativo, y acá se ve contra la superficie.** Mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra: el primero advierte con su par de valores, el segundo produce **cero** observaciones. Un validador que advirtiera siempre pasaría el primero y fallaría el segundo.

**Las dos líneas de `[7]` no son intercambiables.** Eliminar un trabajo propio fuera de `Borrador` responde **409** —el estado no lo permite— y eliminar un trabajo ajeno responde **404** —`RN-00003`: el ajeno es indistinguible del inexistente—. Colapsarlas en un `403` revelaría que ese trabajo existe.

**Las dos líneas de `[8]` juntas son `RN-00013` de punta a punta.** Después del reseteo el alumno queda **confinado al cambio**, y recién después de cambiar la contraseña la misma petición funciona. Es `CA-06` de `CU-00012` §8.

**Lo que esta salida no declara, a propósito.** El recuento de observaciones de `E-7` no aparece: **ninguna fuente lo declara** para ese escenario, y esta categoría **no lo calcula**. Es la misma abstención que `CU-00012` §6 declara en su fila de `E-7`.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Correr dos veces sin reiniciar | Repetir el paso 4 sin el paso 2 | El guion 1 responde con su negativa porque el administrador ya está configurado, y el 2 con la suya porque el correo ya está registrado. **Es un resultado legítimo y la colección lo declara**: reiniciar el almacén es el paso 1 por este motivo (`CU-00012` §5 `FA-02`) |
| Sólo la parte del dato del alumno | Ejecutar los guiones 1 a 5 | Se ve qué observaciones produce cada escenario y en qué estado queda cada trabajo, sin desenlaces ni reseteo (`CU-00012` §5 `FA-01`) |
| Reformatear `E2.txt` | Abrir el archivo con una herramienta que lo normalice | `Cuerpos modificados` pasa a **1** y `CA-02` deja de cumplirse. Es lo que la extensión `.txt` viene a evitar |
| Colapsar el `404` del trabajo ajeno en `403` | Responder «no autorizado» en lugar de «no encontrado» | `RN-00003` deja de cumplirse: la respuesta revela que el trabajo existe |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00012`](CU-00012-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md) | Caso de uso | **Es** la colección que ese contrato declara, con su recorrido de ocho guiones y sus ocho criterios de aceptación |
| [`CU-00021`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | Caso de uso | Guion 1: el alta de la cuenta de alumno |
| [`CU-00025`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | Caso de uso | Guion 1: la configuración del administrador en el primer arranque |
| [`CU-00022`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Caso de uso | Guiones 1 y 2: el ingreso, la guardia y el cambio de la credencial propia |
| [`CU-00023`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Caso de uso | Guion 2: listado de cuentas y cambio de situación con la provisoria devuelta |
| [`CU-00024`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | Caso de uso | Guion 8: reseteo con la provisoria devuelta una sola vez |
| [`CU-00026`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Caso de uso | Guion 4: los **ocho** envíos, con su estado resuelto y sus observaciones localizadas |
| [`CU-00027`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | Caso de uso | Guion 7: las eliminaciones forzadas, con los dos alcances |
| [`CU-00028`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Guion 5: listado y detalle con los dos papeles, con **0** borradores para el administrador |
| [`CU-00029`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | Caso de uso | Guion 6: aprobación con comentario y rechazo sin comentario |
| [`RN-02003`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Regla de negocio | El trabajo ajeno responde **404**, igual que el inexistente |
| [`RN-02004`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | Regla de negocio | La eliminación fuera de `Borrador` responde **409**, forzada contra la superficie |
| [`RN-02010`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | Regla de negocio | El desenlace pedido por un alumno responde **403** |
| [`RN-02012`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | Regla de negocio | Guion 8: el reseteo no pierde nada |
| [`RN-02013`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Regla de negocio | Guion 8: el confinamiento y su levantamiento |
| [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | Decisión arquitectónica | La colección es parte del contrato hacia afuera y se actualiza **en la misma intervención** en que cambia la superficie |
| `PRODUCT-INTAKE` §18 **S-2** | Muestra del producto | Este sample **es** S-2. Su alcance —los **ocho** cuerpos— hereda la lectura de `CU-00012` §10, y la divergencia con §18 está declarada en §4 del [`README.md`](README.md) de esta carpeta |
| `PRODUCT-INTAKE` §20 `E-1` a `E-8` | Escenario con payload real | Los ocho textos se transcriben sin modificación y son los cuerpos del guion 4 |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-00002
  verifica: [CU-00003, CU-00004, CU-00005, CU-00006, CU-00007, CU-00008, CU-00012, US-00007, US-00008, US-00009, US-00010, US-00011, US-00012, US-00013, US-00014, US-00015, US-00016, US-00017, US-00018, US-00019, US-00020, US-00021, US-00022, US-00023, US-00030]
  comando: "bash samples/api/02-intermedio/run.sh"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén queda en su estado de primer arranque, sin ninguna cuenta y sin ningún trabajo"
    - "`bash scripts/run-api.sh` ejecutado y el punto de salud respondiendo antes de la primera petición"
    - "Dirección del servicio y clave de firma provistas por configuración del entorno; ninguna de las dos escrita en el sample"
    - "Los ocho cuerpos transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear y con sus dos comas finales"
  criterio_aceptacion:
    exit_code: 0
    http:
      - punto: "envío de cada uno de los ocho escenarios"
        status: 201
      - punto: "eliminación de un trabajo ajeno por un alumno"
        status: 404
      - punto: "eliminación de un trabajo propio fuera de Borrador"
        status: 409
      - punto: "desenlace pedido por un alumno"
        status: 403
      - punto: "listado pedido por el alumno inmediatamente después del reseteo"
        status: 403
        body_json: { codigo: "CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO" }
    stdout_contiene:
      - "[4] E-4: 201 estado=Pendiente | observaciones=0"
      - "[4] E-5: 201 estado=Borrador | error indice-figura=1 campo=Tipo"
      - "[4] Envios: 8 | Respuestas de exito: 8 | Pendiente: 6 | Borrador: 2"
      - "[5] Listado del alumno: 200 con 8 | Listado del administrador: 200 con 6 | borradores visibles: 0"
      - "[7] Caminos prohibidos forzados: 4 | Rechazados por el servicio: 4 | Rechazados por una pantalla: 0"
      - "[8] Cambio de contrasena: 200 | La misma peticion de listado: 200"
      - "[cobertura] Puntos de acceso ejercitados: 13 de 15"
      - "[datos] Cuerpos inventados: 0 | Cuerpos modificados: 0"
      - "Pasos de la coleccion: 3 | Peticiones: 34 | Diferencias contra lo esperado: 0"
    stdout_no_contiene:
      - "[7] Eliminar un trabajo ajeno: 403"
      - "borradores visibles: 1"
      - "estado=Pendiente | error indice-figura"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye tres aserciones negativas.** Las tres son los defectos que **pasarían todas las aserciones positivas**: responder `403` en lugar de `404` ante un trabajo ajeno revela que existe y viola `RN-00003`; un solo borrador visible en el listado del administrador viola `RN-00011`, que no admite gradación; y un trabajo con observación de error que quedara en `Pendiente` violaría `RN-00005` sin cambiar ningún código de respuesta.

**Y una precisión sobre `Pasos de la coleccion: 3`.** Es el recuento que `CU-00012` §4 declara para el flujo principal y el que el NFR de `05` §8 mide con umbral **5 o menos**. Los cinco pasos de §4 de este documento incluyen la apertura del repositorio y la comparación, que no son pasos de la colección.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Materializa la muestra **S-2** del `PRODUCT-INTAKE` §18 y el contrato `CU-00012`: **ocho** guiones en el orden que ese caso de uso declara, con los **ocho** escenarios del §20 como cuerpo, transcriptos sin modificación, y **13 de los 15** puntos de acceso ejercitados. Declara por qué los archivos de cuerpo llevan extensión `.txt`, por qué el guion 7 justifica que la colección exista, y **qué no declara a propósito** —el recuento de observaciones de `E-7`, que ninguna fuente da—. El contrato `VER-00002` declara **cinco** aserciones de respuesta HTTP, nueve líneas exactas de salida y **tres aserciones negativas**; `evidencia` queda en `No verificado — sin código`. |
