# Ejemplo 03 — Error, desenlace y reseteo: quince códigos vivos y una frontera que no filtra

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ejemplo-03-avanzado.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/contracts/03-avanzado/`
**Trazabilidad upstream:** [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md), [`CU-07`](../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Contrato-De-Desenlace-De-La-Revision.md) y [`CU-08`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, familias de error, de desenlace y de reseteo, y §3.2; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3.2, la única tabla del proyecto de código donde los **dieciocho** identificadores emitidos están enumerados juntos; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.1 `TC-15` a `TC-22`; `PRODUCT-INTAKE` 1.22 §20, escenarios `E-5` y `E-8`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-03` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar las tres familias que cierran el contrato —error, desenlace y reseteo— y las dos inspecciones que sostienen la frontera entera: que el conjunto cerrado de códigos tiene **quince vivos** sobre **dieciocho** emitidos, con **tres** retirados que no se reciclan, y que **ningún** tipo del ensamblado habilita al navegador a invocar el servicio de datos. Al terminar, quien lo ejecuta sabe comprobar un conjunto cerrado en las dos direcciones y verificar una regla de arquitectura sobre una superficie de tipos.

## 2. Nivel

**Avanzado.** Es el único de los tres que ejerce las **tres** reglas de arquitectura del producto sobre este ensamblado, y el único que compara un conjunto cerrado contra su fuente en las dos direcciones. Supone hechos los ejemplos 01 y 02: reusa sus cuerpos como material.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3, y un agregado:

| Ítem | Versión mínima | Motivo |
| --- | --- | --- |
| Acceso de lectura al archivo de proyecto del ensamblado | — | El acto `[9]` cuenta sus referencias hacia `GeometriaFactory-Domain`; sin ese archivo el recuento no se puede hacer |

**Sin servicio de datos levantado**, igual que los dos anteriores. El sample compone la respuesta de error a partir de la superficie del tipo, no de una respuesta obtenida por la red.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/contracts/03-avanzado`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/contracts/03-avanzado/
├── README.md                        # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                    # Los diez actos, en orden
├── Escenarios/
│   ├── E5.txt  E8.txt               # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── Ubicacion.<ext>              # Compone el detalle de ubicación de cada uno
├── Inspeccion/
│   ├── ConjuntoCerrado.<ext>        # Compara los códigos vivos contra 03 §3.2, en las dos direcciones
│   ├── FronteraQueNoFiltra.<ext>    # Cuenta campos capaces de transportar dirección, ruta o secreto
│   └── ReferenciasSalientes.<ext>   # Cuenta referencias hacia GeometriaFactory-Domain
└── tests/
    └── SalidaEsperada.<ext>         # Compara la salida contra el snapshot de §6
```

## 6. Qué esperar

```
[1] Tipo de error: campos=4 (codigo, texto, detalles, momento)
[2] Campos capaces de transportar una direccion de servicio, una ruta de datos o un secreto: 0
[3] Conjunto cerrado: codigos vivos=15 | emitidos=18 | retirados=3 | reciclados=0
[4] Codigos cuya causa es una cuenta habilitada sin contrasena, o un reseteo sobre una cuenta sin contrasena: 0
[5] Detalle de ubicacion de E-5: indice-figura=1 campo=Tipo
[6] Detalle de ubicacion de E-8: indice-figura=1 campo=Largo
[7] Desenlace: valores del conjunto cerrado=2 | comentario opcional en los dos=si
[8] Campos o valores que permiten salir de Finalizado o de Rechazado: 0
[9] Solicitud de reseteo: campos=1 | campos de contrasena=0
[9b] Solicitud de cambio obligatorio: tipos nuevos respecto del cambio voluntario: 0
[10] Referencias hacia GeometriaFactory-Domain: 0
[11] Tipos que habilitan al navegador a armar la solicitud: 0
Familias recorridas: 3 de 8 | Total de familias cubiertas por los tres samples: 8 de 8 | Recuentos en 0: 7
```

**El acto `[3]` es el sample.** Un conjunto cerrado no se verifica leyéndolo: se verifica **en las dos direcciones**, comprobando que no falta ninguno de los quince vivos y que no aparece ninguno que no esté. Los **tres** retirados no se reciclan, y su reaparición es una falla, no un empate.

**El acto `[4]` es la consecuencia de `RN-16` sobre este contrato.** Esa regla volvió imposibles dos causas —una cuenta habilitada sin contraseña, y un reseteo sobre una cuenta sin contraseña—, y el recuento en 0 es lo que impide que un código para ellas vuelva a nacer.

