using UnityEngine;

public class MaterialItemFeedback : MonoBehaviour
{
    private Camera uiCamera;
    private Ray UICameraRay;
    private RaycastHit UICameraRayHit;

    public bool hasMultipleMeshes = false;

    private MeshRenderer meshRenderer;
    private MeshRenderer[] meshRenderers;
    
    public Material cellMaterial;
    public Material pressedCellMaterial;


    private void Start()
    {
        uiCamera = FindAnyObjectByType<Camera>();
        if(transform.childCount > 0)
        {
            meshRenderers = transform.parent.GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = cellMaterial;
            }
        }
        else
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = cellMaterial;
        }
        
    }

    void Update()
    {
        UICameraRay = uiCamera.ScreenPointToRay(Input.mousePosition);

        if (hasMultipleMeshes)
        {
            if (Physics.Raycast(UICameraRay, out UICameraRayHit, 30f))
            {
                if (UICameraRayHit.collider.gameObject == gameObject)
                {
                    for (int i = 0; i < meshRenderers.Length; i++)
                    {
                        meshRenderers[i].material = pressedCellMaterial;
                    }
                }
                else
                {
                    for (int i = 0; i < meshRenderers.Length; i++)
                    {
                        meshRenderers[i].material = cellMaterial;
                    }
                }
            }
        }
        else
        {
            if (Physics.Raycast(UICameraRay, out UICameraRayHit, 30f))
            {
                if (UICameraRayHit.collider.gameObject == gameObject)
                {
                    meshRenderer.material = pressedCellMaterial;
                }
                else
                {
                    meshRenderer.material = cellMaterial;
                }
            }
        }
    }
}
