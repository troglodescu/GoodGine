namespace GoodGine;

public class Engine
{
    private static Engine instance;

    private Scene scene;
    public static Scene Scene => Instance.scene;

    private static Engine Instance
    {
        get
        {
            if (instance == null)
                instance = new Engine();
            return instance;
        }
    }

    public static void Start(string firstScenePath, Func<string, Type> typeFinder)
    {
        Instance.StartInternal(firstScenePath, typeFinder);
    }

    public static void Quit()
    {
        Instance.QuitInternal();
    }

    public static GoodBject FindObject(string objName)
    {
        return Scene.GoodBjects.Find((g) => g.Name == objName);
    }

    private void QuitInternal()
    {
        scene.Quit();
    }

    private void StartInternal(string firstScenePath, Func<string, Type> typeFinder)
    {
        scene = new Scene(typeFinder);

        try
        {
            scene.Load(firstScenePath);

            while (!scene.ShouldQuit)
            {
                scene.Update();

                Thread.Sleep(10);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}