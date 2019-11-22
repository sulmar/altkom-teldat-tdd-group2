using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    public class Logger
    {
        public string LastMessage { get; private set; }

        public event EventHandler<DateTime> MessageLogged;

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException();

            LastMessage = message;

            // Write the log to a storage
            // ...

           MessageLogged?.Invoke(this, DateTime.UtcNow);
        }


        public async Task LogAsync(string message)
        {
            await Task.Run(() => LongTime(message));

            LastMessage = message;
        }

        private void LongTime(string message)
        {
            while (true)
            {
                // Writing to log...
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
            // Saved.
        }
    }
}
