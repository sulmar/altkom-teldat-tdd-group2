using System;

namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; private set; }

        public Rent(User rentee)
        {
            if (rentee == null)
                throw new ArgumentNullException(nameof(rentee));

            this.Rentee = rentee;
        }

        public bool CanReturn(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.IsAdmin)
                return true;

            if (Rentee == user)
                return true;

            return false;
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        
    }
}
