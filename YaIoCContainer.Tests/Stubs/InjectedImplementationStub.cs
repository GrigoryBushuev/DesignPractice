using YaIoCContainer.Tests.Abstractions;

namespace YaIoCContainer.Tests.Stubs
{
    public class InjectedImplementationStub : IInjectedInterface
    {
        public string Method()
        {
            return "InjectedImplementationStub";
        }
    }
}
