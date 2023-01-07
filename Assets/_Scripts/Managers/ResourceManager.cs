using UnityEngine;
using UnityEngine.Serialization;

public class ResourceManager : PersistentSingleton<ResourceManager>
{
    public Water Water;

    public Plant Plant;
    public PlantBranch PlantBranch;
}