# Sample `domain/01-basico` — Ciclo de vida de una cuenta, de la configuración del administrador a la admisibilidad

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Domain/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-01`](../../../SDD/Docs/Proyectos/GeometriaFactory-Domain/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/domain/01-basico
```

---

## 1. Objetivo del sample

Demostrar el camino más corto que un consumidor recorre contra esta biblioteca: configurar la única cuenta de administrador, dar de alta un alumno, habilitarlo fijándole la credencial provisoria y preguntar si la cuenta admite acceso. Al terminar, quien lo ejecuta sabe cómo se invoca una operación del dominio, cómo llega el rechazo tipado cuando la operación no procede y por qué la admisibilidad es una puerta única y no una comprobación repartida.

## 2. Prerequisites

- **.NET 10**, la plataforma que el `PRODUCT-INTAKE` declara para los seis proyectos de código de la solución de código.
- **Entorno de desarrollo contenido del repositorio**: el host no tiene el SDK.
- **Etapa `a` del plan de entrega cerrada.**
- **Linux**, la plataforma del entorno contenido.
- **Sin servicios externos**: el proyecto de código declara **0** dependencias salientes.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-01` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
