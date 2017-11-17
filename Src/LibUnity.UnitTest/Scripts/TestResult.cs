using System;

namespace LibUnity.UnitTest {
  /**
   * \class TestResult
   *
   * \brief 테스트 결과를 처리하는 클래스
   * \author Lee, Hyeon-gi
   */
  public class TestResult {

    public TestResult(string suite_name) {
      this.suite_name = suite_name;
    }
  
    public void TestStart() {
      run_count++;
    }

    public void TestFailed() {
      failed_count++;
    }

    public bool IsFailed() {
      return failed_count > 0 ? true : false;
    }

    public string Summary() {
      return suite_name + ": " +  run_count + " run, " + failed_count + " failed";
    }

    private string suite_name;
    private int run_count = 0;
    private int failed_count = 0;

  }
}
