using System.Collections;
using UnityEngine;

public class DelayActive : MonoBehaviour
{
    //public float dealy = 1;

    //// Use this for initialization
    //private void Start()
    //{
    //    StartCoroutine(Delay());
    //}

    //private IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(dealy);
    //    this.gameObject.SetActive(false);
    //}

    /// <summary>
    /// ������������
    /// </summary>
    private void notActive()
    {
        gameObject.SetActive(false);
    }
}