using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMaterialController : MonoBehaviour
{
    //material
    Material hoverMaterial;
    float defaultAlpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //get the mesh renderer material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        hoverMaterial = meshRenderer.material;
        //ensure glow is off
        GlowOff();
        //save the default alpha
        defaultAlpha = hoverMaterial.GetColor("_Base_Color").a;
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

    public void Alpha(float alpha)
    {
        //get color
        Color color = hoverMaterial.GetColor("_Base_Color");
        hoverMaterial.SetColor("_Base_Color", new Color(color.r, color.g, color.b, alpha));
    }

    public void DefaultAlpha()
    {
        Alpha(defaultAlpha);
    }
}
