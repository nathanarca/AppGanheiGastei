using GanheiGastei.Model;
using GanheiGastei.Model.ModelDB;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GanheiGastei.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageNovoValor : Rg.Plugins.Popup.Pages.PopupPage
    {
        public bool Entrada;
        EventHandler evento;
        GanhoGasto _ganhoGasto;

        public PageNovoValor(bool entrada, EventHandler eventHandler)
        {
            InitializeComponent();

            labelTipo.Text = entrada ? "Quanto você ganhou?" : "Quanto você gastou?";
            labelDescricao.Text = entrada ? "Como você ganhou?" : "Como você gastou?";
            textBoxDescricao.Placeholder = entrada ? "ex:pagamento adiantado" : "ex:paguei a conta de luz";

            Entrada = entrada;
            evento = eventHandler;

        }

        public PageNovoValor(GanhoGasto ganhoGasto, EventHandler eventHandler)
        {
            InitializeComponent();

            _ganhoGasto = ganhoGasto;

            labelTipo.Text = _ganhoGasto.Entrada ? "Quanto você ganhou?" : "Quanto você gastou?";
            labelDescricao.Text = _ganhoGasto.Entrada ? "Como você ganhou?" : "Como você gastou?";
            textBoxDescricao.Placeholder = _ganhoGasto.Entrada ? "ex:pagamento adiantado" : "ex:paguei a conta de luz";


            buttonApagar.IsVisible = true;

            textBoxDescricao.Text = _ganhoGasto.Descricao.Trim();
            textBoxValor.Text = _ganhoGasto.Valor.ToString("N2").Replace("-", "");


            Entrada = _ganhoGasto.Entrada;

            evento = eventHandler;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            textBoxValor.Focus();
        }

        private async void buttonPronto_Clicked(object sender, EventArgs e)
        {
            Salvar();
        }

        private async void buttonApagar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_ganhoGasto == null) return;

                var pergunta = string.Empty;

                var tipo = _ganhoGasto.Entrada ? "ganho" : "gasto";

                pergunta = $"Você quer remover este {tipo} de {_ganhoGasto.Valor.ToString("C")} do dia {_ganhoGasto.DataHora.ToString("dd/MM/yyyy")}?";

                var sim = await DisplayAlert("Atenção!", pergunta, "Sim", "Não");
                if (sim)
                {
                    new GanhoGastoBD().Deletar(_ganhoGasto);

                    evento.Invoke(null, null);

                    await PopupNavigation.PopAsync(true);

                }

            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private void textBoxDescricao_Completed(object sender, EventArgs e)
        {
            Salvar();
        }

        private async void Salvar()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxValor.Text))
                {
                    textBoxValor.Focus();
                    return;
                };

                if (string.IsNullOrEmpty(textBoxDescricao.Text))
                {
                    textBoxDescricao.Focus();
                    return;
                };

                var _valor = decimal.Parse(textBoxValor.Text.Replace("-", "").Replace("/", "").Trim());


                var ggSalvar = new GanhoGasto()
                {
                    Entrada = Entrada,
                    DataHora = DateTime.Now,
                    Descricao = textBoxDescricao.Text.Trim(),
                    Valor = Entrada ? _valor : _valor * -1

                };

                if (_ganhoGasto == null)
                {
                    new GanhoGastoBD().Inserir(ggSalvar);
                }
                else
                {
                    ggSalvar.Id = _ganhoGasto.Id;
                    ggSalvar.DataHora = _ganhoGasto.DataHora;

                    new GanhoGastoBD().Atualizar(ggSalvar);
                }



                evento.Invoke(null, null);

                await PopupNavigation.PopAsync(true);

            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private void textBoxValor_Completed(object sender, EventArgs e)
        {
            Salvar();
        }
        
    }
}