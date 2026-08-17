# Ejemplo 03 — El administrador: gobierno de las cuentas, revisión de la comisión, desenlace y reseteo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-03-avanzado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/application/03-avanzado/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-04002`, `CU-04007`, `CU-04008` y `CU-04011`; [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, componentes de gobierno de cuentas, de consulta y de desenlace; [`../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) §2, comprobaciones 3 y 4
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-04003` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar todo lo que sólo el administrador puede hacer y **por qué esta capa es la que lo hace cumplir**: habilitar, bloquear, rehabilitar y dar de baja con confirmación escrita y arrastre; recorrer la entrega de la comisión sin ver un solo borrador; aprobar y rechazar desde `Pendiente` con comentario opcional y propagar la terminalidad; y resetear la contraseña de un alumno sin perder ni la cuenta ni sus trabajos. Al terminar, quien lo ejecuta sabe **en qué se diferencian la negativa por facultad y la negativa por alcance**, y por qué la primera puede ser explícita y la segunda no.

## 2. Nivel

**Avanzado.** Supone hechos los ejemplos 01 y 02: hay cuentas y hay trabajos en los cuatro estados. Agrega los **dos** componentes que faltaban ejercer —gobierno de cuentas y desenlace—, el **cuarto** puerto con doble, y es el único de los tres que recorre las comprobaciones **3** y **4** de `ADR-04004` §2: facultad y alcance del administrador.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico-api.md) §3: **.NET 10**, entorno de desarrollo contenido del repositorio, etapa `a` cerrada y Linux.

**Un prerequisito propio.** El sample **no** parte del estado que dejaron los otros dos: lo reconstruye desde cero con los **cuatro** dobles de puerto, para que se pueda ejecutar solo. Ése es el motivo de que su tiempo de setup sea mayor, y no una dependencia externa.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/03-avanzado`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/application/03-avanzado/
├── README.md                             # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                         # Recorre los cuatro actos en orden
├── Dobles/
│   ├── RepositorioDeCuentasEnMemoria.<ext>
│   ├── RepositorioDeTrabajosEnMemoria.<ext>
│   ├── ValidadorDeFigurasDeclarado.<ext>
│   └── RelojFijo.<ext>
├── Semilla/
│   └── ComisionDeEjemplo.<ext>           # 1 administrador, 3 alumnos, 4 trabajos en los cuatro estados
├── Actos/
│   ├── ActoGobiernoDeCuentas.<ext>       # CU-04002
│   ├── ActoRevisarLaComision.<ext>       # CU-04007
│   ├── ActoDesenlace.<ext>               # CU-04008
│   └── ActoReseteo.<ext>                 # CU-04011
└── tests/
    └── SalidaEsperada.<ext>              # Compara la salida contra el snapshot de §6
