using GanheiGastei.Model.ModelDB;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanheiGastei.Model
{
    public class GanhoGasto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Entrada { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }

        [Ignore]
        public string CorTexto
        {

            get
            {
                if (Entrada)
                {
                    return "#27ae60";
                }
                else
                {
                    return "#727376";
                }


            }

        }
        



    }
}
