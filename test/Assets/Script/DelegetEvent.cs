using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class DelegetEvent : MonoBehaviour
{
    public delegate void EventHander();

    private LoadLua load;

    public EventHander Onclick;
    public EventHander handerr1;
    void Start()
    {
        //Onclick(gameObject);
        load=GetComponent<LoadLua>();
        load.Dostring("EventHandler");
        load.luaEnv.Global.Get("eventTable:func1",out handerr1);
        Onclick += Func1;
        Onclick();
        handerr1?.Invoke();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [LuaCallCSharp]
    public void Func1()
    {
        Debug.Log(22222222222);
    }
}
