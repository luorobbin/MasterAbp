using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MasterAbp.Pages;

public class Index_Tests : MasterAbpWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
