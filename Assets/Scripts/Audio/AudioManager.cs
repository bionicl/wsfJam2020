using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds; 
        [SerializeField] AudioMixerGroup mixerGroup = default;
        
        public static AudioManager instance;
        
        void Awake() 
        { 
            if( instance == null ) 
            { 
                instance = this; 
            } 
            else 
            { 
                Destroy( gameObject ); 
                return; 
            } 
             
            DontDestroyOnLoad( gameObject ); 
             
            foreach( Sound s in sounds ) 
            { 
                s.source = gameObject.AddComponent<AudioSource>(); 
                s.source.clip = s.clip; 
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = mixerGroup;
            } 
        } 

        public void Play( string soundName ) 
        {
            Sound s = Array.Find( sounds, sound => sound.name == soundName ); 
 
            if( s == null ) 
            { 
                Debug.LogWarning( $"Sound '{soundName}' not found!" ); 
                return; 
            } 
             
            s.source.volume = s.volume; 
            s.source.pitch = s.pitch;

            s.source.Play(); 
        }
        
        public void Play( string soundName, float volumeModifier ) 
        {
            Sound s = Array.Find( sounds, sound => sound.name == soundName ); 
 
            if( s == null ) 
            { 
                Debug.LogWarning( $"Sound '{soundName}' not found!" ); 
                return; 
            } 
             
            s.source.volume = s.volume * volumeModifier; 
            s.source.pitch = s.pitch;

            s.source.Play();
        }
        
        public void Play( string soundName, float volumeModifier, float pitchModifier ) 
        {
            Sound s = Array.Find( sounds, sound => sound.name == soundName ); 
 
            if( s == null ) 
            { 
                Debug.LogWarning( $"Sound '{soundName}' not found!" ); 
                return; 
            } 
             
            s.source.volume = s.volume * volumeModifier; 
            s.source.pitch = s.pitch * pitchModifier; 
             
            s.source.Play();
        }
    }
}
