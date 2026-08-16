# Ejemplo 02 — Trabajo, listado y detalle: el texto original que viaja intacto y la proyección que no arrastra

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** `/samples/contracts/02-intermedio/`
**Trazabilidad upstream:** [`CU-08003`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md), [`CU-08004`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08004-Contrato-De-Listado-De-Trabajos.md) y [`CU-08005`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, familias de trabajo, de listado y de detalle; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.1 `TC-08007` a `TC-08010`, `TC-08012`, `TC-08013` y `TC-08014`; `PRODUCT-INTAKE` 1.22 §20, escenarios `E-1`, `E-2`, `E-3`, `E-4`, `E-6` y `E-7`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-08002` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar las tres familias que transportan el trabajo del alumno, con los datos reales que salen de su aplicación de escritorio: el envío con el texto original **como cadena y sin interpretar**, la proyección de listado que **no arrastra** ni el texto ni los componentes ni el comentario, y el detalle que sí los trae. Al terminar, quien lo ejecuta sabe por qué el listado y el detalle son dos contratos y no dos vistas del mismo.

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01. Agrega tres de las **ocho** familias —trabajo, listado y detalle— y **seis** de los **ocho** escenarios reales del `PRODUCT-INTAKE` §20 como cuerpos, transcriptos sin modificación.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3, y la misma condición: **sin servicio de datos levantado**. Un prerequisito de datos propio: los seis textos de escenario que el sample usa como cuerpo se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, incluido el de `E-2`, que **no es JSON estrictamente válido** —trae dos comas finales— y por eso es la mejor prueba de que el contrato transporta una cadena y no una estructura.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/contracts/02-intermedio`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/contracts/02-intermedio/
├── README.md                       # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                   # Los ocho actos, en orden
├── Escenarios/
│   ├── E1.txt  E2.txt  E3.txt      # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   ├── E4.txt  E6.txt  E7.txt      #   E2.txt NO es JSON estrictamente válido: es su gracia
│   └── Interpretacion.<ext>        # Compone el detalle interpretado de cada escenario
├── Inspeccion/
│   └── ProyeccionDeListado.<ext>   # Recorre la familia de listado y cuenta ausencias
└── tests/
    └── SalidaEsperada.<ext>        # Compara la salida contra el snapshot de §6
```

**Los archivos de escenario llevan extensión `.txt` y no `.json`, a propósito.** El de `E-2` no es JSON estrictamente válido, y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo, que es exactamente lo que rompería la comparación carácter por carácter del acto `[2]`.

## 6. Qué esperar

```
[1] Envio de E-2 compuesto: texto-original=cadena estructura-interpretada=no
[2] Texto original comparado caracter por caracter tras el ida y vuelta: identico=si
[3] Estado del resultado: pertenece al conjunto cerrado de 4 valores
[4] Solicitud de eliminacion: tipos distintos para alumno y administrador: 0
[5] Proyeccion de listado: ocurrencias de texto original=0 componentes de pieza=0 comentario=0
[6] Proyeccion de listado: campo de estado presente=si
[7] Detalle de E-1: piezas=3 | Detalle de E-7: piezas=6 | Detalle de E-6: piezas=1
[8] Observacion de E-3: severidad presente | declarado=36.00 | derivado=54.00 en campos separados
[8b] Observaciones de E-4: 0
[9] Comentario del administrador: campos compartidos con la coleccion de observaciones: 0
Familias recorridas: 3 de 8 | Escenarios usados como cuerpo: 6 de 8 | Recuentos en 0: 5
```

**El acto `[2]` es el corazón del sample.** `E-2` trae la clave `Tapas` donde el visualizador previo exigía `Bases`, y dos comas finales que lo dejan fuera del JSON estricto. Que vuelva **idéntico carácter por carácter** es lo que prueba que este ensamblado transporta y **no interpreta**, y es la forma en que `RN-08008` de `GeometriaFactory-Domain` cruza esta frontera.

**Los tres recuentos en 0 del acto `[5]`** son la razón de que `CU-08004` y `CU-08005` sean dos contratos separados: la proyección de listado no arrastra la carga del detalle. Es el NFR «Carga útil del listado» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, y su umbral viene rotulado **[ASUNCIÓN derivada]** de `PRODUCT-INTAKE` §17.4.P.10, de modo que el gate `QG-06` que lo mide es **condicionado** y no bloquea la fusión hasta que el Product Owner lo confirme.

**El acto `[8]` contrasta `E-3` con `E-4`**: son el **mismo cubo de lado 3** emitido por los dos ejemplos de la cátedra, y el contrato transporta **una** observación en el primero y **cero** en el segundo, con los dos valores en **campos separados** y no en un texto compuesto.

**El acto `[7]` incluye `E-6` a propósito**: su figura declara una dimensión en `0.00`, y el contrato **no la descarta**: la pieza viaja como cualquier otra. Descartar un dato del alumno es una decisión que ningún contrato de transporte puede tomar.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Reformatear el texto de `E-2` | Normalizar el JSON antes de componer el cuerpo | El acto `[2]` deja de dar `identico=si`: el ida y vuelta ya no conserva el texto |
| Agregar el comentario a la proyección de listado | Declarar el campo en la familia de listado | El acto `[5]` deja de dar 0 y el sample falla: es `QG-06` ejercido desde afuera del pipeline |
| Unir los dos valores de la observación en un texto | Reemplazar los dos campos por uno compuesto | El acto `[8]` deja de encontrar campos separados; el consumidor pierde la capacidad de mostrar el par |
| Componer el cuerpo de `E-5` | Agregar el escenario del tipo desconocido | El detalle trae la observación de error; su forma completa, con índice y campo, es del ejemplo 03 |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-08003`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08003-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) | Contrato de uso | Actos `[1]` a `[4]`: el envío con el texto crudo y la solicitud única de eliminación |
| [`CU-08004`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08004-Contrato-De-Listado-De-Trabajos.md) | Contrato de uso | Actos `[5]` y `[6]`: la proyección y sus tres ausencias |
| [`CU-08005`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md) | Contrato de uso | Actos `[7]` a `[9]`: piezas, componentes, observaciones y comentario |
| `RT-03` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 | Restricción transversal | El texto original como cadena no interpretada |
| `RT-04` de la misma sección | Restricción transversal | Los tres recuentos en 0 del acto `[5]` |
| `RT-09` de la misma sección | Restricción transversal | El acto `[9]`: el comentario es bloque propio y no comparte campos con las observaciones |
| [`ADR-08005`](../05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md) | Decisión arquitectónica | La separación entre los actos `[5]` y `[7]` |
| NFR «Carga útil del listado» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8 | Requisito no funcional | Acto `[5]`, con su rótulo [ASUNCIÓN derivada] declarado en §6 |
| `PRODUCT-INTAKE` §20 `E-1`, `E-2`, `E-3`, `E-4`, `E-6`, `E-7` | Escenario con payload real | Los seis textos, transcriptos sin modificación, son los cuerpos del recorrido |

