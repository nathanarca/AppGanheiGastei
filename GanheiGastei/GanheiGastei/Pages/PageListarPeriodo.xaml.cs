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
    public partial class PageListarPeriodo : ContentPage
    {
        public PageListarPeriodo(DateTime inicio, DateTime fim)
        {
            InitializeComponent();

            datePickerFIm.Date = fim;
            datePickerInicio.Date = inicio;

            datePickerFIm.MinimumDate = datePickerFIm.Date;
            datePickerInicio.MaximumDate = datePickerInicio.Date;

            CarregarDados();

        }

        

        private void CarregarDados()
        {
            try
            {
                ListViewInfoInicial.BeginRefresh();

                var dados = new GanhoGastoBD().GetPeriodoAsync(datePickerInicio.Date.Date, datePickerFIm.Date).Result;

                if (dados?.Count > 0)
                {
                    ListViewInfoInicial.ItemsSource = dados;
                    ListViewInfoInicial.IsVisible = true;
                    labelNadaNestePeriodo.IsVisible = false;

                }
                else
                {
                    ListViewInfoInicial.IsVisible = false;
                    labelNadaNestePeriodo.IsVisible = true;
                }

                ListViewInfoInicial.EndRefresh();

            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        private async void GridInfo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var linha = e.Item as GanhoGasto;

                await PopupNavigation.PushAsync(new PageNovoValor(linha, (ee, ss) => { CarregarDados(); }));

            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private void datePickerInicio_DateSelected(object sender, DateChangedEventArgs e)
        {
            CarregarDados();
            datePickerFIm.MinimumDate = datePickerFIm.Date;
        }

        private void datePickerFIm_DateSelected(object sender, DateChangedEventArgs e)
        {
            CarregarDados();
            datePickerInicio.MaximumDate = datePickerInicio.Date;
        }
    }
}