**Los actos `[5]` y `[6]` usan `E-5` y `E-8`, y el índice es 1 en los dos.** El primer elemento de `E-5` es válido a propósito: que el índice reportado sea **1 y no 0** es lo que prueba que la ubicación se calcula y no se informa siempre la primera figura (`PRODUCT-INTAKE` §20.E-5, punto 2). En `E-8`, el intake resuelve la dimensión no legible como **error y no como advertencia**, de modo que el estado que el contrato transporta es `Borrador`.

**Los actos `[2]`, `[10]` y `[11]` son las tres reglas de arquitectura del producto sobre esta superficie**: `RA-03` en `[2]`, la regla de dependencias en `[10]` y `RA-01` en `[11]`. La verificación estructural completa de `RA-01` pertenece a `05` y a `09`; lo que este sample comprueba es que **ningún tipo la presuponga**.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Agregar un código al conjunto | Declarar un decimosexto código vivo | El acto `[3]` deja de dar 15 y el sample falla: es `QG-04` ejercido desde afuera del pipeline |
| Reciclar un identificador retirado | Reusar uno de los tres retirados para otra condición | El acto `[3]` deja de dar `reciclados=0`. Un identificador retirado no vuelve a nombrar otra condición |
| Agregar un campo de traza al tipo de error | Declarar un quinto campo con la traza de la implementación | El acto `[1]` deja de dar 4 y el `[2]` deja de dar 0: la frontera empieza a filtrar |
| Declarar una referencia hacia el dominio | Agregarla al archivo de proyecto | El acto `[10]` deja de dar 0. Es la vía por la que el intake declara que el acoplamiento vuelve, y se rechaza en revisión |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) | Contrato de uso | Actos `[1]` a `[6]`: el tipo único de error, su conjunto cerrado y su detalle de ubicación |
| [`CU-07`](../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Contrato-De-Desenlace-De-La-Revision.md) | Contrato de uso | Actos `[7]` y `[8]`: los dos valores y la ausencia de vuelta atrás |
| [`CU-08`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) | Contrato de uso | Actos `[9]` y `[9b]`: la solicitud de un solo campo y la reutilización del tipo de cambio |
| `RT-02` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 | Restricción transversal | El detalle de ubicación con índice y campo de `[5]` y `[6]` |
| `RT-05` de la misma sección | Restricción transversal | El acto `[10]` |
| `RT-11` de la misma sección | Restricción transversal | El acto `[11]` |
| [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) | Decisión arquitectónica | Actos `[1]`, `[3]` y `[4]` |
| [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) | Decisión arquitectónica | El acto `[10]`: los dos extremos se compilan contra el mismo ensamblado |
| **RA-01** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Acto `[11]`: ningún tipo habilita al navegador a armar la solicitud |
| **RA-03** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Acto `[2]`: ningún campo transporta una dirección de servicio interno |
| `PRODUCT-INTAKE` §20 `E-5` y `E-8` | Escenario con payload real | Los dos textos, transcriptos sin modificación, producen los detalles de ubicación de `[5]` y `[6]` |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-03
  verifica: [CU-06, CU-07, CU-08, US-14, US-15, US-16, US-17, US-21, US-22]
  comando: "dotnet run --project samples/contracts/03-avanzado"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Sin servicio de datos levantado"
    - "El archivo de proyecto del ensamblado es legible desde el sample"
    - "Los textos de E-5 y E-8 transcriptos del PRODUCT-INTAKE §20 sin modificación"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Tipo de error: campos=4 (codigo, texto, detalles, momento)"
      - "[3] Conjunto cerrado: codigos vivos=15 | emitidos=18 | retirados=3 | reciclados=0"
      - "[5] Detalle de ubicacion de E-5: indice-figura=1 campo=Tipo"
      - "[8] Campos o valores que permiten salir de Finalizado o de Rechazado: 0"
      - "[10] Referencias hacia GeometriaFactory-Domain: 0"
      - "[11] Tipos que habilitan al navegador a armar la solicitud: 0"
    stdout_no_contiene:
      - "indice-figura=0 campo=Tipo"
  evidencia:
    estado: "No verificado — sin código"
```

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-06`, `CU-07` y `CU-08` sobre las **tres** familias restantes, con lo que los tres samples completan **8 de 8** familias de tipos. Verifica el conjunto cerrado en las dos direcciones —**15** vivos sobre **18** emitidos, **3** retirados sin reciclar— y las reglas de arquitectura **RA-01** y **RA-03** sobre la superficie del ensamblado. El contrato `VER-03` declara seis líneas exactas de salida y **una aserción negativa** sobre el índice reportado; `evidencia` queda en `No verificado — sin código`. |
