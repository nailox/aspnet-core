using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using BookStore.Authorization;
using BookStore.Authorization.Roles;
using BookStore.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Castle.Core.Logging;

namespace BookStore.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly BookStoreDbContext _context;
        private readonly int _tenantId;
        public ILogger Logger { get; set; }

        public TenantRoleAndUserBuilder(BookStoreDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
            Logger = NullLogger.Instance;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //Admin role

            var adminRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();

                //Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new BookStoreAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });

                    Logger.Info("permission granted to admin: "+permission.Name);
                }

                _context.SaveChanges();
            }

            //Author role

            var authorRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Author);
            if (authorRole == null)
            {
                authorRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Author, StaticRoleNames.Tenants.Author) { IsStatic = true }).Entity;
                _context.SaveChanges();

                Logger.Info("added author role");


                //var permissions = PermissionFinder
                //    .GetAllPermissions(new BookStoreAuthorizationProvider())
                //    .Where(p => p.Name.Equals("Pages.Users"))
                //    .ToList();

                //log
                //   Logger.Info("----------No. of permissions:" + permissions.Count());


                //grant Pages.Users permission to author
                _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = "Pages.Users",
                            IsGranted = true,
                            RoleId = authorRole.Id
                        });
              

                 _context.SaveChanges();
            }


            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();

                //User account of admin user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = adminUser.Id,
                        UserName = AbpUserBase.AdminUserName,
                        EmailAddress = adminUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }

            //author user
            var authorUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == "AuthorName");
            if (authorUser == null)
            {
                authorUser = User.CreateTenantAuthorUser(_tenantId, "author@bookstore.com");
                authorUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(authorUser, "authorpass");
                authorUser.IsEmailConfirmed = true;
                authorUser.IsActive = true;

                _context.Users.Add(authorUser);
                _context.SaveChanges();

                //Assign Author role to author user
                _context.UserRoles.Add(new UserRole(_tenantId, authorUser.Id, authorRole.Id));
                _context.SaveChanges();

                //User account of author user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = authorUser.Id,
                        UserName = "AuthorName",
                        EmailAddress = "author@bookstore.com"
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
