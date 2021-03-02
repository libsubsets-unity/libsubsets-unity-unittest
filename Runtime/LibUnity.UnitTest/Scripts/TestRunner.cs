using UnityEngine;
using System;
using System.Collections;

namespace LibUnity.UnitTest {
  /**
   * \class TestRunner
   *
   * \brief 테스트슈트 차례대로 실행해주는 클래스.
   * 
   * \author Lee, Hyeon-gi
   */
  public class TestRunner : MonoBehaviour {
    public void Start() {
      testSuites = FindObjectsOfType(typeof(TestSuite)) as TestSuite[];
      StartCoroutine(RunSuites());
    }

    private IEnumerator RunSuites() {
      foreach (TestSuite suite in testSuites) {
        suite.Run();
        while (!suite.IsComplete()) {
          yield return null;
        }
        suite.PrintSummary();
      }
    }

    private TestSuite[] testSuites;
  }

}
