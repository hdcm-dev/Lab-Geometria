---
doc_id: AUD-GEO-001
doc_type: registro-audiencias
title: Registro de audiencias de Lab-Geometria
description: A quién se entrega cada pieza documental de Lab-Geometria, con qué nivel y dónde se cumple; en estos repos públicos, todo es publico.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, po, docente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Registro de audiencias — Lab-Geometria

Artefacto A-12 de la [ficha de diseño documental](ficha-diseno-documental.md). Aplica las cinco reglas de D-18 del
conocimiento DocsAsCode-Designing-Specification 1.4: una audiencia es una identidad que el producto autentica y a la
que da acceso propio. Citas releídas en `aaef547`.

## 1. Audiencias

| Audiencia | Acceso propio | `incluye:` | Nivel | Cumplimiento | Superficie |
| --- | --- | --- | --- | --- | --- |
| `alumno` | Rol `Student` (`src/GeometriaFactory.Domain/Values/Role.cs:14-15`). Un rol desconocido cae en Student (`src/GeometriaFactory.Api/Endpoints/WorkEndpoints.cs:544-551`) | — | `publico` | Repo público y build | Web: `StudentWorkPanel`, `WorkSubmission`, `WorkView`. **Consume las pistas de la capa de IA (expediente 0002)**, que la ayuda no duplica |
| `docente` | Rol `Administrator` (`Role.cs:17-18`). Chequeos de Administrator en los handlers (`WorkEndpoints.cs:407`, `:460`); `:112`, `:173`, `:218` y `:255` rechazan al docente en los endpoints de sólo-alumno (`!= Role.Student`) | **plana** (H-03, default aplicado) | `publico` | Repo público | Web: `AccountsPanel`, `ClassSubmissionList`, `WorkResolution` |
| `consumidor-api` | Sin acceso propio: la API autentica personas, no aplicaciones (`ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md:1`, `:19`); la autenticación por cliente pasó a `Descartada` (`SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md:199-200`) | — | `publico` con `Documentacion:Publicada` | Configuración: `src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:43`, `:108-109` | `/openapi/v1.json`, `/documentacion` (`ApiDocumentation.cs:37-40`) |

**Por qué `docente` no incluye a `alumno` (regla 2).** Para declarar `docente incluye: alumno`, todo punto que
autoriza al alumno tendría que autorizar también al docente. Pasa lo contrario: `WorkEndpoints.cs:112`, `:173`, `:218`
y `:255` rechazan a quien no es Student. Jerarquía plana, H-03 con su default.

**Alcance (regla 4).** No se activa `por-aplicacion`: el alumno ve sólo lo suyo por identidad
(`WorkEndpoints.cs:416-422`) y no hay inquilinos ni aplicaciones (`Role.cs:12-18`, dos valores).

**Nivel de la API (regla 3).** El documento de la API no exige acceso: describir la forma «no revela ningún dato de
ninguna cuenta» (`ApiDocumentation.cs:101-103`). Lo expone la llave `Documentacion:Publicada` (o el entorno de
desarrollo), que es decisión de despliegue (`:108-109`). Par declarado: con la llave encendida, `200`; apagada, distinto
de `200`. La prueba negativa pareada es de la ola 2.

## 2. Descartados

- **`mantenedor`, `desarrollo`, `auditor`, `operador`:** roles de lectura sin política en el producto (homónimos,
  regla 5). Orientan la escritura en `audience:`, no son audiencias de entrega.
- **El agente:** hereda la audiencia de su usuario y por su canal sólo pasa `publico`.
- **Documentación de API restringida a desarrolladores específicos (H-16):** no en estos repos. Los dos son públicos
  (expediente 0003, evidencia 002) y `README.md:3` los declara «público de consulta». Si algún día hiciera falta, requiere
  un repo privado y un expediente propio.

## 3. Cobertura del concepto de audiencias del Product Owner

| Audiencia del concepto | Equivalente en Geometría | Evidencia |
| --- | --- | --- |
| Administrador raíz del panel | `docente` = `Administrator`, único rol con privilegio | `Role.cs:12-18`; `WorkEndpoints.cs:407`, `:460` |
| Administradores con menos privilegios, por aplicación | **No aplica**: una aplicación y un solo rol administrativo | `Role.cs:12-18` |
| Perfil de la app móvil | **No aplica**: los proyectos son Api, Application, Contracts, Domain, Infrastructure y Web | `git ls-files 'src/*.csproj'` → seis proyectos, ninguno móvil |
| Segundo servicio con otro tipo de usuario | **No aplica**: un solo servicio en la composición | `deploy/compose.yaml:41-42` (`services:` → único `api:`) |
| Documentación de API restringida | **Descartado** (H-16) | §2 |
| Integración con formularios | Ayuda en el punto de uso sobre los formularios de la Web (PR 3) | `InitialProvisioning.razor:97-100`; `AccountRegistration.razor:125-129` |

## 4. Control y elevación

- **Control único `delivery-publico`** (llega en el PR 2): en los dos repos, todo `delivery:` distinto de `publico` va
  en rojo. Reemplaza a `nivel-vs-repo`, `delivery-cerrado-al-registro` y `audiencia-vocabulario`.
- **Elevación** (expediente 0003, folio 003, punto 2): falta la fila «alumno» en el mapa de lectura por rol de
  `SDD/Docs/README.md:127-136`.
