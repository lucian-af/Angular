import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { DividaCliente } from './../models/dividaCliente';
import { Divida } from './../models/divida';
import { Cliente } from 'src/models/cliente';

@Injectable({
  providedIn: 'root',
})
export class DividaService {

  url = 'https://localhost:44336/api/dividas';
  urlCliente = 'https://localhost:44336/api/clientes';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  getClientes(): Observable<Cliente[]>{
    return this.httpClient.get<Cliente[]>(this.urlCliente).pipe(take(1));
  }

  obterDividaPorCliente(idCliente: number): Observable<DividaCliente[]> {
    if(idCliente > 0){
      const apiUrl = `${this.url}/${idCliente}`;
      return this.httpClient.get<DividaCliente[]>(apiUrl).pipe(take(1));
    }
  }

  public inserirDivida(divida: Divida): Observable<Divida> {
    return this.httpClient.post<Divida>(this.url, divida, this.httpOptions);
  }

  public atualizarDivida(divida: Divida): Observable<Divida> {
    return this.httpClient.put<Divida>(this.url, divida, this.httpOptions);
  }

  public deletarDivida(idDivida: number): Observable<Divida> {
    const apiUrl = `${this.url}/${idDivida}`;
    return this.httpClient.delete<Divida>(apiUrl, this.httpOptions);
  }
}
