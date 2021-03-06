﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace BookStore.Authorization
{
    public class BookStoreAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var delete = context.GetPermissionOrNull(PermissionNames.Delete);
            if (delete == null)
            {
                delete = context.CreatePermission(PermissionNames.Delete, L("Delete"));
            }

            var edit = context.GetPermissionOrNull(PermissionNames.Edit);
            if (edit == null)
            {
                edit = context.CreatePermission(PermissionNames.Edit, L("Edit"));
            }

            var rate = context.GetPermissionOrNull(PermissionNames.Rate);
            if (rate == null)
            {
                rate = context.CreatePermission(PermissionNames.Rate, L("Rate"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BookStoreConsts.LocalizationSourceName);
        }
    }
}
