using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace User.Library
{
    public interface IUsersServices
    {
        Result Register(User model);
        Result Update(User model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Result Delete(int id);
    }
    public class UsersServices : IUsersServices
    {
        private readonly IDbContext _dbContext;
        private readonly UserValidatador _userValidator;

        public UsersServices(IDbContext dbContext, UserValidatador userValidator)
        {
            _dbContext = dbContext;
            _userValidator = userValidator;
        }

        public Result Delete(int id)
        {
            var result = new Result();
            try
            {
                var user = GetById(id);
                if (user == null)
                {
                    result.AddError("Usuário não encontrado");
                    return result;
                }

                using (var con = _dbContext.CreateConnection())
                {
                    con.Execute("DELETE FROM dbo.Users WHERE Id = @Id", new { Id = id});
                }
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
                return result;
            }

            return result;
        }

        public IEnumerable<User> GetAll()
        {
            using (var con = _dbContext.CreateConnection())
            {
                return con.Query<User>("SELECT * FROM dbo.Users");
            }
        }

        public User GetById(int id)
        {
            using (var con = _dbContext.CreateConnection())
            {
                return con.Query<User>("SELECT * FROM dbo.Users WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public Result Register(User model)
        {
            var result = new Result();
            var validationResult = _userValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                {
                    result.AddError(err.ErrorMessage);
                }
                return result;
            }

            using (var con = _dbContext.CreateConnection())
            {
                try
                {
                    con.Execute("INSERT INTO dbo.Users(Name, Email, Password, Telephone, Address, Birthdate) VALUES (@Name, @Email, @Password, @Telephone, @Address, @Birthdate)", 
                        model);
                }
                catch (Exception e)
                {
                    result.AddError(e.Message);
                }
            }

            return result;
        }

        public Result Update(User model)
        {
            var result = new Result();
            var validationResult = _userValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                {
                    result.AddError(err.ErrorMessage);
                }
                return result;
            }

            using (var con = _dbContext.CreateConnection())
            {
                try
                {
                    var user = GetById(model.Id);
                    if (user == null)
                    {
                        result.AddError("Usuário não encontrado");
                        return result;
                    }
                    con.Execute("UPDATE dbo.Users SET Name = @Name, Password = @Password, Email = @Email, Telephone = @Telephone, Address = @Address, Birthdate = @Birthdate WHERE Id = @Id",
                        model);
                }
                catch (Exception e)
                {
                    result.AddError(e.Message);
                }
            }

            return result;
        }
    }
}
