using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example1 : MonoBehaviour
{
    List<int> numbers = new List<int> { 1, 4, 7, 10, 13, 16, 19, 22 };

    List<string> words = new List<string> { "sun", "moon", "sky", "cloud", "a", "hi" };



    private void Start()
    {

    }

    private int CountNumberIfGreaterThanThree(List<string> listString)
    {
        return listString.Count(x => x.Length > 3);
    }


    private List<int> FindListNumberEven(List<int> list)
    {
        var listEven = list.Where(x => x % 2 == 0).ToList();

        return listEven;
    }

    private List<int> FindListNumberEven2(List<int> list)
    {
        var listEven = new List<int>();
        foreach (var element in list)
        {
            if (element % 2 == 0) listEven.Add(element);
        }

        return listEven;
    }
}

class Student
{
    List<Student> students = new List<Student> {
    new Student { Name = "An", Score = 7.5 },
    new Student { Name = "Bình", Score = 8.2 },
    new Student { Name = "Chi", Score = 9.1 }
};
    public string Name { get; set; }
    public double Score { get; set; }


    public List<Student> GetNameStudyHasCoreGreaterThanEight(List<Student> listStudents)
    {
        return listStudents.Where(x => x.Score > 8).ToList();
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }

    List<Product> products = new List<Product> {
    new Product { Name = "Phone", Price = 500 },
    new Product { Name = "Laptop", Price = 1200 },
    new Product { Name = "Tablet", Price = 300 }
};

    private void SortListPricesGraduallyDecrease()
    {
        products.OrderByDescending(x => x.Price).ToList();
    }

}








