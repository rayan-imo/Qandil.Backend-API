
using Qandil.Services.AuthServices.Hasher;
using Qandil.Core.Interfacres;
using Qandil.Services.AuthServices.GenerateToken;
using Qandil.Services.AuthServices.Services;
using Qandil.Services.AuthServices.Helper;
using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Services.AuthServices.Service;

public class AuthService(IUnitOfWork _uow, IPasswordHasher _passwordHasher,
    IGenerateTokenJwt _generateTokenJwt) : IAuthService
{

    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email) is not null)
        {
            return new AuthModel { Message = "Email is already Register " };
        }
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Name == model.Name) is not null)

        {
            return new AuthModel { Message = "Name is already Register " };
        }
        var haspassword = _passwordHasher.HashPassword(model.Password);
        var user = new User
        {
            Email = model.Email,
            Name = model.Name,
            Password = haspassword,
            Role=RoleType.User
        };
        await _uow.UsersRepository.AddAsync(user);

        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id,user.Role,user.Name,user.Email); 
        await _uow.CompleteAsync();

        return new AuthModel
        {
            Email = model.Email,
            Name = model.Name,
            IsAuthenticated = true,
            Token = jwt,
            Message = "Registration successful"
        };

    }
    public async Task<AuthModel> LogInAsync(LogInModel model)
    {
        var authmodel = new AuthModel();
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email);

        if (user == null)
        {
            authmodel.Message = "Email or Password is incorrect";
            return authmodel;

        }
        var IsValidPassword = _passwordHasher.VerifyHashedPassword(user.Password, model.Password);
        if (!IsValidPassword)
        {
            authmodel.Message = "Email or Password is incorrect";
            return authmodel;
        }
        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Name, user.Email);

        return new AuthModel
        {
            Email = user.Email,
            Name = user.Name,
            IsAuthenticated = true,
            Token = jwt,
            Message = "LogIn successful"
        };
    }
}