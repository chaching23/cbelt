using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace belt.Models
{
    public class Join
    {
        [Key]
        [Column("id")]
        public int JoinId {get;set;}
   

        [Column("activity_Id")]
        public int ActivityId {get;set;}



        [Column("user_id")]
        public int UserId {get;set;}



        [Column("join")]
        public bool join {get;set;}



        // Navigation Properties
        public User ppUsers {get;set;}
        public Act ppActivity {get;set;}
    }
}