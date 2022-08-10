using System;

namespace GanheiGastei.Model.NoDB
{
    internal class InformacaoInicial
    {
        public string Descricao { get; set; }
        public decimal ValorGanhei { get; set; }
        public decimal ValorGastei { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}