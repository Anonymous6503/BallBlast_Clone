
using UnityEngine;

public class Game : MonoBehaviour
{
    public float _screenWidth;

    #region Singleton class: Game
    
    public static Game Instance;
    private void Awake()
    {
        Instance = this;
        _screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width,0f,0f)).x;
    }

    #endregion
}
