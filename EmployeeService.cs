using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SCRDesktopApp
{
    internal class EmployeeService
    {
        static HttpClient client = new HttpClient();
        private string token;
        public void createConnection()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = null;

            // Set the Bearer token in the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            // Send the GET request
            HttpResponseMessage response = client.GetAsync("employee").Result;

            // Reset the Authorization header to its default state
            client.DefaultRequestHeaders.Authorization = null;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                
                employees = JsonSerializer.Deserialize<List<Employee>>(resultString);
                return employees;
            }

            return null;
        }

        public string login() {
            User user = new User("admin", "admin");

            // Serialize the User object to JSON
            string jsonUser = JsonSerializer.Serialize(user);
            Console.WriteLine(user.email+" "+user.password);
            // Create the request content with the serialized User object
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = client.PostAsync("auth/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                var resultObject = JsonSerializer.Deserialize<LoginResponse>(resultString);
                
                // Access the token property from the resultObject
                this.token = resultObject.Token;

                return this.token;
            }

            return null;
        }

        public List<Department> getDepartments()
        {
            List<Department> departments = null;

            // Set the Bearer token in the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            // Send the GET request
            HttpResponseMessage response = client.GetAsync("departments").Result;

            // Reset the Authorization header to its default state
            client.DefaultRequestHeaders.Authorization = null;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;

                departments = JsonSerializer.Deserialize<List<Department>>(resultString);
                return departments;
            }

            return null;
        }

        public List<Employee> getEmployeesPerDepartment(int departmentId)
        {
            List<Employee> employees = null;

            // Set the Bearer token in the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            // Send the GET request
            HttpResponseMessage response = client.GetAsync($"employee/department/{departmentId}").Result;

            // Reset the Authorization header to its default state
            client.DefaultRequestHeaders.Authorization = null;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;

                employees = JsonSerializer.Deserialize<List<Employee>>(resultString);
                return employees;
            }

            return null;
        }

        public List<Employee> getManagersPerDepartment(int departmentId)
        {
            List<Employee> employees = null;

            // Set the Bearer token in the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            // Send the GET request
            HttpResponseMessage response = client.GetAsync($"manager/department/{departmentId}").Result;

            // Reset the Authorization header to its default state
            client.DefaultRequestHeaders.Authorization = null;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;

                employees = JsonSerializer.Deserialize<List<Employee>>(resultString);
                return employees;
            }

            return null;
        }
    }
}
