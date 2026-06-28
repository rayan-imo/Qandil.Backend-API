using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.Diagnosis.Requests;
using Qandil.Service.Dtos.UserDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Diagnosis;
using Qandil.Service.Validation.User;

public class UserService(IUnitOfWork _uow) : IUserService
{

    public async Task<Result<PagedResult<User>>> GetAllAsync(PaginationParameter paginationParameter)
    {
        var spac = BaseSpecification<User>
            .Create()
            .Where(x => x.DeletedAt == null)
            .Paginate(paginationParameter.page, paginationParameter.pageSize);

        return Result<PagedResult<User>>.Success(await _uow.UsersRepository.PagedListAsync(spac));

    }


    public async Task<Result<User>> GetById(Guid id)
    {
        if (id == Guid.Empty)
            return Result<User>.Failure("User ID cannot be empty.");

        var user = await _uow.UsersRepository.GetByIdAsync(id);

        if (user == null || user.DeletedAt != null)
            return Result<User>.Failure($"User with ID was not found.");

        return Result<User>.Success(user);
    }

    public async Task<Result<Guid>> AddAsync(UserRequestdto dto)
    {
        await new UserValidator().ValidateAndThrowAsync(dto);
        var user = new User
        {
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,

        };

        await _uow.UsersRepository.AddAsync(user);
        await _uow.CompleteAsync();
        return Result<Guid>.Success(user.Id);


    }

    public async Task<Result<Guid>> UpdateAsync(UserRequestdto dto, Guid id)
    {

        if (id == Guid.Empty)
            return Result<Guid>.Failure("User ID cannot be empty.");

        var user = await _uow.UsersRepository.GetByIdAsync(id);

        if (user == null || user.DeletedAt != null)
            return Result<Guid>.Failure($"User with ID was not found.");
        await new UserValidator().ValidateAndThrowAsync(dto);

        user.Email = dto.Email;
        user.Password = dto.Password;
        user.Role = dto.Role;
        await _uow.UsersRepository.UpdateAsync(user);
        await _uow.CompleteAsync();
        return Result<Guid>.Success(user.Id);

    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            return Result<bool>.Failure("User ID cannot be empty.");

        var user = await _uow.UsersRepository.GetByIdAsync(id);

        if (user == null || user.DeletedAt != null)
            return Result<bool>.Failure("User with iD was not found ");

        user.DeletedAt = DateTime.UtcNow;
        await _uow.CompleteAsync();

        return Result<bool>.Success(true);

    }
}