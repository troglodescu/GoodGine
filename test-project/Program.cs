using GoodGine;
using System.Reflection;

var pathToScene = $"{Assembly.GetEntryAssembly().Location}\\..\\..\\..\\..\\Scenes\\Scene1.scene";

Engine.Start(pathToScene, (arg) => Type.GetType(arg));