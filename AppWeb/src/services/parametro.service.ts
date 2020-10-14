import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Parametro } from './../models/parametro';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ParametroService {
  url = 'https://localhost:44336/api/parametros';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  obterTodosParametros(): Observable<Parametro[]> {
    return this.httpClient.get<Parametro[]>(this.url).pipe(take(1));
  }

  obterParametro(idParametro: number): Observable<Parametro> {
    return this.httpClient.get<Parametro>(`${this.url}/${idParametro}`).pipe(take(1));
  }

  public atualizarParametro(parametro: Parametro): Observable<Parametro> {
    return this.httpClient.put<Parametro>(this.url, parametro, this.httpOptions);
  }
}
