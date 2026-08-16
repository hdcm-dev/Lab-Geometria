# Ejemplo 01 — La cuenta entra al laboratorio: alta, administrador, credencial y la guarda que corta primero

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** ejemplo-01-basico.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/application/01-basico/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-04001`, `CU-04003` y `CU-04010`; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §3.1, componentes de alta de cuentas y de ingreso y credencial; [`../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) §2
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../../08-Calidad-Y-Pruebas/_fusion/Application/Matriz-Sensado-Deriva.md), que toma `VER-04001` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el camino de entrada al laboratorio tal como esta capa lo orquesta: constituir la cuenta de un alumno sin credencial y en situación pendiente, configurar la única cuenta de administrador mientras no exista ninguna, resolver la admisibilidad de un ingreso con su motivo, y reemplazar la credencial propia. Al terminar, quien lo ejecuta sabe **por qué la comprobación de cambio de contraseña pendiente corta antes que las otras tres** y cuál es su única excepción.

## 2. Nivel

**Básico.** Punto de entrada absoluto del proyecto de código. No supone ningún otro sample, no usa ningún escenario del intake §20 —esta parte de la capa no toca el texto del alumno— y necesita **dos** de los cuatro puertos con doble: el de repositorio de cuentas y el del reloj del sistema.

## 3. Prerequisites

- **.NET 10**, la plataforma que el `PRODUCT-INTAKE` declara para los seis proyectos de código de la solución de código (encabezado de la Parte C).
- **Entorno de desarrollo contenido del repositorio.** Todo el ciclo ocurre adentro, porque el host no tiene el SDK.
- **Etapa `a` del plan de entrega cerrada**, que es donde quedan anclados el tooling y los nombres de tipo.
- **Linux**, que es la plataforma del entorno contenido.

Sin servicios externos, sin base de datos y sin frontera de proceso: los **cuatro** puertos de [`ADR-04002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) se satisfacen con dobles que viven dentro del sample.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/01-basico`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/application/01-basico/
├── README.md                         # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                     # Recorre los cuatro actos en orden
├── Dobles/
│   ├── RepositorioDeCuentasEnMemoria.<ext>   # Doble del puerto de repositorio de cuentas
│   └── RelojFijo.<ext>                       # Doble del puerto de reloj: momento fijo y declarado
├── Actos/
│   ├── ActoAltaDeAlumno.<ext>        # CU-04001
│   ├── ActoAltaDeAdministrador.<ext> # CU-04010
│   ├── ActoAdmisibilidad.<ext>       # CU-04003, consulta con motivo
│   └── ActoCambioDeCredencial.<ext>  # CU-04003, reemplazo por la propia cuenta
└── tests/
    └── SalidaEsperada.<ext>          # Compara la salida contra el snapshot de §6
```

**El reloj es un doble y no el reloj de la máquina, y eso es lo que hace comparable la salida.** El dominio no lee el reloj: lo recibe por parámetro ([`Domain ADR-02006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)), y esta capa es quien lo aporta por el puerto. Un sample que leyera el reloj real produciría una salida distinta en cada corrida y su criterio de aceptación dejaría de ser comparable.

## 6. Qué esperar

```
[1] Alta de alumno: constituida situacion=Pendiente credencial=ausente
[1] Alta repetida con el mismo correo: rechazada CORREO_YA_REGISTRADO
[2] Alta de administrador: constituida situacion=Habilitada papel=Administrador
[2] Segundo administrador: rechazado ADMINISTRADOR_YA_CONFIGURADO
[3] Admisibilidad de la cuenta pendiente: no admisible motivo=CUENTA_PENDIENTE
[3] Admisibilidad de la cuenta habilitada con marca: no admisible motivo=CAMBIO_DE_CONTRASENA_PENDIENTE
[3] Admisibilidad de la cuenta habilitada sin marca: admisible
[4] Cuenta marcada pide listar sus trabajos: rechazado CAMBIO_DE_CONTRASENA_PENDIENTE
[4] Cuenta marcada reemplaza su credencial: aceptado (unica excepcion de ADR-04004)
[4] Marca levantada por la propia cuenta: la misma peticion de listado ahora procede
[4] Reemplazo sin presentar la vigente: rechazado CREDENCIAL_VIGENTE_NO_VERIFICADA
Actos recorridos: 4 | Rechazos tipados: 5 | Excepciones: 0
```

