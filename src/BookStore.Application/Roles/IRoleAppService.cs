using System.Threading.Tasks;
using Abp.Application.Services;
using BookStore.Roles.Dto;

namespace BookStore.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
