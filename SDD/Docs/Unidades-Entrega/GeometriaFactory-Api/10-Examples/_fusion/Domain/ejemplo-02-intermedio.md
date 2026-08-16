# Ejemplo 02 — Un trabajo real del alumno: constitución, adopción de la interpretación y envío

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** [`/samples/domain/02-intermedio/`](../../../../../samples/domain/02-intermedio/), esqueletada con su README local y su comando previsto
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-02005` a `CU-02008`; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §3, operaciones `OP-05` a `OP-08`; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Casos-Prueba-Referenciales.md) 1.0 `TC-02011` a `TC-02018`; `PRODUCT-INTAKE` **1.25** §20, escenarios `E-1`, `E-3`, `E-4`, `E-5`, `E-6` y `E-8`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/_fusion/Domain/Matriz-Sensado-Deriva.md), que toma `VER-02002` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el ciclo de vida del trabajo dentro del dominio, con los datos reales que salen de la aplicación de escritorio de los alumnos: constituir un trabajo con su texto original íntegro, adoptar el conjunto de piezas y las observaciones que el consumidor interpretó afuera, y resolver el envío. Al terminar, quien lo ejecuta sabe por qué el dominio **adopta** una interpretación y no la produce, y por qué una advertencia deja pasar el trabajo mientras un error de validación lo retiene en `Borrador`.

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01: la cuenta ya existe, está habilitada y es admisible. Agrega dos de los **cinco** componentes que aquél no tocaba —adopción de la interpretación y máquina de estados del trabajo—, e introduce **seis** de los ocho escenarios reales del intake §20 como material de entrada —`E-1`, `E-3`, `E-4`, `E-5`, `E-6` y `E-8`—, con el motivo de los dos que no entran declarado en el §2 del [`README.md`](README.md) de la categoría.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3, sin agregados: **.NET 10**, entorno de desarrollo contenido del repositorio, etapa `a` cerrada y Linux. El sample sigue siendo autocontenido, porque el proyecto de código declara **0** dependencias salientes (`PRODUCT-INTAKE` §17.1.P.1).

**Un prerequisito de datos, no de herramientas:** los seis textos de escenario que el sample usa se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**. Son datos reales emitidos por la aplicación de escritorio de los alumnos y no se sustituyen por datos sintéticos.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/02-intermedio`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/domain/02-intermedio/
├── README.md                       # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                   # Recorre los seis escenarios en orden
├── Escenarios/
│   ├── E1.txt   E3.txt   E4.txt    # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   ├── E5.txt   E6.txt   E8.txt
│   └── Interpretacion.<ext>        # Compone el resultado de interpretación de cada texto
├── Recorrido/
│   ├── ActoConstituirTrabajo.<ext>       # OP-05
│   ├── ActoAdoptarPiezas.<ext>           # OP-06
│   ├── ActoAdoptarObservaciones.<ext>    # OP-07
│   └── ActoEnviar.<ext>                  # OP-08
└── tests/
    └── SalidaEsperada.<ext>        # Compara la salida contra el snapshot de §6
```

**Los seis archivos de escenario llevan extensión `.txt` y no `.json`.** Es la convención de los siete proyectos de código del producto y tiene un motivo verificable: el texto de `E-2` **no es JSON estrictamente válido** —trae dos comas finales— y nombrar `.json` a un archivo de escenario invita a que una herramienta lo reformatee al abrirlo. Acá `E-2` no está entre los seis, de modo que el riesgo no se materializa en este sample; la convención se sigue igual para que la carpeta de escenarios sea la misma en los siete proyectos de código.

**`Interpretacion.<ext>` no es un intérprete.** El dominio **no produce** el resultado de la interpretación: lo adopta ya producido ([`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §3, `OP-06` y `OP-07`). El archivo compone a mano, para cada escenario, el conjunto de piezas y observaciones que el consumidor le entregaría, y es deliberado que se vea así de explícito: es la frontera que el sample enseña.

## 6. Qué esperar

```
[E-1] Trabajo constituido: texto-identico=si estado=Borrador
[E-1] Piezas adoptadas: 3 | Observaciones adoptadas: 2 | Errores de validacion: 0
[E-1] Envio: estado=Pendiente (las advertencias no impiden el envio)
[E-3] Observacion adoptada: especie=Advertencia campo=Area declarado=36.00 derivado=54.00
[E-4] Observaciones adoptadas: 0 (mismo cubo de lado 3, area declarada coincidente)
[E-6] Piezas adoptadas: 1 | Envio: estado=Pendiente (el cero es un valor, no una ausencia)
[E-5] Pieza del indice 0 adoptada | Pieza del indice 1 rechazada: TIPO_DE_PIEZA_DESCONOCIDO
[E-5] Posicion 1 reservada: observacion de error aceptada sobre esa posicion
[E-5] Observacion de error: indice-figura=1 campo=Tipo
[E-5] Envio: estado=Borrador (RN-02005: un error de validacion retiene el trabajo)
[E-8] Observacion de error localizada: indice-figura=1 campo=Largo
[E-8] Envio: estado=Borrador | texto-original-intacto=si
Trabajos recorridos: 6 | Envios a Pendiente: 4 | Envios retenidos en Borrador: 2 | Excepciones: 0
```

**El contraste `E-3` contra `E-4` es el corazón del sample.** Son el **mismo cubo de lado 3**, emitido por los dos ejemplos de la cátedra: `E-3` declara área 36.00 y produce advertencia con el par de valores 36.00 y 54.00; `E-4` declara 54.00 y produce **cero** observaciones. Un dominio que advirtiera siempre pasaría el primero y fallaría el segundo, que es exactamente lo que el `PRODUCT-INTAKE` §20.E-4 declara al describir el criterio negativo.

