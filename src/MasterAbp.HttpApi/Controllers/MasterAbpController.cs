using MasterAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MasterAbp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MasterAbpController : AbpControllerBase
{
    protected MasterAbpController()
    {
        LocalizationResource = typeof(MasterAbpResource);
    }
}
