using System;

namespace Sat.Recruitment.Api.Model
{
    public abstract class Role
    {
        public Guid Id { get; set; }
        public abstract void CalculateMe(User user, decimal value);
    }

    public class Normal : Role
    {
        public override void CalculateMe(User user, decimal value)
        {
            user.NormalUserMoney(value);
        }
    }

    public class SuperUser : Role
    {
        public override void CalculateMe(User user, decimal value)
        {
            user.SuperUserMoney(value);
        }
    }

    public class Premium : Role
    {
        public override void CalculateMe(User user, decimal value)
        {
            user.PremiumUserMoney(value);
        }
    }
}