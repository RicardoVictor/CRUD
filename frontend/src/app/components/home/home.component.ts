import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Cidade } from 'src/app/models/Cidade';
import { CidadeResponse } from 'src/app/models/CidadeResponse';
import { PessoaRequest } from 'src/app/models/PessoaRequest';
import { PessoaResponse } from 'src/app/models/PessoaResponse';
import { PessoasResponse } from 'src/app/models/PessoasResponse';
import { CidadeService } from 'src/app/services/cidade.service';
import { PessoaService } from 'src/app/services/pessoa.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [PessoaService],
})
export class HomeComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>;
  displayedColumns: string[] = ['nome', 'cpf', 'idade', 'cidade', 'actions'];
  dataSource!: PessoaResponse[];

  constructor(
    public dialog: MatDialog,
    public pesssoService: PessoaService,
    public cidadeService: CidadeService
  ) {
    this.pesssoService.getElements().subscribe((data: PessoasResponse) => {
      this.dataSource = data.pessoas;
    });
  }

  openDialog(element: PessoaResponse | null): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '250px',
      data:
        element === null
          ? { id: null, nome: '', cpf: '', idade: null }
          : {
              id: element.id,
              nome: element.nome,
              cpf: element.cpf,
              idade: element.idade,
              idCidade: element.cidade.id,
            },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        if (this.dataSource.map((p) => p.id).includes(result.id)) {
          this.pesssoService
            .editElement(result)
            .subscribe((data: PessoaRequest) => {
              const index = this.dataSource.findIndex((p) => p.id === data.id);
              const pessoa: PessoaResponse = {
                id: result.id,
                nome: result.nome,
                cpf: result.cpf,
                idade: result.idade,
                cidade: {
                  id: result.idCidade,
                  nome: '',
                  uf: '',
                },
              };
              this.dataSource[index] = pessoa;
              this.table.renderRows();
            });
        } else {
          this.pesssoService.createElement(result).subscribe(() => {
            this.cidadeService
              .getElementById(result.idCidade)
              .subscribe((cidadeResponse: CidadeResponse) => {
                const cidade: Cidade = cidadeResponse.cidade;
                const pessoa: PessoaResponse = {
                  id: result.id,
                  nome: result.nome,
                  cpf: result.cpf,
                  idade: result.idade,
                  cidade: {
                    id: cidade.id,
                    nome: cidade.nome,
                    uf: cidade.nome,
                  },
                };
                this.dataSource.push(pessoa);
                this.table.renderRows();
              });
          });
        }
      }
    });
  }

  ngOnInit(): void {}

  deleteElement(id: number): void {
    this.pesssoService.deleteElement(id).subscribe(() => {
      this.dataSource = this.dataSource.filter((e) => e.id !== id);
    });
  }

  editElement(element: PessoaResponse): void {
    this.openDialog(element);
  }
}