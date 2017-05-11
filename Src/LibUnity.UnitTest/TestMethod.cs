using System;
using UnityEngine;

namespace LibUnity.UnitTest {
  [AttributeUsage(AttributeTargets.Method)]
  public class TestMethod : Attribute {
    public TestMethod() {
    }
  }
}