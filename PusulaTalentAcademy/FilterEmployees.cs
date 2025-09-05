using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Solution
{
    /// <summary>
    /// Çalışan listesini filtreler ve istatistikleri JSON formatında döndürür.
    /// Kriterler:
    /// - Yaş 25 ile 40 arasında (dahil)
    /// - Departman IT veya Finans
    /// - Maaş 5000 ile 9000 arasında (dahil)
    /// - İşe giriş tarihi 2017’den sonra
    /// </summary>
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null || !employees.Any())
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });

        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList();

        if (!filtered.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var result = new
        {
            Names = filtered
                .Select(f => f.Name)
                .OrderByDescending(n => n.Length)
                .ThenBy(n => n)
                .ToList(),
            TotalSalary = filtered.Sum(f => f.Salary),
            AverageSalary = Math.Round(filtered.Average(f => f.Salary), 2),
            MinSalary = filtered.Min(f => f.Salary),
            MaxSalary = filtered.Max(f => f.Salary),
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }
}