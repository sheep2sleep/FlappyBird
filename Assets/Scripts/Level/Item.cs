using UnityEngine;

public class Item : MonoBehaviour
{
    public int AddHP = 20;

    public GameObject bullet;

    public float lifeTime = 30;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //道具产生后自己下落
        this.transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="target"></param>
    public void Use(Unit target)
    {
        target.AddHP(this.AddHP);
        Destroy(this.gameObject);
    }
}