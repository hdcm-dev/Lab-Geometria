# El acuse de la escena, en los dos estados

El peritaje del 2026-09-02 lo dejó en dos líneas:

```text
con3d · canvas en la escena: 1
sin3d · canvas en la escena: 0     TEXTO IDENTICO
```

En una PC sin aceleración, o en un escritorio remoto, o si el paquete del visor no llegó,
el alumno veía un recuadro liso y la página le decía **en letras** que sus figuras estaban
dibujadas. El único aviso era un `console.warn` que nadie lee.

## Después
```text
   CON 3D  canvas=1  clases="gf-caption gf-mt-3"
           acuse: Se dibujaron las 3 figuras del trabajo.
   SIN 3D  canvas=0  clases="gf-mt-3 gf-banner gf-banner--warning"
           acuse: Este equipo no pudo dibujar la escena. Los datos del trabajo están completos: mirá el árbol del texto y las observaciones.
   CONFORME · el acuse dice la verdad en los dos estados
```

## La propiedad que se buscaba

**El texto que sirve el servidor tiene que ser verdadero aunque el guion nunca corra.**
Por eso el estado inicial no afirma nada sobre el dibujo: cuenta figuras, que es un dato
del trabajo. El guion lo mueve a «se dibujaron» o a «no se pudo» según lo que ocurrió.
