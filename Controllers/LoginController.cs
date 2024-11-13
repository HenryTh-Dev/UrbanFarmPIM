// Controllers/LoginController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UrbanFarmPIM.Models;
using System.Threading.Tasks;

namespace UrbanFarmPIM.Controllers
{
    [Route("Login")]   
    
    public class LoginController : Controller
    {
        private readonly FarmContext _context;

        public LoginController(FarmContext context)
        {
            _context = context;
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        public async Task<IActionResult> Index(Account model)
        {

            // Busca o usuário no banco de dados pelo Email e Password
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == model.Email && a.Password == model.Password);

            if (account != null)
            {

                // Redireciona para a página inicial ou outro local
                return RedirectToAction("Index", "Farm");
            }
            else
            {
                // Exibe uma mensagem de erro se as credenciais estiverem incorretas
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }
        }



    }
}
