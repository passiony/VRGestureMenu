using System.Collections;
using UnityEngine;

public class StartFade : MonoBehaviour
{
    private CanvasGroup group;
    
    IEnumerator Start()
    {
        group = gameObject.GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            group.alpha-= 0.1f;
        }
        yield return new WaitForSeconds(0.1f);
        group.alpha = 0f;
    }

}