**Las tres líneas del acto `[4]` son el corazón del sample.** Muestran, en el mismo recorrido, la comprobación que corta primero, su **única** excepción declarada —el reemplazo de la propia credencial— y el hecho de que **es ese reemplazo, hecho por la propia cuenta, lo único que levanta la marca**. Es `ADR-04004` §2 punto 1, visto de punta a punta.

**`Rechazos tipados: 5` y `Excepciones: 0` juntos no son adorno.** Toda negativa prevista viaja como resultado tipado y ninguna como excepción, que es lo que [`ADR-04006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) decide y lo que el nivel 0 compró antes.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Alta de alumno con credencial | Pasar una credencial en el alta de `CU-04001` | Rechazo `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`: el alta de alumno **prohíbe** la credencial, y el de administrador la exige |
| Administrador con el almacén ya poblado | Invocar `CU-04010` con una cuenta de administrador ya existente | `ADMINISTRADOR_YA_CONFIGURADO`, sin camino alternativo ofrecido |
| Saltear la guarda | Invocar el orquestador de consulta sin pasar por la guarda de autorización | La cuenta marcada alcanza una capacidad que `INV-09` prohíbe. Es la dependencia de disciplina que `ADR-04004` §6 declara que el compilador no garantiza |
| Reloj real en vez de doble | Reemplazar `RelojFijo` por el reloj del sistema | La salida deja de ser comparable entre corridas y el criterio de aceptación falla, aunque el comportamiento sea correcto |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-04001`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-04001-Registrar-El-Alta-De-Una-Cuenta.md) | Caso de uso | Constituye la cuenta de alumno pendiente y sin credencial, y recorre el rechazo por correo ya registrado |
| [`CU-04003`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md) | Caso de uso | Resuelve la admisibilidad con su motivo en los tres desenlaces y reemplaza la credencial exigiendo la vigente |
| [`CU-04010`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) | Caso de uso | Configura el administrador mientras no exista ninguno y rechaza el segundo |
| [`RN-02001`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Regla de negocio | El segundo administrador se rechaza |
| [`RN-02002`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | Regla de negocio | El alta repetida con el mismo correo se rechaza |
| [`RN-02006`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Regla de negocio | La cuenta pendiente no es admisible, y el motivo se devuelve |
| [`RN-02013`](../../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Regla de negocio | La cuenta marcada queda confinada al reemplazo de su credencial |
| [`ADR-04004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) | Decisión arquitectónica | El acto `[4]` recorre la comprobación que corta primero y su única excepción |
| [`ADR-04006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Decisión arquitectónica | Los **5** rechazos viajan como resultado tipado y **0** como excepción |
| [`ADR-04002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) | Decisión arquitectónica | Los dos puertos que el sample necesita se satisfacen con dobles, sin base de datos |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-04001
  verifica: [CU-04001, CU-04003, CU-04010, US-04001, US-04002, US-04003, US-04007, US-04009, US-04028, US-04030, US-04032]
  comando: "dotnet run --project samples/application/01-basico"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "El puerto de reloj se satisface con el doble de momento fijo del propio sample, no con el reloj de la máquina"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Alta de alumno: constituida situacion=Pendiente credencial=ausente"
      - "[2] Segundo administrador: rechazado ADMINISTRADOR_YA_CONFIGURADO"
      - "[3] Admisibilidad de la cuenta habilitada con marca: no admisible motivo=CAMBIO_DE_CONTRASENA_PENDIENTE"
      - "[4] Cuenta marcada reemplaza su credencial: aceptado (unica excepcion de ADR-04004)"
      - "[4] Marca levantada por la propia cuenta: la misma peticion de listado ahora procede"
      - "Actos recorridos: 4 | Rechazos tipados: 5 | Excepciones: 0"
    stdout_no_contiene:
      - "credencial=presente"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye una aserción negativa.** `credencial=presente` no debe aparecer en el alta de alumno: `CU-04001` prohíbe la credencial en ese camino, y una implementación que la aceptara y la ignorara en silencio pasaría todas las aserciones positivas.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-04001`, `CU-04003` y `CU-04010` con **dos** de los cuatro puertos satisfechos por dobles, y recorre la comprobación de cambio de contraseña pendiente con su **única** excepción declarada en `ADR-04004` §2. El contrato `VER-04001` declara seis líneas exactas de salida y **una aserción negativa** sobre la credencial en el alta de alumno; `evidencia` queda en `No verificado — sin código`. |
