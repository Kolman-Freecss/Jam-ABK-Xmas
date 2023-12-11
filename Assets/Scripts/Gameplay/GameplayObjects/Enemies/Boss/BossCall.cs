using System.Collections;
using System.Collections.Generic;
using Gameplay.Config;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BossCall : MonoBehaviour
{
    private RoundManager roundManager;
    [SerializeField] GameObject bossPref;
    PlayableDirector playableDirector;

    private float timelineDuration;

    private bool timerActivated;
    private float timelineTimer;

    private void Awake() 
    {
        roundManager = RoundManager.Instance;
    }
    private void OnEnable() 
    {
        roundManager.bossCall.AddListener(OnBossCalled);
        timelineDuration = (float)playableDirector.duration;
    }

    private void Update() 
    {
        if (timerActivated && timelineTimer < timelineDuration)
        {
            timelineTimer += Time.deltaTime;
        }
    }

    private void OnBossCalled()
    {
        Instantiate(bossPref, transform.position, Quaternion.identity);
        StartCoroutine(BossAppearance());
    }

    private IEnumerator BossAppearance()
    {
        playableDirector.Play();
        timerActivated = true;
        //freeze player

        //wait until the timer reaches whatever the timeline duration is
        yield return new WaitUntil(() => timelineTimer == timelineDuration);
        //as soon as the values are the same, the cinematic has ended
        
        ResetTimer();
        //unfreeze player
    }

    private void ResetTimer()
    {
        timerActivated = false;
        timelineTimer = 0f;
    }
}
