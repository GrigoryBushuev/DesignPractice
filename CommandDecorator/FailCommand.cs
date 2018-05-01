using System;

namespace CommandDecorator
{
    public class FailCommand : ICommand
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
