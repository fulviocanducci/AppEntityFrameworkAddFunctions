using System;
namespace AppEntityFrameworkAddFunctions.Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}
