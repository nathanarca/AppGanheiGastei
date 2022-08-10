using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppBibliaRapida.Config
{
    public static class ConfiguracaoApp
    {
        /// <summary>
        /// Caminho do banco de dados
        /// </summary>
        public static string CaminhoBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GanheiGastei.db3");


    }
}
