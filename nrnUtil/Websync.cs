using System;
using System.Threading;

namespace nrnUtil
{
    public class Websync
    {
        public delegate object tryFunc();

        //public static Thread worker;
        public static T Exec<T>(tryFunc tf) where T : new()
        {
            T tmp = new T();
            try
            {
                Thread worker = new Thread(() =>
                       {
                           try
                           {
                               tmp = (T)tf();
                           }
                           catch
                           {

                           }
                       });

                worker.Start();
                while (worker.IsAlive)
                    Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return tmp;
        }
    }
}