using System.Collections;
using System.Linq;
using UnityEngine;

public class BookcaseController : MonoBehaviour
{

    #region Public Variables
    public PaperPileController[] paperPilesToComplete;
    public float moveVerticalTo;
    #endregion Public Variables

    #region Private variables
    private float _originalY;
    private bool _moved;
    #endregion Private variables

    void Start()
    {
        this._originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (paperPilesToComplete.Length > 0)
        {
            float moveYto = this.transform.position.y;

            var countDone = paperPilesToComplete.Count(p => p.IsPapersDone);
            var allDone = (countDone == paperPilesToComplete.Length);

            //Move
            if (allDone)
            {
                _moved = true;
                moveYto = this._originalY + moveVerticalTo;
            }
            //Get back to the original position
            else if(_moved)
            {
                _moved = false;
                moveYto = this._originalY;
            }
            //print(@"transform_Y: " + this.transform.position.y +
            //    "_movetoY: "+ moveYto +
            //    "_allDone: "+ allDone);
            if (this.transform.position.y != moveYto)
            {
                Vector3 newPosition = new Vector3(
                this.transform.position.x,
                moveYto,
                this.transform.position.z);

                this.transform.position = newPosition;
            }
        }
    }
}
