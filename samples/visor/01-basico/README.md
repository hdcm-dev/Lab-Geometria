# Sample `visor/01-basico` — La página integradora mínima: crear la escena, dibujar `E-1` y liberar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-13`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify
```

---

## 1. Objetivo del sample

Demostrar el recorrido mínimo del archivo de guion sobre una página sin ninguna pieza del backend: crear una instancia sobre una superficie de dibujo, cargar el texto del escenario `E-1` y ver dibujadas sus **tres** piezas, y liberar la instancia. Es la primera de las tres partes del sample **S-1** del `PRODUCT-INTAKE` §18.

## 2. Prerequisites

- **Entorno de ejecución de la cadena de herramientas**, en versión de soporte prolongado, **dentro** del entorno de desarrollo contenido.
- **Navegador con capacidad gráfica tridimensional**, declarada por capacidad y no por versión.
- **Conductor de navegador capaz de contar peticiones de red.**
- **Etapa que genera el archivo de guion, cerrada.**
- **Sin backend, y es la propiedad del sample**: ni base de datos, ni servicio de datos, ni credencial, ni acceso a redes de distribución externas.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion con el comando corto: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/01-basico run verify`.
4. Para mirarlo a mano, abrir `samples/visor/01-basico/index.html` en un navegador con capacidad gráfica tridimensional y pegar el texto de `E-1` en el área de texto.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-13` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
