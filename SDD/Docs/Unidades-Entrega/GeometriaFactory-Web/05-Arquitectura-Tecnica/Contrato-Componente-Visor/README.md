# Contrato del componente visor

| Campo | Valor |
| --- | --- |
| Nivel | Unidad de entrega |
| Versión | 1.0 |
| Fecha | 2026-08-16 |
| Estado | Vigente |

---

## 1. Qué hay acá

Siete documentos que hasta la migración a SDD 8.x vivían en `02-Especificacion-Funcional` como casos
de uso del proyecto de código `GeometriaFactory-Visor`.

**No son casos de uso de esta unidad de entrega.** Describen la **API del componente**: inicializar
una instancia sobre un elemento de dibujo, cargar el texto y dibujar, seleccionar una pieza por su
índice, redimensionar la escena, destruir la instancia y liberar recursos, gobernar el movimiento
automático y ejercitar la fachada sin backend.

Quien las invoca es **el portal**, no una persona. Lo que la persona efectivamente hace con el visor
ya está declarado en `CU-10007`, «abrir un trabajo y explorarlo en escena y árbol».

## 2. Por qué el contrato vive acá y no en el nivel producto

Porque el visor lo compone **una sola** unidad de entrega. La matriz de composición de
`Producto/Vista-Producto.md` lo muestra: `GeometriaFactory-Visor` solo aparece en la fila de
`GeometriaFactory-Web`.

Un proyecto de código compartido entre varias entregas —como `GeometriaFactory-Contracts`— sí va al
nivel producto, porque no tiene una unidad dueña. Éste la tiene.
