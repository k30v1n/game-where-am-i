using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemController : MonoBehaviour
{
    #region Private Variables
    private Canvas _canvas;
    private Text _text;
    private List<string> _dialogMessagesQueue;
    #endregion Private Variables

    #region Unity Methodss
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        this._dialogMessagesQueue = new List<string>();
        this._canvas = this.GetComponent<Canvas>();
        this._text = GameObject.FindGameObjectWithTag("DialogMessageText").GetComponent<Text>();
    }
    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        HandleMessage();
    }
    #endregion Unity Methods

    #region Private Methods
    private void HandleMessage()
    {
        //Verifies if have some message to be shown (first time)
        if (!IsNotShowingMessage() && !_canvas.enabled)
        {
            LoadNextMessage();
        }

        //The message is showing (other times)
        if (_canvas.enabled)
        {
            //If the user press RETURN, the system will show next message
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadNextMessage();
            }
        }
    }

    private void LoadNextMessage()
    {
        if (this._dialogMessagesQueue.Count == 0)
        {
            this._canvas.enabled = false;
            return;
        }

        var message = this._dialogMessagesQueue[0];
        this._text.text = message;
        this._canvas.enabled = true;
        this._dialogMessagesQueue.RemoveAt(0);
    }
    #endregion Private Methods

    #region Public Methods
    /// <summary>
    /// Verifies if the message queue is empty
    /// </summary>
    public bool IsNotShowingMessage()
    {
        return this._dialogMessagesQueue.Count == 0 && _canvas.enabled == false;
    }
    /// <summary>
    /// Queue a message on the msg stack
    /// </summary>
    /// <param name="msg"></param>
    public void QueueMsg(string msg)
    {
        this._dialogMessagesQueue.Add(msg);
    }
    /// <summary>
    /// Queue messages to be shown
    /// </summary>
    /// <param name="msgs"></param>
    public void QueueMsgs(string[] msgs)
    {
        foreach (var msg in msgs)
        {
            QueueMsg(msg);
        }
    }
    #endregion Public Methods

}
