using UnityEngine;

internal class GameUtil : Singleton<GameUtil>
{
    /// <summary>
    /// 判断游戏物体是否超出屏幕
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool InScreen(Vector3 position)
    {
        return Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(position));
    }
}
