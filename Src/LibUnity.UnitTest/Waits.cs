using System;

namespace LibUnity.UnitTest {
  public class Waits : AsyncTask {
    public Waits(long wait_time) {
      this.wait_time = wait_time;
    }

    public override void Start() {
      this.complete_time = GetCurrentTime() + wait_time;
    }

    override public bool IsWait() {
      return GetCurrentTime() < complete_time ? true : false;
    }

    override public void End() {
    }

    private long GetCurrentTime() {
      return (long)(DateTime.Now.Ticks / 10000.0f);
    }

    private long wait_time;
    private long complete_time;
  }
}
