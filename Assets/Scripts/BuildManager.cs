using Supyrb;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BuildManager : MonoBehaviour
{ 
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private TurretLootTable _turretLootTable;
    private PlayerInputActions playerInputActions;
    
    private GameObject selectedTurret;
    private bool isPlacingTurret;
    
    private void OnEnable()
    {
        _playerInputManager.PlayerInputActions.Gameplay.BuyTurret.performed += OnBuyTurretPerformed;
        _playerInputManager.PlayerInputActions.BuildMode.PlaceTurret.performed += OnPlaceTurretPerformed;
        _playerInputManager.PlayerInputActions.BuildMode.CancelTurret.performed += OnCancelTurretPerformed;
    }

    private void OnDisable()
    {
        _playerInputManager.PlayerInputActions.Gameplay.BuyTurret.performed -= OnBuyTurretPerformed;
        _playerInputManager.PlayerInputActions.BuildMode.PlaceTurret.performed -= OnPlaceTurretPerformed;
        _playerInputManager.PlayerInputActions.BuildMode.CancelTurret.performed -= OnCancelTurretPerformed;
    }

    private void OnBuyTurretPerformed(InputAction.CallbackContext obj)
    {
        if (_turretLootTable != null && _turretLootTable.TurretPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, _turretLootTable.TurretPrefabs.Length);
            selectedTurret = _turretLootTable.TurretPrefabs[randomIndex];
            isPlacingTurret = true;
            Signals.Get<BuildModeStartedSignal>().Dispatch();
        }
    }
    
    private void OnPlaceTurretPerformed(InputAction.CallbackContext obj)
    {
        if (isPlacingTurret)
        {
            // Raycast to detect which tile is clicked
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null && tile.IsFree)
                {
                    Vector3 newPosition = new Vector3(tile.transform.position.x, 2.7f, tile.transform.position.z);
                    Instantiate(selectedTurret, newPosition, Quaternion.identity);
                    tile.IsFree = false;
                    isPlacingTurret = false; 
                    Signals.Get<BuildModeStoppedSignal>().Dispatch();
                }
            }
        }
    }

    private void OnCancelTurretPerformed(InputAction.CallbackContext obj)
    {
        if (isPlacingTurret)
        {
            isPlacingTurret = false;
            selectedTurret = null;
            Signals.Get<BuildModeStoppedSignal>().Dispatch();
        }
    }
}
