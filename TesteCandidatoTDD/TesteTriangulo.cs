using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesteCandidatoTriangulo;

namespace TesteCandidatoTDD
{
    [TestClass]
    public class TesteTriangulo
    {
        [TestMethod]
        public void TestResultadoTriangulo()
        {
            int retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 6 }, new[] { 3, 5 }, new[] { 9, 7, 1 }, new[] { 4, 6, 8, 4 } });
            Assert.IsTrue(retorno == 26);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 6 }, new[] { 3, 5 }, new[] { 9, 7, 1 } });
            Assert.IsTrue(retorno == 18);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 6 }, new[] { 3, 5 } });
            Assert.IsTrue(retorno == 11);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 6 }, new[] { 3, 5 }, new[] { 9, 1, 3 }, new[] { 4, 6, 1, 4 } });
            Assert.IsTrue(retorno == 18);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 6 }, new[] { 3, 5 }, new[] { 9, 1, 3 }, new[] { 4, 6, 6, 4 } });
            Assert.IsTrue(retorno == 20);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 1 }, new[] { 1, 1 }, new[] { 1, 1, 1 }, new[] { 1, 1, 1, 1 } });
            Assert.IsTrue(retorno == 4);

            retorno = new Triangulo().ResultadoTriangulo(new[] { new[] { 1 }, new[] { 1, 1 }, new[] { 1, 1, 1 } });
            Assert.IsTrue(retorno == 3);
        }
    }
}
