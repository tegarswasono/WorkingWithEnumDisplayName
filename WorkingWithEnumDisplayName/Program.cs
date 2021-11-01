using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WorkingWithEnumDisplayName
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TransactionType.InlineRepairment.GetDisplayName());
            Console.WriteLine(".......................");
            ListData();
            Console.ReadLine();
        }

        private static void ListData()
        {
            List<DefaultEnumModelIntType> result = Enum.GetValues(typeof(TransactionType))
                            .Cast<TransactionType>()
                            .Select(t => new DefaultEnumModelIntType
                            {
                                Id = ((int)t),
                                Name = t.GetDisplayName()
                            })
                            .ToList();
            foreach (var data in result)
            {
                Console.WriteLine(data.Name);
            }
        }
    }
    public static class Helper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            string displayName;
            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }
            return displayName;
        }
    }
    public enum TransactionType
    {
        ReAssignment,
        Loan,
        [Display(Name = "Inline Repairment")]
        InlineRepairment,
        [Display(Name = "Workshop Repairment")]
        WorkshopRepairment,
        Disposal
    }
    public class DefaultEnumModelIntType
    {
        public int Id { get; set; }
        public string Name { set; get; }
    }
}
