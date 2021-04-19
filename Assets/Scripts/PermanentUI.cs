using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PermanentUI : MonoBehaviour
{
    public int cherries = 0;
    public TextMeshProUGUI cherryText;


    public static PermanentUI perm;

    private void Start()
    {

 
        if (!perm)
        {
            perm = this;
        }
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        cherries = 0;
        cherryText.text = cherries.ToString();   
    }

    public override bool Equals(object obj)
    {
        return obj is PermanentUI uI &&
               base.Equals(obj) &&
               EqualityComparer<TextMeshProUGUI>.Default.Equals(cherryText, uI.cherryText);
    }
}
