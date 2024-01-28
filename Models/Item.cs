namespace AppAprovador.Models
{
    public class ListItem
    {
        public List<Item> Value { get; set; }
    }

    public class Item
    {
        //public Item(int id, string nome, string imagemURL, string preco, string descricao, string status)
        //{
        //    Id = id;
        //    Nome = nome;
        //    ImagemURL = imagemURL;
        //    Preco = preco;
        //    Descricao = descricao;
        //    Status = status;
        //}

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Nome { get; set; }
        public string ImagemURL { get; set; }
        public string Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
    }

}
