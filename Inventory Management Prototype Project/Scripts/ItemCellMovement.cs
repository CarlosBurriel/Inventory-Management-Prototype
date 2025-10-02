using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class ItemCellMovement : MonoBehaviour
{
    [SerializeField] public bool takingItem;
    [SerializeField] private GameObject currentItem;

    public Camera UICamera;
    private ItemCellCollision itemCollisionScript;
    [HideInInspector] public Ray mouseRay;
    [HideInInspector] public RaycastHit mouseRaycastHit;
    private Vector3 mousePosition;
    private Vector3 aimingPoint;

    private bool rotating = false;

    //Input System
    public InputSystem_Actions inventoryDemoInputSystem;

    void Start()
    {
        inventoryDemoInputSystem = new InputSystem_Actions();
        inventoryDemoInputSystem.Enable();
        inventoryDemoInputSystem.InventoryDemo.ItemMovement.performed += DragItem;
        inventoryDemoInputSystem.InventoryDemo.ItemMovement.canceled += ReleaseItem;

        inventoryDemoInputSystem.InventoryDemo.ItemRotate.performed += RotateItem;

        

        if (UICamera == null) { UICamera = Camera.main; }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        UICamera = GetComponent<Camera>();   
    }

    public void DragItem(InputAction.CallbackContext context) { takingItem = true; }
    public void ReleaseItem(InputAction.CallbackContext context) 
    { 
        takingItem = false; currentItem = null; 
        if (itemCollisionScript != null) 
        {
            itemCollisionScript.DampItem();
        } 
    }

    public void RotateItem(InputAction.CallbackContext context) { if (currentItem != null && !rotating) { rotating = true; currentItem.transform.DORotate(new Vector3(0, 0, currentItem.transform.rotation.eulerAngles.z + 90), 0.2f).OnComplete(() => rotating = false); } }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mouseRay = UICamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(mouseRay, out mouseRaycastHit, 30f) && takingItem)
        {
            if (mouseRaycastHit.collider.gameObject.CompareTag("InventoryDemoItem") && currentItem == null)
            {
                currentItem = mouseRaycastHit.collider.gameObject.transform.parent.gameObject;
                aimingPoint =  new Vector3(mouseRaycastHit.point.x, mouseRaycastHit.point.y, 0.5f);
                currentItem.transform.position = aimingPoint;
                return;
            }
            else if(currentItem != null)
            {
                itemCollisionScript = currentItem.GetComponentInChildren<ItemCellCollision>();
                aimingPoint = new Vector3(mouseRaycastHit.point.x, mouseRaycastHit.point.y, 0.5f);
                currentItem.transform.position = aimingPoint;
                if (itemCollisionScript.dampedItem)
                {
                    itemCollisionScript.ReleasingItem();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        mouseRay = UICamera.ScreenPointToRay(mousePosition);
        Vector3 fin = mouseRay.origin + mouseRay.direction * 30;
        Gizmos.DrawLine(mouseRay.origin, fin);
    }

}
