using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarlosAriel20250324.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarlosAriel20250324.Controllers
{
    public class UsersController : Controller
    {
        private readonly Test20250324DbContext _context;

        public UsersController(Test20250324DbContext context)
        {
            _context = context;
        }

        // GET: Users/Index
        public async Task<IActionResult> Index(string? username, string? email, string? role, int topRegistro = 10)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(username))
                query = query.Where(u => u.Username.Contains(username));
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(u => u.Email.Contains(email));
            if (!string.IsNullOrWhiteSpace(role))
                query = query.Where(u => u.Role.Contains(role));
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            return View(await query.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Email,Password,Role,Notes")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Password,Role,Notes")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Verifica si un usuario existe
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: Users/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            // Verifica el usuario en la base de datos
            var userAuth = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (userAuth != null)
            {
                // Aquí deberías usar un mecanismo más seguro para la contraseña, como hashing
                var claims = new[] {
                    new Claim(ClaimTypes.Name, userAuth.Email),
                    new Claim("Id", userAuth.Id.ToString()),
                    new Claim("Username", userAuth.Username),
                    new Claim(ClaimTypes.Role, userAuth.Role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Home");
            }

            // Si no encuentra el usuario, muestra un mensaje de error
            ModelState.AddModelError("", "El email o la contraseña son incorrectos.");
            return View();
        }

        // GET: Users/Logout
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
