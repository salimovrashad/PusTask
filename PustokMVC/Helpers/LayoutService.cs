using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;

namespace PustokMVC.Helpers
{
    public class LayoutService
    {
        PustokDBContext _db { get; }

        public LayoutService(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<Setting> GetSettingsAsync()
        => await _db.Settings.FindAsync(1);
    }
}
