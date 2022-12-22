using System;
using UnityEngine;

namespace LibSubsets.UnitTest {
  [AttributeUsage(AttributeTargets.Method)]
  public class TestMethod : Attribute {
    public TestMethod() {
    }
  }
}