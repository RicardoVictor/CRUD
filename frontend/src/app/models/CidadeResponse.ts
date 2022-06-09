import { Cidade } from "./Cidade";

export interface CidadeResponse {
  cidade: Cidade,
  success: boolean,
  error: string | null
}
