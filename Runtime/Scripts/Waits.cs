using System;
using UnityEngine;

namespace LibSubsets.UnitTest {
  public class Waits : AsyncTask {
    public Waits(long waitTime) {
      this.waitTime = waitTime;
    }

    public override void Start() {
      this.completeTime = GetCurrentTime() + waitTime;
    }

    override public bool IsWait() {
      return GetCurrentTime() < completeTime ? true : false;
    }

    override public void End() {
    }

    private long GetCurrentTime() {
      return (long)(DateTime.Now.Ticks / 10000);
    }

    private long waitTime;
    private long completeTime;
  }
}
