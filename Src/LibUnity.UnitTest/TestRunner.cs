using UnityEngine;
using System;
using System.Collections;

namespace LibUnity.Test {
  /**
   * \class TestRunner
   *
   * \brief �׽�Ʈ��Ʈ ���ʴ�� �������ִ� Ŭ����.
   * 
   * \author Lee, Hyeon-gi
   */
  public class TestRunner : MonoBehaviour {
    public void Start() {
      test_suites = FindObjectsOfType(typeof(TestSuite)) as TestSuite[];
      StartCoroutine(RunSuites());
    }

    private IEnumerator RunSuites() {
      foreach (TestSuite suite in test_suites) {
        suite.Run();
        while (!suite.IsComplete()) {
          yield return null;
        }
        suite.PrintSummary();
      }
    }

    private TestSuite[] test_suites;
  }

}
