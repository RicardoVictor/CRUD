import { Cidade } from "./Cidade";

export interface CidadesResponse {
  cidades: Cidade[],
  success: boolean,
  error: null
}
