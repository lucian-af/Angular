export class Lib {
  static validarCPF(cpf: string): boolean {
    let out = false;

    if (cpf.length !== 11) {
      return false;
    }

    try {
      out =
        Lib.validarCPFPrimeiroDigitoVerificador(cpf) &&
        Lib.validarCPFSegundoDigitoVerificador(cpf);
    } catch (error) {
      return false;
    }

    return out;
  }

  private static validarCPFPrimeiroDigitoVerificador(cpf: string): boolean {
    const primeiroDigitoVerificador = Number(cpf[9]);

    const Parte1 = Number(cpf[0]) * 10;
    const Parte2 = Number(cpf[1]) * 9;
    const Parte3 = Number(cpf[2]) * 8;
    const Parte4 = Number(cpf[3]) * 7;
    const Parte5 = Number(cpf[4]) * 6;
    const Parte6 = Number(cpf[5]) * 5;
    const Parte7 = Number(cpf[6]) * 4;
    const Parte8 = Number(cpf[7]) * 3;
    const Parte9 = Number(cpf[8]) * 2;

    const resultadoPrimeiraValidacao =
      Parte1 +
      Parte2 +
      Parte3 +
      Parte4 +
      Parte5 +
      Parte6 +
      Parte7 +
      Parte8 +
      Parte9;

    const primeiraValidacao =
      (resultadoPrimeiraValidacao * 10) % 11 === 10
        ? 0
        : (resultadoPrimeiraValidacao * 10) % 11;

    return primeiroDigitoVerificador === primeiraValidacao;
  }

  private static validarCPFSegundoDigitoVerificador(cpf: string): boolean {
    const segundoDigitoVerificador = Number(cpf[10]);

    const Parte1 = Number(cpf[0]) * 11;
    const Parte2 = Number(cpf[1]) * 10;
    const Parte3 = Number(cpf[2]) * 9;
    const Parte4 = Number(cpf[3]) * 8;
    const Parte5 = Number(cpf[4]) * 7;
    const Parte6 = Number(cpf[5]) * 6;
    const Parte7 = Number(cpf[6]) * 5;
    const Parte8 = Number(cpf[7]) * 4;
    const Parte9 = Number(cpf[8]) * 3;
    const Parte10 = Number(cpf[9]) * 2;

    const resultadoSegundaValidacao =
      Parte1 +
      Parte2 +
      Parte3 +
      Parte4 +
      Parte5 +
      Parte6 +
      Parte7 +
      Parte8 +
      Parte9 +
      Parte10;

    const segundaValidacao =
      (resultadoSegundaValidacao * 10) % 11 === 10
        ? 0
        : (resultadoSegundaValidacao * 10) % 11;

    return segundoDigitoVerificador === segundaValidacao;
  }
}
