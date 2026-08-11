# Ejemplo 01 — La frontera de sesión y de cuentas: cuatro campos y ninguno que filtre

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ejemplo-01-basico.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/contracts/01-basico/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md`](../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) y [`CU-02`](../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, familias de sesión y de cuentas, y §3.2; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.1 `TC-01` a `TC-06`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-01` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar cómo se arma y se lee la frontera de sesión y de cuentas de este ensamblado, y cómo se comprueba desde afuera lo que el contrato promete **por ausencia**: que la respuesta de sesión tiene exactamente cuatro campos, que ninguno transporta el hash de la contraseña ni la clave de firma, y que ninguna condición que impida operar viaja como campo de esa respuesta. Al terminar, quien lo ejecuta sabe recorrer la superficie pública de una familia de tipos y contar lo que no está.

## 2. Nivel

**Básico.** Toca dos de las **ocho** familias de tipos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 —sesión y cuentas—, y usa la familia de error sólo como destinatario de los rechazos, sin abrirla: eso es del ejemplo 03. No usa ningún escenario del intake §20, porque ninguno de los ocho es un dato de cuenta.

## 3. Prerequisites

| Ítem | Versión mínima | Origen |
| --- | --- | --- |
| Plataforma de ejecución de la solución de código | **.NET 10** | `PRODUCT-INTAKE` §17.4.P.1 y encabezado de la Parte C |
| Entorno de desarrollo contenido del repositorio | El del propio repositorio, `.devcontainer/` | `PRODUCT-INTAKE` §16; el host no tiene la plataforma |
| Etapa `a` del plan de entrega cerrada | — | Crea la estructura de proyectos de código y los comandos de construcción de `scripts/` (`PRODUCT-INTAKE` §15) |
| Sistema operativo | Linux | `PRODUCT-INTAKE` §17.4.P.9 |

**Sin servicio levantado, y es deliberado.** El sample **no golpea** el servicio de datos: recorre la superficie pública del ensamblado y compone cuerpos. Ejercitar los tipos contra el servicio real es de la batería de integración, que **no vive en este proyecto de código** sino en `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.4.P.6). El sample es lo que se puede correr **antes** de que ese proyecto de código exista.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/contracts/01-basico`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/contracts/01-basico/
├── README.md                      # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                  # Los cinco actos, en orden
├── Cuerpos/
│   ├── canje.json                 # Cuerpo del canje de credenciales
│   ├── registro.json              # Cuerpo del registro de una cuenta de alumno
│   └── baja.json                  # Cuerpo de la baja con su confirmación escrita
├── Inspeccion/
│   └── RecuentoDeCampos.<ext>     # Recorre la superficie de las dos familias y cuenta
└── tests/
    └── SalidaEsperada.<ext>       # Compara la salida contra el snapshot de §6
```

**`RecuentoDeCampos.<ext>` es la mitad del sample.** La forma característica de aserción de este proyecto de código es el **recuento** —cero campos, cuatro campos— y no la comparación de un valor de negocio ([`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §4). El sample la enseña ejerciéndola.

## 6. Qué esperar

```
[1] Respuesta de sesion: campos=4
[2] Campos que transportan el hash de la contrasena o la clave de firma: 0
[3] Campos que transportan una condicion que impide operar: 0
[4] Registro de cuenta compuesto: campos-obligatorios=presentes
[5] Listado de cuentas: campos que transportan alguna forma de la contrasena almacenada: 0
[6] Cambio de situacion: habilitar devuelve la provisoria producida
[7] Baja: la solicitud lleva su confirmacion escrita como campo propio
[8] Tipos de establecimiento anonimo de contrasena en la superficie: 0
Familias recorridas: 2 de 8 | Recuentos en 0: 4 | Referencias hacia el dominio: 0
```

**Las cuatro líneas que terminan en 0 son el sample.** Un tipo de transferencia no se prueba viendo qué transporta, sino comprobando qué **no** puede transportar: es la regla de exposición de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.2, registrada en [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md).

**La línea `[6]` es la consecuencia de `RN-16` sobre este contrato**: habilitar **produce** la provisoria, de modo que el resultado la devuelve. Y la línea `[8]` es su contracara: `RN-16` eliminó todo tipo de establecimiento anónimo de contraseña, y el recuento en 0 es lo que impide que vuelva por la puerta de atrás.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Agregar un campo a la respuesta de sesión | Declarar un quinto campo en la familia de sesión | El acto `[1]` deja de dar 4 y el sample falla: es `QG-07` ejercido desde afuera del pipeline |
| Registro sin un campo obligatorio | Quitar el nombre de `registro.json` | El cuerpo deja de tener sus campos obligatorios presentes y el acto `[4]` lo señala; contra el servicio real, el contrato de uso declara `CONTRATO_CAMPO_REQUERIDO_AUSENTE` con **un** detalle con el campo señalado |
| Baja sin su confirmación escrita | Quitar el campo de confirmación de `baja.json` | El acto `[7]` deja de encontrarlo. La confirmación escrita es una barrera deliberada, porque la baja arrastra los trabajos |
| Correr el mismo cuerpo contra el servicio real | Reusar `Cuerpos/` como fixture de la batería de integración de `GeometriaFactory-Api` | Es el puente hacia el sample **S-2** del `PRODUCT-INTAKE` §18, que golpea los endpoints reales con estos mismos tipos |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-01`](../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) | Contrato de uso | Actos `[1]` a `[3]`: la respuesta de sesión con sus cuatro campos y sus dos ausencias |
| [`CU-02`](../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) | Contrato de uso | Actos `[4]` a `[8]`: registro, listado, cambio de situación y baja |
| `RT-01` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 | Restricción transversal | Los recuentos en 0 de `[2]` y `[5]` |
| `RT-10` de la misma sección | Restricción transversal | El recuento en 0 de `[3]`: las condiciones que impiden operar viajan como error |
| [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) | Decisión arquitectónica | Los cuerpos se componen sin invocar comportamiento: los tipos son planos |
| [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md) | Decisión arquitectónica | Las cuatro líneas que terminan en 0 |
| NFR «Campos de la respuesta de sesión» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8 | Requisito no funcional | El acto `[1]` |
| `RN-16` de `GeometriaFactory-Domain` | Regla de negocio del proyecto de código vecino | Actos `[6]` y `[8]` |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-01
  verifica: [CU-01, CU-02, US-01, US-02, US-03, US-04, US-05]
  comando: "dotnet run --project samples/contracts/01-basico"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Sin servicio de datos levantado: el sample recorre la superficie pública y no golpea el servicio real"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Respuesta de sesion: campos=4"
      - "[2] Campos que transportan el hash de la contrasena o la clave de firma: 0"
      - "[3] Campos que transportan una condicion que impide operar: 0"
      - "[8] Tipos de establecimiento anonimo de contrasena en la superficie: 0"
      - "Familias recorridas: 2 de 8 | Recuentos en 0: 4 | Referencias hacia el dominio: 0"
  evidencia:
    estado: "No verificado — sin código"
```

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño** de `Rules-Examples.md` §0.2. Cubre `CU-01` y `CU-02` sobre **dos** de las **ocho** familias de tipos, con la forma de aserción por recuento que la categoría 08 declara como característica del proyecto de código. Declara que el sample **no golpea** el servicio de datos, porque la batería de integración vive en `GeometriaFactory-Api` y no acá. El contrato `VER-01` declara cinco líneas exactas de salida; `evidencia` queda en `No verificado — sin código`. |
