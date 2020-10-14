import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Comissao } from 'src/models/comissao';
import { ComissaoService } from 'src/services/comissao.service';

@Component({
  selector: 'app-comissao',
  templateUrl: './comissao.component.html',
  styleUrls: ['./comissao.component.css'],
})
export class ComissaoComponent implements OnInit {
  comissoes: Observable<Comissao[]>;
  valorTotalComissao = 0;

  constructor(private comissaoService: ComissaoService) {}

  ngOnInit(): void {
    this.obterComissoes();
  }

  obterComissoes(): void {
    this.comissoes = this.comissaoService.obterComissoes();

    this.comissoes.subscribe(
      (data) => (this.valorTotalComissao = data[0].valorTotalComissao)
    );
  }
}
