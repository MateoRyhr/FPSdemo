using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInstancier : MonoBehaviour
{
    [SerializeField] private GameObject _sound;

    public void PlaySound(){
        GameObject soundInstance = Instantiate(_sound,gameObject.transform);
        soundInstance.GetComponent<PlayAudioEvent>().PlayAudio();
        this.Invoke(() => Destroy(soundInstance),soundInstance.GetComponent<AudioSource>().clip.length);
    }
}
