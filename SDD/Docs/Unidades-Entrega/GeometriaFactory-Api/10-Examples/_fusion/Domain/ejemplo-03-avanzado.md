# Ejemplo 03 — Acceso, alcance del administrador y desenlace, con la superficie tipada bajo inspección

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** ejemplo-03-avanzado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/domain/03-avanzado/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-02009`, `CU-02010`, `CU-02011` y `CU-02013`; [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §3, operaciones `OP-09` a `OP-11` y `OP-13`, y §5; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../../../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0 `TC-02005`, `TC-02007`, `TC-02019` a `TC-02022`, `TC-02024`, `TC-02025` y `TC-02027`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-02003` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar las tres decisiones que el dominio toma sobre un trabajo ya enviado —quién accede a él, qué ve el administrador y cómo se cierra—, junto con la propiedad estructural que sostiene todo lo anterior: que **ninguna condición prevista viaja como excepción**. Al terminar, quien lo ejecuta sabe por qué un trabajo ajeno es indistinguible de uno inexistente, por qué el administrador no ve los borradores, y cómo se comprueba desde afuera que la superficie pública devuelve resultados tipados.

## 2. Nivel

**Avanzado.** Es el único de los tres que combina recorrido funcional con **inspección estructural** de la propia biblioteca: cuenta dependencias salientes, comprueba que ninguna operación obtiene el momento por su cuenta y provoca condiciones para verificar que llegan por valor de retorno. Supone hechos los ejemplos 01 y 02: usa sus cuentas y sus trabajos como punto de partida.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3. Un agregado propio:

| Ítem | Versión mínima | Motivo |
| --- | --- | --- |
| Acceso de lectura al archivo de proyecto de la biblioteca | — | El acto `[8]` cuenta sus referencias declaradas; sin ese archivo el recuento no se puede hacer |

**Sin fijar el reloj del entorno.** El acto `[9]` corre dos veces seguidas sin tocar la hora del sistema, y compara los dos resultados. Fijar el reloj invalidaría la comprobación.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/03-avanzado`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/domain/03-avanzado/
├── README.md                         # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                     # Los diez actos, en orden
├── Recorrido/
│   ├── ActoResolverAcceso.<ext>            # OP-09
│   ├── ActoAlcanceDelAdministrador.<ext>   # OP-11
│   ├── ActoDesenlace.<ext>                 # OP-10
│   └── ActoResetear.<ext>                  # OP-13
├── Inspeccion/
│   ├── DependenciasSalientes.<ext>   # Cuenta las referencias del archivo de proyecto
│   ├── SinRelojNiConjunto.<ext>      # Dos corridas consecutivas, resultado comparado
│   └── CondicionesTipadas.<ext>      # Provoca condiciones y cuenta excepciones
└── tests/
    └── SalidaEsperada.<ext>          # Compara la salida contra el snapshot de §6
```

## 6. Qué esperar

```
[1] Trabajo ajeno: TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE
[2] Trabajo inexistente: TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE
[3] Resultados [1] y [2] comparados campo por campo: identicos=si
[4] Alcance del administrador: en-alcance=3 fuera-de-alcance=1 (Borrador)
[5] Eliminacion por el administrador admitida en: Pendiente, Finalizado, Rechazado
[6] Aprobar trabajo en Pendiente: estado=Finalizado comentario=ausente
[7] Rechazar trabajo en Pendiente: estado=Rechazado comentario=presente
[7b] Desenlace sobre estado terminal: TRANSICION_DESDE_ESTADO_TERMINAL
[7c] Desenlace sin papel de administrador: DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR
[8] Reseteo: estado-de-cuenta=sin-cambio trabajos-antes=4 trabajos-despues=4
[9] Dependencias salientes declaradas: 0 | Bibliotecas de persistencia o transporte: 0
[10] Dos corridas consecutivas sin fijar el reloj: resultado-identico=si
[11] Condiciones provocadas: 12 | Devueltas por valor: 12 | Excepciones de negocio: 0
```

**Las líneas `[1]`, `[2]` y `[3]` juntas son `RN-02003`.** No alcanza con que los dos casos devuelvan el mismo código: el sample los compara **campo por campo**, porque un resultado que trajera un dato distinto en cualquier otro campo volvería distinguible el trabajo ajeno del inexistente y vaciaría la regla.

