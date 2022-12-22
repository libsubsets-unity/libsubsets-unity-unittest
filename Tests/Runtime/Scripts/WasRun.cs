using System.Collections.Generic;
using LibSubsets.UnitTest;

namespace LibSubsets.UnitTestTest {

  public class WasRun : TestCase {
    public string result = "";

    public WasRun() : base() {
    }

    override protected void SetUp() {
      result += "SetUp ";
    }

    override protected void TearDown() {
      result += "TearDown";
    }

    public void TestMethod() {
      result += "TestMethod ";
    }
  }
}
