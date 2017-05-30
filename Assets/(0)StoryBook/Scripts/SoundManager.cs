using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    private AudioSource audioSource;
    private MarkerStateManager markerManager;

    public AudioClip Page1_music;
    public AudioClip Page2_music;
    public AudioClip Page3_music;
    public AudioClip Page4_music;
    public AudioClip Page5_music;
    public AudioClip Page6_music;
    public AudioClip Page7_music;
    public AudioClip Page8_music;

    IEnumerator Sound;
    bool SoundSetup;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        markerManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        SoundSetup = false;
        audioSource.Stop();

        Sound = SoundCall();
        StartCoroutine(Sound);
    }

    IEnumerator SoundCall()
    {
        while (true)
        {
            //

            switch ((int)markerManager.getBookMarkerPageNumber())
            {
                case 1:
                    audioSource.clip = Page1_music;
                    break;
                case 2:
                    audioSource.clip = Page2_music;
                    break;
                case 3:
                    audioSource.clip = Page3_music;
                    break;
                case 4:
                    audioSource.clip = Page4_music;
                    break;
                case 5:
                    audioSource.clip = Page5_music;
                    break;
                case 6:
                    audioSource.clip = Page6_music;
                    break;
                case 7:
                    audioSource.clip = Page7_music;
                    break;
                case 8:
                    audioSource.clip = Page8_music;
                    break;

            }

            if (markerManager.getBookMarkerPageNumber() != MarkerStateManager.PageType.Nothing
                && audioSource.isPlaying == false)
            {
                yield return null;

                
                audioSource.Play();


            }
            else
            {
                yield return null;
            }

            if (GameManager.manager.getARTrigger(0) 
                || GameManager.manager.getARTrigger(1) 
                || GameManager.manager.getARTrigger(2))
            {
                yield return audioSource.mute = false;
            }
            else if (markerManager.getOnePageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getTwoPageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getThreePageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getFourPageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getFivePageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getSixPageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getSevenPageMarker() == MarkerStateManager.StateType.Off &&
                markerManager.getEightPageMarker() == MarkerStateManager.StateType.Off)
            {
                yield return audioSource.mute = true;
            }
            else  yield return audioSource.mute = false;
            {
              
            }


        }
    }

}

