using UnityEngine;

public class MaterialCellFeedback : MonoBehaviour
{
    private Camera uiCamera;
    private Ray UICameraRay;
    private RaycastHit UICameraRayHit;

    private MeshRenderer meshRenderer;
    public Material cellMaterial;
    public Material pressedCellMaterial;


    private void Start()
    {
        uiCamera = FindAnyObjectByType<Camera>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = cellMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        UICameraRay = uiCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(UICameraRay, out UICameraRayHit, 30f))
        {
            if (UICameraRayHit.collider.gameObject == transform.parent.gameObject)
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
