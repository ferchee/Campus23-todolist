using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TodoListApp.Model;

namespace TodoListApp.Data.Local
{
    public class TodoListJsonContext
    {
        private const string path = "./Data/DataFiles/items.json";

        private IList<ListEntry>? listEntries;

        public TodoListJsonContext()
        {
            
        }

        public IList<ListEntry> ListEntries => listEntries ?? GetListEntries();



        public async Task Save()
        {
            await File.WriteAllTextAsync(path, string.Empty);

            string json = JsonSerializer.Serialize(ListEntries);

            await File.WriteAllTextAsync(path, json);
        }

        private IList<ListEntry> GetListEntries()
        {
            try
            {
                string json = File.ReadAllText(path);
                var list = JsonSerializer.Deserialize<List<ListEntry>>(json);
                if (list != null)
                {
                    listEntries = list;
                }
                else
                {
                    listEntries = new List<ListEntry>();
                }
                return listEntries; 
            }
            catch (JsonException)
            {
                listEntries = new List<ListEntry>();
                return listEntries;
            }
        }
    }
}
