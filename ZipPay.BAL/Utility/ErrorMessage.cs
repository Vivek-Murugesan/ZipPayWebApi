namespace ZipPay.BAL.Utility
{
    public static class ErrorMessage
    {
        public const string MONTHLY_EXPENSE_NEGATIVE_MSG = "Monthly Expense value shouldn't be in negative";
        public const string MONTHLY_SALARY_NEGATIVE_MSG = "Monthly Salary value shouldn't be in negative";
        public const string EMAIL_ID_DUPLICATE_MSG = "Email Id already exists";
        public const string LESS_CREDIT_LIMIT_MSG = "Account creation failed since credit is below $1000";
    }
}
