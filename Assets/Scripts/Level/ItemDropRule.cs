using UnityEngine;

public class ItemDropRule : MonoBehaviour
{
    public Item item;
    public float dropRatio;

    /// <summary>
    /// Ö´ÐÐµôÂä
    /// </summary>
    /// <param name="pos"></param>
    public void Execute(Vector3 pos)
    {
        if (Random.Range(0f, 100f) < dropRatio)
        {
            Item rule = Instantiate<Item>(item);
            rule.transform.position = pos;
        }
    }
}