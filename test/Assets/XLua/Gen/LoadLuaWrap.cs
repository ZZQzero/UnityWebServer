#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class LoadLuaWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LoadLua);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dostring", _m_Dostring);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaEnv", _g_get_luaEnv);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaStart", _g_get_LuaStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaUpdate", _g_get_LuaUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaEnable", _g_get_LuaEnable);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "luaEnv", _s_set_luaEnv);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaStart", _s_set_LuaStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaUpdate", _s_set_LuaUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaEnable", _s_set_LuaEnable);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					LoadLua gen_ret = new LoadLua();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LoadLua constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dostring(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _filename = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Dostring( _filename );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaEnv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LuaStart);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LuaUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaEnable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LuaEnable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.luaEnv = (XLua.LuaEnv)translator.GetObject(L, 2, typeof(XLua.LuaEnv));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LuaStart = translator.GetDelegate<LoadLua.function>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LuaUpdate = translator.GetDelegate<LoadLua.function>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaEnable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoadLua gen_to_be_invoked = (LoadLua)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LuaEnable = translator.GetDelegate<LoadLua.function>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
