import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Cidade } from 'src/app/models/Cidade';
import { CidadeResponse } from 'src/app/models/CidadeResponse';
import { PessoaRequest } from 'src/app/models/PessoaRequest';
import { Pessoa } from 'src/app/models/Pessoa';
import { PessoasResponse } from 'src/app/models/PessoasResponse';
import { CidadeService } from 'src/app/services/cidade.service';
import { PessoaService } from 'src/app/services/pessoa.service';
import { DialogComponent } from '../dialog/dialog.component';
import { PessoaResponse } from 'src/app/models/PessoaResponse';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  dataSource!: Pessoa[];

  constructor(
    public dialog: MatDialog,
    public pesssoService: PessoaService,
    public cidadeService: CidadeService,
    private snackBar: MatSnackBar
  ) {
    this.pesssoService.getElements().subscribe((data: PessoasResponse) => {
      this.dataSource = data.pessoas;
    });
  }

  openDialog(element: Pessoa | null): void {
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
          this.pesssoService.editElement(result).subscribe(
            (data: PessoaResponse) => {
              const index = this.dataSource.findIndex(
                (p) => p.id === data.pessoa.id
              );
              this.dataSource[index] = data.pessoa;
              this.table.renderRows();
            },
            (error) => {
              if (typeof error.error.error == "undefined") {
                this.snackBar.open('Erro ao editar Pessoa.', 'Fechar');
              } else {
                this.snackBar.open(
                  'Erro ao editar Pessoa: ' + error.error.error,
                  'Fechar'
                );
              }
            }
          );
        } else {
          this.pesssoService.createElement(result).subscribe(
            () => {
              this.cidadeService
                .getElementById(result.idCidade)
                .subscribe((cidadeResponse: CidadeResponse) => {
                  const cidade: Cidade = cidadeResponse.cidade;
                  const pessoa: Pessoa = {
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
            },
            (error) => {
              if (typeof error.error.error == "undefined") {
                this.snackBar.open('Erro ao criar Pessoa.', 'Fechar');
              } else {
                this.snackBar.open(
                  'Erro ao criar Pessoa: ' + error.error.error,
                  'Fechar'
                );
              }
            }
          );
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

  editElement(element: Pessoa): void {
    this.openDialog(element);
  }
}
