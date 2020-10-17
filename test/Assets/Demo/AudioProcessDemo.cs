using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.immusician.Audio;
using AOT;
using System;

public class AudioProcessDemo : MonoBehaviour
{

    private static float _curFre;
    private static float _curVolume;
    private static string chordTuneResult;
    private static string audioEventResult;
    // Use this for initialization
    private int beatIndexDemo = 1;

    void Start()
    {

        AudioProcess.resetAudioSession(); //iOS重设AudioSession
        //创建音频模块，采样率默认32000，
        AudioProcess.create(32000, 1 + 2 + 16 + 64, 0);
        AudioProcess.audioInit(); //初始化音频

        AudioProcess.start();

        AudioProcess.debugFree(1);
    }

    private void OnDestroy()
    {
        AudioProcess.enableCal(0);
        AudioProcess.pause();
        AudioProcess.free();
    }

    private void OnGUI()
    {
        //居中显示文字
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUI.skin.label.fontSize = 20;

        GUI.skin.button.fontSize = 20;

        //背景音乐
        GUILayout.Label("Audio Track播放背景音乐相关 ");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("addAudioTrackFiles", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.clearAllTracks();
            AudioProcess.addAudioTrackFileName(Application.streamingAssetsPath + "/mp3/0.mp3", 30);
            AudioProcess.addAudioTrackFileName(Application.streamingAssetsPath + "/mp3/1.mp3", 30);
        }
        if (GUILayout.Button("playAllTracks()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //设置轨道开始时间
            AudioProcess.setTrackStartTime(0, 0);
            AudioProcess.setTrackStartTime(1, 0);
            //播放所有轨道
            AudioProcess.playAllTracks();
        }
        if (GUILayout.Button("silentTrack(0)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //静音0轨道
            AudioProcess.silentTrack(0);
            //恢复1轨道
            AudioProcess.resumeSilentTrack(1);
        }
        if (GUILayout.Button("silentTrack(1)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //静音1轨道
            AudioProcess.silentTrack(1);
            //恢复0轨道
            AudioProcess.resumeSilentTrack(0);
        }
        if (GUILayout.Button("setTrackSpeed(0.5f)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //设置播放速度
            AudioProcess.setTrackSpeed(0.5f);
        }
        if (GUILayout.Button("stopAllTracks()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //停止轨道播放
            AudioProcess.stopAllTracks();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(50);

        //录音音量
        GUILayout.Label("Audio RecordVolume录音音量相关");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("startRecordVolume()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.setRecordVolumeCallback(recordVolumeCallback);
            AudioProcess.startRecordVolume();
            AudioProcess.enableRecordVolumeCal(1);
            AudioProcess.enableCal(1);
        }
        GUILayout.Label("_curVolume=" + _curVolume);
        if (GUILayout.Button("stopRecordVolume()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.enableRecordVolumeCal(0);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(50);


        //Tune
        GUILayout.Label("Audio Tune调音相关");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("startTune()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.audioCallBackWith<TuneCallBackDelegate>(tuneCallBack, 2);
            AudioProcess.startTune();
            AudioProcess.enableTuneCal(1);
            AudioProcess.enableCal(1);
        }
        GUILayout.Label("_curFre=" + _curFre);
        if (GUILayout.Button("stopTune()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.enableTuneCal(0);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(50);

        //ChordTune
        GUILayout.Label("Audio ChordTune调音相关");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("startChordTune()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.audioCallBackWith<ChordToneCallBackDelegate>(chordTuneCallback, 16);
            AudioProcess.startChordTune();
            AudioProcess.enableChordCal(0);
            AudioProcess.enableCal(0);

            //标准调音设置 根据实际情况设置
            int[] turnArr = new int[] { 69, 64, 60, 67 };
            int capo = 0;
            int key = 0;
            AudioProcess.setChordInfor(turnArr, capo, key);
        }

        if (GUILayout.Button("setChordFreArr()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            //设置和弦数据 示例 尤克里里C和弦 
            int[] fretArr = new int[] {3,0,0,0 };
            //转成dataArr格式（弦品弦品...）
            int[] dataArr = AudioProcess.fretArrToDataArr(fretArr,0);

            //传入
            AudioProcess.setChordFreArr(dataArr,true);
            AudioProcess.enableChordCal(1);
            AudioProcess.enableCal(1);
        }
        GUILayout.Label("识别结果"+ chordTuneResult);
        if (GUILayout.Button("stopChordTune()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.enableChordCal(0);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(50);


        //AudioEvent
        GUILayout.Label("Audio AudioEvnet识别相关");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("startAuioTune()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioProcess.audioCallBackWith<CallBackDelegate>(audioEventCallBack, 1);
            AudioProcess.setPlayType(1);
            AudioProcess.enableES(1); //开启消音
            AudioProcess.setRecognizeType(0);
            AudioProcess.startTune();
            AudioProcess.enableRecognize(1);
            AudioProcess.enableCal(1);
        }
        if (GUILayout.Button("dispatchAudioEvent()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            AudioEvent audioEvent = new AudioEvent();
            audioEvent.timeStamp = AudioProcess.getTimeStamp();
            audioEvent.soundArr = new int[] { 1, 3, 2, 0, 3, 0, 4, 0 }; //示例 C和弦，实际可从谱子里得到beatRender.soundArr;
            audioEvent.beatIndex = beatIndexDemo; // beatRender.beatRenderIndex;
            audioEvent.duration = 1f; //beatRender.beatTime;
            audioEvent._tuneArr = new byte[] { 69, 64, 60, 67 };  //标准调音 musicScoreRender.trackRenderArr[curTrackIndex].tuneArr;
            audioEvent._adKey = 0; //变调 musicScoreRender.trackRenderArr[curTrackIndex]._adKey;

            audioEvent.sfftFlag = 1; //是否启用端点检测新算法
            if(audioEvent.sfftFlag == 1)
            {
                int curStringNum = 4;
                int capo = 0;
                audioEvent.sfft_isFirst = beatIndexDemo==1?1:0; //是否是第一个音
                audioEvent.sfft_referFreArr = AudioProcess.convertSoundArrToFreArr(audioEvent._tuneArr, audioEvent.soundArr, capo); //包含哪些音？
                audioEvent.sfft_isBeforeSame = 0;//是否和上个音相同？
                audioEvent.sfft_enableDetectEndPoint = 0; //检测端点？
                audioEvent.sfft_tieFreArr = AudioProcess.convertSoundArrToFreArr(audioEvent._tuneArr, null, capo); //上个延音？
               
                //验证。。
                audioEvent.sfft_validFreArr = AudioProcess.validFreArr(audioEvent._tuneArr, audioEvent.soundArr, curStringNum, capo, audioEvent.sfft_referFreArr, audioEvent.sfft_tieFreArr);

            }

            AudioProcess.audioDispatch(audioEvent);

            beatIndexDemo++;

        }
        GUILayout.Label("识别结果" + audioEventResult);
        GUILayout.EndHorizontal();
        GUILayout.Space(50);
    }

    //录音音量回调
    [MonoPInvokeCallback(typeof(RecordVolumeDelegate))]
    static void recordVolumeCallback(float data)
    {
        Debug.Log("recordVolumeCallback=" + data);
        _curVolume = data;
    }

    //调音回调
    [MonoPInvokeCallback(typeof(TuneCallBackDelegate))]
    static void tuneCallBack(float fre, float amp)
    { 
        Debug.Log("tuneCallBack=" + fre);
        _curFre = fre;
    }

    //和弦识别回调
    [MonoPInvokeCallback(typeof(ChordToneCallBackDelegate))]
    static void chordTuneCallback(IntPtr toneDataArrPtr, int length)
    {
        ToneData[] toneDataArr = AudioProcess.convertToneDataArr(toneDataArrPtr, length);

        ToneData toneData = toneDataArr[0];


        if (toneData.chord_status == 1 && toneData.chord_validStatus == 0)
        {
            Debug.Log("chordTuneCallback 成功 status=1  freArr=" + arrToString(toneData.chord_freArr));
        }
        else
        {
            Debug.Log("chordTuneCallback 失败 status=0  freArr=" + arrToString(toneData.chord_freArr));
        }

        chordTuneResult = string.Format("status={0} valid={1} freArr={2}", toneData.chord_status, toneData.chord_validStatus, arrToString(toneData.chord_freArr));
    }

    //识别回调
    [MonoPInvokeCallback(typeof(CallBackDelegate))]
    static void audioEventCallBack(int status, int reserve, int isExistEndPoint, int offsetTime)
    {
        audioEventResult = string.Format("status={0} index={1} isExistEndPoint={2} offsetTime={3}", status, reserve, isExistEndPoint, offsetTime);

        Debug.Log("audioCallBack " + audioEventResult);
    }

        public static string arrToString<T>(T[] arr)
    {
        string str = "[";
        foreach (T i in arr)
        {
            str += i.ToString() + ",";
        }

        str += "]";
        return str;
    }

}
