using System;
using System.Collections.Generic;
using System.Text;
using MasterAbp.Localization;
using Volo.Abp.Application.Services;

namespace MasterAbp;

/* Inherit your application services from this class.
 */
public abstract class MasterAbpAppService : ApplicationService
{
    protected MasterAbpAppService()
    {
        LocalizationResource = typeof(MasterAbpResource);
    }
}
