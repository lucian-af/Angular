import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Cliente } from './../models/cliente';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {
  url = 'https://localhost:44336/api/clientes';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  todosClientes(): Observable<Cliente[]> {
    return this.httpClient.get<Cliente[]>(this.url).pipe(take(1));
  }

  obterCliente(idCliente: number): Observable<Cliente> {
    const apiUrl = `${this.url}/${idCliente}`;
    return this.httpClient.get<Cliente>(apiUrl);
  }

  public inserirCliente(cliente: Cliente): Observable<Cliente> {
    return this.httpClient.post<Cliente>(this.url, cliente, this.httpOptions);
  }

  public atualizarCliente(cliente: Cliente): Observable<Cliente> {
    return this.httpClient.put<Cliente>(this.url, cliente, this.httpOptions);
  }

  public deletarCliente(idCliente: number): Observable<Cliente> {
    const apiUrl = `${this.url}/${idCliente}`;
    return this.httpClient.delete<Cliente>(apiUrl, this.httpOptions);
  }
}
