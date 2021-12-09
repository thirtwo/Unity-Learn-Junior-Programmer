using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject trailObject;
    private bool _isCutting = false;
    private GameObject _currentTrail;
    private BoxCollider _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndCutting();
        }
        if (_isCutting)
        {
            Cut();
        }

    }

    private void StartCutting()
    {
        _isCutting = true;
        _currentTrail = Instantiate(trailObject, transform);
        _boxCollider.enabled = true;
    }
    private void EndCutting()
    {
        _isCutting = false;
        _currentTrail.transform.SetParent(null);
        Destroy(_currentTrail, 2f);
        _boxCollider.enabled = false;
    }

    private void Cut()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
    }
}
