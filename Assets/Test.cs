using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    AudioClip[] clips;

    const int MIC_FREQ=48000;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Application.targetFrameRate = 60; 

        Debug.Log("Start");

        // マイクを選ぶ部分はアプリケーション側で実装する。MMVV側でデフォルトマイクを検索する機能があってもよいかも
        // TODO:　マイクが複数ある場合、選択できるようにする
        // TODO: androidの場合 camcoderを選択する。
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

        if (Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Debug.Log("Microphone authorized");
        }
        else
        {
            Debug.Log("Microphone not authorized");
        }

        var devs=Microphone.devices;
        Debug.Log("mic device num:"+devs.Length);
        clips = new AudioClip[devs.Length];
        for(int i=0;i<devs.Length;i++) {
            Debug.Log("Mic device "+i+" : "+devs[i]);
        }
        clips[0] = Microphone.Start(devs[0], true, 1, MIC_FREQ);
        while(Microphone.GetPosition(Microphone.devices[0])==0) {

        }
        clips[1] = Microphone.Start(devs[1], true, 1, MIC_FREQ);
        
    }

    public int devnum;
    // Update is called once per frame
    void Update()
    {
        devnum=Microphone.devices.Length;
        for(int i=0;i<devnum;i++) {
            int pos=Microphone.GetPosition(Microphone.devices[i]);
            Debug.Log("dev "+i+" " + Microphone.devices[i]+" pos:"+pos);
        }
        /*
        int diff=pos-lastMicPos;
        float[] data = new float[clip.samples * clip.channels];
        clip.GetData(data,0);
         //  Debug.Log("diff:"+diff+" last:"+lastMicPos+" pos:"+pos + " dat0:"+data[0]);
        if(diff<0) { 
            // getpositionの返り値は2chなら1ふえたらFloatが2個進む
            int tail=MIC_FREQ-lastMicPos;
            if(debugSineWave) for(int i=0;i<tail;i++) data[lastMicPos+i]+=GenerateNextSine(debugSineVolume);
            micbuf.PushSamples(data,lastMicPos,tail);
            if(debugSineWave) for(int i=0;i<pos;i++) data[i]+=GenerateNextSine(debugSineVolume);
            micbuf.PushSamples(data,0,pos);
        } else if(diff>0){ 
            if(debugSineWave) for(int i=0;i<diff;i++) data[lastMicPos+i]+=GenerateNextSine(debugSineVolume);
            micbuf.PushSamples(data,lastMicPos,diff);
        } else if(diff==0){
            // skip
        }
        */

    }
}
