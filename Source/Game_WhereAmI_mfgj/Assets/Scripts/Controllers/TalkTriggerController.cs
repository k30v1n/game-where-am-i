using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTriggerController : MonoBehaviour
{

    #region Public
    public PlayerTalkType playerTalkType;
    #endregion Public

    #region Privates
    private TalkingController talkingController;
    private bool alreadyUsed;
    #endregion Privates

    void Start()
    {
        this.talkingController = FindObjectOfType<TalkingController>();
        this.alreadyUsed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !alreadyUsed)
        {
            switch (playerTalkType)
            {
                case PlayerTalkType.Talk_01:
                    this.talkingController.PlayerTalk_01_WhereAmI();
                    break;
                case PlayerTalkType.Talk_02:
                    this.talkingController.PlayerTalk_02_WorkDone();
                    break;
                case PlayerTalkType.Talk_03:
                    this.talkingController.PlayerTalk_03_WorkUndo();
                    break;
                case PlayerTalkType.Talk_04:
                    this.talkingController.PlayerTalk_04_WorkDoneDiscoverNewPlaces();
                    break;
                case PlayerTalkType.Talk_InDevelopment:
                    this.talkingController.Talk_InDevelopment();
                    break;
                default:
                    break;
            }
            alreadyUsed = true;
        }
    }
}
