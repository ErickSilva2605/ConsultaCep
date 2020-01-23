using System;
using System.ComponentModel;
using Xamarin.Forms;
using ConsultarCep.Model;
using ConsultarCep.Services;

namespace ConsultarCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            #region Eventos
            BtnBuscar.Clicked += BuscarCep;
            #endregion
        }

        #region Métodos
        private void BuscarCep(Object sender, EventArgs args)
        {
            if ((EtrCep.Text != null))
            {
                string cep = EtrCep.Text.Trim();

                if (IsValidCep(cep))
                {
                    try
                    {
                        EnderecoModel end = ViaCepService.BuscarEndereco(cep);

                        if (end != null)
                        {
                            LblEndereco.Text = $"Endereço: {end.bairro}, {end.localidade}, {end.uf}, {end.logradouro}";
                        }
                        else
                        {
                            DisplayAlert("Oops..", $"O endereço não foi encontrado para o CEP informado: {cep}.", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Servidor Instavel", ex.Message, "OK");
                    }
                }
            }
            else
            {
                DisplayAlert("Oops..", "Informe o CEP.", "OK");
            }
            
        }

        private bool IsValidCep(string cep)
        {
            if (cep.Length != 8)
            {
                DisplayAlert("CEP INVÁLIDO!", "O CEP deve possuir 8 Digitos.", "OK");
                return false;
            }

            if (!int.TryParse(cep, out int novoCep))
            {
                DisplayAlert("CEP INVÁLIDO!", "O CEP deve composto apenas por números.", "OK");
                return false;
            }

            return true;
        }
        #endregion
    }
}
