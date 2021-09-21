using Unity.Collections;
using Unity.Jobs;

namespace HelloJobSystem.Scripts
{
    /// <summary>
    /// 注意这里用 struct 不要用 class
    /// </summary>
    struct HelloJobSystemJob : IJob
    {
        [ReadOnly]
        public int a;

        [ReadOnly]
        public int b;

        public NativeArray<int> results;

        public void Execute()
        {
            results[0] = a + b;
        }
    }
}