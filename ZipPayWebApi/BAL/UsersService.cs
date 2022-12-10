using AutoMapper;
using System.Collections.Generic;
using ZipPayWebApp.BAL.Entity;
using ZipPayWebApp.BAL.Exceptions;
using ZipPayWebApp.BAL.Utility;
using ZipPayWebApp.DAL;
using ZipPayWebApp.DAL.Entity;

namespace ZipPayWebApp.BAL
{
    public interface IUsersService
    {
        IEnumerable<UserModel> GetAllUser();
        UserModel GetUser(int id);
        int CreateUser(UserModel user);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersRepo _usersRepo;
        private readonly IMapper _mapper;
        

        public UsersService(IUsersRepo usersRepo, IMapper mapper)
        {
            _usersRepo = usersRepo;
            _mapper = mapper;
        }
        public int CreateUser(UserModel user)
        {
            if (_usersRepo.GetUser(user.Email_Id) != null)
            {
                throw new BadRequestException(ErrorCodes.DUPLICATE_FOUND, ErrorMessage.EMAIL_ID_DUPLICATE_MSG);
            }
            if (user.Monthly_Expense <= 0)
            {
                throw new BadRequestException(ErrorCodes.VALUE_NAGATIVE_ERROR_CODE, ErrorMessage.MONTHLY_EXPENSE_NEGATIVE_MSG);
            }
            if (user.Monthly_Salary <= 0)
            {
                throw new BadRequestException(ErrorCodes.VALUE_NAGATIVE_ERROR_CODE, ErrorMessage.MONTHLY_SALARY_NEGATIVE_MSG);
            }
            
            return _usersRepo.CreateUser(_mapper.Map<USER>(user));
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            return _mapper.Map<IEnumerable<UserModel>>(_usersRepo.GetAllUser());
        }

        public UserModel GetUser(int id)
        {
            return _mapper.Map<UserModel>(_usersRepo.GetUser(id));
        }
    }
}
