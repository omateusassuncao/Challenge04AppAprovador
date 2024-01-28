namespace AppAprovador.Models
{
    public class ListItem
    {
        public List<Item> Value { get; set; }
    }

    public class Item
    {
        public Item(string rowkey, string status, string partitioney)
        {
            RowKey = rowkey;
            Status = status;
            PartitionKey = partitioney;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Nome { get; set; }
        public string ImagemURL { get; set; }
        public string Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
    }

}
