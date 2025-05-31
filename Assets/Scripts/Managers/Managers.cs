using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static bool s_init = false;

    #region Contents
    GameManager _game = new GameManager();
    ObjectManager _object = new ObjectManager();
    RecipeManager _recipe = new RecipeManager();
    public static GameManager Game { get { return Instance?._game; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static RecipeManager Recipe { get { return Instance?._recipe; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    public static DataManager Data { get { return Instance?._data; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static UIManager UI { get { return Instance?._ui; } }
    #endregion

    public static Managers Instance
    {
        get
        {
            if (s_init == false)
            {
                s_init = true;

                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject() { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();
            }

            return s_instance;
        }
    }
}