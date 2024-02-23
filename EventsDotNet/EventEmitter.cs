namespace EventsDotNet;

public class EventEmitter<I, O>
{
  public delegate void EmiterDelegate(I oldValue, I newValue);

  public event EmiterDelegate Emitter;

  public void On(Func<I, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(n) && !condition(o))
      {
        cb(n);
      }
    };
  }
  
  public void On(Func<I, I, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(o, n))
      {
        cb(n);
      }
    };
  }
  
  public void On(Func<I, I, Task<bool>> condition, Action<I> cb)
  {
    Emitter += async (o, n) =>
    {
      if (await condition(o, n))
      {
        cb(n);
      }
    };
  }

  public void On(Func<I, Task<bool>> condition, Action<I> cb)
  {
    Emitter += async (o, n) =>
    {
      if (await condition(n) && !await condition(o))
      {
        cb(n);
      }
    };
  }

  public void On<R, RI>(Func<RI> input, Func<RI, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(input()))
      {
        cb(n);
      }
    };
  }
}
