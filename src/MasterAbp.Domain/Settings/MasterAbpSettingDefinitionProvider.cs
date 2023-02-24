using Volo.Abp.Settings;

namespace MasterAbp.Settings;

public class MasterAbpSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MasterAbpSettings.MySetting1));
    }
}