**Qué queda deliberadamente fuera de este sample.** `US-08010`, el resumen por alumno y por estado, **no** se ilustra: deriva de una capacidad `Could Have` y su caso de prueba `TC-08011` está declarado **fuera del tramo comprometido** en [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md). Un sample que la ilustrara comprometería lo que la categoría 08 declaró no comprometido.

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-08002
  verifica: [CU-08003, CU-08004, CU-08005, US-08006, US-08007, US-08008, US-08011, US-08012, US-08013, US-08018, US-08019]
  comando: "dotnet run --project samples/contracts/02-intermedio"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Sin servicio de datos levantado"
    - "Los seis textos de Escenarios/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[2] Texto original comparado caracter por caracter tras el ida y vuelta: identico=si"
      - "[5] Proyeccion de listado: ocurrencias de texto original=0 componentes de pieza=0 comentario=0"
      - "[7] Detalle de E-1: piezas=3 | Detalle de E-7: piezas=6 | Detalle de E-6: piezas=1"
      - "[8] Observacion de E-3: severidad presente | declarado=36.00 | derivado=54.00 en campos separados"
      - "[8b] Observaciones de E-4: 0"
      - "Familias recorridas: 3 de 8 | Escenarios usados como cuerpo: 6 de 8 | Recuentos en 0: 5"
  evidencia:
    estado: "No verificado — sin código"
```

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-08003`, `CU-08004` y `CU-08005` sobre **tres** de las **ocho** familias, con **seis** de los **ocho** escenarios del `PRODUCT-INTAKE` §20 como cuerpos, transcriptos sin modificación. Declara por qué los archivos de escenario llevan extensión `.txt` y por qué `US-08010` queda deliberadamente fuera, por estar su caso de prueba fuera del tramo comprometido. El contrato `VER-08002` declara seis líneas exactas de salida; `evidencia` queda en `No verificado — sin código`. |
