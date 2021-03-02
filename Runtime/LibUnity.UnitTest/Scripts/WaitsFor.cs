using System;
using System.Collections;

namespace LibUnity.UnitTest {
  public delegate bool IsDoneCallback();

  public class WaitsFor : AsyncTask {
    public WaitsFor(IsDoneCallback callback, long timeout) {
      this.callback = callback;
      this.timeout = timeout;
    }

    public override void Start() {
      this.completeTime = GetCurrentTime() + timeout;
    }

    override public bool IsWait() {
      if (IsTimeout()) {
        return false;
      }
      return !callback();
    }

    private bool IsTimeout() {
      return GetCurrentTime() >= completeTime;
    }

    override public void End() {
      if (IsTimeout()) {
        throw new Exception("Timeout");
      }
    }

    private long GetCurrentTime() {
      return (long)(DateTime.Now.Ticks / 10000);
    }

    private IsDoneCallback callback;
    private long timeout;
    private long completeTime;
  }
}
