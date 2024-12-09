using System.ComponentModel.DataAnnotations;

namespace SkillUpBackend.Model
{
    public class UserEditModel
    {
        public int Id { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  DateTime? DateOfBirth { get; set; }
        public string Qualification { get; set; }
        [MaxLength(8)]
        [MinLength(6)]
       
        public  string Password { get; set; }
        
    }
}
