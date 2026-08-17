import * as THREE from 'three';

/**
 * Los colores de la escena, **leídos del catálogo de diseño y no inventados acá**.
 *
 * ES LA REGLA QUE EL PORT DE LA MAQUETA YA DECLARABA: «no define color ad hoc: los toma de los
 * tokens del catálogo de diseño». Un color escrito a mano en este archivo sería un valor visual
 * fuera del sistema, que es exactamente lo que la puerta del sistema visual existe para impedir —y
 * que ella no puede ver, porque vive en un paquete y no en la hoja de estilo—.
 *
 * CADA TIPO TIENE SU COLOR Y NO SE DERIVA DEL ÍNDICE. Derivarlo del índice —como hacía la primera
 * versión de esta capa— hace que la misma figura cambie de color según con quién la manden, y que
 * dos trabajos distintos no se puedan comparar de un vistazo.
 *
 * EL RESPALDO EXISTE PARA CUANDO NO HAY HOJA DE ESTILO, que es el caso de la página suelta del
 * paquete: ahí no hay documento del producto del que leer, y sin respaldo no se vería nada.
 */

type TokenName =
  | '--color-background-secondary'
  | '--color-border-secondary'
  | '--color-brand-primary'
  | '--color-brand-primary-dark'
  | '--color-accent-module-b'
  | '--color-accent-module-c'
  | '--color-accent-module-d'
  | '--color-text-secondary';

function token(name: TokenName, fallback: string): string {
  try {
    const value = getComputedStyle(document.documentElement).getPropertyValue(name).trim();
    return value.length > 0 ? value : fallback;
  } catch {
    return fallback;
  }
}

export interface Palette {
  readonly background: THREE.Color;
  readonly grid: THREE.Color;
  readonly selection: THREE.Color;
  readonly byType: Readonly<Record<string, string>>;
}

/** La paleta de la escena, con los mismos tokens que la maqueta aprobada declara. */
export function palette(): Palette {
  return {
    background: new THREE.Color(token('--color-background-secondary', '#F1F1EF')),
    grid: new THREE.Color(token('--color-border-secondary', '#D9D9D4')),
    selection: new THREE.Color(token('--color-brand-primary', '#0F6E56')),
    byType: {
      Cylinder: token('--color-accent-module-d', '#185FA5'),
      Cube: token('--color-accent-module-c', '#854F0B'),
      Orthohedron: token('--color-brand-primary', '#0F6E56'),
      Rectangle: token('--color-accent-module-b', '#534AB7'),
      Square: token('--color-text-secondary', '#5C5C57'),
      Circle: token('--color-brand-primary-dark', '#04342C'),
    },
  };
}
