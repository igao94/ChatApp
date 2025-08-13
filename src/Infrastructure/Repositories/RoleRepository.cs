using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class RoleRepository(AppDbContext context)
    : RepositoryBase<AppRole>(context), IRoleRepository
{

}
