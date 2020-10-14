import { DividaParcela } from './dividaParcela';

export class DividaCliente {
  public idDivida: number;
  public nomeCliente: string;
  public documentoCliente: string;
  public dataVencimento: Date;
  public quantidadeParcela: number;
  public valorOriginal: number;
  public diasAtraso: number;
  public valorJuros: number;
  public valorDivida: number;
  public parcelas: DividaParcela[];

  constructor() {
    this.parcelas = [];
  }
}
