using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Sound
{
    public string name;
    public AudioSource audio_Source;
}


public class SoundMgr : MonoBehaviour
{

    static public SoundMgr inst;

    [SerializeField]      //총8개 태양 구름 비 눈 가뭄 폭우 폭설 번개 
    Sound[] weather_Sound;

    [SerializeField]     //총2개 - 도둑 화재 
    Sound[] event_Sound;

    [SerializeField]     // ui사운드 총 10개-버튼 토글, 버튼 클릭, 캐스팅, 던지기, 건물 발전,맵-상태창 생성사운드,순간이동,!,X, 타이머카운트   
    Sound[] ui_Sound;

    [SerializeField]     // 아이템 사운드 총4개 -태양, 구름, 번개, 물  
    Sound[] item_Sound;

    [SerializeField]     //총 3개 -로비배경, 엔딩배경 환호와 항의
    Sound[] bgm_Sound;


    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            inst = this;
        }
    }
    private void start(){

    }
    public void Play(string name, Vector3 pos, float _time=15.0f)
    {
        GameObject obj = new GameObject("AudioSource");
        var _audio = obj.AddComponent<AudioSource>();
        var _destroy = obj.AddComponent<DestroyWaitTime>();

        _destroy.SetTime(_time);
        for (int i = 0; i < weather_Sound.Length; i++)
        {
            if (weather_Sound[i].name.Equals(name))
            {
                weather_Sound[i].audio_Source.transform.position = pos;

                _audio.clip =  weather_Sound[i].audio_Source.clip;
                _audio.PlayOneShot(_audio.clip);
//                _audio.Play();

                return;
            }
        }
        for (int i = 0; i < event_Sound.Length; i++)
        {
            if (event_Sound[i].name.Equals(name))
            {
                event_Sound[i].audio_Source.transform.position = pos;
   
                _audio.clip = event_Sound[i].audio_Source.clip;
                _audio.PlayOneShot(_audio.clip);
  //              _audio.Play();
                return;
            }
        }
        for (int i = 0; i < ui_Sound.Length; i++)
        {
            if (ui_Sound[i].name.Equals(name))
            {
                ui_Sound[i].audio_Source.transform.position = pos;

                _audio.clip = ui_Sound[i].audio_Source.clip;
                _audio.PlayOneShot(_audio.clip);
//                _audio.Play();
                return;
            }
        }
        for (int i = 0; i < item_Sound.Length; i++)
        {
            if (item_Sound[i].name.Equals(name))
            {
                item_Sound[i].audio_Source.transform.position = pos;

                _audio.clip = item_Sound[i].audio_Source.clip;
                _audio.PlayOneShot(_audio.clip);
//                _audio.Play();
                return;
            }
        }
        for (int i = 0; i < bgm_Sound.Length; i++)
        {
            if (bgm_Sound[i].name.Equals(name))
            {
                bgm_Sound[i].audio_Source.transform.position = pos;

                _audio.clip = bgm_Sound[i].audio_Source.clip;
                _audio.PlayOneShot(_audio.clip);
//                _audio.Play();
                return;
            }
        }
    }
    public void Stop(string name)
    {
        for (int i = 0; i < weather_Sound.Length; i++)
        {
            if (weather_Sound[i].name.Equals(name))
            {
                weather_Sound[i].audio_Source.Stop();
                return;
            }
        }
        for (int i = 0; i < event_Sound.Length; i++)
        {
            if (event_Sound[i].name.Equals(name))
            {
                event_Sound[i].audio_Source.Stop();
                return;
            }
        }
        for (int i = 0; i < ui_Sound.Length; i++)
        {
            if (ui_Sound[i].name.Equals(name))
            {
                ui_Sound[i].audio_Source.Stop();
                return;
            }
        }
        for (int i = 0; i < item_Sound.Length; i++)
        {
            if (item_Sound[i].name.Equals(name))
            {
                item_Sound[i].audio_Source.Stop();
                return;
            }
        }
        for (int i = 0; i < bgm_Sound.Length; i++)
        {
            if (bgm_Sound[i].name.Equals(name))
            {
                bgm_Sound[i].audio_Source.Stop();
                return;
            }
        }
    }
}
/*
    public void Play3D(string name,Vector3 pos)
    {
        for (int i = 0; i < weather_Sound.Length; i++)
        {
            if (weather_Sound[i].name.Equals(name))
            {
                weather_Sound[i].audio_Source.transform.position = pos;
                weather_Sound[i].audio_Source.Play();
                return;
            }
        }
    }
    */