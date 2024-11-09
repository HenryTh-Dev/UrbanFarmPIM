using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UrbanFarm.Models;

[Route("Client")]
public class ClientController : Controller
{
    private readonly FarmContext _context;

    public ClientController(FarmContext context)
    {
        _context = context;
    }


    // GET: Client
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Clients.ToListAsync());
    }

    // GET: Client/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.ClientId == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // GET: Client/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Client/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClientId,Name,CNPJ,Address,Phone")] Client client)
    {
        if (ModelState.IsValid)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    // GET: Client/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }

    // POST: Client/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,CNPJ,Address,Phone")] Client client)
    {
        if (id != client.ClientId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.ClientId))
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
        return View(client);
    }

    // GET: Client/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.ClientId == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: Client/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClientExists(int id)
    {
        return _context.Clients.Any(e => e.ClientId == id);
    }
}
