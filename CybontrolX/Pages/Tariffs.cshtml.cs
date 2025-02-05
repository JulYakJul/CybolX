using CybontrolX.DataBase;
using CybontrolX.DBModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TariffsModel : PageModel
{
    private readonly AppDbContext _context;

    public TariffsModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Tariff> Tariffs { get; set; } = new List<Tariff>();

    public async Task OnGetAsync()
    {
        Tariffs = await _context.Tariffs.ToListAsync();
    }
}