**La línea de `E-5` con la posición reservada** materializa `RN-02009`: la figura del índice 1 no se adopta como pieza, su posición **queda reservada**, la del índice 0 conserva la suya sin renumerar, y una observación de error **sí** se acepta sobre la posición reservada, porque pertenece al rango.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Advertencia con un solo número | Emitir la observación de `E-3` sin el valor derivado | Rechazo `ADVERTENCIA_SIN_LOS_DOS_VALORES`: la observación no se adopta |
| Error sin ubicación | Quitar el índice y el campo de la observación de `E-5` | Rechazo `ERROR_SIN_UBICACION` (`RN-02009`) |
| Reeditar fuera del borrador | Reeditar el trabajo de `E-1` después de que pasó a `Pendiente` | Rechazo `REEDICION_FUERA_DE_BORRADOR` (`RN-02004`), el texto no cambia |
| Enviar sin interpretación | Invocar el envío antes de adoptar el resultado | Rechazo `ENVIO_SIN_INTERPRETACION`; el trabajo queda en `Borrador` |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-02005`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md) | Caso de uso | Constituye el trabajo con dueño, identidad y texto original; la variación de reedición recorre el rechazo |
| [`CU-02006`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) | Caso de uso | Adopta las piezas de `E-1`, `E-5`, `E-6` con identidad posicional y familia derivada del tipo |
| [`CU-02007`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) | Caso de uso | Adopta la advertencia de `E-3` con su par de valores y el error de `E-5` con índice y campo |
| [`CU-02008`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) | Caso de uso | Resuelve los seis envíos: cuatro a `Pendiente` y dos retenidos en `Borrador` |
| [`RN-02005`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Regla de negocio | Las advertencias no impiden el envío; un error de validación sí |
| [`RN-02008`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | Regla de negocio | `texto-identico=si` en `E-1` y `texto-original-intacto=si` en `E-8` |
| [`RN-02009`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Regla de negocio | Índice 1 y campo en `E-5` y en `E-8`; la posición reservada |
| [`ADR-02001`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Decisión arquitectónica | Las cuatro adopciones se rechazan por invariante y no por validación del consumidor |
| [`ADR-02006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) | Decisión arquitectónica | Las fechas de creación y de modificación llegan por parámetro; el sample no fija el reloj |
| `PRODUCT-INTAKE` §20 `E-1`, `E-3`, `E-4`, `E-5`, `E-6`, `E-8` | Escenario con payload real | Los seis textos se transcriben sin modificación y son la entrada del recorrido |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-02002
  verifica: [CU-02005, CU-02006, CU-02007, CU-02008, US-02009, US-02010, US-02011, US-02012, US-02013, US-02014, US-02015, US-02016]
  comando: "dotnet run --project samples/domain/02-intermedio"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Los seis textos de Escenarios/ transcriptos del PRODUCT-INTAKE §20 sin modificación"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[E-1] Piezas adoptadas: 3 | Observaciones adoptadas: 2 | Errores de validacion: 0"
      - "[E-3] Observacion adoptada: especie=Advertencia campo=Area declarado=36.00 derivado=54.00"
      - "[E-4] Observaciones adoptadas: 0 (mismo cubo de lado 3, area declarada coincidente)"
      - "[E-5] Observacion de error: indice-figura=1 campo=Tipo"
      - "[E-5] Envio: estado=Borrador (RN-02005: un error de validacion retiene el trabajo)"
      - "Trabajos recorridos: 6 | Envios a Pendiente: 4 | Envios retenidos en Borrador: 2 | Excepciones: 0"
    stdout_no_contiene:
      - "indice-figura=0 campo=Tipo"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye una aserción negativa.** `indice-figura=0 campo=Tipo` no debe aparecer: el primer elemento de `E-5` es válido a propósito, y que el índice reportado sea **1 y no 0** es lo que prueba que la ubicación se calcula en lugar de informar siempre la primera figura (`PRODUCT-INTAKE` §20.E-5, punto 2).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-1 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** El árbol de §5 nombraba los **seis** archivos de escenario con extensión `.json` —`E1`, `E3`, `E4`, `E5`, `E6` y `E8`—, contra la convención `.txt` que declaran los otros seis proyectos de código del producto, y este proyecto de código no declaraba el fundamento en ninguna parte de su categoría 10. Se corrigen las **dos** líneas del árbol y se agrega el fundamento debajo. **Ningún dato estaba en riesgo**: el único escenario que no es JSON estrictamente válido es `E-2`, que no está entre los seis. Se corrige además, fuera del informe, la §2, que declaraba «los **ocho** escenarios reales del intake §20 como material de entrada» cuando el sample usa **seis** y su propia §5, su §6 y el `README.md` de la categoría dicen seis. Se enlaza la carpeta esqueletada de `/samples` creada al resolver el **P0-1**, y se actualiza la trazabilidad al `PRODUCT-INTAKE` **1.25**. Ningún acto, criterio de aceptación ni recuento del contrato cambia. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-02005` a `CU-02008` con las operaciones `OP-05` a `OP-08`, sobre los **seis** escenarios reales `E-1`, `E-3`, `E-4`, `E-5`, `E-6` y `E-8` del `PRODUCT-INTAKE` §20, transcriptos sin modificación. El contrato `VER-02002` declara seis líneas exactas de salida y **una aserción negativa** sobre el índice reportado; `evidencia` queda en `No verificado — sin código`. |
