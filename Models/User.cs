using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using belt.Models;

    namespace belt.Models
    {
        public class User
        {
            [Key]
            [Column("id")]
            public int UserId {get;set;}
            

            [Required(ErrorMessage="First name is required and must be at least 2 characters long ")]
            [MinLength(2, ErrorMessage="First name is required and must be at least 2 characters long ")]
            [RegularExpression("^[a-zA-Z ]*$")]
            [Display(Name = "name")]
            public string Name {get; set;}



            [Required(ErrorMessage="Email is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            [Display(Name = "Email")]
            public string Email {get; set;}



            [Required(ErrorMessage="Password must contain 1 letter, 1 number and 1 special character and MUST be 8 characters long")]
            [MinLength(6, ErrorMessage = "Password must contain 1 letter, 1 number and 1 special character and MUST be 8 characters long")]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password must contain 1 letter, 1 number and 1 special character and MUST be 6 characters long")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password {get; set;}



            [Compare("Password", ErrorMessage="Your Passwords should match")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm")]
            public string Confirm {get; set;}



            [Column("created_at")] 
            public DateTime CreatedAt {get;set;} = DateTime.Now; 



            [Column("updated_at")] 
            public DateTime UpdatedAt {get;set;} = DateTime.Now; 


        // Navigation Properties
        public List<Act> CreatedActivity {get;set;}
        public List<Join> JoinIssued {get;set;}

        // public User()
        // {
        //     CreatedActivity = new List<Activity>();
        // }

        }
    }

  


    