import { Lib } from './../../libs/lib';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Cliente } from 'src/models/cliente';
import { ClienteService } from 'src/services/cliente.service';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';
import { ThemePalette } from '@angular/material/core';
import { SELECT_PANEL_INDENT_PADDING_X } from '@angular/material/select';

@Component({
  selector: 'app-cliente',
  templateUrl: './Cliente.component.html',
  styleUrls: ['./Cliente.component.css'],
})
export class ClienteComponent implements OnInit {
  mostrarSpinner = true;
  clientes: Cliente[] = [];
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
      nome: ['', [Validators.required]],
      documento: ['', [Validators.required]],
    });
    this.obterTodosClientes();
  }

  obterTodosClientes(): void {
    this.clienteService.todosClientes().subscribe((data) => {
      this.clientes = data;
      this.mostrarSpinner = false;
    });
  }

  onFormSubmit(): void {
    this.dataSaved = false;
    const cliente = this.clienteForm.value;

    if (!Lib.validarCPF(cliente.documento)) {
      alert('CPF inválido!');
      return;
    }

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
      this.clienteForm.controls['nome'].setValue(cliente.nome);
      this.clienteForm.controls['documento'].setValue(cliente.documento);
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
