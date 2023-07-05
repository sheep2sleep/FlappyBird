/// <summary>
/// 常规单例类
/// </summary>
/// <typeparam name="T"></typeparam>
internal class Singleton<T> where T : new()
{
    //需要对T添加泛型约束，new()代表必须要有一个无参的构造函数
    private static T instance;
    
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}