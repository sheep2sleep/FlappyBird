using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //子弹移动
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //子弹离开屏幕空间后1s销毁
        if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(transform.position)) == false)
        {
            Destroy(gameObject, 1f);
        }
    }
}
