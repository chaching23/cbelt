using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using belt.Models;

namespace belt.Models
{
    public class Act
    {
        [Key]
        [Column("id")]
        public int ActivityId {get;set;}



        [Column("user_id")]
        public int UserId {get;set;}



        [Column("title")]
        [Required(ErrorMessage="Title is required!")]
        public string Title {get;set;}
        


        [Column("date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Date is required!")]
        [FutureDate]
        public DateTime Date {get;set;}



        [Column("time")]
        [Required(ErrorMessage="Time is required!")]
        public string Time {get;set;}



        [Column("duration")]
        public int Duration {get;set;}



        [Column("description")]
        [Required(ErrorMessage="Description is required!")]
        public string Description {get;set;}




        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        
        
        
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


        // Navigation Properties    
        public User Creator {get;set;}
        public List<Join> Participants {get;set;}
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime wedDate = (DateTime)value;
            DateTime currDate = DateTime.Now;
            if(wedDate < currDate)
            {
                return new ValidationResult("Not a Valid Date");
            }
            return ValidationResult.Success; 
        }
    }
}


