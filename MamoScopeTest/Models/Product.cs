using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MamoScopeTest.Models
{
    public class MotorDriver
    {
        [Key]
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public double Voltage { get; set; }
        public DateTime TestDate { get; set; }
        
        public bool IsPassed { get; set; }


    }
}
