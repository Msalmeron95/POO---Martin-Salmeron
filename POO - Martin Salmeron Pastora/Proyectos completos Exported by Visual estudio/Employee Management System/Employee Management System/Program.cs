using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the employee management system.");

            Console.Write("Enter the department name: ");
            string? departmentName = Console.ReadLine();

            Department department = new Department(departmentName);

            while (true)
            {
                Console.Write("\nEnter the type of employee (1 for full-time, 2 for part-time, or any other key to exit):");
                string? employeeType = Console.ReadLine();

                if (employeeType == "1")
                {
                    try
                    {
                        Console.Write("\nEnter the name of the full-time employee: ");
                        string? employeeName = Console.ReadLine();

                        Console.Write("Enter the employee ID: ");
                        int employeeId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the employee's monthly base salary: ");
                        double employeeBaseSalary = double.Parse(Console.ReadLine());

                        Console.Write("Enter the benefits for the full-time employee: ");
                        double employeeBenefits = double.Parse(Console.ReadLine());

                        FullTimeEmployee fullTimeEmployee = new FullTimeEmployee(employeeName, employeeId, employeeBaseSalary, employeeBenefits);
                        department.AddEmployee(fullTimeEmployee);
                    }
                    catch (FormatException)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nError: Invalid value entered for benefits. Please enter a valid number.\n");
                        Console.ResetColor();
                    }
                }
                else if (employeeType == "2")
                {
                    Console.Write("\nEnter the name of the part-time employee: ");
                    string? employeeName = Console.ReadLine();

                    Console.Write("Enter the employee ID: ");
                    int employeeId = int.Parse(Console.ReadLine());

                    Console.Write("Enter the employee's base salary: ");
                    double employeeBaseSalary = double.Parse(Console.ReadLine());

                    Console.Write("Enter the hours worked by the employee: ");
                    int hoursWorked = int.Parse(Console.ReadLine());

                    Console.Write("Enter the hourly rate for the employee: ");
                    double hourlyRate = double.Parse(Console.ReadLine());

                    PartTimeEmployee partTimeEmployee = new PartTimeEmployee(employeeName, employeeId, employeeBaseSalary, hoursWorked, hourlyRate);
                    department.AddEmployee(partTimeEmployee);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"\nThe total salary of the {department.Name} department is: {Employee.CalculateTotalSalary(department.Employees)}");
        }
    }

    public class Employee
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public double BaseSalary { get; set; }

        public Employee(string name, int id, double baseSalary)
        {
            Name = name;
            Id = id;
            BaseSalary = baseSalary;
        }

        public virtual double CalculateSalary()
        {
            return BaseSalary;
        }

        public static double CalculateTotalSalary(List<Employee> employees)
        {
            double totalSalary = 0;
            foreach (var employee in employees)
            {
                totalSalary += employee.CalculateSalary();
            }
            return totalSalary;
        }
    }

    public class FullTimeEmployee : Employee
    {
        public double Benefits { get; set; }

        public FullTimeEmployee(string? name, int id, double baseSalary, double benefits) : base(name, id, baseSalary)
        {
            Benefits = benefits;
        }

        public override double CalculateSalary()
        {
            return base.CalculateSalary() + Benefits;
        }
    }

    public class PartTimeEmployee : Employee
    {
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }

        public PartTimeEmployee(string name, int id, double baseSalary, int hoursWorked, double hourlyRate) : base(name, id, baseSalary)
        {
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }

        public override double CalculateSalary()
        {
            return BaseSalary + (HoursWorked * HourlyRate);
        }
    }

    public class Department
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

        public Department(string name)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}
