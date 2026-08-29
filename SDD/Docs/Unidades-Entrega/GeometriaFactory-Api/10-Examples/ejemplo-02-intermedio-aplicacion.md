# Ejemplo 02 — Los ocho trabajos del alumno: carga, envío interpretado, consulta y retiro

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** `/samples/application/02-intermedio/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-04004`, `CU-04005`, `CU-04006` y `CU-04009`; [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, componentes de orquestación del trabajo y de la consulta; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §6, la tabla de los **ocho** escenarios; `PRODUCT-INTAKE` 1.23 §20, escenarios `E-1` a `E-8`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-04002` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el ciclo del trabajo tal como esta capa lo orquesta, con los **ocho** escenarios reales del `PRODUCT-INTAKE` §20 recorridos uno por uno: cargar el trabajo con su texto original íntegro, enviarlo interpretando su texto **por el puerto** y no acá, consultar lo propio con los cuatro estados distinguibles, y retirarlo sólo desde `Borrador`. Al terminar, quien lo ejecuta sabe **por qué el desenlace del envío lo decide el resultado de la interpretación y no el caso de uso**, y por qué una advertencia deja pasar el trabajo mientras un error lo retiene.

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01: la cuenta ya existe, está habilitada y es admisible. Agrega **dos** de los ocho componentes que aquél no tocaba —orquestación del trabajo y orquestación de la consulta—, el tercer puerto con doble —el de validación de figuras— y los ocho escenarios reales como material de entrada.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico-api.md) §3, sin agregados: **.NET 10**, entorno de desarrollo contenido del repositorio, etapa `a` cerrada y Linux. El sample sigue siendo autocontenido: la única dependencia saliente del proyecto de código es `GeometriaFactory-Domain` (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application) y los puertos se satisfacen con dobles.

**Un prerequisito de datos, no de herramientas.** El doble del puerto de validación devuelve, para cada uno de los ocho escenarios, **el resultado de interpretación que la sección «qué verificar» de ese escenario declara** en el `PRODUCT-INTAKE` §20, transcripto sin modificación. **No se compone ningún resultado nuevo**: esta capa no interpreta, y un resultado inventado acá sería un dato de geometría fabricado, que es exactamente lo que la regla de delivery del producto prohíbe.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/02-intermedio`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/application/02-intermedio/
├── README.md                            # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                        # Recorre los ocho escenarios en orden E-1 a E-8
├── Escenarios/
│   ├── E1.txt  E2.txt  E3.txt  E4.txt   # Textos originales, transcriptos del PRODUCT-INTAKE §20
│   ├── E5.txt  E6.txt  E7.txt  E8.txt   #   E2.txt NO es JSON estrictamente válido: es su gracia
│   └── ResultadosDeclarados.<ext>       # Piezas, observaciones y cantidad de figuras, por escenario
├── Dobles/
│   ├── ValidadorDeFigurasDeclarado.<ext>  # Doble del puerto de validación: devuelve lo declarado
│   ├── RepositorioDeTrabajosEnMemoria.<ext>
│   └── RelojFijo.<ext>
├── Actos/
│   ├── ActoCargarYReeditar.<ext>        # CU-04004
│   ├── ActoEnviar.<ext>                 # CU-04005
│   ├── ActoConsultarLoPropio.<ext>      # CU-04006
│   └── ActoEliminar.<ext>               # CU-04009
└── tests/
    └── SalidaEsperada.<ext>             # Compara la salida contra el snapshot de §6
```

**Los archivos de escenario llevan extensión `.txt` y no `.json`, a propósito.** El de `E-2` **no es JSON estrictamente válido** —tiene dos comas finales—, y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo. Acá el texto viaja íntegro por la capa y `RN-04008` exige que se conserve carácter por carácter: un reformateo silencioso rompería exactamente lo que el sample verifica. Es el mismo criterio que ya adoptaron las categorías 10 de `GeometriaFactory-Contracts` y de `GeometriaFactory-Visor`.

**`ValidadorDeFigurasDeclarado` no es un intérprete.** Devuelve, por escenario, el resultado que el intake declara. Es deliberado que se vea así de explícito: **esta capa no lee el texto del alumno**, y esa frontera es lo que el sample enseña.

## 6. Qué esperar

```
[E-1] Cargado: texto-identico=si estado=Borrador | Envio: 3 piezas, 2 advertencias, 0 errores -> Pendiente
[E-2] Envio: 1 pieza, 1 advertencia de volumen, 0 errores -> Pendiente
[E-3] Envio: advertencia de area declarado=36.00 derivado=54.00 -> Pendiente
[E-4] Envio: 0 observaciones -> Pendiente (mismo cubo de lado 3, area declarada coincidente)
[E-5] Envio: observacion Error indice-figura=1 campo=Tipo -> Borrador (RN-04005)
[E-6] Envio: la figura se interpreta y no se descarta -> Pendiente
[E-7] Detalle: 6 piezas con componentes | Listado: 6 piezas sin componentes
[E-8] Envio: observacion Error indice-figura=1 campo=Largo -> Borrador (RN-04005)
[Consulta] Listado propio: 8 trabajos | Pendiente=6 Borrador=2 Aprobado=0 Rechazado=0
[Retiro] Trabajo en Borrador por su dueno: retirado
[Retiro] Trabajo en Pendiente por su dueno: rechazado OPERACION_FUERA_DE_BORRADOR
[Retiro] Trabajo ajeno: rechazado TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE
[Reedicion] Trabajo fuera de Borrador: rechazado REEDICION_FUERA_DE_BORRADOR | texto-original-intacto=si
Escenarios recorridos: 8 | Envios a Pendiente: 6 | Retenidos en Borrador: 2 | Excepciones: 0
```

**El contraste `E-3` contra `E-4` es el que hay que mirar primero.** Son el **mismo cubo de lado 3**, emitido por los dos ejemplos de la cátedra: `E-3` declara área 36.00 y produce advertencia con el par 36.00 y 54.00; `E-4` declara 54.00 y produce **cero** observaciones. Una capa que incorporara siempre una observación pasaría el primero y fallaría el segundo, que es el criterio negativo que el `PRODUCT-INTAKE` §20.E-4 punto 4 declara.

**La línea `[E-7]` es la que separa el detalle del listado.** El detalle lleva piezas **y** componentes; el listado no lleva componentes ni texto original. Es la proyección que `US-04019` exige y que el contrato del producto ya había separado.

**La línea de retiro del trabajo ajeno no dice «no autorizado».** Dice `WORK_NOT_FOUND_FOR_REQUESTER`, porque `RN-04003` obliga a que el trabajo ajeno sea indistinguible del inexistente. Es la negativa por pertenencia de `ADR-04004` §2, que no se colapsa con la negativa por facultad ni se intercambia con ella.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Enviar sin interpretación disponible | Hacer que el doble del puerto de validación declare indisponibilidad | Rechazo `PARSE_RESULT_UNAVAILABLE`; el trabajo queda en `Borrador` y la capa **termina de forma controlada** (`US-04016`) |
| Reeditar el texto y compararlo | Reeditar `E-2` cambiando un solo carácter y volver a leer el guardado | El texto guardado es el reeditado, carácter por carácter, sin normalizar las comas finales |
| Reformatear `E2.txt` | Abrir `E2.txt` con una herramienta que lo normalice y volver a correr | El texto deja de ser el del alumno; `texto-identico` pasa a `no` y el criterio de aceptación falla. **Es lo que la extensión `.txt` viene a evitar** |
| Envío repetido sobre un trabajo ya enviado | Invocar `CU-04005` sobre un trabajo en `Pendiente` | Rechazo `SUBMISSION_OUTSIDE_DRAFT` |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00026`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Caso de uso | Carga el trabajo con dueño, identificador propio y sello del puerto de reloj, y recorre el rechazo de reedición |
| [`CU-00026`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Caso de uso | Resuelve los **ocho** envíos: 6 a `Pendiente` y 2 retenidos en `Borrador` |
| [`CU-00028`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Devuelve el listado propio con los cuatro estados distinguibles, y el detalle con componentes frente al listado sin ellos |
| [`CU-00027`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | Caso de uso | Retira sólo desde `Borrador` y sólo lo propio, con las dos negativas distinguidas |
| [`RN-02003`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Regla de negocio | El retiro de un trabajo ajeno responde como inexistente |
| [`RN-02004`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | Regla de negocio | El retiro fuera de `Borrador` se rechaza |
| [`RN-02005`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Regla de negocio | Las advertencias no impiden el envío; los errores de `E-5` y `E-8` sí |
| [`RN-02008`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | Regla de negocio | `texto-identico=si` en `E-1` y `texto-original-intacto=si` en la reedición rechazada |
| [`RN-02009`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Regla de negocio | Índice **1** y campo en `E-5` y en `E-8` |
| [`ADR-04005`](../05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) | Decisión arquitectónica | Cada uno de los ocho envíos es una unidad de trabajo, cuyo alcance fija esta capa |
| `PRODUCT-INTAKE` §20 `E-1` a `E-8` | Escenario con payload real | Los ocho textos se transcriben sin modificación y sus resultados de interpretación salen de la sección «qué verificar» de cada uno |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-04002
  verifica: [CU-04004, CU-04005, CU-04006, CU-04009, US-04010, US-04011, US-04012, US-04013, US-04014, US-04015, US-04016, US-04017, US-04018, US-04019, US-04026]
  comando: "dotnet run --project samples/application/02-intermedio"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Los ocho textos de Escenarios/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
    - "El doble del puerto de validación devuelve, por escenario, el resultado declarado en la sección «qué verificar» de ese escenario"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[E-1] Cargado: texto-identico=si estado=Borrador | Envio: 3 piezas, 2 advertencias, 0 errores -> Pendiente"
      - "[E-4] Envio: 0 observaciones -> Pendiente (mismo cubo de lado 3, area declarada coincidente)"
      - "[E-5] Envio: observacion Error indice-figura=1 campo=Tipo -> Borrador (RN-04005)"
      - "[E-8] Envio: observacion Error indice-figura=1 campo=Largo -> Borrador (RN-04005)"
      - "[Consulta] Listado propio: 8 trabajos | Pendiente=6 Borrador=2 Aprobado=0 Rechazado=0"
      - "[Retiro] Trabajo ajeno: rechazado TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE"
      - "Escenarios recorridos: 8 | Envios a Pendiente: 6 | Retenidos en Borrador: 2 | Excepciones: 0"
    stdout_no_contiene:
      - "indice-figura=0"
      - "[Retiro] Trabajo ajeno: rechazado FACULTAD_DE_ADMINISTRADOR_REQUERIDA"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye dos aserciones negativas.** `indice-figura=0` no debe aparecer: el primer elemento de `E-5` y el de `E-8` son válidos a propósito, y que el índice reportado sea **1 y no 0** es lo que prueba que la ubicación se calcula en lugar de informar siempre la primera figura (`PRODUCT-INTAKE` §20.E-5 punto 2). Y la negativa por pertenencia **no puede** salir como negativa por facultad: son las dos negativas que `ADR-04004` §2 prohíbe intercambiar, y colapsarlas revelaría la existencia de un trabajo ajeno.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3d` del renombre `F-03`, que lo cierra.** **3 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni **la prosa que narra el renombre** —una línea que trae la forma vieja y su par vigente está reportando, no usando—. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-04004`, `CU-04005`, `CU-04006` y `CU-04009` sobre los **ocho** escenarios reales `E-1` a `E-8` del `PRODUCT-INTAKE` §20, transcriptos sin modificación y con sus resultados de interpretación tomados de la sección «qué verificar» de cada uno. Declara por qué los archivos de escenario llevan extensión `.txt` y por qué el doble del puerto de validación no es un intérprete. El contrato `VER-04002` declara siete líneas exactas de salida y **dos aserciones negativas** —el índice reportado y la negativa por pertenencia que no puede salir como negativa por facultad—; `evidencia` queda en `No verificado — sin código`. |
