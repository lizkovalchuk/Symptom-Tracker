//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SymptomTrackerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Symptom
    {
        [Display(Name = "ID")]
        public int Symptom_Id { get; set; }

        [Display(Name = "Symptom Description")]
        public string Symptom_Desc { get; set; }

        [Display(Name = "User")]
        public Nullable<int> User_Id { get; set; }

        [Display(Name = "Date of Symptom")]
   //     [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}")]
        [DataType(DataType.Date)]
        public System.DateTime C_Time { get;
            set;
        }
        public Symptom()
        {
            this.C_Time = DateTime.Now;
        }
        public virtual UserTable UserTable { get; set; }
        public virtual UserTable UserTable1 { get; set; }
    }
}
