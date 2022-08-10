using AppBibliaRapida.Config;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GanheiGastei.Model.ModelDB
{
    class GanhoGastoBD
    {
        readonly SQLiteAsyncConnection database;

        public GanhoGastoBD()
        {
            database = new SQLiteAsyncConnection(ConfiguracaoApp.CaminhoBD);
            database.CreateTableAsync<GanhoGasto>().Wait();
        }

        public Task<int> Count()
        {
            return database.Table<GanhoGasto>().CountAsync();
        }

        public Task<List<GanhoGasto>> GetAsync()
        {
            return database.Table<GanhoGasto>().ToListAsync();
        }

        public Task<List<GanhoGasto>> GetIdAsync(int id)
        {
            return database.QueryAsync<GanhoGasto>($"SELECT * FROM [GanhoGasto] WHERE [Id] = {id}");
        }

        public Task<List<GanhoGasto>> GetPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return database.Table<GanhoGasto>().Where(n => n.DataHora >= dataInicio && n.DataHora <= dataFim).OrderByDescending(n => n.DataHora).ToListAsync();
        }

        public Task<int> Salvar(GanhoGasto objeto)
        {
            if (objeto.Id != 0)
            {
                return Atualizar(objeto);
            }
            else
            {
                return Inserir(objeto);
            }
        }
        public Task<int> Inserir(GanhoGasto objeto)
        {
            return database.InsertAsync(objeto);
        }

        public Task<int> Inserir(List<GanhoGasto> objeto)
        {
            return database.InsertAllAsync(objeto);
        }

        public Task<int> Atualizar(GanhoGasto objeto)
        {
            return database.UpdateAsync(objeto);
        }

        public Task<int> Deletar(GanhoGasto objeto)
        {
            return database.DeleteAsync(objeto);
        }
    }
}
