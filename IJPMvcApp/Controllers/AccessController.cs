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
            List<AspNetUserRole> userRoles = await client.GetFromJsonAsync<List<AspNetUserRole>>("");
            return View();
        }

        // GET: AccessController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id);
            return View(userRole);
        }

        // GET: AccessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(AspNetUserRole userRole)
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

        // GET: AccessController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id);
            return View(userRole);
        }

        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, AspNetUserRole userRole)
        {
            try
            {
                await client.PutAsJsonAsync(id, userRole);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id);
            return View(userRole);
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, AspNetUserRole userRole)
        {
            try
            {
                await client.DeleteAsync(""+id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
