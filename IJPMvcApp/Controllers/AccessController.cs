using AccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace IJPMvcApp.Controllers
{
    public class AccessController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5280/api/AccessController") };
        // GET: AccessController
        public async  Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<AspNetUserRole> userRoles = await client.GetFromJsonAsync<List<AspNetUserRole>>("");
            return View();
        }
        public async Task<ActionResult> IndexRoles()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<AspNetRole> userRoles = await client.GetFromJsonAsync<List<AspNetRole>>("");
            return View();
        }

        // GET: AccessController/Details/5
        public async Task<ActionResult> UserDetails(string id)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id);
            return View(userRole);
        }
        public async Task<ActionResult> RoleDetails(string id)
        {
            AspNetRole Role = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role);
        }
        // GET: AccessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserRole(AspNetUserRole userRole)
        {
            try
            {
                await client.PostAsJsonAsync("", userRole);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> CreateRole(AspNetRole Role)
        {
            try
            {
                await client.PostAsJsonAsync("", Role);
                return RedirectToAction(nameof(IndexRoles));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Edit/5
        public async Task<ActionResult> EditRole(string id)
        {
            AspNetRole Role = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role);
        }

        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(string id, AspNetRole Role)
        {
            try
            {
                await client.PutAsJsonAsync(id, Role);
                return RedirectToAction(nameof(IndexRoles));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Delete/5
        public async Task<ActionResult> DeleteUserRole(string id, string role)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id +"/"+role);
            return View(userRole);
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserRole(string id, string role, AspNetUserRole userRole)
        {
            try
            {
                await client.DeleteAsync(""+id + "/" + role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteRole(string id)
        {
            AspNetRole Role = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role);
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRole(string id, AspNetUserRole userRole)
        {
            try
            {
                await client.DeleteAsync("" + id);
                return RedirectToAction(nameof(IndexRoles));
            }
            catch
            {
                return View();
            }
        }
    }
}
