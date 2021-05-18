using System;

namespace Sat.Recruitment.Api.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        protected decimal money;
        public Role Role { get; set; }
        public decimal Money
        {
            get { return this.money; }
            set { this.Role.CalculateMe(this, value); }
        }

        public void NormalUserMoney(in decimal value)
        {
            if (value > 100)
            {
                var gif = value * 0.12m;
                this.money = value + gif;
            }

            if (money < 100)
            {
                if (money > 10)
                {
                    var gif = value * 0.8m;
                    this.money = value + gif;
                }
            }
        }

        public void SuperUserMoney(in decimal value)
        {
            if (value > 100)
            {
                this.money = value + (value * 0.20m);
            }
            else
            {
                this.money = value;
            }
        }

        public void PremiumUserMoney(in decimal value)
        {
            if (value > 100)
            {
                this.money = value + (value * 2);
            }
            else
            {
                this.money = value;
            }
        }
    }
}