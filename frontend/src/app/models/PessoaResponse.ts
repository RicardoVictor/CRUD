import { Cidade } from './Cidade';

export interface PessoaResponse {
  id: number;
  nome: string;
  cpf: string;
  idade: number;
  cidade: Cidade;
}
