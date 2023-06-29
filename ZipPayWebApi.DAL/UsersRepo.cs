using Dapper;
using System.Collections.Generic;
using ZipPayWebApp;
using ZipPayWebApp.DAL.Entity;

namespace ZipPay.DAL
{
    public interface IUsersRepo
    {
        IEnumerable<USER> GetAllUser();
        USER GetUser(int id);
        int CreateUser(USER user);

        USER GetUser(string emailId);
    }
    public class UsersRepo : IUsersRepo
    {
        private readonly IDbContext _dbContext;

        public UsersRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateUser(USER user)
        {
            string query = "INSERT INTO Users(EMAIL_ID, LAST_NAME,FIRST_NAME,MONTHLY_SALARY,MONTHLY_EXPENSE) values(@EMAIL_ID, @LAST_NAME,@FIRST_NAME,@MONTHLY_SALARY,@MONTHLY_EXPENSE)";
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Execute(query, user);
            }
        }

        public IEnumerable<USER> GetAllUser()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Query<USER>("SELECT * FROM Users");
            }
        }

        public USER GetUser(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.QueryFirstOrDefault<USER>("SELECT * FROM Users WHERE ID = @UserId", new { userId = id });
            }
        }

        public USER GetUser(string emailId)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.QueryFirstOrDefault<USER>("SELECT * FROM Users WHERE EMAIL_ID = @Email_Id", new { Email_Id = emailId });
            }
        }
    }
}
