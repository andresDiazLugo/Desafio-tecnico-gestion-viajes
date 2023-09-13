using WB.EntrevistaABP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WB.EntrevistaABP.Permissions;

public class EntrevistaABPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EntrevistaABPPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EntrevistaABPPermissions.MyPermission1, L("Permission:MyPermission1"));
        var pasajerosPermmision = myGroup.AddPermission(EntrevistaABPPermissions.Pasajeros.Default,L("Permission:Pasajeros"));
        pasajerosPermmision.AddChild(EntrevistaABPPermissions.Pasajeros.Create, L("Permission:Pasajeros.Create"));
        pasajerosPermmision.AddChild(EntrevistaABPPermissions.Pasajeros.Edit, L("Permission:Pasajeros.Edit"));
        pasajerosPermmision.AddChild(EntrevistaABPPermissions.Pasajeros.Delete, L("Permission:Pasajeros.Delete"));

        var viajesPermission = myGroup.AddPermission(
        EntrevistaABPPermissions.Viajes.Default, L("Permission:Viajes"));
        viajesPermission.AddChild(
        EntrevistaABPPermissions.Viajes.Create, L("Permission:Viajes.Create"));
        viajesPermission.AddChild(
        EntrevistaABPPermissions.Viajes.Edit, L("Permission:Viajes.Edit"));
        viajesPermission.AddChild(
        EntrevistaABPPermissions.Viajes.Delete, L("Permission:Viajes.Delete"));

        var reservasPermission = myGroup.AddPermission(
        EntrevistaABPPermissions.Reservas.Default, L("Permission:Reservas"));
        reservasPermission.AddChild(
        EntrevistaABPPermissions.Reservas.Create, L("Permission:Reservas.Create"));
        reservasPermission.AddChild(
        EntrevistaABPPermissions.Reservas.Edit, L("Permission:Reservas.Edit"));
        reservasPermission.AddChild(
        EntrevistaABPPermissions.Reservas.Delete, L("Permission:Reservas.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EntrevistaABPResource>(name);
    }
}
