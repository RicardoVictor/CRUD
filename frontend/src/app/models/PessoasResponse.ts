import { PessoaResponse } from "./PessoaResponse";

export interface PessoasResponse {
  pessoas: PessoaResponse[],
  success: boolean,
  error: null
}
