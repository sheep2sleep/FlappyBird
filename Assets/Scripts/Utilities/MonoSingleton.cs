using UnityEngine;

/// <summary>
/// 实现Mono单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //在继承时要约束T的类型为MonoBehaviour
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                //不能直接new一个T，因为MonoBehaviour的脚本是要挂载在组件上的，重新new就和挂载的那个不一致了
                instance = (T)FindObjectOfType<T>();
            }
            return instance;
        }
    }
}
