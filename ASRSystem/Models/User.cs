// Model copied from Matthew Bolger's model of Week7 example code

using Microsoft.AspNetCore.Identity;

namespace ASRSystem.Models
{
    // Add profile data for users by adding properties to the User class
    public class User : IdentityUser
    {
        public string StaffID { get; set; }
        public virtual Staff Staff { get; set; }

        public string StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
