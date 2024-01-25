using AmoebaProject.DAL;
using Microsoft.EntityFrameworkCore;

namespace AmoebaProject.Services
{
    public class _LayoutService
    {
        private readonly AppDbContext _context;

        public _LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingAsync()
        {
            Dictionary<string, string> setting = await _context.Settings.ToDictionaryAsync(c => c.Key, c => c.Value);
            return setting;
        }
    }
}
