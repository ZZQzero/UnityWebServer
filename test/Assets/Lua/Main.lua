require "Init"
require("Test1")
require("EventHandler")
local cube=nil
function Start1()

    local manager=GameObject.Find("GameManager")
    local UIManager=manager:GetComponent("UIManager")
    local Go= GameObject.Instantiate(UIManager:GetPrefab("Prefab/Panel"))
    Go.transform:SetParent(GameObject.Find("Canvas").transform)

    --local Go= Instantiate(Resources.Load("Prefab/Panel"))
    --Go.transform:SetParent(UIManager:GetCanvas())
    Go.transform.localPosition= Vector3.zero
    --Go:AddComponent("CS.Test2")
    local table={}
    for i = 1, Go.transform.childCount do
        table[i]=Go.transform:GetChild(i-1)      
    end

    --for i, v in pairs(table) do
      --  v:GetComponent("Image").color=CS.UnityEngine.Color.blue
    --end
    table[1]:GetComponent("Image").color=CS.UnityEngine.Color.green
    table[2]:GetComponent("Image").color=CS.UnityEngine.Color.blue
    table[3]:GetComponent("Image").color=CS.UnityEngine.Color.red
    
    table[1]:GetComponent("Button").onClick:AddListener(function()
        cube= GameObject.Instantiate(UIManager:GetPrefab("Prefab/Cube"))
        cube.transform.position=Vector3.zero
    end)
    table[2]:GetComponent("Button").onClick:AddListener(function()
        if cube ~=nil then
            cube.transform.localScale=Vector3.one*5
        end
    end)
    table[3]:GetComponent("Button").onClick:AddListener(function()
        if cube ~=nil then
            Destroy(cube)
            
        end
    end)
    --test2:test2func()
    --test:func()
    --test:func1(3,5)
    --eventTable:Onclick()
    --local go=manager:GetComponent("DelegetEvent")
    --go:Func1()
    --eventTable:Onclick(go)
    --Event:Func1()
end

function update()
    if cube ~=nil then
        cube.transform:Rotate(Vector3.up*deltaTime*120)
    end
end

function start()
    print("lua start...")
    --self.gameObject:GetComponent("Image").color=CS.UnityEngine.Color.blue

    --[[self.gameObject:SetActive(false);
    self:GetComponent("Button").onClick:AddListener(function()
        print("clicked, you input is '" ..input:GetComponent("InputField").text .."'")
    end)--]]
end

function enable()
    print("运行时间："..Time.time)
end

function start2()
    local a=3
    local b=4
    print(a+b)
end

--[[local Game=class("Game")

function Game.Init()

end



return Game--]]