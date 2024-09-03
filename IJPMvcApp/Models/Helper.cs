using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace IJPMvcApp.Models
{
    public class Helper
    { 
        IHttpContextAccessor accessor = new HttpContextAccessor();
        public string GetToken()
        {
            string token = accessor.HttpContext.Session.GetString("token");
            return token;
        }

        public static async Task<List<SelectListItem>> GetJobs()
        {
            List<SelectListItem> jobIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token =obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Job> jobs = await client.GetFromJsonAsync<List<Job>>("");
            foreach (Job job in jobs)
            {
                jobIds.Add(new SelectListItem { Text = $"{job.JobId} : {job.JobTitle}", Value = job.JobId  });
            }
            return jobIds;
        }
        public static async Task<List<SelectListItem>> GetSkills()
        {
            List<SelectListItem> skillIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/SkillSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Skill> skills = await client.GetFromJsonAsync<List<Skill>>("");
            foreach (Skill skill in skills)
            {
                skillIds.Add(new SelectListItem { Text = $"{skill.SkillId} : {skill.SkillName}", Value = skill.SkillId });
            }
            return skillIds;
        }
        public static async Task<List<SelectListItem>> GetEmployees()
        {
            List<SelectListItem> employeeIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/EmployeeSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            foreach (Employee employee in employees)
            {
                employeeIds.Add(new SelectListItem { Text = $"{employee.EmpId} : {employee.EmpName}", Value = employee.EmpId });
            }
            return employeeIds;
        }
        public static async Task<List<SelectListItem>> GetJobposts()
        {
            List<SelectListItem> postIds = new List<SelectListItem>();
            Helper obj = new Helper();
            string token = obj.GetToken();
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5003/JobPostSvc/") };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<JobPost> JobPosts = await client.GetFromJsonAsync<List<JobPost>>("");
            foreach (JobPost jobPost in JobPosts)
            {
                postIds.Add(new SelectListItem { Text = $"{jobPost.PostId}", Value = $"{jobPost.PostId}" });
            }
            return postIds;
        }
        public static async Task<List<SelectListItem>> GetStatus()
        {
             List<SelectListItem> status = new List<SelectListItem>();

             status.Add(new SelectListItem { Text = "Reviewing", Value = "Reviewing" });
             status.Add(new SelectListItem { Text = "Accepted", Value = "Accepted" });
             status.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
             return status;
        }
        public enum SkillLevel
        {
            Beginner= 'B',
            Intermediate = 'I',
            Advanced = 'A'

        }
        public static async Task<List<SelectListItem>> GetSkillLevel()
        {
            return Enum.GetValues(typeof(SkillLevel))
                .Cast<SkillLevel>()
                .Select(e => new SelectListItem
                {
                    Value = ((char)e).ToString(), // Convert enum to char and then to string
                    Text = e.ToString()
                })
                .ToList();
        }

        public static string GetSkillLevelName(char skillLevelChar)
        {
            foreach (SkillLevel level in Enum.GetValues(typeof(SkillLevel)))
            {
                if ((char)level == skillLevelChar)
                {
                    return level.ToString();
                }
            }
            return "Unknown";
        }

        public enum SkillCategory
        {
            Database = 1,
            Programming = 2,
            Networking = 3,
            Security = 4,
            Cloud=5
        }
       
        public static async Task<List<SelectListItem>> GetSkillCategories()
        {
            return Enum.GetValues(typeof(SkillCategory))
                .Cast<SkillCategory>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(), 
                    Text = e.ToString() 
                })
                .ToList();
        }


        public static string GetSkillCategoryName(int skillCategoryValue)
        {
            if (Enum.IsDefined(typeof(SkillCategory), skillCategoryValue))
            {
                return ((SkillCategory)skillCategoryValue).ToString();
            }
            return "Unknown";
        }

    }
}
