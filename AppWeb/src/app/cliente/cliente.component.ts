import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Cliente } from 'src/models/cliente';
import { ClienteService } from 'src/services/cliente.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './Cliente.component.html',
  styleUrls: ['./Cliente.component.css']
})

export class ClienteComponent implements OnInit {
  clientes: Observable<Cliente[]>;
  dataSaved = false;
  clienteForm: any;
  idCliente = null;
  message = null;

  constructor(
    private formbulider: FormBuilder,
    private clienteService: ClienteService
  ) {}

  ngOnInit(): void {
    this.clienteForm = this.formbulider.group({
      Nome: ['', [Validators.required]],
      Documento: ['', [Validators.required]],
    });
    this.obterTodosClientes();
  }

  obterTodosClientes(): void {
    this.clientes = this.clienteService.todosClientes();
  }

  onFormSubmit(): void {
    this.dataSaved = false;
    const cliente = this.clienteForm.value;
    this.inserirCliente(cliente);
    this.cancelarForm();
  }

  inserirCliente(cliente: Cliente): void {
    if (this.idCliente == null) {
      this.clienteService.inserirCliente(cliente).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Registro salvo com sucesso';
        this.obterTodosClientes();
        this.idCliente = null;
        this.cancelarForm();
      });
    } else {
      cliente.idCliente = this.idCliente;
      this.clienteService.atualizarCliente(cliente).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Registro atualizado com sucesso';
        this.obterTodosClientes();
        this.idCliente = null;
        this.cancelarForm();
      });
    }
  }

  carregarCliente(idCliente: number): void {
    this.clienteService.obterCliente(idCliente).subscribe((cliente) => {
      this.message = null;
      this.dataSaved = false;
      this.idCliente = cliente.idCliente;
      this.clienteForm.controls['Nome'].setValue(cliente.nome);
      this.clienteForm.controls['Documento'].setValue(cliente.documento);
    });
  }

  deletarCliente(idCliente: number): void {
    if (confirm('Deseja realmente deletar este Cliente ?')) {
      this.clienteService.deletarCliente(idCliente).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Registro deletado com sucesso';
        this.obterTodosClientes();
        this.idCliente = null;
        this.cancelarForm();
      });
    }
  }

  cancelarForm(): void {
    this.clienteForm.reset();
    setTimeout(() => {
      this.message = null;
      this.dataSaved = false;
    }, 1500);
  }
}
