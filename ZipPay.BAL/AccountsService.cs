using AutoMapper;
using System.Collections.Generic;
using ZipPay.BAL.Entity;
using ZipPay.BAL.Exceptions;
using ZipPay.BAL.Utility;
using ZipPay.DAL;

using ZipPayWebApp.DAL.Entity;

namespace ZipPay.BAL
{
    public interface IAccountsService
    {
        int CreateAccount(AccountModel account);
        AccountModel GetAccount(int userId);

        IEnumerable<AccountModel> GetAll(int recordsPerPage, int pageNo);

    }
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepo _accountsRepo;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public AccountsService(IAccountsRepo accountsRepo, IMapper mapper, IUsersService usersService)
        {
            _accountsRepo = accountsRepo;
            _mapper = mapper;
            _usersService = usersService;
        }   

        public int CreateAccount(AccountModel account)
        {
            UserModel user = _usersService.GetUser(account.User_Id);
            if (user != null && user.Monthly_Salary - user.Monthly_Expense < 1000)
            {
                throw new BadRequestException(ErrorCodes.NOT_MEET_CREDIT_LIMIT, ErrorMessage.LESS_CREDIT_LIMIT_MSG);
            }
            return _accountsRepo.CreateAccount(_mapper.Map<ACCOUNT>(account));
        }

        public AccountModel GetAccount(int userId)
        {
           return _mapper.Map<AccountModel>(_accountsRepo.GetAccount(userId));
        }

        public IEnumerable<AccountModel> GetAll(int recordsPerPage, int pageNo)
        {
            return  _mapper.Map<IEnumerable<AccountModel>>(_accountsRepo.GetAll(recordsPerPage, pageNo));
        }
    }
}
