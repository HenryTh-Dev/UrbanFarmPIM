using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("Sale")]
public class SaleController : Controller
{
    private readonly FarmContext _context;

    public SaleController(FarmContext context)
    {
        _context = context;
    }

    // GET: Sale
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var sales = _context.Sales
            .Include(s => s.Client)
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Resource); // Inclui os recursos associados aos itens da venda
        return View(await sales.ToListAsync());
    }

    // GET: Sale/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var sale = await _context.Sales
            .Include(s => s.Client)
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Resource)
            .FirstOrDefaultAsync(s => s.SaleId == id);

        if (sale == null) return NotFound();

        return View(sale);
    }

    // GET: Sale/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Name");
        ViewData["Resources"] = new MultiSelectList(_context.Resources, "ResourceId", "Name"); // Seleção de recursos para itens de venda
        return View();
    }

    // POST: Sale/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SaleId,SaleDate,ClientId,TotalAmount")] Sale sale)
    {
            _context.Add(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }

    // GET: Sale/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var sale = await _context.Sales.FindAsync(id);
        if (sale == null) return NotFound();

        ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Name", sale.ClientId);
        return View(sale);
    }

    // POST: Sale/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SaleId,SaleDate,ClientId,TotalAmount")] Sale sale)
    {
        if (id != sale.SaleId) return NotFound();


            try
            {
                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(sale.SaleId)) return NotFound();
                throw;
            }   
            return RedirectToAction(nameof(Index));
       
    }

    // GET: Sale/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var sale = await _context.Sales
            .Include(s => s.Client)
            .FirstOrDefaultAsync(s => s.SaleId == id);

        if (sale == null) return NotFound();

        return View(sale);
    }

    // POST: Sale/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SaleExists(int id)
    {
        return _context.Sales.Any(e => e.SaleId == id);
    }
}
