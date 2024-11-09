using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("PlantingArea")]
public class PlantingAreaController : Controller
{
    private readonly FarmContext _context;

    public PlantingAreaController(FarmContext context)
    {
        _context = context;
    }

    // GET: PlantingArea
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var plantingAreas = _context.PlantingAreas.Include(p => p.Farm);
        return View(await plantingAreas.ToListAsync());
    }

    // GET: PlantingArea/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var plantingArea = await _context.PlantingAreas
            .Include(p => p.Farm)
            .Include(p => p.Plantings)
            .FirstOrDefaultAsync(p => p.PlantingAreaId == id);

        if (plantingArea == null) return NotFound();

        return View(plantingArea);
    }

    // GET: PlantingArea/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Name");
        return View();
    }

    // POST: PlantingArea/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlantingAreaId,Name,Size,FarmId")] PlantingArea plantingArea)
    {
        if (ModelState.IsValid)
        {
            _context.Add(plantingArea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Name", plantingArea.FarmId);
        return View(plantingArea);
    }

    // GET: PlantingArea/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var plantingArea = await _context.PlantingAreas.FindAsync(id);
        if (plantingArea == null) return NotFound();

        ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Name", plantingArea.FarmId);
        return View(plantingArea);
    }

    // POST: PlantingArea/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PlantingAreaId,Name,Size,FarmId")] PlantingArea plantingArea)
    {
        if (id != plantingArea.PlantingAreaId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(plantingArea);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantingAreaExists(plantingArea.PlantingAreaId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["FarmId"] = new SelectList(_context.Farms, "FarmId", "Name", plantingArea.FarmId);
        return View(plantingArea);
    }

    // GET: PlantingArea/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var plantingArea = await _context.PlantingAreas
            .Include(p => p.Farm)
            .FirstOrDefaultAsync(p => p.PlantingAreaId == id);

        if (plantingArea == null) return NotFound();

        return View(plantingArea);
    }

    // POST: PlantingArea/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var plantingArea = await _context.PlantingAreas.FindAsync(id);
        _context.PlantingAreas.Remove(plantingArea);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlantingAreaExists(int id)
    {
        return _context.PlantingAreas.Any(e => e.PlantingAreaId == id);
    }
}
