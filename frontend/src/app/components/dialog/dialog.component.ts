import { Component, OnInit, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Cidade } from 'src/app/models/Cidade';
import { CidadesResponse } from 'src/app/models/CidadesResponse';
import { PessoaRequest } from 'src/app/models/PessoaRequest';
import { CidadeService } from 'src/app/services/cidade.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css'],
})
export class DialogComponent implements OnInit {
  element!: PessoaRequest;
  isChange!: boolean;
  cidades = new FormControl('');

  cidadeList!: Cidade[];

  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: PessoaRequest,
    public dialogRef: MatDialogRef<DialogComponent>,
    public cidadeService: CidadeService
  ) {
    this.cidadeService.getElements().subscribe((data: CidadesResponse) => {
      this.cidadeList = data.cidades;
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    if (this.data.id != null) {
      this.isChange = true;
    } else {
      this.isChange = false;
    }
  }

  getErrorMessage() {
    return 'You must enter a value';
  }
}
