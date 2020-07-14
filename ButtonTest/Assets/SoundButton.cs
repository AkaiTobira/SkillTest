using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
//1 36 09
    [SerializeField] AudioClip[] m_audioClips;

    private Dictionary<int, bool> m_enabledClips = new Dictionary<int, bool>();

    private AudioSource m_audioSource;

    void Start() {
        for( int audioClipIndex = 0; audioClipIndex < m_audioClips.Length; audioClipIndex++){
            m_enabledClips[audioClipIndex] = true;
        }
        m_audioSource = GetComponent<AudioSource>();
    }

    private List<int> GetIndexesOfActiveSoundClips(){
        List<int> activeSoundClipsIndexes = new List<int>();
        foreach( KeyValuePair<int, bool> entry in m_enabledClips){
            if( entry.Value ) activeSoundClipsIndexes.Add( entry.Key);
        }
        return activeSoundClipsIndexes;
    }

    public void ToogleAudioClip( int clipIndex){
        m_enabledClips[clipIndex] = !m_enabledClips[clipIndex];
    }

    public void PlayRandomSound(){
        List<int> activeIndexes = GetIndexesOfActiveSoundClips();
        if( activeIndexes.Count == 0 ) return;
        int soundToPlayIndex = Random.Range( 0, activeIndexes.Count );
        m_audioSource.clip   = m_audioClips[activeIndexes[soundToPlayIndex]];
        m_audioSource.Play();
    }

}
