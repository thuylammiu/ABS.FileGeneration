
using ABS.FileGeneration.Interfaces;
using ABS.FileGenerationAPI.ExampleData;

namespace ABS.FileGenerationAPI.Services
{
    internal class EmployeeFileDataProvider : IDataSourceProvider<Employee>
    {

        public IEnumerable<Employee> GetData()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "555-0101", Title = "Software Engineer" });
            employees.Add(new Employee { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "555-0102", Title = "Project Manager" });
            employees.Add(new Employee { Id = 3, Name = "Mike Johnson", Email = "mike.johnson@example.com", Phone = "555-0103", Title = "QA Engineer" });
            employees.Add(new Employee { Id = 4, Name = "Emily Davis", Email = "emily.davis@example.com", Phone = "555-0104", Title = "UI/UX Designer" });
            employees.Add(new Employee { Id = 5, Name = "William Brown", Email = "william.brown@example.com", Phone = "555-0105", Title = "DevOps Engineer" });
            employees.Add(new Employee { Id = 6, Name = "Linda Wilson", Email = "linda.wilson@example.com", Phone = "555-0106", Title = "HR Specialist" });
            employees.Add(new Employee { Id = 7, Name = "James White", Email = "james.white@example.com", Phone = "555-0107", Title = "Database Administrator" });
            employees.Add(new Employee { Id = 8, Name = "Barbara Miller", Email = "barbara.miller@example.com", Phone = "555-0108", Title = "Business Analyst" });
            employees.Add(new Employee { Id = 9, Name = "Robert Martinez", Email = "robert.martinez@example.com", Phone = "555-0109", Title = "Network Engineer" });
            employees.Add(new Employee { Id = 10, Name = "Elizabeth Taylor", Email = "elizabeth.taylor@example.com", Phone = "555-0110", Title = "Product Manager" });

            return employees;
        }
        
    }
}
