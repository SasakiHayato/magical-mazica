using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ResultController : MonoBehaviour,IGameLateSetupable
{
    [SerializeField]
    PlayableDirector _director;
    public int Priority => 1;
    private void Awake()
    {
        GameController.Instance.AddGameLateSetupble(this);
    }

    public void GameLateSetup()
    {
        StartResultTimeLine();
    }

    void StartResultTimeLine()
    {
        _director.Play();
    }
}
