using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace LibUnity.Test {
  /**
   * \class TestSuite
   *
   * \brief TestCase를 테스트 주제 단위로 묶어서 실행해주는 역할을 한다.
   *
   * \author Lee, Hyeon-gi
   */
  public class TestSuite : MonoBehaviour {
    public string suite_name;

    public void Awake() {
      if (0 == suite_name.Length)
        suite_name = gameObject.name;

      tests = new List<TestCase>();
      TestCase[] test_cases = GetComponents<TestCase>();
      for (int i = 0; i < test_cases.Length; i++) {
        AddTests(test_cases[i]);
      }
      test_result = new TestResult(suite_name);
    }

    private void AddTests(TestCase test_case) {
      List<TestCase> results = test_case.get_tests();
      for (int i = 0; i < results.Count; i++) {
        tests.Add(results[i]);
      }
    }

    protected TestCase CreateTestCase(Type test_case_type, string method_name) {
      TestCase test = gameObject.AddComponent(test_case_type) as TestCase;
      test.SetRunMethodName(method_name);
      return test;
    }

    public void Run() {
      StartCoroutine(RunTests());
    }

    private IEnumerator RunTests() {
      foreach (TestCase current in tests) {
        current.Run(test_result);
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
      return test_result.Summary();
    }

    public void PrintSummary() {
      if (test_result.IsFailed())
        Debug.LogError(Summary());
      else
        Debug.Log(Summary());
    }

    private List<TestCase> tests;
    private TestResult test_result;
  }
}
