using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorSignalRApp.Client.Pages
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        [Display(Name = "男")]
        Male = 1,
        [Display(Name = "女")]
        Female = 2
    }
    public partial class DataBinding
    {
        private Person personModel = new Person();

        private void HandleValidSubmit()
        {
            Console.WriteLine("OnValidSubmit");
        }


        public int CurrentValue { get; set; }

        public void Count()
        {
            CurrentValue++;
        }
    }
}
