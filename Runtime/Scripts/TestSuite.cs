using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace LibSubsets.UnitTest {
  /**
   * \class TestSuite
   *
   * \brief TestCase를 테스트 주제 단위로 묶어서 실행해주는 역할을 한다.
   *
   * \author Lee, Hyeon-gi
   */
  public class TestSuite : MonoBehaviour {
    public string suiteName;

    public void Awake() {
      if (0 == suiteName.Length)
        suiteName = gameObject.name;

      tests = new List<TestCase>();
      TestCase[] testCases = GetComponents<TestCase>();
      for (int i = 0; i < testCases.Length; i++) {
        AddTests(testCases[i]);
      }
      testResult = new TestResult(suiteName);
    }

    private void AddTests(TestCase test_case) {
      List<TestCase> results = test_case.get_tests();
      for (int i = 0; i < results.Count; i++) {
        tests.Add(results[i]);
      }
    }

    protected TestCase CreateTestCase(Type testCaseType, string methodName) {
      TestCase test = gameObject.AddComponent(testCaseType) as TestCase;
      test.SetRunMethodName(methodName);
      return test;
    }

    public void Run() {
      StartCoroutine(RunTests());
    }

    private IEnumerator RunTests() {
      foreach (TestCase current in tests) {
        current.Run(testResult);
        while (!current.IsComplete()) {
          yield return null;
        }
      }
    }

    public bool IsComplete() {
      foreach (TestCase test_case in tests) {
        if (!test_case.IsComplete())
          return false;
      }
      return true;
    }

    public string Summary() {
      return testResult.Summary();
    }

    public void PrintSummary() {
      if (testResult.IsFailed())
        Debug.LogError(Summary());
      else
        Debug.Log(Summary());
    }

    private List<TestCase> tests;
    private TestResult testResult;
  }
}
