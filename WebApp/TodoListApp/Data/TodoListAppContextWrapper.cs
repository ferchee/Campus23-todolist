using Microsoft.EntityFrameworkCore;
using TodoListApp.Data.Local;
using TodoListApp.Data.Remote;
using TodoListApp.Model;

namespace TodoListApp.Data
{
    public class TodoListAppContextWrapper
    {
        private readonly TodoListRemoteDbContext? _todoListRemoteDbContext;
        private readonly TodoListJsonContext? _todoListJsonContext;

        public TodoListAppContextWrapper(TodoListRemoteDbContext todoListRemoteDbContext)
        {
            _todoListRemoteDbContext = todoListRemoteDbContext;
            //_todoListJsonContext = todoListJsonContext;            
        }

        public async Task Create(ListEntry listEntry)
        {
            if (_todoListRemoteDbContext != null)
            {
                _todoListRemoteDbContext.ListEntry.Add(listEntry);
                await _todoListRemoteDbContext.SaveChangesAsync();
            }
            else if (_todoListJsonContext != null)
            {
                _todoListJsonContext.ListEntries.Add(listEntry);
                await _todoListJsonContext.Save();
            }
        }

        public async Task<bool> Remove(int? id)
        {
            var entries = await ListEntries;
            var listEntry = entries.FirstOrDefault(m => m.ID == id);

            if (listEntry == null)
            {
                return false;
            }

            if (_todoListRemoteDbContext != null)
            {
                try
                {
                    _todoListRemoteDbContext.ListEntry.Remove(listEntry);
                    await _todoListRemoteDbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_todoListRemoteDbContext.ListEntry.Any(e => e.ID == listEntry.ID))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else if (_todoListJsonContext != null)
            {
                _todoListJsonContext.ListEntries.Remove(listEntry);
                await _todoListJsonContext.Save();
                return true;
            }

            return false;
        }

        public async Task<bool> Attach(ListEntry listEntry)
        {
            if (_todoListRemoteDbContext != null)
            {
                _todoListRemoteDbContext.Attach(listEntry).State = EntityState.Modified;

                try
                {
                    await _todoListRemoteDbContext.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_todoListRemoteDbContext.ListEntry.Any(e => e.ID == listEntry.ID))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else if (_todoListJsonContext != null)
            {
                _todoListJsonContext.ListEntries.Add(listEntry);
                await _todoListJsonContext.Save();
                return true;
            }

            return false;
        }

        public Task<IList<ListEntry>> ListEntries => GetListEntries();

        private async Task<IList<ListEntry>> GetListEntries()
        {
            if (_todoListRemoteDbContext != null)
            {
                return await _todoListRemoteDbContext.ListEntry.ToListAsync();
            }
            else if (_todoListJsonContext != null)
            {
                return _todoListJsonContext.ListEntries;
            }

            throw new InvalidOperationException("Context is not set");
        }

    }
}
