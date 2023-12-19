using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SCRDesktopApp
{
    internal class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("managerId")]
        public int? ManagerId { get; set; }

        [JsonPropertyName("departmentId")]
        public int DepartmentId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
