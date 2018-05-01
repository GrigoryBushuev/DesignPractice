using System;

namespace CommandDecorator
{
    public class RetryOnFailCommand : ICommand
    {
        private ICommand _command;
        private int _attemptsCount;
        private int _maxAttempts;

        public RetryOnFailCommand(ICommand command, int maxAttempts)
        {
            _command = command;
            _maxAttempts = maxAttempts;
        }

        public void Execute()
        {
            try
            {
                _command.Execute();
            }
            catch (Exception)
            {
                if (_attemptsCount == _maxAttempts - 1)
                    throw;

                _attemptsCount++;
                Execute();
            }
        }
    }
}
