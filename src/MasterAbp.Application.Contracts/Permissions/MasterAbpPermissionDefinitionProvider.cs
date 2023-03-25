using MasterAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MasterAbp.Permissions;

public class MasterAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup("ProductManagement");
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MasterAbpPermissions.MyPermission1, L("Permission:MyPermission1"));

        myGroup.AddPermission("ProductManagement.ProductCreation", L("ProductCreation"));
        myGroup.AddPermission("ProductManagement.ProductDeletion", L("ProductDeletion"));

        var parent = myGroup.AddPermission("MyParentPermission");
        parent.AddChild("MyChildPermission");
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterAbpResource>(name);
    }
}
