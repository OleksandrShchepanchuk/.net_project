namespace Hospital.Models;

public class UserConstants
{
    
        public static List<User> Users = new List<User>()
        {
            new User
            {
                UserId = 1,
                Username = "jason_admin",
                Email = "jason.admin@email.com",
                Password = "MyPass_w0rd",
                Roles = new List<Role>
                {
                    new Role { RoleId = 1, RoleName = "Administrator" }
                },
                Wishes = new List<Wish>(),
                Reviews = new List<Review>()
            },
            new User
            {
                UserId = 2,
                Username = "elyse_seller",
                Email = "elyse.seller@email.com",
                Password = "MyPass_w0rd",
                Roles = new List<Role>
                {
                    new Role { RoleId = 2, RoleName = "User" }
                },
                Wishes = new List<Wish>(),
                Reviews = new List<Review>()
            }
        };
    
}