# Índice de decisiones de arquitectura — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Decisiones-Arquitectura.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## 1. Qué es este documento

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Visor`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 2. ADR vigentes

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-12001](Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | Tres capas con fachada plana, y el motor de dibujo confinado a la capa interna | Estilo | Propuesto | 2026-08-10 |
| [ADR-12002](Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | La superficie pública son seis funciones planas, siete garantías y siete códigos | Estilo | Propuesto | 2026-08-10 |
| [ADR-12003](Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | Visualizador puro: cero red, cero persistencia, cero configuración y cero identidad | Seguridad | Propuesto | 2026-08-10 |
| [ADR-12004](Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | Motor de dibujo empaquetado dentro del bundle y aislado tras la capa 3 | Despliegue | Propuesto | 2026-08-10 |
| [ADR-12005](Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) | Disposición determinista derivada del índice, y el determinismo es de posición y no de orientación | Estilo | Propuesto | 2026-08-10 |
| [ADR-12006](Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) | El artefacto es un bundle generado, y su versionado es el del punto de extensión | Despliegue | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 3. Por qué son seis y no tres

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-12001, ADR-12002 y ADR-12006. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-12003 | Es la regla de arquitectura del producto que este proyecto de código materializa. `RA-02` no es una preferencia de diseño: es lo que hace **imposible** violar `RA-01` desde el navegador, y romperla en un solo proyecto de código reabre las tres propiedades de la topología |
| ADR-12004 | Es lo que mide la puerta técnica `PT-03`, y es la única dependencia externa real del proyecto de código. Su tratamiento condiciona que la página funcione desde la red del aula |
| ADR-12005 | Reemplaza una conducta del visualizador previo y, desde la capacidad F-25, tiene una acotación que hay que declarar en cada lugar donde se afirma el determinismo: es de posición y no de orientación. Sin ADR, la acotación se lee como un detalle |

## 4. Cobertura de las categorías de decisión

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-12001, ADR-12002, ADR-12005 | — |
| Persistencia | **Ninguna, y es prohibición explícita** | `tiene_persistencia` es false y el intake declara «no aplica, y es prohibición explícita» en §17.7.P.4. La ausencia está registrada como garantía G-2 dentro de ADR-12003, que es su lugar |
| Comunicación | **Ninguna** | Este proyecto de código no se comunica con nada: la ausencia de red es una decisión de seguridad y está en ADR-12003 |
| Seguridad | ADR-12003 | Su contribución a la seguridad del producto es **negativa por diseño**: no hacer red |
| Observabilidad | **Ninguna** | El bundle no instrumenta ni emite registros. `tiene_observabilidad_critica` es false |
| Despliegue | ADR-12004, ADR-12006 | No hay unidad de despliegue propia: el artefacto viaja dentro del despliegue del anfitrión |
| Extensibilidad | ADR-12002 y ADR-12006, con su desarrollo en [`Extensibilidad.md`](Extensibilidad.md) | `tiene_extensibilidad` es **true**, y es el único proyecto de código del producto en el que lo es |

Las tres categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Indexa las seis ADR de `GeometriaFactory-Visor` con su categoría, su estado y su fecha, declara por qué son seis y no tres, y declara vacías con su motivo las tres categorías de decisión que este proyecto de código no toca. |
