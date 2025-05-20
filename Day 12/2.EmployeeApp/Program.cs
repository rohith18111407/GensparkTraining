using System;
using System.Collections.Generic;
using System.Linq;


class Employee : IComparable<Employee>
{
    int id, age;
    string name;
    double salary;

    public Employee() { }

    public Employee(int id, int age, string name, double salary)
    {
        this.id = id;
        this.age = age;
        this.name = name;
        this.salary = salary;
    }

    public void TakeEmployeeDetailsFromUser()
    {
        Console.WriteLine("Please enter the employee ID");
        id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter the employee name");
        name = Console.ReadLine();
        Console.WriteLine("Please enter the employee age");
        age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter the employee salary");
        salary = Convert.ToDouble(Console.ReadLine());
    }

    public override string ToString()
    {
        return $"Employee ID : {id}\nName : {name}\nAge : {age}\nSalary : {salary}";
    }

    public int Id { get => id; set => id = value; }
    public int Age { get => age; set => age = value; }
    public string Name { get => name; set => name = value; }
    public double Salary { get => salary; set => salary = value; }

    public int CompareTo(Employee other)
    {
        return this.salary.CompareTo(other.salary); 
    }
}

class EmployeePromotion
{
    static List<string> promotionList = new List<string>();

    public static void RunEasyQuestions()
    {
        Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion (blank to stop):");
        string name;
        while (true)
        {
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) 
                break;
            promotionList.Add(name);
        }

        // Question 2: Find position
        Console.WriteLine("Please enter the name of the employee to check promotion position:");
        string searchName = Console.ReadLine();
        int index = promotionList.IndexOf(searchName);
        if (index != -1)
            Console.WriteLine($"{searchName} is at position {index + 1} for promotion.");
        else
            Console.WriteLine("Employee not found in promotion list.");

        // Question 3: Trim excess memory
        Console.WriteLine($"The current size of the collection is {promotionList.Capacity}");
        promotionList.TrimExcess();
        Console.WriteLine($"The size after removing the extra space is {promotionList.Capacity}");

        // Question 4: Sort and print
        promotionList.Sort();
        Console.WriteLine("Promoted employee list in ascending order:");
        foreach (var emp in promotionList)
        {
            Console.WriteLine(emp);
        }
    }
}

class EmployeeManager
{
    static Dictionary<int, Employee> employeeDict = new Dictionary<int, Employee>();

    public static void RunMediumAndHardQuestions()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Modify Employee");
            Console.WriteLine("3. Display All Employees");
            Console.WriteLine("4. Find Employee by ID");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Sort by Salary and Display");
            Console.WriteLine("7. Find by Name");
            Console.WriteLine("8. Find Employees Elder Than Given ID");
            Console.WriteLine("9. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": 
                    Employee emp = new Employee();
                    emp.TakeEmployeeDetailsFromUser();
                    if (!employeeDict.ContainsKey(emp.Id))
                    {
                        employeeDict.Add(emp.Id, emp);
                        Console.WriteLine("Employee added.");
                    }
                    else
                    {
                        Console.WriteLine("Employee with this ID already exists.");
                    }
                    break;

                case "2": 
                    Console.Write("Enter employee ID to modify: ");
                    int modId = Convert.ToInt32(Console.ReadLine());
                    if (employeeDict.ContainsKey(modId))
                    {
                        Employee e = employeeDict[modId];
                        Console.Write("Enter new name: "); 
                        e.Name = Console.ReadLine();
                        Console.Write("Enter new age: "); 
                        e.Age = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new salary: "); 
                        e.Salary = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Employee details updated.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;

                case "3": 
                    Console.WriteLine("All Employees:");
                    foreach (var e in employeeDict.Values)
                        Console.WriteLine(e + "\n");
                    break;

                case "4": 
                    Console.Write("Enter employee ID: ");
                    int findId = Convert.ToInt32(Console.ReadLine());
                    if (employeeDict.TryGetValue(findId, out Employee foundEmp))
                        Console.WriteLine(foundEmp);
                    else
                        Console.WriteLine("Employee not found.");
                    break;

                case "5": 
                    Console.Write("Enter employee ID to delete: ");
                    int delId = Convert.ToInt32(Console.ReadLine());
                    if (employeeDict.Remove(delId))
                        Console.WriteLine("Employee removed.");
                    else
                        Console.WriteLine("Employee ID not found.");
                    break;

                case "6": 
                    var list = employeeDict.Values.ToList();
                    list.Sort();
                    Console.WriteLine("Employees sorted by salary:");
                    foreach (var e in list)
                        Console.WriteLine(e + "\n");
                    break;

                case "7": 
                    Console.Write("Enter name to search: ");
                    string name = Console.ReadLine();
                    var matches = employeeDict.Values.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    foreach (var m in matches)
                        Console.WriteLine(m + "\n");
                    break;

                case "8": 
                    Console.Write("Enter employee ID to compare: ");
                    int compId = Convert.ToInt32(Console.ReadLine());
                    if (employeeDict.TryGetValue(compId, out Employee refEmp))
                    {
                        var elders = employeeDict.Values.Where(e => e.Age > refEmp.Age);
                        Console.WriteLine("Employees elder than given employee:");
                        foreach (var e in elders)
                            Console.WriteLine(e + "\n");
                    }
                    else
                        Console.WriteLine("Employee not found.");
                    break;

                case "9": return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        //EmployeePromotion.RunEasyQuestions();
        EmployeeManager.RunMediumAndHardQuestions();
        Console.ReadKey();
    }
}
