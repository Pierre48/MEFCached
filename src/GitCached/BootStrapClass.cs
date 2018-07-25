using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorConsole;

namespace GitCached
{
    internal class BootStrapClass
    {
        private bool _started = false;

        internal void Start()
        {
            if (_started)
            {
                CConsole.WriteLine("Bootstrap is already started !",ConsoleColor.Red);
                return;
            }
            CConsole.WriteLine("Starting ... ");
            CConsole.WriteLine("Started", ConsoleColor.Green);
            _started = true;
        }
        internal void Stop()
        {

            if (!_started)
            {
                CConsole.WriteLine("Bootstrap is not started !", ConsoleColor.Red);
                return;
            }
        }
    }
}
