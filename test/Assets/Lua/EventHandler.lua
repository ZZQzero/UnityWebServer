---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by mac.
--- DateTime: 2020/7/22 10:12 上午
---

eventTable={}

function eventTable:Onclick(de)
    de.Onclick=function()
        print("C#委托")
    end
end
function eventTable:func1()
    print("1111111111")
end

return eventTable