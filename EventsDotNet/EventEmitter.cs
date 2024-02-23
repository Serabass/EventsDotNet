namespace EventsDotNet;

public class EventEmitter<I, O>
{
  public delegate void EmiterDelegate(I oldValue, I newValue);

  public event EmiterDelegate Emitter;

  public EventEmitter<I, O> On(Func<I, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(n) && !condition(o))
      {
        cb(n);
      }
    };

    return this;
  }

  public EventEmitter<I, O> On(Func<I, bool> condition, Func<I, Task> cb)
  {
    Emitter += async (o, n) =>
    {
      if (condition(n) && !condition(o))
      {
        await cb(n);
      }
    };

    return this;
  }

  public EventEmitter<I, O> On(Func<I, Task<bool>> condition, Action<I> cb)
  {
    Emitter += async (o, n) =>
    {
      if (await condition(n) && !await condition(o))
      {
        cb(n);
      }
    };

    return this;
  }

  public EventEmitter<I, O> On(Func<I, Task<bool>> condition, Func<I, Task> cb)
  {
    Emitter += async (o, n) =>
    {
      if (await condition(n) && !await condition(o))
      {
        await cb(n);
      }
    };

    return this;
  }

  public EventEmitter<I, O> On(Func<I, I, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(o, n))
      {
        cb(n);
      }
    };

    return this;
  }
  
  public EventEmitter<I, O> On(Func<I, I, Task<bool>> condition, Action<I> cb)
  {
    Emitter += async (o, n) =>
    {
      if (await condition(o, n))
      {
        cb(n);
      }
    };

    return this;
  }

  public EventEmitter<I, O> On<R, RI>(Func<RI> input, Func<RI, bool> condition, Action<I> cb)
  {
    Emitter += (o, n) =>
    {
      if (condition(input()))
      {
        cb(n);
      }
    };

    return this;
  }

  public void On(I value, Action<I> cb)
  {
    Emitter += async (o, n) =>
    {
      if (n.Equals(value) && !o.Equals(value))
      {
        cb(n);
      }
    };
  }
}
