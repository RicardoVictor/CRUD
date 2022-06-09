import { Pessoa } from "./Pessoa";

export interface PessoaResponse {
  pessoa: Pessoa,
  success: boolean,
  error: string | null
}
