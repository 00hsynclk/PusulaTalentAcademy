using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class Solution
{
    /// <summary>
    /// XML içerisinden kişileri filtreleyip JSON formatında döndürür.
    /// Kriterler:
    /// - Departman "IT"
    /// - Maaş > 5000
    /// - İşe giriş tarihi 2018 öncesi
    /// </summary>
    /// <param name="xmlData">Kişilerin bulunduğu XML string</param>
    /// <returns>Filtrelenmiş sonuçların JSON string karşılığı</returns>
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
            return "{}";

        XDocument doc = XDocument.Parse(xmlData);

        var filtered = doc.Descendants("Person")
            .Select(p => new
            {
                Name = (string)p.Element("Name"),
                Department = (string)p.Element("Department"),
                Salary = (int?)p.Element("Salary") ?? 0,
                HireDate = DateTime.TryParse((string)p.Element("HireDate"), out DateTime d) ? d : DateTime.MinValue
            })
            .Where(p => p.Department == "IT"
                     && p.Salary > 5000
                     && p.HireDate.Year < 2018)
            .ToList();

        if (!filtered.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var result = new
        {
            Names = filtered.Select(f => f.Name).OrderBy(n => n).ToList(),
            TotalSalary = filtered.Sum(f => f.Salary),
            AverageSalary = (int)filtered.Average(f => f.Salary),
            MaxSalary = filtered.Max(f => f.Salary),
            Count = filtered.Count
        };

        return JsonSerializer.Serialize(result);
    }
}