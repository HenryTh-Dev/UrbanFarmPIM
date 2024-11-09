using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;

[Route("Farm")]
public class FarmController : Controller
{
    private readonly FarmContext _context;

    public FarmController(FarmContext context)
    {
        _context = context;
    }

    // GET: Farm
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Farms.Include(f => f.PlantingAreas).ToListAsync());
    }

    // GET: Farm/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var farm = await _context.Farms
            .Include(f => f.PlantingAreas)
            .FirstOrDefaultAsync(f => f.FarmId == id);

        if (farm == null) return NotFound();

        return View(farm);
    }

    // GET: Farm/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Farm/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FarmId,Name,Location")] Farm farm)
    {

        if (ModelState.IsValid)
        {
            _context.Add(farm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(farm);
    }

    // GET: Farm/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var farm = await _context.Farms.FindAsync(id);
        if (farm == null) return NotFound();

        return View(farm);
    }

    // POST: Farm/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FarmId,Name,Location")] Farm farm)
    {
        if (id != farm.FarmId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(farm);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmExists(farm.FarmId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(farm);
    }

    // GET: Farm/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var farm = await _context.Farms.FirstOrDefaultAsync(f => f.FarmId == id);
        if (farm == null) return NotFound();

        return View(farm);
    }

    // POST: Farm/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var farm = await _context.Farms.FindAsync(id);
        _context.Farms.Remove(farm);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FarmExists(int id)
    {
        return _context.Farms.Any(e => e.FarmId == id);
    }
}
