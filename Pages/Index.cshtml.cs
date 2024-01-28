using AppAprovador.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace AppAprovador.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public ListItem Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            string StorageName = "rgchallenge04aafc";
            string StorageKey = "hWHecAcrdxjGChU4UYkSgZI5soMPP85v0V+sWuXhaHtz+vvDO2fqrPa3xrFv0tbMbm2Cb8SzvIJP+AStlBzcBQ==";

            string TableName = "vendas()";
            string jsonDataVendas;
            AzureTables.GetAllEntity(StorageName, StorageKey, TableName, out jsonDataVendas);
            ListItem ItemsVendas = JsonConvert.DeserializeObject<ListItem>(jsonDataVendas);

            TableName = "respostas()";
            string jsonDataRespostas;
            AzureTables.GetAllEntity(StorageName, StorageKey, TableName, out jsonDataRespostas);
            ListItem ItemsRespostas = JsonConvert.DeserializeObject<ListItem>(jsonDataRespostas);

            foreach(var item in ItemsRespostas.Value) 
            {
                ItemsVendas.Value.Remove(ItemsVendas.Value.FirstOrDefault(v => v.RowKey == item.RowKey));
            }

            Items = ItemsVendas;

        }

        public async Task<IActionResult> OnGetSendMessage(string id)
        {

            Item item = new Item(id, "Venda Aprovada", "respostas");

            string code = "QCUv2RzXnmA1AKlWmgFzeyIrEZtJUVZ6cjPM88MkpMaeAzFuPjYKjA==";
            string apiUrl = "https://challenge04vendas.azurewebsites.net/api/HttpTriggerGetData?code={code}";

            var client = new HttpClient();
            var uri = new Uri(apiUrl.Replace("{code}", code));
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var objAsJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri,content);

            if (response.IsSuccessStatusCode) 
            { 
                return Page();
            }
            else
            {
                throw new Exception("Erro ao enviar mensagem: " + response.StatusCode);
            }

        }

    }
}