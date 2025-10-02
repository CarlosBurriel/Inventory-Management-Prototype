using UnityEngine;
using DG.Tweening;

public class ItemCellCollision : MonoBehaviour
{
    [SerializeField] public bool dampedItem;
    [SerializeField] public bool CellActived;
    [SerializeField] private Transform currentCell;

    private int cellcount;
    [SerializeField] private bool allCellsActivated;
    private GameObject parentGO;

    [SerializeField]private BoxCollider[] activechilds;

    private void Start()
    {
        parentGO = gameObject.transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InventoryDemoCell"))
        {
            if (other.gameObject.GetComponent<CellColision>().avaliableCell == true && !dampedItem)
            {
                CellActived = true;
                currentCell = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InventoryDemoCell"))
        {
            CellActived = false;
            
        }
    }

    public void DampItem()
    {
        CheckCellsPositions();
        if (currentCell != null && allCellsActivated) { allCellsActivated = false; dampedItem = true; transform.parent.parent.transform.DOMove(new Vector3(currentCell.position.x, currentCell.position.y, 0.5f), 0.2f); DesactivateCells();}
        else { currentCell = null; print("The Piece is Placed in a Wrong Position"); transform.root.DOMove(new Vector3(-8, -3, 0.5f), 0.5f); }
    }

    private void CheckCellsPositions()
    {
        int count = transform.parent.childCount;
        activechilds = transform.parent.GetComponentsInChildren<BoxCollider>(false);

        cellcount = 0;
        for (int i = 0; i < activechilds.Length; i++)
        {
            if (activechilds[i].GetComponent<ItemCellCollision>().CellActived == true)
            {
                cellcount++;
            }
            if (cellcount == activechilds.Length)
            {
                allCellsActivated = true;
                break;
            }
        }
    }

    private void DesactivateCells()
    {
        for(int i = 0; i < activechilds.Length; i++)
        {
            activechilds[i].gameObject.GetComponent<ItemCellCollision>().currentCell.GetComponent<CellColision>().avaliableCell = false;
        }
    }

    public void ReleasingItem()
    {
        dampedItem = false;
        if(activechilds == null || currentCell == null) { return; }
        for (int i = 0; i < activechilds.Length; i++)
        {
            activechilds[i].gameObject.GetComponent<ItemCellCollision>().currentCell.GetComponent<CellColision>().avaliableCell = true;
        }
        activechilds = null;
    }
}
