using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanFarm.Models;
using System.Threading.Tasks;
using System.Linq;

[Route("Resource")]
public class ResourceController : Controller
{
    private readonly FarmContext _context;

    public ResourceController(FarmContext context)
    {
        _context = context;
    }

    // GET: Resource
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Resources.ToListAsync());
    }

    // GET: Resource/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources
            .Include(r => r.Plantings)
            .FirstOrDefaultAsync(r => r.ResourceId == id);

        if (resource == null) return NotFound();

        return View(resource);
    }

    // GET: Resource/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Resource/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ResourceId,Name,Type,Description,Quantity,Price")] Resource resource, IFormFile imageFile)
    {

            var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            resource.ImagePath = "/images/" + imageFile.FileName;
        

        _context.Add(resource);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Resource/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources.FindAsync(id);
        if (resource == null) return NotFound();

        return View(resource);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ResourceId,Name,Type,Description,Quantity,Price,ImagePath")] Resource resource, IFormFile imageFile)
    {
        if (id != resource.ResourceId)
        {
            return NotFound();
        }

        if (imageFile != null && imageFile.Length > 0)
        {
            var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            resource.ImagePath = "/images/" + imageFile.FileName;
        }

        try
        {
            _context.Update(resource);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ResourceExists(resource.ResourceId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }


    // GET: Resource/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.ResourceId == id);

        if (resource == null) return NotFound();

        return View(resource);
    }

    // POST: Resource/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        _context.Resources.Remove(resource);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ResourceExists(int id)
    {
        return _context.Resources.Any(e => e.ResourceId == id);
    }
}
