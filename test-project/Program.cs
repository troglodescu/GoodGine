using Good;
using System.Reflection;

Type TypeFinder(string arg)
{
    return Type.GetType(arg);
}

Console.WriteLine(Assembly.GetEntryAssembly().Location);

var pathToScene = $"{Assembly.GetEntryAssembly().Location}\\..\\..\\..\\..\\Scenes\\Scene1.scene";

var scene = new Scene(TypeFinder);

try
{
    scene.Load(pathToScene);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

Console.WriteLine("Succesffully read");