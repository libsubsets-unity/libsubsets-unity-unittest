using System.Collections;

namespace LibSubsets.UnitTest {
  /**
   * \class AsyncTask
   *
   * \brief 비동기 태크스를 위한 베이스 클래스
   * 
   * \authro Lee, Hyeon-gi
   */
  abstract public class AsyncTask {
    abstract public void Start();
    abstract public bool IsWait();
    abstract public void End();
  }
}
