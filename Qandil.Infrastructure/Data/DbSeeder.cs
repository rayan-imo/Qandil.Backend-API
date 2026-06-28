using Microsoft.Extensions.Configuration;
using Qandil.Core.AuthServices.Hasher;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;

namespace Qandil.Infrastructure.Data
{
    public class DbSeeder(IUnitOfWork _uow, IConfiguration configuration,
         IPasswordHasher _passwordHasher)
    {
        public  async Task SeedSuperAdminAsync()
        {
            var email = configuration["SuperAdmin:Email"];
            var password = configuration["SuperAdmin:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            var existingUser = await _uow.UsersRepository.GetByItemAsync(u => u.Email == email);

            if (existingUser is not null)
            {
                return;
            }

            var user = new User
            {
                Email = email,
                Password = _passwordHasher.HashPassword(password),
                Role = RoleType.SuperAdmin
            };
      

            await _uow.UsersRepository.AddAsync(user);
            await _uow.CompleteAsync();
        }
    }
}