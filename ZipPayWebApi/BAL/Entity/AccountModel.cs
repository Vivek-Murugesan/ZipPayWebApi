using System;

namespace ZipPayWebApp.BAL.Entity
{
    public class AccountModel
    {
        public int Id { get; set; }

        public int User_Id { get;set; }

        public string Account_No { get; set; }

        public string Card_No { get; set; }

        public decimal Max_Credit_Limit { get; set; }

        public DateTime? Statement_Date { get; set; }

        public decimal? Due_Amount { get; set; }
    }
}
