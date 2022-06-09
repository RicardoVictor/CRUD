import { Pessoa } from "./Pessoa";

export interface PessoasResponse {
  pessoas: Pessoa[],
  success: boolean,
  error: string | null
}
