using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Skyline.Pages
{
    public class Index_Tests : SkylineWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
