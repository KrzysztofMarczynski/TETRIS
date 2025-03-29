using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Terminoes; 
    private bool canSpawn = true;
    
    public GameObject nextPiece;
    public GameObject heldPiece;

    public Transform nextPiecePreviewPosition; 
    public Transform holdPiecePreviewPosition;

    private GameObject previewObject; 
    private GameObject holdPreviewObject;

    private bool hasSwapped = false;

    void Start()
    {
        nextPiece = GetRandomPiece();
        NewTermino();
    }

    public void NewTermino()
    {
        if (!canSpawn) return;

        GameObject currentPiece = Instantiate(nextPiece, transform.position, Quaternion.identity);
        FindFirstObjectByType<Move>().SetCurrentPiece(currentPiece);

        nextPiece = GetRandomPiece();
        UpdatePreview();
        hasSwapped = false;
    }

    private GameObject GetRandomPiece()
    {
        return Terminoes[Random.Range(0, Terminoes.Length)];
    }

    private void UpdatePreview()
    {
        if (previewObject != null) Destroy(previewObject);
        previewObject = Instantiate(nextPiece, nextPiecePreviewPosition.position, Quaternion.identity);
        previewObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void UpdateHoldPreview()
    {
        if (holdPreviewObject != null) Destroy(holdPreviewObject);
        if (heldPiece != null)
        {
            holdPreviewObject = Instantiate(heldPiece, holdPiecePreviewPosition.position, Quaternion.identity);
            holdPreviewObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void HoldPiece(GameObject currentPiece)
    {
        if (hasSwapped) return;

        if (heldPiece == null)
        {
            heldPiece = currentPiece;
            Destroy(currentPiece);
            NewTermino();
        }
        else
        {
            GameObject temp = heldPiece;
            heldPiece = currentPiece;
            Destroy(currentPiece);
            GameObject swappedPiece = Instantiate(temp, transform.position, Quaternion.identity);
            FindFirstObjectByType<Move>().SetCurrentPiece(swappedPiece);
        }

        UpdateHoldPreview();
        hasSwapped = true;
    }

    public void StopSpawn()
    {
        canSpawn = false;
        FindFirstObjectByType<GameOver>().Over();
    }
}
