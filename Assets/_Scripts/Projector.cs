using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Projector : MonoBehaviour
{
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private int maxSimulatedFrameCount = 100;
    [SerializeField] private int physicsFrameSkipMultiplier = 5;

    private Scene simulationScene;
    private PhysicsScene2D physicsScene;

    private void Start()
    {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("Simulation",
            new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        foreach (var planet in PlanetManager.Instance.Planets)
        {
            var planetPosition = planet.transform.position;
            var planetRotation = planet.transform.rotation;
            var ghostPlanet = Instantiate(planet, planetPosition, planetRotation);
            ghostPlanet.Hide();

            SceneManager.MoveGameObjectToScene(ghostPlanet.gameObject, simulationScene);
        }
    }

    public void SimulateTrajectory(Saw saw, Vector2 startPosition, Vector2 velocity)
    {
        var ghostMachine = Instantiate(saw, startPosition, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostMachine.gameObject, simulationScene);
        ghostMachine.SetVelocity(velocity);

        trajectoryLine.positionCount = maxSimulatedFrameCount + 1;
        trajectoryLine.SetPosition(0, transform.position);
        for (int i = 0; i < maxSimulatedFrameCount; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime * physicsFrameSkipMultiplier);
            trajectoryLine.SetPosition(i + 1, ghostMachine.transform.position);
        }

        Destroy(ghostMachine.gameObject);
    }
}