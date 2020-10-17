using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.immusician.BGT;
using com.immusician.BGT.structs;
using com.immusician.Midi;
using System;

public class MidiPlayerDemo : MonoBehaviour {

    private MusicScoreRender musicScoreRender;
    private IntPtr musicScoreRenderPtr;

    private string text = "1";
    private string text2 = "60";
    
    void Start () {

        parseBGTFile(Application.streamingAssetsPath + "/bgt/juhuatai.bgt");
        debugTrackInfo();

        initMIDIPlayer();
        _turnOnAllTrack();

        createMidiToolbox();
    }

    private void OnDestroy()
    {
        _stopMIDI();
        _freeMidiPlayer();

        freeBGT();
    }

    private void OnGUI()
    {

        //居中显示文字
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUI.skin.label.fontSize = 20;

        GUI.skin.button.fontSize = 20;
        GUI.skin.textField.fontSize = 20;
         

        GUILayout.Space(100);
        GUILayout.Label("MidiPlayer播放bgt曲谱 ");
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("_playMIDI()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            _playMIDI();
        }

        if (GUILayout.Button("_stopMIDI()", GUILayout.Width(200), GUILayout.Height(50)))
        {
            _stopMIDI();
        }

        if (GUILayout.Button("_setMidiSpeed(" + text + ")", GUILayout.Width(200), GUILayout.Height(50)))
        {
            _setMidiSpeed(float.Parse(text));
        }

        text = GUILayout.TextField(text, GUILayout.Width(50), GUILayout.Height(50));
        
        if (GUILayout.Button("打开轨道", GUILayout.Width(150), GUILayout.Height(50)))
        {
            _turnOnTrack(int.Parse(text));
        }
        if (GUILayout.Button("关闭轨道", GUILayout.Width(150), GUILayout.Height(50)))
        {
            _turnOffTrack(int.Parse(text));
        }
        if (GUILayout.Button("打开所有轨道", GUILayout.Width(150), GUILayout.Height(50)))
        {
            _turnOnAllTrack();
        }
        if (GUILayout.Button("关闭所有轨道", GUILayout.Width(150), GUILayout.Height(50)))
        {
            _turnOffAllTrack();
        }
        
        GUILayout.EndHorizontal();
        GUILayout.Space(50);
        
        
        GUILayout.Label("MidiToolbox 播放midi指令 ");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("playOneNode", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{60},new []{127},0,1);
        }
        
        if (GUILayout.Button("play3Node", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{60,62,64},new []{127,127,127},0,1);
        }
        
        if (GUILayout.Button("play3Arpeggio", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playArpeggio(new []{60,62,64},new []{127,127,127},0,0.3f,1.0f);
        }
        GUILayout.EndHorizontal();
        
        
        GUILayout.Label("MidiToolbox Standard Drum ");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("playNode(52)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{52},new []{127},1,0);
        }
        
        if (GUILayout.Button("playNode(53)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{53},new []{127},1,0);
        }
        
        if (GUILayout.Button("playNode(35)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{35},new []{127},1,0);
        }
        if (GUILayout.Button("playNode(38)", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{38},new []{127},1,0);
        }
        GUILayout.EndHorizontal();
        
        
        GUILayout.Label("MidiToolbox Violin");
        GUILayout.BeginHorizontal();
        text2 = GUILayout.TextField(text2, GUILayout.Width(50), GUILayout.Height(50));

        if (GUILayout.Button("playNode("+ text2 + ")", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_playNode(new []{int.Parse(text2)},new []{127},2,0);
        }
        
        if (GUILayout.Button("stopNode("+ text2 + ")", GUILayout.Width(200), GUILayout.Height(50)))
        {
            MidiToolbox.midiToolbox_stopNode(new []{int.Parse(text2)},new []{127},2);
        }
        
        GUILayout.EndHorizontal();
    } 

    #region BGT

    private MusicScoreRender parseBGTFile(string bgtPath)
    {
        int headNum = 1;

        musicScoreRender = BGTParse.parse(bgtPath, headNum);
        musicScoreRenderPtr = BGTParse.currentMSRenderPtr();

        //渲染参数
        RenderInfor _renderInfor = new RenderInfor();
        _renderInfor.baseNoteWidth = 1334 / 22.2f; //2.0f * 30 ;
        _renderInfor.spaceHeight = 30;
        _renderInfor.viewWidth = 1334;
        _renderInfor.viewHeight = 750;
        _renderInfor.type = 0; //除五线谱需要变速的用1 其他都用0

        //可选参数 要渲染的轨道
        int[] indexArr = { 0 };

        //渲染谱子数据
        musicScoreRender = BGTParse.render(_renderInfor, indexArr);

        return musicScoreRender;
    }

    private void debugTrackInfo()
    {
        int trackNum = musicScoreRender.trackNum;

        for (int i = 0; i < trackNum; i++)
        {
            Debug.LogFormat("trackIndex={0}, trackName={1}", i, musicScoreRender.trackRenderArr[i].name);
        }
    }

    private void freeBGT()
    {
        BGTParse.bgt_free();
    }

    #endregion

    #region MIDI Player

    void initMIDIPlayer()
    {
        string instrument = Application.streamingAssetsPath + "/MIDI/" + "default.sf2";

//        if (PlatformUtil.isAndroid())
//        {
//            instrument = Application.persistentDataPath + "/default.sf2";
//        }

        MidiPlayer.createWithMSRender(musicScoreRenderPtr, instrument);
    }

    void _freeMidiPlayer()
    {
        MidiPlayer.free();
    }

    void _playMIDI()
    {
        MidiPlayer.start();
        //MidiPlayer.startAtTime(0);
    }

    void _stopMIDI()
    {
        MidiPlayer.stop();
    }

    void _setMidiSpeed(float speed)
    {
        MidiPlayer.setSpeed(speed);
    }

    void _turnOnTrack(int trackIndex)
    {
        MidiPlayer.turnOnTrack(trackIndex);
    }

    void _turnOffTrack(int trackIndex)
    {
        MidiPlayer.turnOffTrack(trackIndex);
    }

    void _turnOnAllTrack()
    {
        int trackNum = musicScoreRender.trackNum;
        for (int i = 0; i < trackNum; i++)
        {
            _turnOnTrack(i);
        }
    }

    void _turnOffAllTrack()
    {
        int trackNum = musicScoreRender.trackNum;
        for (int i = 0; i < trackNum; i++)
        {
            _turnOffTrack(i);
        }
    }

    void _turnMetronomeOn(bool flag)
    {
        if (flag)
        {
            turnOnMetronome();
        }
        else
        {
            turnOffMetronome();
        }
    }

    public void turnOnMetronome()
    {
        int trackNum = musicScoreRender.trackNum;
        _turnOnTrack(trackNum);
    }

    public void turnOffMetronome()
    {
        int trackNum = musicScoreRender.trackNum;
        _turnOffTrack(trackNum);
    }

    #endregion


    #region MidiToolbox

    private void createMidiToolbox()
    {
        string soundPath = Application.streamingAssetsPath + "/MIDI/" + "default.sf2";
        
        //创建MidiToolbox， soundPath,bankId=0,presetId=0
        MidiToolbox.create(soundPath,0,0);
        
        //Standard Drum    bank=126,presetId=1
        MidiToolbox.addTrack(1,126);
        
        //Violin           bank=0,presetId=40
        MidiToolbox.addTrack(40,0);
    }

     

    #endregion
}
