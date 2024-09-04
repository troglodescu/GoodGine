// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Good;

Console.WriteLine(Assembly.GetEntryAssembly().Location);

var pathToScene = $"{Assembly.GetEntryAssembly().Location}\\..\\..\\..\\..\\TestProj\\Scenes\\Scene1.scene";

var scene = new Scene();
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