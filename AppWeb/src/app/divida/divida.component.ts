import { DividaCliente } from './../../models/dividaCliente';
import { DividaParcela } from './../../models/dividaParcela';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DividaService } from 'src/services/divida.service';
import { Cliente } from 'src/models/cliente';

@Component({
  selector: 'app-divida',
  templateUrl: './divida.component.html',
  styleUrls: ['./divida.component.css'],
})
export class DividaComponent implements OnInit {
  dividas: Observable<DividaCliente[]>;
  clientes: Observable<Cliente[]>;
  clienteSelecionado = 0;
  parcelas: DividaParcela[] = [];
  mostrarParcela = false;
  mostrarContato = false;
  numeroContato = "";

  constructor(private dividaService: DividaService) {}

  ngOnInit(): void {
    this.obterClientes();
    this.mostrarParcela = false;
  }

  obterTodosDividas(idCliente: number): void {
    this.dividas = this.dividaService.obterDividaPorCliente(idCliente);
  }

  obterClientes(): void {
    this.clientes = this.dividaService.getClientes();
  }

  onClienteSelecionado(): void {
    this.mostrarContato = this.clienteSelecionado > 0;
    this.mostrarParcela = false;
    this.obterTodosDividas(this.clienteSelecionado);
    this.gerarContatoAleatorio();
  }

  carregarParcelas(divida: DividaCliente): void {
    if (this.mostrarParcela && this.parcelas === divida.parcelas) {
      this.mostrarParcela = false;
      return;
    }

    this.parcelas = divida.parcelas;
    this.mostrarParcela = this.parcelas.length > 0;
  }

  gerarContatoAleatorio(): void {
    const random = Math.floor(Math.random() * (9999999999 - 1000000000)) + 1000000000;
    const prefixo = random.toString().substr(0, 2);
    const numeroAntes = random.toString().substr(2, 4);
    const numeroDepois = random.toString().substr(6, 4);

    this.numeroContato =  `(${prefixo}) ${numeroAntes} - ${numeroDepois}`;
  }
}
