using System;

namespace ZipPayWebApp.DAL.Entity
{
    public class ACCOUNT
    {
        public int ID { get; set; }

        public int USER_ID { get; set; }

        public string ACCOUNT_NO { get; set; }

        public string CARD_NO { get; set; }

        public decimal MAX_CREDIT_LIMIT { get; set; }

        public DateTime? STATEMENT_DATE { get; set; }

        public decimal? DUE_AMOUNT { get; set; }
    }
}
