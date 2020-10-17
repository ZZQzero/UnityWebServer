using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject go= GameObject.Instantiate(GetPrefab("Prefab/Panel"));
        //go.transform.SetParent(GetCanvas());
        //go.transform.localPosition=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [LuaCallCSharp]
    public GameObject GetPrefab(string name)
    {
        GameObject go= Resources.Load<GameObject>(name);
        go.AddComponent<Test2>();
        return go;
    }
    [LuaCallCSharp]
    public Transform GetCanvas()
    {
        return GameObject.Find("Canvas").transform;
    }
}
