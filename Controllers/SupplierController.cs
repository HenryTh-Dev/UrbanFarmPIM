using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;

[Route("Supplier")]
public class SupplierController : Controller
{
    private readonly FarmContext _context;

    public SupplierController(FarmContext context)
    {
        _context = context;
    }

    // GET: Supplier
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Suppliers.ToListAsync());
    }

    // GET: Supplier/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var supplier = await _context.Suppliers
            .Include(s => s.Resources)
            .FirstOrDefaultAsync(s => s.SupplierId == id);

        if (supplier == null) return NotFound();

        return View(supplier);
    }

    // GET: Supplier/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Supplier/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SupplierId,Name,CNPJ,Address,Phone")] Supplier supplier)
    {
        if (ModelState.IsValid)
        {
            _context.Add(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(supplier);
    }

    // GET: Supplier/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return NotFound();

        return View(supplier);
    }

    // POST: Supplier/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SupplierId,Name,CNPJ,Address,Phone")] Supplier supplier)
    {
        if (id != supplier.SupplierId) return NotFound();


            try
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(supplier.SupplierId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        
    }

    // GET: Supplier/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(s => s.SupplierId == id);

        if (supplier == null) return NotFound();

        return View(supplier);
    }

    // POST: Supplier/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SupplierExists(int id)
    {
        return _context.Suppliers.Any(e => e.SupplierId == id);
    }
}
