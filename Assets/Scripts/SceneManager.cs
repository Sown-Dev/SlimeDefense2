public class SceneManager{

    public static void LoadScene(Scenum s){
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)s);
    }


}

public enum Scenum{
    MainMenu=1,
    CharSelect=2,
    Game=3,
}