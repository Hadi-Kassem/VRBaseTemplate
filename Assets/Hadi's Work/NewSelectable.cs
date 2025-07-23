using UnityEngine;

public class NewSelectable : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    public GameObject deletingCanvasPrefab; // Drag prefab here (the one with DeleteButtonUI on the button)

    private Renderer rend;
    private Color originalColor;
    private GameObject deleteButtonObject; // The actual instantiated button

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void OnSelect()
    {
        if (rend != null)
            rend.material.color = highlightColor;

        // Only instantiate if not already created
        if (deleteButtonObject == null && deletingCanvasPrefab != null)
        {
            // Instantiate the canvas prefab
            deleteButtonObject = Instantiate(deletingCanvasPrefab);

            // Position it above the cylinder
            Vector3 above = transform.position + Vector3.up * 1.5f;

            // Set position and assign target in the script
            DeleteButtonUI deleteButton = deleteButtonObject.GetComponent<DeleteButtonUI>();
            deleteButton.Show(above, gameObject);
        }
        else if (deleteButtonObject != null)
        {
            // Already created: just re-show it in case it was hidden
            Vector3 above = transform.position + Vector3.up * 1.5f;
            deleteButtonObject.transform.position = above;
            deleteButtonObject.SetActive(true);
        }
    }

    public void OnDeselect()
    {
        if (rend != null)
            rend.material.color = originalColor;

        if (deleteButtonObject != null)
        {
            deleteButtonObject.SetActive(false);
        }
    }
}
