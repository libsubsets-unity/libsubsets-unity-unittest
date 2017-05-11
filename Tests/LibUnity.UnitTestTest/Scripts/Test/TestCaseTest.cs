using System.Collections.Generic;
using LibUnity.UnitTest;

namespace LibUnity.UnitTestTest.Test {

  public class TestCase_test : TestCase {
    [TestMethod]
    public void TestMethod() {
      test_case = CreateTestCase(typeof(WasRun), "TestMethod") as WasRun;
      test_case.Run(result);
      Waits(1);
      Runs(on_run_result);
    }

    [TestMethod]
    public void test_async() {
      WaitsFor(delegate () {
        return true;
      }, 1000);
      Runs(delegate () {
        test_case = CreateTestCase(typeof(WasRun), "TestMethod") as WasRun;
        test_case.Run(result);
      });
      Runs(on_run_result);
    }

    private void on_run_result() {
      Assert(test_case.result == "SetUp TestMethod TearDown");
    }

    [TestMethod]
    public void TestResult() {
      test_case = CreateTestCase(typeof(WasRun), "TestMethod") as WasRun;
      test_case.Run(result);
      Assert(result.Summary() == "WasRun: 1 run, 0 failed");
    }

    override protected void SetUp() {
      string suite_name = "WasRun";
      result = new TestResult(suite_name);
    }

    override protected void TearDown() {
    }

    private TestResult result;
    private WasRun test_case = null;
  }
}
