using UnityEngine;

internal class GameUtil : Singleton<GameUtil>
{
    /// <summary>
    /// �ж���Ϸ�����Ƿ񳬳���Ļ
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool InScreen(Vector3 position)
    {
        return Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(position));
    }
}
