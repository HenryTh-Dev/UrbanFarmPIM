using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("Planting")]
public class PlantingController : Controller
{
    private readonly FarmContext _context;

    public PlantingController(FarmContext context)
    {
        _context = context;
    }

    // GET: Planting
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var plantings = _context.Plantings
            .Include(p => p.PlantingArea)
            .Include(p => p.Resource)
            .Include(p => p.Employees);
        return View(await plantings.ToListAsync());
    }

    // GET: Planting/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var planting = await _context.Plantings
            .Include(p => p.PlantingArea)
            .Include(p => p.Resource)
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.PlantingId == id);

        if (planting == null) return NotFound();

        return View(planting);
    }

    // GET: Planting/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewData["PlantingAreaId"] = new SelectList(_context.PlantingAreas, "PlantingAreaId", "Name");
        ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "Name");
        ViewData["Employees"] = new MultiSelectList(_context.Employees, "EmployeeId", "Name");
        return View();
    }

    // POST: Planting/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PlantingId,PlantingDate,PlantingAreaId,ResourceId,Employees")] Planting planting)
    {
        if (ModelState.IsValid)
        {
            _context.Add(planting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PlantingAreaId"] = new SelectList(_context.PlantingAreas, "PlantingAreaId", "Name", planting.PlantingAreaId);
        ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "Name", planting.ResourceId);
        ViewData["Employees"] = new MultiSelectList(_context.Employees, "EmployeeId", "Name", planting.Employees.Select(e => e.EmployeeId));
        return View(planting);
    }

    // GET: Planting/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var planting = await _context.Plantings
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.PlantingId == id);
        if (planting == null) return NotFound();

        ViewData["PlantingAreaId"] = new SelectList(_context.PlantingAreas, "PlantingAreaId", "Name", planting.PlantingAreaId);
        ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "Name", planting.ResourceId);
        ViewData["Employees"] = new MultiSelectList(_context.Employees, "EmployeeId", "Name", planting.Employees.Select(e => e.EmployeeId));
        return View(planting);
    }

    // POST: Planting/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PlantingId,PlantingDate,PlantingAreaId,ResourceId,Employees")] Planting planting)
    {
        if (id != planting.PlantingId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(planting);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantingExists(planting.PlantingId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["PlantingAreaId"] = new SelectList(_context.PlantingAreas, "PlantingAreaId", "Name", planting.PlantingAreaId);
        ViewData["ResourceId"] = new SelectList(_context.Resources, "ResourceId", "Name", planting.ResourceId);
        ViewData["Employees"] = new MultiSelectList(_context.Employees, "EmployeeId", "Name", planting.Employees.Select(e => e.EmployeeId));
        return View(planting);
    }

    // GET: Planting/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var planting = await _context.Plantings
            .Include(p => p.PlantingArea)
            .Include(p => p.Resource)
            .FirstOrDefaultAsync(p => p.PlantingId == id);
        if (planting == null) return NotFound();

        return View(planting);
    }

    // POST: Planting/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var planting = await _context.Plantings.FindAsync(id);
        _context.Plantings.Remove(planting);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlantingExists(int id)
    {
        return _context.Plantings.Any(e => e.PlantingId == id);
    }
}
