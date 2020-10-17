using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LoadLua : MonoBehaviour
{
    public LuaEnv luaEnv;
    [CSharpCallLua]
    public delegate void function();
    public function LuaStart;
    public function LuaUpdate;
    public function LuaEnable;
    private float lasttime = 0;
    private float GCInterval = 1;
    private void Awake()
    {
        luaEnv=new LuaEnv();
        LuaEnv.CustomLoader method = Custom;
        luaEnv.AddLoader(method);
        Dostring("Main");
        GetFunc();
    }

    private void OnEnable()
    {
        if (LuaEnable != null)
        {
            LuaEnable();
        }
    }

    void Start()
    {
        if (LuaStart != null)
        {
            LuaStart();
        }

        if (Time.time - lasttime > GCInterval)
        {
            luaEnv.Tick();
            lasttime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LuaUpdate?.Invoke();
    }

    private byte[] Custom(ref string filename)
    {
        string path = Application.dataPath + "/Lua/" + filename + ".lua";
        Debug.Log(path);
        if(File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
    }

    private void GetFunc()
    {
        luaEnv.Global.Get("Start1",out LuaStart);
        luaEnv.Global.Get("update",out LuaUpdate);
        luaEnv.Global.Get("enable",out LuaEnable);
    }

    public void Dostring(string filename)
    {
        string str = "require '" + filename + "'";
        Debug.Log(str);
        luaEnv.DoString(str);
    }

    private void OnDestroy()
    {
        //LuaUpdate = null;
        //LuaStart = null;
        luaEnv.Dispose();
    }
}
