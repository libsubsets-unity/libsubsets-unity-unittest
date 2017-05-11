using System;
using UnityEngine;

namespace LibUnity.Test {
  [AttributeUsage(AttributeTargets.Method)]
  public class TestMethod : Attribute {
    public TestMethod() {
    }
  }
}