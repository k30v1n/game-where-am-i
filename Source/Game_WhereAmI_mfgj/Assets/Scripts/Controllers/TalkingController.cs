
using System;
using UnityEngine;


/// <summary>
/// Enum to define the talk type
/// </summary>
public enum PlayerTalkType
{
    Talk_01, Talk_02, Talk_03, Talk_04, Talk_InDevelopment
}

/// <summary>
/// Class responsible by manage the player talking
/// </summary>
public class TalkingController : MonoBehaviour
{
    public DialogSystemController DialogSystem { get; private set; }

    void Start()
    {
        this.DialogSystem = FindObjectOfType<DialogSystemController>();
    }

    public void Talk_InDevelopment()
    {
        var msgs =
            new string[] {
                "DEV NOTE: Sorry, this part is on development"
            };

        this.DialogSystem.QueueMsgs(msgs);
    }
    
    public void PlayerTalk_01_WhereAmI()
    {
        var msgs =
            new string[] {
                "Where Am I ...",
                "I think I know this place. This looks like my job office.",
                "And looks like I left this work to be done."
            };

        this.DialogSystem.QueueMsgs(msgs);
    }

    public void PlayerTalk_02_WorkDone()
    {
        var msgs =
            new string[] {
                "YES! Work done!",
                "Now I have to be careful to not undo my work."
            };

        this.DialogSystem.QueueMsgs(msgs);
    }

    public void PlayerTalk_03_WorkUndo()
    {
        var msgs =
            new string[] {
                "OH NO! I lost my work. I am a dumb :(",
                "Now I have to do all this work again! And be careful to not undo it..."
            };

        this.DialogSystem.QueueMsgs(msgs);
    }

    public void PlayerTalk_04_WorkDoneDiscoverNewPlaces()
    {
        var msgs =
            new string[] {
                "OH! That was cool.",
                "I think that work DONE can make me discover new places..."
            };

        this.DialogSystem.QueueMsgs(msgs);
    }
}