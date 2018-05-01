using System;
using YaIoCContainer.Tests.Abstractions;


namespace YaIoCContainer.Tests.Stubs
{
    public class YaImplementationStub : IYaInjectedInterface
    {
        public string YaMethod()
        {
            return String.Empty;
        }
    }
}
