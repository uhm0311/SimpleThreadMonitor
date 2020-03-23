# SimpleThreadMonitor
`SimpleThreadMonitor` provides simple mutex feature.

```csharp
using SimpleThreadMonitor;

object lock = new object();

Mutex.Lock(lock, () => 
{
  // Do something
});
```

# Implementaion
```csharp
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
```

# Note
Since this project is only for simple mutex, there is **NO plan to provide any other complex features**.
