using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PaperPileController : MonoBehaviour
{
    #region Public variables
    public bool IsPapersDone;
    #endregion Public variables

    #region Private Variables
    private SpriteRenderer _spriteRend;
    #endregion Private Variables

    void Awake()
    {
        this._spriteRend = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);

        VerifyPapers();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            IsPapersDone = !IsPapersDone;
            VerifyPapers();
        }
    }

    private void VerifyPapers()
    {
        string papersDoneHex = "#52F861FF";
        string papersNotDoneHex = "#FFF";
        Color color = new Color();

        if (IsPapersDone)
        {
            ColorUtility.TryParseHtmlString(papersDoneHex, out color);
        }
        else
            ColorUtility.TryParseHtmlString(papersNotDoneHex, out color);
        
        this._spriteRend.color = color;
    }
}

