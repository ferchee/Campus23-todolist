using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Model
{
    public class ListEntry
    {
        public ListEntry(string title, string description, DateTime deadline, TimeSpan duration)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
        }

        public ListEntry()
        {

        }

        public int ID { get; set; }

        public string? Title { get;  set; }   

        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }
    }
}
