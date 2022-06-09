import { Cidade } from './Cidade';

export interface Pessoa {
  id: number;
  nome: string;
  cpf: string;
  idade: number;
  cidade: Cidade;
}
