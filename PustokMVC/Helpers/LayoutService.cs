using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;

namespace PustokMVC.Helpers
{
    public class LayoutService
    {
        PustokDBContext _db { get; }
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(PustokDBContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Setting> GetSettingsAsync()
        => await _db.Settings.FindAsync(1);
        
        
    }
}