**La línea `[11]` es la que convierte al sample en arnés.** `Excepciones de negocio: 0` sobre **12** condiciones provocadas materializa [`ADR-02002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); el recuento no cubre las **42** condiciones del catálogo de `03`, que es alcance de `TC-02023` y de la batería de `tests/`, no de un sample.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Dar de baja arrastrando los trabajos | Invocar la baja con la confirmación escrita coincidente sobre la cuenta del acto `[8]` | La cuenta y sus **cuatro** trabajos se materializan como una sola unidad (`RN-02007`); con la confirmación no coincidente, rechazo |
| Baja declarando que los trabajos se conservan | Pedir la baja sin arrastre | Rechazo `BAJA_SIN_ARRASTRE_DE_TRABAJOS`: no hay baja parcial |
| Consultar un trabajo propio fuera de `Borrador` | Pedir reeditar y eliminar sobre el trabajo en `Pendiente` | `OPERACION_FUERA_DE_BORRADOR` en los dos; **ver** procede en los cuatro estados |
| Agregar una referencia al archivo de proyecto | Declarar una referencia a otro proyecto de código del producto | El acto `[9]` deja de dar 0 y el sample falla: es la puerta `QG-04` ejercida desde afuera del pipeline |

La última variación es el puente hacia `09-Devops`: muestra que el gate de dependencias salientes no depende del pipeline para detectarse.

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00028`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Actos `[1]` a `[3]`: el ajeno y el inexistente con resultado idéntico |
| [`CU-00029`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | Caso de uso | Actos `[6]`, `[7]`, `[7b]` y `[7c]`: los dos desenlaces y sus dos rechazos |
| [`CU-00028`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Actos `[4]` y `[5]`: el borrador excluido y la eliminación en los tres estados que ve |
| [`CU-00024`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | Caso de uso | Acto `[8]`: la cuenta conserva su situación y sus cuatro trabajos |
| [`RN-02003`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Regla de negocio | La comparación campo por campo de `[3]` |
| [`RN-02010`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | Regla de negocio | `[7b]` y `[7c]`: exclusividad y terminalidad |
| [`RN-02011`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | Regla de negocio | El `fuera-de-alcance=1 (Borrador)` de `[4]` |
| [`RN-02012`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | Regla de negocio | El recuento de trabajos antes y después de `[8]` |
| [`ADR-02002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | Decisión arquitectónica | Acto `[11]` |
| [`ADR-02006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) | Decisión arquitectónica | Acto `[10]`: dos corridas consecutivas sin fijar el reloj |
| NFR «Dependencias salientes» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8 | Requisito no funcional | Acto `[9]`, con los dos recuentos en 0 |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-02003
  verifica: [CU-02009, CU-02010, CU-02011, CU-02013, US-02018, US-02019, US-02020, US-02021, US-02022, US-02023, US-02026]
  comando: "dotnet run --project samples/domain/03-avanzado"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "El archivo de proyecto de la biblioteca es legible desde el sample"
    - "El reloj del entorno NO se fija: el acto [10] compara dos corridas consecutivas"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[3] Resultados [1] y [2] comparados campo por campo: identicos=si"
      - "[4] Alcance del administrador: en-alcance=3 fuera-de-alcance=1 (Borrador)"
      - "[8] Reseteo: estado-de-cuenta=sin-cambio trabajos-antes=4 trabajos-despues=4"
      - "[9] Dependencias salientes declaradas: 0 | Bibliotecas de persistencia o transporte: 0"
      - "[10] Dos corridas consecutivas sin fijar el reloj: resultado-identico=si"
      - "[11] Condiciones provocadas: 12 | Devueltas por valor: 12 | Excepciones de negocio: 0"
  evidencia:
    estado: "No verificado — sin código"
```

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-02009`, `CU-02010`, `CU-02011` y `CU-02013` con las operaciones `OP-09` a `OP-11` y `OP-13`, y agrega tres inspecciones estructurales —dependencias salientes, ausencia de lectura de reloj y condiciones tipadas— que ejercen desde afuera del pipeline los gates `QG-04` y `QG-08`. El contrato `VER-02003` declara seis líneas exactas de salida; `evidencia` queda en `No verificado — sin código`. |
