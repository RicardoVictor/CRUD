import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cidade } from '../models/Cidade';
import { CidadeResponse } from '../models/CidadeResponse';
import { CidadesResponse } from '../models/CidadesResponse';

@Injectable({
  providedIn: 'root',
})
export class CidadeService {
  apiUrl = 'http://localhost:5194/api/cidade';
  constructor(private http: HttpClient) {}
  
  getElements(): Observable<CidadesResponse> {
    return this.http.get<CidadesResponse>(this.apiUrl);
  }

  getElementById(id: number): Observable<CidadeResponse> {
    return this.http.get<CidadeResponse>(`${this.apiUrl}/${id}`);
  }
}
