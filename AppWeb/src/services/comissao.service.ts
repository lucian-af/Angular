import { Comissao } from './../models/comissao';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComissaoService {
  url = 'https://localhost:44336/api/dividas/comissao';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  obterComissoes(): Observable<Comissao[]> {
    return this.httpClient.get<Comissao[]>(this.url).pipe(take(1));
  }
}
