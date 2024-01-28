using AppAprovador.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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
    }
}