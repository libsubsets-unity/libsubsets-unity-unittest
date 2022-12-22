using System;
using System.Collections;

namespace LibSubsets.UnitTest {
  public delegate void RunsCallback();

  public class Runs : AsyncTask {
    public Runs(RunsCallback callback) {
      this.callback = callback;
    }

    override public void Start() {
    }

    override public bool IsWait() {
      return false;
    }

    override public void End() {
      callback();
    }

    private RunsCallback callback;
  }
}
