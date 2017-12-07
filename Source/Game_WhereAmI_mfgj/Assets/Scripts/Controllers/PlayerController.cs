using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    public float JumpForce;
    public float Speed;
    #endregion Public Variables

    #region Private Variables
    private Camera _mainCamera;
    private bool _isJumping = false;
    private TalkingController _talkingController;
    private bool _canMove;
    private float _timer = 0.0f;
    private bool did_WorkDone;
    private bool did_WorkUndo;
    #endregion Private Variables

    #region Unity Methods
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        this._mainCamera = FindObjectOfType<Camera>();
        this._talkingController = FindObjectOfType<TalkingController>();
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        //Check time to be stoped
        CheckTimeToBeStopped();

        //Prevent player to rotate because of Physics
        transform.rotation = new Quaternion(0, 0, 0, 0);

        float directionXInput = Input.GetAxisRaw("Horizontal");
        bool jump = Input.GetAxisRaw("Vertical") > 0;

        //Player movement
        PlayerMovement(directionXInput, jump);

        //Camera
        MoveCamera();
    }

    /// <summary>
    /// Detects 2D collision
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        JumpLanding(coll);
        CheckFirstTimeEvents(coll);
    }
    #endregion Unity Methods

    #region Private Methods
    /// <summary>
    /// Deal with player's movement
    /// </summary>
    /// <param name="directionXInput"></param>
    /// <param name="jump"></param>
    private void PlayerMovement(float directionXInput, bool jump)
    {
        if (_canMove && this._talkingController.DialogSystem.IsNotShowingMessage())
        {
            Movement(directionXInput);
            Jump(jump);
        }
        SpriteDirection(directionXInput);
        Animate(directionXInput);
    }

    /// <summary>
    /// Invert the sprite according to the player direction
    /// </summary>
    private void SpriteDirection(float directionInput)
    {
        //The default direction is right
        bool flipX = this.GetComponent<SpriteRenderer>().flipX;

        if (directionInput != 0)
        {
            if (directionInput < 0)
                //Left
                flipX = true;
            else
                //Right
                flipX = false;
        }

        this.GetComponent<SpriteRenderer>().flipX = flipX;
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    /// <param name="directionInput"></param>
    private void Movement(float directionInput)
    {
        if (directionInput != 0)
        {
            float x = (directionInput * Speed) + this.transform.position.x;
            Vector3 playerPos = new Vector3(x, this.transform.position.y, this.transform.position.z);

            transform.position = playerPos;
        }
    }

    /// <summary>
    /// Makes a jump
    /// </summary>
    /// <param name="jump"></param>
    private void Jump(bool jump)
    {
        if (jump && !_isJumping)
        {
            Vector2 impulse = new Vector2(0, JumpForce);
            this.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
            _isJumping = true;
        }
    }
    private void JumpLanding(Collision2D coll)
    {
        List<string> jumpingPlatforms = new List<string>() { "Floor" , "Bookcase", "PaperPile"};
        //Check if the collision was in the floor
        if (jumpingPlatforms.Contains(coll.gameObject.tag))
        {
            _isJumping = false;
        }
    }

    /// <summary>
    /// Animate according the player's direction
    /// </summary>
    /// <param name="directionXInput"></param>
    private void Animate(float directionXInput)
    {
        var animator = this.GetComponent<Animator>();
        if (directionXInput == 0)
        {
            animator.SetBool("StartRunning", false);
        }
        else
        {
            animator.SetBool("StartRunning", true);
        }
    }

    /// <summary>
    /// Moves the camera with the player
    /// </summary>
    private void MoveCamera()
    {
        Vector3 cameraV3 = new Vector3(
            this.transform.position.x,
            this.transform.position.y + 2,
            this._mainCamera.transform.position.z);

        this._mainCamera.transform.position = cameraV3;
    }

    private void CheckTimeToBeStopped()
    {
        this._timer += Time.deltaTime;
        int seconds = (int) this._timer % 60;

        if (seconds >= 1 && !this._canMove)
        {
            this._canMove = true;
            this._talkingController.PlayerTalk_01_WhereAmI();
        }
    }
    
    private void CheckFirstTimeEvents(Collision2D coll)
    {
        if (coll.gameObject.tag == "PaperPile")
        {
            if(!did_WorkDone || !did_WorkUndo)
            {
                var paperPileCont = coll.gameObject.GetComponent<PaperPileController>();

                //First time work done
                if (!did_WorkDone && paperPileCont.IsPapersDone)
                {
                    this._talkingController.PlayerTalk_02_WorkDone();
                    did_WorkDone = true;
                }
                //First time work undone
                else if (!did_WorkUndo && !paperPileCont.IsPapersDone)
                {
                    this._talkingController.PlayerTalk_03_WorkUndo();
                    did_WorkUndo = true;
                }

            }
        }
    }
    #endregion Private Methods
}