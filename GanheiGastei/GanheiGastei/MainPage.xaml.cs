using Android.Widget;
using GanheiGastei.Model.ModelDB;
using GanheiGastei.Model.NoDB;
using GanheiGastei.Pages;
using GanheiGastei.Util;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GanheiGastei
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            CarregarDadosIniciais();

            BindingContext = this;


            if (Device.RuntimePlatform == Device.Android)
            {
                adMobView.AdUnitId = "ca-app-pub-5541916824987072/9356117636";
            }

            else if (Device.RuntimePlatform == Device.iOS)
            {
                adMobView.AdUnitId = "SEU ID iOS";
            }

        }

        private void CarregarDadosIniciais()
        {
            try
            {
                var ListaInfo = new List<InformacaoInicial>();

                var hoje = DateTime.Now;

                #region Mês Passado

                var diaInicialMesPasado = new DateTime(hoje.Year, hoje.AddMonths(-1).Month, 1);
                var diaFinalMesPassado = diaInicialMesPasado.AddMonths(1);

                var dadoMesPassado = new GanhoGastoBD().GetPeriodoAsync(diaInicialMesPasado, diaFinalMesPassado).Result;

                var ganheiMesPassado = dadoMesPassado?.Where(n => n.Entrada)?.Sum(n => n.Valor) ?? 0;
                var gasteiMesPassado = dadoMesPassado?.Where(n => !n.Entrada)?.Sum(n => n.Valor) ?? 0;

                ListaInfo.Add(new InformacaoInicial
                {

                    Descricao = "Mês Passado",
                    ValorGanhei = ganheiMesPassado,
                    ValorGastei = gasteiMesPassado,
                    Saldo = ganheiMesPassado + gasteiMesPassado,
                    DataInicio = diaInicialMesPasado,
                    DataFim = diaFinalMesPassado

                });
                #endregion

                #region Este Mês

                var diaInicialMes = new DateTime(hoje.Year, hoje.Month, 1);
                var diaFinalMes = diaInicialMes.AddMonths(1);

                var dadoMes = new GanhoGastoBD().GetPeriodoAsync(diaInicialMes, diaFinalMes).Result;

                var ganheiMes = dadoMes?.Where(n => n.Entrada)?.Sum(n => n.Valor) ?? 0;
                var gasteiMes = dadoMes?.Where(n => !n.Entrada)?.Sum(n => n.Valor) ?? 0;

                ListaInfo.Add(new InformacaoInicial
                {

                    Descricao = "Este mês",
                    ValorGanhei = ganheiMes,
                    ValorGastei = gasteiMes,
                    Saldo = ganheiMes + gasteiMes,
                    DataInicio = diaInicialMes,
                    DataFim = diaFinalMes

                });
                #endregion

                #region Esta Semana
                int diaAtualDaSemana = DataUtil.GetIso8601WeekOfYear(DateTime.Today);

                var diaInicialSemana = DataUtil.FirstDateOfWeek(DateTime.Now.Year, diaAtualDaSemana, CultureInfo.CurrentCulture);
                var diaFinalSemana = diaInicialSemana.AddDays(6);

                var dadoSemana = new GanhoGastoBD().GetPeriodoAsync(diaInicialSemana, diaFinalSemana).Result;


                var ganheiSemana = dadoSemana?.Where(n => n.Entrada)?.Sum(n => n.Valor) ?? 0;
                var gasteiSemana = dadoSemana?.Where(n => !n.Entrada)?.Sum(n => n.Valor) ?? 0;

                ListaInfo.Add(new InformacaoInicial
                {

                    Descricao = "Esta semana",
                    ValorGanhei = ganheiSemana,
                    ValorGastei = gasteiSemana,
                    Saldo = ganheiSemana + gasteiSemana,
                    DataInicio = diaInicialSemana,
                    DataFim = diaFinalSemana

                });
                #endregion

                #region Hoje


                var dadosDia = new GanhoGastoBD().GetPeriodoAsync(hoje.Date, hoje).Result;

                var ganheiDia = dadosDia?.Where(n => n.Entrada)?.Sum(n => n.Valor) ?? 0;
                var gasteiDia = dadosDia?.Where(n => !n.Entrada)?.Sum(n => n.Valor) ?? 0;

                ListaInfo.Add(new InformacaoInicial
                {

                    Descricao = "Hoje",
                    ValorGanhei = ganheiDia,
                    ValorGastei = gasteiDia,
                    Saldo = ganheiDia + gasteiDia,
                    DataInicio = hoje.Date,
                    DataFim = hoje.AddDays(1)

                });

                #endregion

                ListViewInfoInicial.ItemsSource = ListaInfo;


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
                var linha = e.Item as InformacaoInicial;

                var pagelistarperiodo = new PageListarPeriodo(linha.DataInicio, linha.DataFim);
                pagelistarperiodo.Disappearing += (ss, ee) => { CarregarDadosIniciais(); };

                await Navigation.PushModalAsync(pagelistarperiodo, false);

                CarregarDadosIniciais();

            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private async void buttonGanhei_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.PushAsync(new PageNovoValor(true, (ee, ss) => { CarregarDadosIniciais(); }));

            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private async void buttonGastei_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.PushAsync(new PageNovoValor(false, (ee, ss) => { CarregarDadosIniciais(); }));
            }
            catch (Exception erro)
            {

                throw erro;
            }
        }

        private async void buttonMenu_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PageMenu());
        }
    }
}