using System;
using System.Threading;

namespace SimpleThreadMonitor
{
    public static class Mutex
    {
        public static void Lock(object Object, Action Process, Action<Exception> ExceptionCallback = null, bool ReleaseLockBeforeExceptionCallback = false)
        {
            if (Object != null && Process != null)
            {
                try
                {
                    Monitor.Enter(Object);
                    Process();
                }
                catch (Exception Exception)
                {
                    if (ReleaseLockBeforeExceptionCallback)
                    {
                        Monitor.Exit(Object);
                    }

                    ExceptionCallback?.Invoke(Exception);
                }
                finally
                {
                    if (!ReleaseLockBeforeExceptionCallback)
                    {
                        Monitor.Exit(Object);
                    }
                }
            }
        }
    }
}
