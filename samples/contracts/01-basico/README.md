# Sample `contracts/01-basico` — La frontera de sesión y de cuentas: cuatro campos y ninguno que filtre

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Contracts/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-01`](../../../SDD/Docs/Proyectos/GeometriaFactory-Contracts/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/contracts/01-basico
```

---

## 1. Objetivo del sample

Demostrar cómo se arma y se lee la frontera de sesión y de cuentas de este ensamblado, y cómo se comprueba desde afuera lo que el contrato promete **por ausencia**: que la respuesta de sesión tiene exactamente cuatro campos, que ninguno transporta el hash de la contraseña ni la clave de firma, y que ninguna condición que impida operar viaja como campo de esa respuesta.

## 2. Prerequisites

- **.NET 10**; entorno de desarrollo contenido del repositorio; etapa `a` cerrada; Linux.
- **Sin servicio de datos levantado, y es deliberado**: el sample recorre la superficie pública del ensamblado y compone cuerpos. Ejercitar los tipos contra el servicio real es de la batería de integración, que vive en `GeometriaFactory-Api`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/contracts/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-01` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
