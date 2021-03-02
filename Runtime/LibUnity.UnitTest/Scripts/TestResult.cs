using System;

namespace LibUnity.UnitTest {
  /**
   * \class TestResult
   *
   * \brief 테스트 결과를 처리하는 클래스
   * \author Lee, Hyeon-gi
   */
  public class TestResult {

    public TestResult(string suiteName) {
      this.suiteName = suiteName;
    }
  
    public void TestStart() {
      runCount++;
    }

    public void TestFailed() {
      failedCount++;
    }

    public bool IsFailed() {
      return failedCount > 0 ? true : false;
    }

    public string Summary() {
      return suiteName + ": " +  runCount + " run, " + failedCount + " failed";
    }

    private string suiteName;
    private int runCount = 0;
    private int failedCount = 0;

  }
}
