using GanheiGastei.Model;
using GanheiGastei.Model.ModelDB;
using Plugin.Share;
using Plugin.Share.Abstractions;
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
    public partial class PageMenu : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PageMenu()
        {
            InitializeComponent();

        }

        private async void buttonCompartilhar_Clicked(object sender, EventArgs e)
        {
            var message = new ShareMessage();
            message.Url = "http://www.ganheigastei.com/downloadapp";
            message.Title = "Ganhei Gastei";
            message.Text = "Uma forma simples de anotar tudo que entra e sai da sua carteira.. e lembrar depois!";

            await CrossShare.Current.Share(message);

            await PopupNavigation.PopAsync(true);
            
        }

        private async void buttonSobre_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageSobre(), false);
            await PopupNavigation.PopAsync(true);
        }
    }
}