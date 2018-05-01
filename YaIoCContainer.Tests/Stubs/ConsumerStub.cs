using YaIoCContainer.Tests.Abstractions;

namespace YaIoCContainer.Tests.Stubs
{
    public class ConsumerStub
    {
        private IInjectedInterface _injectedInterface;
        private IYaInjectedInterface _yaInjectedInterface;

        public ConsumerStub(IInjectedInterface injectedInterface, IYaInjectedInterface yaInjectedInterface)
        {
            _injectedInterface = injectedInterface;
            _yaInjectedInterface = yaInjectedInterface;
        }

        public string CallInjectedMethod()
        {
            return _injectedInterface.Method();
        }

        public string CallYaInjectedMethod()
        {
            return _yaInjectedInterface.YaMethod();
        }
    }
}
