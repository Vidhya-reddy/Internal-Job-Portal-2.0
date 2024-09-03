
using Azure;
using IJPMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace IJPMvcApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccessController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5280/api/Access/") };
        // GET: AccessController
        public async  Task<ActionResult> Index()
        {
            List<AspNetUserRole> userRoles = await client.GetFromJsonAsync<List<AspNetUserRole>>("");
            return View(userRoles);
        }
        public async Task<ActionResult> IndexRoles()
        {
           
            List<AspNetRole> Roles = await client.GetFromJsonAsync<List<AspNetRole>>("Roles");
            return View(Roles);
        }

        // GET: AccessController/Details/5
        
        public async Task<ActionResult> UserDetails(string id, string role)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id +"/"+role);
            return View(userRole);
        }
        public async Task<ActionResult> RoleDetails(string id)
        {
            AspNetRole Role = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role);
        }
        // GET: AccessController/Create
        
        public ActionResult CreateUserRole()
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
                string[] user = userRole.UserId.Split(' ');
                string[] role = userRole.RoleId.Split(' ');
                userRole.UserId = user[0];
                userRole.RoleId = role[0];
                userRole.RoleName = role[1];
                userRole.UserName = user[1];
                await client.PostAsJsonAsync("", userRole);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       // [Route("Access/CreateRole/")]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(AspNetRole Role)
        {
             var response = await client.PostAsJsonAsync("Role/", Role);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errMsg);
            }
        }

        // GET: AccessController/Edit/5
        
        [Route("Access/EditRole/{id}")]
        public async Task<ActionResult> EditRole(string id)
        {
            AspNetRole Role1 = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role1);
        }

        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Access/EditRole/{id}")]
        public async Task<ActionResult> EditRole(string id, string Role,AspNetRole role)
        {
            try
            {
                await client.PutAsJsonAsync(id,role.Name);
                return RedirectToAction(nameof(IndexRoles));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccessController/Delete/5
        [Route("Access/DeleteUserRole/{id}/{role}")]
        public async Task<ActionResult> DeleteUserRole(string id, string role)
        {
            AspNetUserRole userRole = await client.GetFromJsonAsync<AspNetUserRole>("" + id +"/"+role);
            return View(userRole);
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Access/DeleteUserRole/{id}/{role}")]
        public async Task<ActionResult> DeleteUserRole(string id, string role, AspNetUserRole userRole)
        {
            
            var response = await client.DeleteAsync("" + id + "/" + role);
            if(response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errMsg);    
            }
            
        }

        [Route("Access/DeleteRole/{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
           AspNetRole Role = await client.GetFromJsonAsync<AspNetRole>("" + id);
            return View(Role);
        }

        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Access/DeleteRole/{id}")]
        public async Task<ActionResult> DeleteRole(string id, AspNetUserRole userRole)
        {
            try
            {
                await client.DeleteAsync("Role/" + id);
                return RedirectToAction(nameof(IndexRoles));
            }
            catch
            {
                return View();
            }
        }
    }
}
