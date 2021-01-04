using Microsoft.AspNetCore.Hosting;

namespace XUnitTestProjectCRUD
{
    internal class TestServer
    {
        private IWebHostBuilder webHostBuilder;

        public TestServer(IWebHostBuilder webHostBuilder)
        {
            this.webHostBuilder = webHostBuilder;
        }
    }
}