using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[CSharpCallLua]
public class LuaAdd : MonoBehaviour
{
    private LuaEnv luaEnv=new LuaEnv();
    private LuaTable luaTable;
    private Action luaStart;
    private Action luaUpdate;
    
    private void Awake()
    {
        luaTable = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index",luaEnv.Global);
        luaTable.SetMetaTable(meta);
        meta.Dispose();
        
        luaTable.Set("self",this);
        LuaEnv.CustomLoader method = custom;
        luaEnv.AddLoader(method);
        Dostring("Main");
        GetLuaMethod();
    }


    private void Start()
    {
        luaStart?.Invoke();
    }

    private void Update()
    {
        luaUpdate?.Invoke();
    }

    public byte[] custom(ref string name)
    {
        string path = Application.dataPath + "/Lua/" + name + ".lua";
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }

        return null;
    }

    public void Dostring(string name)
    {
        if (name != null)
        {
            luaEnv.DoString("require'" + name + "'","Main",luaTable);
        }
    }

    public void GetLuaMethod()
    {
        luaTable.Get("start",out luaStart);
        luaTable.Get("update",out luaUpdate);
    }
}
