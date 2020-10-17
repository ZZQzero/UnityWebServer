using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using com.immusician.BGT;
using com.immusician.BGT.structs;
using System.IO;

public class BGTDemo : MonoBehaviour
{

	private MusicScoreRender musicScoreRender;
	private RenderInfor mnRenderInfor; 
	void Start ()
	{

		string path = Application.streamingAssetsPath + "/bgt/demo.bgt";
		parseBGTFile(path);
		
		//readMusicScoreRenderData();
	}

	private void OnDestroy()
	{
		freeBGT();
	}


	/// <summary>
	/// 解析bgt文件
	/// </summary>
	/// <param name="bgtPath">bgt文件路径</param>
	private void parseBGTFile(string bgtPath)
	{
		/*int headNum = 1;

		
		//渲染参数
		RenderInfor _renderInfor = new RenderInfor ();
		_renderInfor.baseNoteWidth = 1334 / 22.2f ; //2.0f * 30 ;
		_renderInfor.spaceHeight = 30;
		_renderInfor.viewWidth = 1334;
		_renderInfor.viewHeight = 750;
		_renderInfor.type = 1; //除五线谱需要变速的用1 其他都用0
		msRender = BGTParse.parse(bgtPath, headNum);


		//可选参数 要渲染的轨道
		int[] indexArr = {0};

		//渲染谱子数据
		msRender = BGTParse.render(_renderInfor, indexArr);
		*/
		if (!File.Exists(bgtPath))
		{
			bgtPath = Application.streamingAssetsPath + "/bgt/demo.bgt";
		}

		//		bgtPath = Application.streamingAssetsPath + "/bgt/50.bgt";

		mnRenderInfor = new RenderInfor();
		mnRenderInfor.baseNoteWidth = 1334 / 22.2f;//MusicNotationConfig.baseNoteWidth; //contentSize.x / 16f; //2.0f * 30 ;
		mnRenderInfor.spaceHeight = 30;
		mnRenderInfor.viewWidth = 1334;
		mnRenderInfor.viewHeight = 750;
		mnRenderInfor.type = 1;

//        BGTParse.newBgtRender();

//        if (isNolinear == 1)
//        {
		int headNum = 1;
//        }

		//		bgtPath = Application.streamingAssetsPath + "/bgt/finger.bgt"; //测试谱子
        
		musicScoreRender = BGTParse.parse(bgtPath, headNum);
		musicScoreRender = BGTParse.render(mnRenderInfor);
	}

	/// <summary>
	/// 读取msRender里一些数据
	/// </summary>
	/*private void readMusicScoreRenderData()
	{
		Debug.Log("标题：" + msRender.title);
		Debug.Log("作者：" + msRender.author);
		
		Debug.Log("乐器：" + msRender.trackRenderArr[0].instrument);

		Debug.Log("轨道数：" + msRender.trackNum);
		Debug.Log("速度：" + msRender.tempo);
		Debug.Log("拍号：" + msRender.trackRenderArr[0].measureRenderArr[0].numerator + "/" +  msRender.trackRenderArr[0].measureRenderArr[0].denominator);
		
		Debug.Log("乐器：" + msRender.trackRenderArr[0].instrument);
		Debug.Log("渲染宽度：" + msRender.trackRenderArr[0].totalWidth);
		Debug.Log("谱子时间：" + msRender.totalTime);
		
	}*/


	/// <summary>
	/// 释放BGTRender
	/// </summary>
	private void freeBGT()
	{
		BGTParse.bgt_free();
	}
	
	
}
