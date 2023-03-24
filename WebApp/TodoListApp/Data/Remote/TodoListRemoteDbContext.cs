using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Model;

namespace TodoListApp.Data.Remote
{
    public class TodoListRemoteDbContext : DbContext
    {
        // https://www.npgsql.org/efcore/
        public TodoListRemoteDbContext(DbContextOptions<TodoListRemoteDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<ListEntry> ListEntry { get; set; } = default!;
    }
}