```

**Los datos de identidad de `ComisionDeEjemplo` son evidentemente ficticios y se declaran como tales.** Ningún escenario del `PRODUCT-INTAKE` §20 fija correos, nombres ni identificadores: los escenarios son datos de **geometría**, y los de identidad y orquestación no se toman de ahí ni se fabrican como si lo fueran. Es la regla de [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §6.

## 6. Qué esperar

```
[1] Habilitar cuenta pendiente: habilitada | provisoria producida por el sistema: si
[1] Bloquear cuenta habilitada: bloqueada | Rehabilitar: habilitada + provisoria nueva
[1] Transicion no admitida sobre una cuenta habilitada: rechazada TRANSICION_DE_CUENTA_NO_ADMITIDA
[1] Baja con el correo escrito distinto: rechazada CONFIRMACION_DE_BAJA_NO_COINCIDE
[1] Baja con el correo escrito coincidente: dada de baja | trabajos arrastrados: 2
[1] Baja de la cuenta de administrador: rechazada CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA
[2] Listado de la comision: 3 trabajos | borradores visibles: 0 (RN-04011)
[2] Detalle de un trabajo en Borrador pedido por el administrador: TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR
[2] Listado de la comision pedido por un alumno: rechazado ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR
[3] Aprobar desde Pendiente con comentario: Aprobado
[3] Rechazar desde Pendiente sin comentario: Rechazado (el comentario es opcional)
[3] Desenlace sobre un trabajo ya Aprobado: rechazado TRANSICION_DESDE_ESTADO_TERMINAL
[3] Desenlace pedido por un alumno: rechazado DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR
[4] Reseteo de un alumno bloqueado: aplicado | situacion conservada=Bloqueada trabajos conservados=2
[4] Reseteo sobre la cuenta de administrador: rechazado RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO
[4] Tras el reseteo, el alumno pide su listado: rechazado CAMBIO_DE_CONTRASENA_PENDIENTE
Actos recorridos: 4 | Rechazos tipados: 8 | Excepciones: 0
```

**Las dos líneas de `[2]` que se ven parecidas son la lección del sample.** La primera —el detalle de un borrador pedido por el administrador— es la comprobación **4**, alcance, y por eso su motivo habla del alcance y no del papel. La segunda —el listado de la comisión pedido por un alumno— es la comprobación **3**, facultad, y **sí puede ser explícita**, porque no hay ningún recurso ajeno cuya existencia proteger: la negativa por facultad no oculta nada, y la negativa por pertenencia del ejemplo 02 sí. Es `ADR-04004` §2 en su párrafo de cierre.

**La línea del reseteo con la situación conservada es `RN-04012` y `RN-04015` juntas.** El reseteo se aplica sobre una cuenta **bloqueada** —no exige ni comprueba la situación— y no la cambia; y los trabajos siguen siendo dos. Lo único que cambia es la marca, que la última línea de `[4]` muestra en su efecto.

**`provisoria producida por el sistema: si` es `RN-04014` y `RN-04016` juntas.** La habilitación **produce** la contraseña provisoria y esta capa no la recibe de afuera; el mecanismo que la fabrica vive detrás del puerto, en otro proyecto de código.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Habilitar sin que el puerto produzca la provisoria | Hacer que el doble devuelva ausencia de provisoria en la habilitación | Rechazo `HABILITACION_SIN_CREDENCIAL_PROVISORIA`: la habilitación **no procede** sin ella (`RN-04016`) |
| Baja sin arrastre | Hacer que el doble del repositorio retire la cuenta y deje sus trabajos | Rechazo `BAJA_SIN_ARRASTRE_DE_TRABAJOS`; la unidad de trabajo es todo o nada (`ADR-04005`) |
| Desenlace desde `Borrador` | Invocar `CU-04008` sobre un trabajo que no está en `Pendiente` | Rechazo `DESENLACE_FUERA_DE_PENDIENTE`, distinto del rechazo por estado terminal |
| Alumno que pide el detalle de un trabajo propio en `Borrador` | Repetir la segunda línea de `[2]` con el dueño en lugar del administrador | Procede: el alcance del administrador excluye los borradores, y el del alumno no excluye los suyos |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00023`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Caso de uso | Habilita, bloquea, rehabilita y da de baja con confirmación escrita y arrastre |
| [`CU-00028`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Devuelve la entrega de la comisión con **0** borradores visibles y rechaza el pedido sin facultad |
| [`CU-00029`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | Caso de uso | Aprueba con comentario y rechaza sin comentario desde `Pendiente`, y propaga la terminalidad |
| [`CU-00024`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | Caso de uso | Resetea conservando situación y trabajos, y pone la marca |
| [`RN-02007`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | Regla de negocio | La baja exige el correo escrito y arrastra los **2** trabajos |
| [`RN-02010`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | Regla de negocio | El desenlace es exclusivo del administrador y los dos estados son terminales |
| [`RN-02011`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | Regla de negocio | **0** borradores visibles en el listado de la comisión |
| [`RN-02012`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | Regla de negocio | Situación conservada y **2** trabajos conservados tras el reseteo |
| [`RN-02014`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | Regla de negocio | La provisoria la produce el sistema y no llega de afuera |
| [`RN-02015`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Regla de negocio | El reseteo se aplica sobre una cuenta bloqueada |
| [`RN-02016`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Regla de negocio | Habilitar y rehabilitar producen la provisoria |
| [`ADR-04004`](../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) | Decisión arquitectónica | Las comprobaciones **3** y **4** con sus negativas distinguidas |
| [`ADR-04005`](../05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) | Decisión arquitectónica | La baja con arrastre es todo o nada dentro de una sola unidad de trabajo |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-04003
  verifica: [CU-04002, CU-04007, CU-04008, CU-04011, US-04004, US-04005, US-04006, US-04008, US-04020, US-04021, US-04022, US-04023, US-04024, US-04025, US-04027, US-04029, US-04031]
  comando: "dotnet run --project samples/application/03-avanzado"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "El sample reconstruye su propia semilla y no depende del estado que dejaron los ejemplos 01 y 02"
    - "El puerto de reloj se satisface con el doble de momento fijo del propio sample"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Baja con el correo escrito coincidente: dada de baja | trabajos arrastrados: 2"
      - "[2] Listado de la comision: 3 trabajos | borradores visibles: 0 (RN-04011)"
      - "[2] Detalle de un trabajo en Borrador pedido por el administrador: TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR"
      - "[2] Listado de la comision pedido por un alumno: rechazado ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR"
      - "[3] Desenlace sobre un trabajo ya Aprobado: rechazado TRANSICION_DESDE_ESTADO_TERMINAL"
      - "[4] Reseteo de un alumno bloqueado: aplicado | situacion conservada=Bloqueada trabajos conservados=2"
      - "Actos recorridos: 4 | Rechazos tipados: 8 | Excepciones: 0"
    stdout_no_contiene:
      - "borradores visibles: 1"
      - "[2] Detalle de un trabajo en Borrador pedido por el administrador: TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye dos aserciones negativas.** `borradores visibles: 1` no debe aparecer: `RN-04011` no admite gradación, y un solo borrador visible en la entrega de la comisión es la falla que este sample existe para detectar. Y el motivo del segundo caso **no puede** ser el de pertenencia: el trabajo en `Borrador` no es ajeno al administrador por dueño sino **fuera de su alcance**, y colapsar los dos motivos borraría la diferencia entre las comprobaciones 2 y 4 de `ADR-04004` §2.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-04002`, `CU-04007`, `CU-04008` y `CU-04011` con los **cuatro** puertos satisfechos por dobles y con semilla propia, y recorre las comprobaciones **3** y **4** de `ADR-04004` §2 con sus negativas distinguidas. El contrato `VER-04003` declara siete líneas exactas de salida y **dos aserciones negativas** —el borrador visible en la entrega de la comisión y el motivo que no puede colapsarse con el de pertenencia—; `evidencia` queda en `No verificado — sin código`. |
