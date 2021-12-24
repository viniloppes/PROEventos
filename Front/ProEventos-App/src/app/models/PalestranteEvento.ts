import { Evento } from "./Evento";
import { Palestrante } from "./Palestrante";

export interface PalestranteEvento {
  id: number;
  palestranteId: number;
  palestrante: Palestrante;
  eventoId: number;
  evento: Evento;
}
