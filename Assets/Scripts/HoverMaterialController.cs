using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMaterialController : MonoBehaviour
{
    //material
    Material hoverMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //get the mesh renderer material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        hoverMaterial = meshRenderer.material;
        //ensure glow is off
        GlowOff();
    }

    //turn glow on
    public void GlowOn()
    {
        hoverMaterial.SetInt("_Glow", 1);
    }

    //turn glow off
    public void GlowOff()
    {
        hoverMaterial.SetInt("_Glow", 0);
    }
}
