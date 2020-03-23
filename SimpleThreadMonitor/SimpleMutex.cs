using System;
using System.Threading;

namespace SimpleThreadMonitor
{
    public static class SimpleMutex
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

                    try { ExceptionCallback?.Invoke(Exception); }
                    catch { }
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
