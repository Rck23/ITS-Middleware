using ITS_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITS_Middleware.Tools;
using ITS_Middleware.Models.Entities;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ITS_Middleware.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        public MiddlewareDbContext _context;

        public AuthController(MiddlewareDbContext master, ILogger<AuthController> logger)
        {
            _context = master;
            _logger = logger;
        }


        public IActionResult Login()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userEmail")))
                {
                    return View();
                }
                ViewBag.email = HttpContext.Session.GetString("userEmail");
                return RedirectToAction("Home", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString().Trim());
                return Json("Error");
            }
        }



        /*      Methods Requests        */
        //Autenticacion de credenciales
        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                var user = _context.Usuarios.Where(foundUser => foundUser.email == email);
                if (user.Any())
                {
                    if (user.Where(s => s.email == email && s.pass == pass).Any())
                    {
                        HttpContext.Session.SetString("userEmail", email);
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        ViewBag.msg = "Invalid Password";
                        return View("Login");
                    }
                }
                ViewBag.msg = "User not found";
                return View("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString().Trim());

                return Json("Error");
            }
            
        }

        


        //Clear session
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("userEmail");
                return View("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString().Trim());
                return Json("Error");
            }
        }


        // Registrar usuario
        public IActionResult Registro()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro([Bind("nombre,fechaAlta,puesto,email,pass")] Usuario user)
        {
            
            
            try
            {
                if (ModelState.IsValid)
                {
                    var p = user.pass;
                    Encrypt.GetSHA256(p);

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Home", "Home");
                }
            }
            catch (Exception ex)
            {

                string value = ex.Message.ToString();
                Console.Write(value); 
            }
        

            return View(user);
        }



        //Editar usuario
        public IActionResult Edit()
        {

            return View();
        }


        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("nombre,puesto,email,pass")] Usuario user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users", "Home");
            }


            return View(user);
        }


        //Eliminar usuario
        public async Task<IActionResult> Eliminar(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Usuarios.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        //Get 
       /* public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }*/

        
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {

            var usuar = await _context.Usuarios.FindAsync(id);
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            _context.Usuarios.Remove(usuar);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            await _context.SaveChangesAsync();
            return RedirectToAction("Users", "Home");
          
        }



        private bool UsuarioExiste(int id)
        {
            return _context.Usuarios.Any(e => e.id == id);
        }
    }
}
