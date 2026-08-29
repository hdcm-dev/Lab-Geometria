# Ejemplo 01 — Ciclo de vida de una cuenta, de la configuración del administrador a la admisibilidad

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-01-basico.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/domain/01-basico/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-02001`, `CU-02002`, `CU-02003`, `CU-02004` y `CU-02012`; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §3, operaciones `OP-01` a `OP-04` y `OP-12`; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0 `TC-02001`, `TC-02002`, `TC-02003`, `TC-02006`, `TC-02009` y `TC-02010`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-02001` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el camino más corto que un consumidor recorre contra esta biblioteca: configurar la única cuenta de administrador, dar de alta un alumno, habilitarlo fijándole la credencial provisoria y preguntar si la cuenta admite acceso. Al terminar, quien lo ejecuta sabe cómo se invoca una operación del dominio, cómo llega el rechazo tipado cuando la operación no procede y por qué la admisibilidad es una puerta única y no una comprobación repartida.

## 2. Nivel

**Básico.** Es el punto de entrada absoluto del proyecto de código: no hay trabajo, ni interpretación, ni desenlace. Toca cinco de los **trece** casos de uso y dos de los **cinco** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1 —guardas de cuenta y evaluador de admisibilidad—, apoyados en el núcleo de entidades.

## 3. Prerequisites

| Ítem | Versión mínima | Origen |
| --- | --- | --- |
| Plataforma de ejecución de la solución de código | **.NET 10** | `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain y encabezado de la Parte C |
| Entorno de desarrollo contenido del repositorio | El del propio repositorio, `.devcontainer/` | `PRODUCT-INTAKE` §16; el host **no tiene** el SDK (`PRODUCT-INTAKE` §17.1, encabezado de la Parte C) |
| Etapa `a` del plan de entrega cerrada | — | Es la que crea la estructura de proyectos de código y los comandos de construcción de `scripts/` (`PRODUCT-INTAKE` §15) |
| Sistema operativo | Linux, el del entorno contenido | `PRODUCT-INTAKE` §17.1.P.9 · GeometriaFactory-Domain: `net10.0` sin sufijo de plataforma |

**Sin servicios externos.** El proyecto de código declara **0** dependencias salientes (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain), de modo que el sample no necesita base de datos, red ni credenciales.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/01-basico`.
4. Comparar la salida con §6.

Cuatro pasos, dentro del límite de cinco de `Rules-Examples.md` §4.2.

## 5. Estructura del código

```
samples/domain/01-basico/
├── README.md                     # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                 # Punto de entrada: los cuatro actos del recorrido
├── Recorrido/
│   ├── ActoConfigurarAdministrador.<ext>   # OP-12
│   ├── ActoAltaDeAlumno.<ext>              # OP-01
│   ├── ActoHabilitarConProvisoria.<ext>    # OP-02 y OP-03
│   └── ActoEvaluarAdmisibilidad.<ext>      # OP-04
└── tests/
    └── SalidaEsperada.<ext>      # Compara la salida contra el snapshot de §6
```

