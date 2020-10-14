import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, Validators } from '@angular/forms';
import { Parametro } from 'src/models/parametro';
import { TipoJuros } from 'src/models/TipoJuros';
import { ParametroService } from 'src/services/parametro.service';

@Component({
  selector: 'app-parametro',
  templateUrl: './parametro.component.html',
  styleUrls: ['./parametro.component.css'],
})
export class ParametroComponent implements OnInit {
  parametros: Observable<Parametro[]>;
  dataSaved = false;
  parametroForm: any;
  idParametro = null;
  message = null;
  tipoJurosList: TipoJuros[] = [];
  tipoJurosSelecionado = 0;

  constructor(
    private formbulider: FormBuilder,
    private parametroService: ParametroService
  ) {}

  ngOnInit(): void {
    this.parametroForm = this.formbulider.group({
      quantidadeParcelaMaxima: ['', [Validators.required]],
      tipoJuros: ['', [Validators.required]],
      percentualJuros: ['', [Validators.required]],
      percentualComissao: ['', [Validators.required]],
    });
    this.obterTodosParametros();
    this.tipoJurosList = this.obterTodosTipoJuros();
  }

  obterTodosParametros(): void {
    this.parametros = this.parametroService.obterTodosParametros();
  }

  onFormSubmit(): void {
    this.dataSaved = false;
    const parametro = this.parametroForm.value;
    this.atualizarParametro(parametro);
    this.cancelarForm();
  }

  carregarParametro(idParametro: number): void {
    this.parametroService.obterParametro(idParametro).subscribe((parametro) => {
      this.message = null;
      this.dataSaved = false;
      this.idParametro = parametro.idParametro;
      this.parametroForm.controls['quantidadeParcelaMaxima'].setValue(
        parametro.quantidadeParcelaMaxima
      );
      this.parametroForm.controls['tipoJuros'].setValue(parametro.tipoJuros);
      this.parametroForm.controls['percentualJuros'].setValue(
        parametro.percentualJuros
      );
      this.parametroForm.controls['percentualComissao'].setValue(
        parametro.percentualComissao
      );
    });
  }

  atualizarParametro(parametro: Parametro): void {
    if (this.idParametro == null) {
      return;
    } else {
      parametro.idParametro = this.idParametro;
      this.parametroService.atualizarParametro(parametro).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Registro atualizado com sucesso';
        this.obterTodosParametros();
        this.idParametro = null;
        this.cancelarForm();
      });
    }
  }

  obterTodosTipoJuros(): TipoJuros[] {
    const tipos: TipoJuros[] = [];
    let tipo = new TipoJuros();
    tipo.tipoJuros = 1;
    tipo.descricao = 'JUROS SIMPLES';
    tipos.push(tipo);

    tipo = new TipoJuros();
    tipo.tipoJuros = 2;
    tipo.descricao = 'JUROS COMPOSTOS';
    tipos.push(tipo);

    return tipos;
  }

  cancelarForm(): void {
    this.parametroForm.reset();
    setTimeout(() => {
      this.message = null;
      this.dataSaved = false;
    }, 1500);
  }
}
