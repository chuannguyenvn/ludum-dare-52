using UnityEngine;
using UnityEngine.Serialization;

public class ResourceManager : PersistentSingleton<ResourceManager>
{
    public Saw saw;

    public Plant Plant;
    public PlantBranch PlantBranch;
}