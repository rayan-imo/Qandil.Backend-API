
using System.IdentityModel.Tokens.Jwt;
using Qandil.Core.AuthServices.Hasher;
using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Core.Interfacres.EmailService;
using Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request;
using Qandil.Service.AuthServices.Helper.Dtos.Requests;
using Qandil.Service.AuthServices.Helper.Dtos.Responses;
using Qandil.Service.AuthServices.Helper.EmailTemplates;
using Qandil.Services.AuthServices.GenerateToken;
using Qandil.Services.AuthServices.Helper;
using Qandil.Services.AuthServices.Services;

namespace Qandil.Services.AuthServices.Service;

public class AuthService(
    IUnitOfWork _uow, IPasswordHasher _passwordHasher, IGenerateTokenJwt _generateTokenJwt,
    IEmailService _emailService) : IAuthService
{
    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email) is not null)
            return new AuthModel { Message = "Email is already registered" };

        var user = new User
        {
            Email = model.Email,
            Password = _passwordHasher.HashPassword(model.Password),
            Role = RoleType.User
        };

        await _uow.UsersRepository.AddAsync(user);
        await _uow.CompleteAsync();

        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Email);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return new AuthModel
        {
            Email = user.Email,
            IsAuthenticated = true,
            Token = jwt,
            Role = user.Role,
            ExpiresOn = token.ValidTo,
            Message = "Registration successful"
        };
    }

    public async Task<AuthModel> LogInAsync(LogInModel model)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == model.Email);

        if (user == null)
            return new AuthModel { Message = "Email or Password is incorrect" };

        var isValidPassword = _passwordHasher.VerifyHashedPassword(user.Password, model.Password);

        if (!isValidPassword)
            return new AuthModel { Message = "Email or Password is incorrect" };

        var jwt = _generateTokenJwt.GenerateAccessToken(user.Id, user.Role, user.Email);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return new AuthModel
        {
            Email = user.Email,
            IsAuthenticated = true,
            Role = user.Role,
            Token = jwt,
            ExpiresOn = token.ValidTo,
            Message = "LogIn successful"
        };
    }

    public async Task<CreateUserResponseDto> CreateAdminAsync(CreateAdminDto dto)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email) is not null)
        {
            return new CreateUserResponseDto
            {
                Message = "Email is already registered",
                Email = dto.Email,
                IsAuthenticated = false
            };
        }

        var user = new User
        {
            Email = dto.Email,
            Password = _passwordHasher.HashPassword(dto.Password),
            Role = RoleType.Admin,
        };

        await _uow.UsersRepository.AddAsync(user);
        await _uow.CompleteAsync();

        return new CreateUserResponseDto
        {
            Message = "Admin created successfully",
            Email = user.Email,
            Role = user.Role,
            IsAuthenticated = true
        };
    }

    public async Task<CreateUserResponseDto> CreateTeacherAsync(CreateStaffDto dto, Guid adminId)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email) is not null)
        {
            return new CreateUserResponseDto
            {
                Message = "Email is already registered",
                Email = dto.Email,
                IsAuthenticated = false
            };
        }

        var teacher = new User
        {
            Email = dto.Email,
            Password = _passwordHasher.HashPassword(dto.Password),
            Role = RoleType.Teacher,
            AdminId = adminId
        };

        await _uow.UsersRepository.AddAsync(teacher);
        await _uow.CompleteAsync();

        return new CreateUserResponseDto
        {
            Message = "Teacher created successfully",
            Email = teacher.Email,
            Role = teacher.Role,
            IsAuthenticated = true
        };
    }

    public async Task<CreateUserResponseDto> CreateSpecialistAsync(CreateStaffDto dto, Guid adminId)
    {
        if (await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email) is not null)
        {
            return new CreateUserResponseDto
            {
                Message = "Email is already registered",
                Email = dto.Email,
                IsAuthenticated = false
            };
        }

        var specialist = new User
        {
            Email = dto.Email,
            Password = _passwordHasher.HashPassword(dto.Password),
            Role = RoleType.Specialist,
            AdminId = adminId
        };

        await _uow.UsersRepository.AddAsync(specialist);
        await _uow.CompleteAsync();

        return new CreateUserResponseDto
        {
            Message = "Specialist created successfully",
            Email = specialist.Email,
            Role = specialist.Role,
            IsAuthenticated = true
        };
    }

    public async Task<Result<string>> ForgetPasswordAsync(ForgetPasswordRequestDto dto)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);

        if (user is null)
            return Result<string>.Failure("Account not found.");

        var oldOtps = await _uow.UserOtpRepository.FindAllAsync(o => o.UserId == user.Id && !o.IsUsed);

        foreach (var oldOtp in oldOtps)
        {
            oldOtp.IsUsed = true;
            await _uow.UserOtpRepository.UpdateAsync(oldOtp);
        }

        var otp = new Random().Next(100000, 999999).ToString();

        await _emailService.SendEmailAsync(user.Email, "Password Reset Verification Code", EmailTemplate.ResetPAsswordOtp(otp));

        var result = new UserOtp
        {
            UserId = user.Id,
            Email = user.Email,
            Code = _passwordHasher.HashPassword(otp),
            IsUsed = false,
            CreatedAt = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(10)
        };

        await _uow.UserOtpRepository.AddAsync(result);
        await _uow.CompleteAsync();

        return Result<string>.Success("A verification code has been sent to your email.");
    }

    public async Task<Result<string>> VerfiyOtpAsync(VerifyOtpRequestDto dto)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);

        if (user == null)
            return Result<string>.Failure("Account not found.");

        var otps = await _uow.UserOtpRepository.FindAllAsync(o => o.UserId == user.Id && !o.IsUsed);

        var otp = otps.OrderByDescending(o => o.CreatedAt).FirstOrDefault();

        if (otp == null)
            return Result<string>.Failure("Invalid verification code, Please request a new one.");

        var verify = _passwordHasher.VerifyHashedPassword(otp.Code, dto.Otp);

        if (!verify || DateTime.UtcNow > otp.ExpireDate)
            return Result<string>.Failure("Invalid verification code, Please request a new one.");

        otp.IsUsed = true;

        await _uow.CompleteAsync();

        return Result<string>.Success("Verification code confirmed successfully.");
    }

    public async Task<Result<string>> ResetPasswordAsync(ResetPasswordRequestDto dto)
    {
        var user = await _uow.UsersRepository.GetByItemAsync(u => u.Email == dto.Email);

        if (user == null)
            return Result<string>.Failure("Account not found.");

        user.Password = _passwordHasher.HashPassword(dto.NewPassword);

        await _uow.UsersRepository.UpdateAsync(user);
        await _uow.CompleteAsync();

        return Result<string>.Success("Password has been reset successfully.");
    }
}

