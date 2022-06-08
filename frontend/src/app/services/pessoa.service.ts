import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PessoaRequest } from '../models/PessoaRequest';
import { PessoaResponse } from '../models/PessoaResponse';
import { PessoasResponse } from '../models/PessoasResponse';

@Injectable({
  providedIn: 'root',
})
export class PessoaService {
  apiUrl = 'http://localhost:5194/api/pessoa';
  constructor(private http: HttpClient) {}

  getElements(): Observable<PessoasResponse> {
    return this.http.get<PessoasResponse>(this.apiUrl);
  }

  createElement(element: PessoaRequest): Observable<PessoaRequest> {
    return this.http.post<PessoaRequest>(this.apiUrl, element);
  }

  editElement(element: PessoaRequest): Observable<PessoaRequest> {
    return this.http.put<PessoaRequest>(`${this.apiUrl}/${element.id}`, element);
  }

  deleteElement(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
