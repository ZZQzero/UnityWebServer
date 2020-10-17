using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Detest(int num);

    public Detest test1;
        
        
    void Start()
    {
        
        test1 = func1;
        test1(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void func1(int num)
    {
        Debug.Log(num);
    }
}
