using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class MagmaController : MonoBehaviour
{
    Material magmaMat;
    public Camera cam;
    Coroutine MagmaShowCoroutine;

    void Start()
    {
        magmaMat = GetComponent<DecalProjector>().material;    
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    if (MagmaShowCoroutine != null)
                        StopCoroutine(MagmaShowCoroutine);
                    transform.position = hit.point + hit.normal * .1f;
                   MagmaShowCoroutine = StartCoroutine(MagmaShow());
                }
            }
        }   
    }

    IEnumerator MagmaShow()
    {
        float opacity = 0;
        while (opacity<30)
        {
            opacity += Time.deltaTime * 10;
            magmaMat.SetFloat("_Opacity", opacity);
            yield return null;
        }
        yield return new WaitForSeconds(.3f);
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * 10;
            magmaMat.SetFloat("_Opacity", opacity);
            yield return null;
        }
    }

}
