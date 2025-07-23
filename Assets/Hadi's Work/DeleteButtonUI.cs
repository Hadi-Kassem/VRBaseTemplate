using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonUI : MonoBehaviour
{
    public GameObject targetToDelete;

    private void Start()
    {
        // Get the Button component on this GameObject (which IS the button)
        Button deleteButton = GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(DeleteTarget);
        }

        gameObject.SetActive(false); // Start hidden
    }

    public void Show(Vector3 worldPosition, GameObject target)
    {
        targetToDelete = target;
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DeleteTarget()
    {
        if (targetToDelete != null)
        {
            Destroy(targetToDelete);
        }
        Destroy(gameObject); // Also remove the delete button
    }
}
