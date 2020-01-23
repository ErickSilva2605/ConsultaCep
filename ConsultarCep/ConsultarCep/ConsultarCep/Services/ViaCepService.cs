using System.Net;
using ConsultarCep.Model;
using Newtonsoft.Json;

namespace ConsultarCep.Services
{
    public class ViaCepService
    {
        private static readonly string EnderecoURL = "https://viacep.com.br/ws/{0}/json/";

        public static EnderecoModel BuscarEndereco(string cep)
        {
            string url = string.Format(EnderecoURL, cep);

            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(url);

            EnderecoModel endereco = JsonConvert.DeserializeObject<EnderecoModel>(conteudo);

            if (endereco.cep == null)
                return null;

            return endereco;   
        }
    }
}