**Los nombres de tipos y de espacios de nombres no se fijan acá**: el intake los declara abiertos hasta el punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain) y [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §2 lo repite. El árbol de arriba nombra los archivos por el acto que ejecutan, no por el tipo que contienen.

## 6. Qué esperar

Salida esperada en consola, línea por línea. Es el snapshot contra el que compara el contrato de §9.

```
[1] Administrador configurado: papel=Administrador estado=Habilitado credencial=fijada
[1b] Segundo administrador rechazado: ADMINISTRATOR_ALREADY_CONFIGURED
[2] Alumno constituido: papel=Alumno estado=Pendiente credencial=sin-valor
[2b] Alta sin correo rechazada: REQUIRED_FIELD_MISSING
[3] Admisibilidad de la cuenta Pendiente: no-admisible motivos=ACCOUNT_PENDING
[4] Cuenta habilitada: estado=Habilitado credencial=fijada cambio-pendiente=puesta
[5] Admisibilidad tras habilitar: no-admisible motivos=PASSWORD_CHANGE_PENDING
[6] Credencial reemplazada por la propia cuenta: cambio-pendiente=levantada
[7] Admisibilidad final: admisible motivos=0
Operaciones invocadas: 9 | Rechazos tipados: 2 | Excepciones: 0
```

**Las tres líneas de admisibilidad son el punto del sample.** `[3]`, `[5]` y `[7]` muestran los tres desenlaces de `CU-02004` sobre la misma cuenta a medida que avanza su ciclo de vida, y muestran que el motivo `PASSWORD_CHANGE_PENDING` de `RN-02013` se levanta **sólo** con el cambio efectuado por la propia cuenta.

**La última línea también es contrato**: `Excepciones: 0` materializa [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), que reserva las excepciones a los defectos de programación del consumidor.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Habilitar sin aportar la provisoria derivada | Quitar el valor derivado del acto `[4]` | Rechazo `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` (`RN-02016`), y la cuenta queda `Pendiente` |
| Operar sobre la cuenta de administrador | Invocar habilitar, bloquear o resetear sobre la cuenta del acto `[1]` | Rechazo `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (`RN-02001`), la cuenta queda `Habilitado` |
| Bloquear y volver a preguntar | Bloquear la cuenta después del acto `[7]` | La admisibilidad pasa a no admisible con motivo `ACCOUNT_BLOCKED` (`RN-02006`), sin perder la credencial |
| Alta que aporta credencial derivada | Pasar un valor derivado en el acto `[2]` | Rechazo `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION`: la credencial se fija al habilitar y no antes |

Las cuatro variaciones son el puente hacia el ejemplo 02, donde el sujeto deja de ser la cuenta y pasa a ser el trabajo.

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00021`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | Caso de uso | Acto `[2]`: constituye el alumno con cuenta `Pendiente`, sin credencial y con papel `Alumno` |
| [`CU-00023`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Caso de uso | Acto `[4]`: habilita la cuenta; la variación de bloqueo recorre la transición inversa |
| [`CU-00022`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Caso de uso | Actos `[4]` y `[6]`: fija la provisoria y después la reemplaza exigiendo la vigente |
| [`CU-00022`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Caso de uso | Actos `[3]`, `[5]` y `[7]`: los tres desenlaces de la puerta única |
| [`CU-00025`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | Caso de uso | Actos `[1]` y `[1b]`: la ventana de alta del administrador y su cierre |
| [`RN-02001`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Regla de negocio | El rechazo de `[1b]` y la variación sobre la cuenta de administrador |
| [`RN-02006`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Regla de negocio | El motivo `ACCOUNT_PENDING` de `[3]` |
| [`RN-02013`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Regla de negocio | El motivo de `[5]` y su levantamiento en `[6]` |
| [`RN-02016`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Regla de negocio | Habilitar produce la provisoria: la variación sin ella se rechaza |
| [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | Decisión arquitectónica | La línea final con `Excepciones: 0` |
| [`ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) | Decisión arquitectónica | Las tres invocaciones de admisibilidad son la única puerta que el consumidor consulta |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-02001
  verifica: [CU-02001, CU-02002, CU-02003, CU-02004, CU-02012, US-02001, US-02004, US-02006, US-02024, US-02027]
  comando: "dotnet run --project samples/domain/01-basico"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada: existen la solución de código y los comandos de scripts/"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Sin servicios externos: el proyecto de código declara 0 dependencias salientes"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[3] Admisibilidad de la cuenta Pendiente: no-admisible motivos=ACCOUNT_PENDING"
      - "[5] Admisibilidad tras habilitar: no-admisible motivos=PASSWORD_CHANGE_PENDING"
      - "[7] Admisibilidad final: admisible motivos=0"
      - "Operaciones invocadas: 9 | Rechazos tipados: 2 | Excepciones: 0"
    stdout_no_contiene:
      - "Excepciones: 1"
  evidencia:
    estado: "VERIFICADO"
    fecha: "2026-08-29"
    corrida: "Fase I, incremento 1, dentro del entorno contenido con .NET 10"
    exit_code: 0
    criterios_cumplidos: 5
    criterios_incumplidos: 0
    stdout: |
      [1] Administrador configurado: papel=Administrador estado=Habilitado credencial=fijada
      [1b] Segundo administrador rechazado: ADMINISTRATOR_ALREADY_CONFIGURED
      [2] Alumno constituido: papel=Alumno estado=Pendiente credencial=sin-valor
      [2b] Alta sin correo rechazada: REQUIRED_FIELD_MISSING
      [3] Admisibilidad de la cuenta Pendiente: no-admisible motivos=ACCOUNT_PENDING
      [4] Cuenta habilitada: estado=Habilitado credencial=fijada cambio-pendiente=puesta
      [5] Admisibilidad tras habilitar: no-admisible motivos=PASSWORD_CHANGE_PENDING
      [6] Credencial reemplazada por la propia cuenta: cambio-pendiente=levantada
      [7] Admisibilidad final: admisible motivos=0
      Operaciones invocadas: 9 | Rechazos tipados: 2 | Excepciones: 0
    comparacion_contra_snapshot: "CONFORME · las 10 líneas coinciden, verificado por
      `dotnet run --project samples/domain/01-basico -- --verificar`, que sale 0"
```

**La salida de arriba es la corrida real y no una promesa**, que es lo que la pasada de ejecución de
`Rules-Examples.md` §0.2 convierte. El estado anterior de este campo era `No verificado — sin código`
y llevaba **dieciocho días** siéndolo con el código ya escrito.

**Y hay que decir cómo llegó a cumplirse, porque la primera corrida NO cumplió.** El 2026-08-27, al
implementarse el sample, **cuatro de los cinco `stdout_contiene` fallaron**: el sistema emitía
`ADMINISTRATOR_ALREADY_CONFIGURED` y este documento pedía `ADMINISTRADOR_YA_CONFIGURADO`. **No era un
defecto del sistema**: era el residuo del renombre `F-03` —«los 101 códigos de condición van a
inglés», decisión del Product Owner del **2026-08-12**— cuyos tramos documentales **se suspendieron el
2026-08-13**. El Product Owner lo reconfirmó el **2026-08-29** y §6 de este documento pasó a la forma
vigente, **con el mapeo leído del propio `ConditionCode.cs`** y no elegido acá. El sample **no se
ajustó al documento antes de esa confirmación**, y por eso quedó dos días declarando la divergencia en
lugar de taparla.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 2.0 | 2026-08-29 | **Pasada de ejecución — Fase I, incremento 1.** El sample está **implementado y corrido**, y el campo `evidencia` de §9 pasa de `No verificado — sin código` a **VERIFICADO** con la salida real, su fecha y su exit code. **§6 pasa a la forma vigente de los códigos de condición**: once ocurrencias en siete códigos, de castellano a inglés, con el mapeo **leído de `ConditionCode.cs`** y no elegido acá. Es el residuo del renombre **`F-03`** —decisión del Product Owner del **2026-08-12**, `Norma-De-Nomenclatura.md` §5.3— cuyos tramos documentales se suspendieron el 2026-08-13 y que el Product Owner **reconfirmó el 2026-08-29**. **Lo destapó el propio sample**: su primera corrida, el 2026-08-27, incumplió cuatro de los cinco `stdout_contiene`, y **el snapshot no se ajustó al código hasta que hubo decisión**, para no resolver en silencio una contradicción entre el sistema y veintiún documentos. **Sube MAJOR** porque el contenido de §6 y de §9 cambia para el consumidor del documento, no sólo su redacción. Ninguna otra sección se toca. | Orquestador de Fase I |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño** de `Rules-Examples.md` §0.2: el markdown explicativo queda completo y el contrato de verificación `VER-02001` declara `verifica`, `comando`, `precondiciones` y `criterio_aceptacion`, con `evidencia` en `No verificado — sin código`. Cubre `CU-02001`, `CU-02002`, `CU-02003`, `CU-02004` y `CU-02012` con las operaciones `OP-01` a `OP-04` y `OP-12` del contrato de uso del proyecto de código. El criterio de aceptación es exit code más cuatro líneas exactas de salida, sin prosa evaluable por una persona. |
