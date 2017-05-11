using System;
using System.Collections;

namespace LibUnity.Test {
  public delegate bool IsDoneCallback();

  public class WaitsFor : AsyncTask {
    public WaitsFor(IsDoneCallback callback, long timeout) {
      this.callback = callback;
      this.timeout = timeout;
    }

    public override void Start() {
      this.complete_time = GetCurrentTime() + timeout;
    }

    override public bool IsWait() {
      if (IsTimeout()) {
        return false;
      }
      return !callback();
    }

    private bool IsTimeout() {
      return GetCurrentTime() >= complete_time;
    }

    override public void End() {
      if (IsTimeout()) {
        throw new Exception("Timeout");
      }
    }

    private long GetCurrentTime() {
      return (long)(DateTime.Now.Ticks / 10000.0f);
    }    

    private IsDoneCallback callback;
    private long timeout;
    private long complete_time;
  }
}
