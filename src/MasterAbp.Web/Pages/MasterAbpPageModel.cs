using MasterAbp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MasterAbp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class MasterAbpPageModel : AbpPageModel
{
    protected MasterAbpPageModel()
    {
        LocalizationResourceType = typeof(MasterAbpResource);
    }
}
