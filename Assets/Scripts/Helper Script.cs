
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    public void DoFlipObject(bool flip)
    {
        // get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (flip == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public bool IsFlipped()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        //Helper script code to return if the player is flipped or not
        if (sr.flipX == true)
        {
            return true;
        }
        return false;
    }
}
