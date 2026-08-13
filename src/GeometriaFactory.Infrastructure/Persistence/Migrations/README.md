# Transformaciones de esquema

Acá viven las transformaciones de esquema generadas por `scripts/migrate.sh`, versionadas
con el código de su etapa. **Una transformación ya fusionada no se edita**
(intake §17.3.P.7; `Infrastructure ADR-07`).

**La etapa `a` no genera ninguna.** El esquema mapea las cinco entidades del modelo, y el
Product Owner ancló el modelado de las entidades a la etapa `c` (`Domain BT-06`). La primera
transformación se genera cuando esas entidades tienen atributos: es el riesgo `R-02` de
`Plan-Etapa-A.md` §7, resuelto a favor de la etapa `c`.
