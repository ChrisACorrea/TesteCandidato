using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(int[][] dadosTriangulo)
        {
            int totalMaximo = 0;
            int indexUsadoNaUltimaSoma = 0;
            for (int linha = 0; linha < dadosTriangulo.Length; linha++)
            {
                ValidaLinha(linha, dadosTriangulo[linha]);

                var resultado = GetProximoElementoParaSoma(dadosTriangulo[linha], indexUsadoNaUltimaSoma);
                totalMaximo += resultado.valor;
                indexUsadoNaUltimaSoma = resultado.index;
            }

            return totalMaximo;
        }

        private void ValidaLinha(int indexAtual, int[] linhaDoTriangulo)
        {
            if (linhaDoTriangulo.Length != indexAtual + 1)
                throw new Exception("Dados do Triângulo inválidos.");

            return;
        }

        private (int valor, int index) GetProximoElementoParaSoma(int[] linha, int indexUsadoNaUltimaSoma)
        {
            if (IsPrimeiraLinha(linha))
                return (linha[0], 0);

            var primeiroElemento = (valor: linha[indexUsadoNaUltimaSoma], index: indexUsadoNaUltimaSoma);
            var segundoElemento = (valor: linha[indexUsadoNaUltimaSoma + 1], index: indexUsadoNaUltimaSoma + 1);

            if (primeiroElemento.valor > segundoElemento.valor)
                return primeiroElemento;

            return segundoElemento;
        }

        private bool IsPrimeiraLinha(int[] linha) => linha.Length == 1;
    }
}
