using ZipPayWebApp.DAL.Entity;
using Dapper;
using System.Collections.Generic;

namespace ZipPayWebApp.DAL
{
    public interface IAccountsRepo
    {
        int CreateAccount(ACCOUNT account);
        ACCOUNT GetAccount(int id);

        IEnumerable<ACCOUNT> GetAll(int recordsPerPage, int pageNo);
    }
    public class AccountsRepo : IAccountsRepo
    {
        private readonly IDbContext _dbContext;
        
        public AccountsRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateAccount(ACCOUNT account)
        {
            string query = "INSERT INTO Accounts (USER_ID, ACCOUNT_NO,CARD_NO,MAX_CREDIT_LIMIT,STATEMENT_DATE, DUE_AMOUNT) values(@USER_ID, @ACCOUNT_NO,@CARD_NO,@MAX_CREDIT_LIMIT,@STATEMENT_DATE, @DUE_AMOUNT)";
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Execute(query, account);
            }
        }

        public ACCOUNT GetAccount(int userId)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.QueryFirstOrDefault<ACCOUNT>("SELECT * FROM Accounts WHERE User_Id = @UserId" , new { userId = userId});
            }
        }

        public IEnumerable<ACCOUNT> GetAll(int recordsPerPage, int pageNo)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Query<ACCOUNT>("SELECT * FROM Accounts Order By ID OFFSET (@PageNumber-1)*@RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY", 
                    new { PageNumber = pageNo,RecordsPerPage = recordsPerPage });
            }
        }
    }
}
