using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MasterAbp.Web;

[Dependency(ReplaceServices = true)]
public class MasterAbpBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MasterAbp";
}
