using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Picker
{
    public class MainViewModel : BaseViewModel
    {
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string item;

        public string Item
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }


        public Command CarregarCommand { get; }

        public Command SelecionarCommand { get; }

        public ObservableCollection<string> Itens { get; }



        public MainViewModel()
        {
            Itens             = new ObservableCollection<string>();
            CarregarCommand   = new Command(async () => await ExecuteCarregarCommand(), () => !isBusy);
            SelecionarCommand = new Command(async () => await ExecuteSelecionarCommand(), () => !IsBusy);

        }

        async Task ExecuteSelecionarCommand()
        {

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await DisplayAlert("Selecionado", $"Você clicou em: {Item.ToString()}");
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Erro", $"Erro:{ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return;
        }

        async Task ExecuteCarregarCommand()
        {

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var minhaLista = await Servico.CarregarString();
                    foreach (var item in minhaLista)
                    {
                        Itens.Add(item);
                    }

                }
                catch (Exception ex)
                {

                    await DisplayAlert("Erro", $"Erro:{ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return;
        }
    }



    public class Servico
    {
        public static async Task<List<string>> CarregarString()
        {
            var lista = new List<string> { "oi", "olá", "tudo bem?" };

            //Simula uma requisição Http
            await Task.Delay(6000);
            return lista;
        }
    }
}
