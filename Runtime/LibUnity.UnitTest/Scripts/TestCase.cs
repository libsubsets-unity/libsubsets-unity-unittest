using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace LibUnity.UnitTest {
  /**
   * \ingroup LibUnity.UnitTest
   *
   * \class TestCase
   *
   * \brief 테스트 메서드들을 하나의 테스트 케이스로 묶어주는 역할을 처리
   * \author Lee, Hyeon-gi
   */
  public abstract class TestCase : MonoBehaviour {

    public TestCase() {
    }

    abstract protected void SetUp();
    abstract protected void TearDown();

    public void SetRunMethodName(string methodName) {
      this.methodName = methodName;
    }

    public void Run(TestResult testResult) {
      this.testResult = testResult;
      StartCoroutine(RunTest());
    }

    private IEnumerator RunTest() {
      testResult.TestStart();
      SetUp();
      RunMethod();

      foreach (AsyncTask task in asyncTasks) {
        yield return null;
        task.Start();
        while (task.IsWait()) {
          yield return null;
        }
        try {
          task.End();
        }
        catch (Exception e) {
          Failed(e);
        }
      }

      TearDown();
      test_complete = true;
    }

    private void RunMethod() {
      try {
        try {
          Type thisType = this.GetType();
          MethodInfo theMethod = thisType.GetMethod(methodName);
          if (null == theMethod)
            throw new Exception(methodName + " is null");
          theMethod.Invoke(this, null);
        }
        catch (TargetInvocationException e) {
          throw e.InnerException;
        }
      }
      catch (Exception e) {
        Failed(e);
      }
    }

    private void Failed(Exception e) {
      testResult.TestFailed();
      System.Diagnostics.StackTrace stack_trace =
        new System.Diagnostics.StackTrace(e, true);
      string file_name = stack_trace.GetFrame(1).GetFileName();
      int file_line = stack_trace.GetFrame(1).GetFileLineNumber();
      Debug.LogError(e.Message + " : " + GetType().Name + "::" + methodName +
        "\n" + file_name + ":" + file_line);
    }

    public bool IsComplete() {
      return test_complete;
    } 

    public void AssertFalse(bool condition, string message = "Test failed") {
      if (!condition == false) {
        throw new Exception(message);
      }
    }

    public void AssertTrue(bool condition, string message = "Test failed") {
      if (!condition == true) {
        throw new Exception(message);
      }
    }

    public void Assert(bool condition, string message = "Test failed") {
      if (!condition) {
        throw new Exception(message);
      }
    }

    public void AssertEqual<T>(T expected, T actual,
      string message = "Test faield") {
      if (!EqualityComparer<T>.Default.Equals(expected, actual))
        throw new Exception(message);
    }

    /**
     * 시간만큼 대기후 다음 테스트를 진행
     *
     * \param wait_time 대기시간(milliseconds)
     */
    public void Waits(long wait_time) {
      asyncTasks.Add(new Waits(wait_time));
    }

    /**
      * 넘겨진 조건 검사함수를 통해 대기를 결정한후 다음 테스트 진행
      * 
      * \parma isDone 작업이 완료 되었는지를 조회하는 콜백 함수. 
      * \param timeout 작업의 타임 아웃 시간
      */
    public void WaitsFor(IsDoneCallback isDone, long timeout) {
      asyncTasks.Add(new WaitsFor(isDone, timeout));
    }

    public void Runs(RunsCallback callback) {
      asyncTasks.Add(new Runs(callback));
    }

    virtual public List<TestCase> get_tests() {
      List<TestCase> result = new List<TestCase>();
      MethodInfo[] methods = GetType().GetMethods(BindingFlags.NonPublic |
          BindingFlags.Public | BindingFlags.Instance);
      foreach (MethodInfo method in methods) {
        if (Attribute.IsDefined(method, typeof(TestMethod))) {
          result.Add(CreateTestCase(GetType(), method.Name));
        }
      }
      return result;
    }

    protected TestCase CreateTestCase(Type test_case_type, string methodName) {
      TestCase test = gameObject.AddComponent(test_case_type) as TestCase;
      test.SetRunMethodName(methodName);
      return test;
    }

    private string methodName;
    private TestResult testResult;
    private List<AsyncTask> asyncTasks = new List<AsyncTask>();
    private bool test_complete = false;

  }
}
