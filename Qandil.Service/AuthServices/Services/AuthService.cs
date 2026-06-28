
using Microsoft.AspNetCore.Identity;
using Qandil.Core.AuthServices.Hasher;
using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Core.Interfacres.EmailService;
using Qandil.Service.AuthServices.Helper.Dtos;
using Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request;
using Qandil.Service.AuthServices.Helper.EmailTemplates;
using Qandil.Services.AuthServices.GenerateToken;
using Qandil.Services.AuthServices.Hasher;
using Qandil.Services.AuthServices.Helper;
using Qandil.Services.AuthServices.Services;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;




namespace Qandil.Services.AuthServices.Service;

public class AuthService(IUnitOfWork _uow, IPasswordHasher _passwordHasher,
    IGenerateTokenJwt _generateTokenJwt,IEmailService _emailService) : IAuthService
{
 private AuthModel _authModel;
    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email) is not null)
        {
            return new AuthModel { Message = "Email is already Register " };
        }
        var haspassword = _passwordHasher.HashPassword(model.Password);
        var user = new User
        {
            Email = model.Email,
            Password = haspassword,
            Role = RoleType.User
        };
        await _uow.UsersRepository.AddAsync(user);

        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Email);
        await _uow.CompleteAsync();

        return new AuthModel
        {
            Email = model.Email,
            IsAuthenticated = true,
            Token = jwt,
            Message = "Registration successful"
        };

    }
    public async Task<AuthModel> LogInAsync(LogInModel model)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email);

        if (user == null)
        {
            _authModel.Message = "Email or Password is incorrect";
            return _authModel;

        }
        var IsValidPassword = _passwordHasher.VerifyHashedPassword(user.Password, model.Password);
        if (!IsValidPassword)
        {
            _authModel.Message = "Email or Password is incorrect";
            return _authModel;
        }
        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Email);

        return new AuthModel
        {
            Email = user.Email,
            IsAuthenticated = true,
            Token = jwt,
            Message = "LogIn successful"
        };
    }
    public async Task<AuthModel> CreateAdminAsync(CreateAdminDto dto)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email) is not null)
        {
            return new AuthModel { Message = "Email is already Register " };
        }
        var haspassword = _passwordHasher.HashPassword(dto.Password);
        var user = new User
        {
            Email = dto.Email,
            Password = haspassword,
            Role = RoleType.Admin
        };
        await _uow.UsersRepository.AddAsync(user);

        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Email);
        await _uow.CompleteAsync();

        return new AuthModel
        {
            Email = dto.Email,
            IsAuthenticated = true,
            Token = jwt,
            Message = "Admin created successfully"
        };
    }
       public async Task<Result<string>> ForgetPasswordAsync(ForgetPasswordRequestDto dto)
       {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);

        if (user is null)
        {
            return Result<string>.Failure("Account not found.");
        }

        var otp = new Random().Next(100000, 999999).ToString();

        await _emailService.SendEmailAsync( user.Email, "Password Reset Verification Code", EmailTemplate.ResetPAsswordOtp(otp));

        var hashCode = _passwordHasher.HashPassword(otp);

        var result = new UserOtp
        {
            UserId = user.Id,
            Email = user.Email,
            Code = hashCode,
            IsUsed = false,
            CreatedAt=DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(5)
        };

        await _uow.UserOtpRepository.AddAsync(result);
        await _uow.CompleteAsync();
        return Result<string>.Success("A verification code has been sent to your email.");

      }
    public async  Task<Result<string>> VerfiyOtpAsync(VerifyOtpRequestDto dto)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return Result<string>.Failure("Account not found.");
        }
        var otps = await _uow.UserOtpRepository.FindAllAsync(o => o.UserId == user.Id
        && o.IsUsed == false);
        var otp = otps.OrderByDescending(o => o.CreatedAt).FirstOrDefault();

        var verfiy = _passwordHasher.VerifyHashedPassword(otp.Code,dto.Otp);

        if (otp == null || !verfiy || DateTime.Now > otp.ExpireDate)
        {
            return Result<string>.Failure("Invalid verification code,Please request a new one.");
        }
        otp.IsUsed = true;
        await _uow.UserOtpRepository.UpdateAsync(otp);
        await _uow.CompleteAsync();
        return Result<string>.Success("Verification code confirmed successfully.");
    }
    public async  Task<Result<string>> ResetPasswordAsync(ResetPasswordRequestDto dto)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return Result<string>.Failure("Account not found.");
        }
        user.Password = _passwordHasher.HashPassword(dto.NewPassword);
        await _uow.UsersRepository.UpdateAsync(user);
        await _uow.CompleteAsync();
        return Result<string>.Success("Password has been reset successfully.");

    }

}

