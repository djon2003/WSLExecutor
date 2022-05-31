using System;

namespace WSLExecutor
{
    internal class Program
    {
        private const string WSL_PATH = @"C:\Windows\System32\wsl.exe";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Has to be run with arguments representing the Linux command to execute");
                Environment.Exit(-100);
            }

            if (!System.IO.File.Exists(WSL_PATH))
            {
                Console.WriteLine(WSL_PATH + " doesn't exist");
                Environment.Exit(-101);
            }

            var exec = new System.Diagnostics.Process();
            exec.StartInfo.FileName = WSL_PATH;
            exec.StartInfo.UseShellExecute = false;
            exec.StartInfo.RedirectStandardOutput = true;
            exec.StartInfo.RedirectStandardError = true;
            exec.StartInfo.Arguments = String.Join(" ", args);
            exec.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            exec.Start();
            exec.WaitForExit();
            var myout = exec.StandardOutput.ReadToEnd();
            Console.WriteLine(myout);
            Environment.Exit(exec.ExitCode);
        }
    }
}
