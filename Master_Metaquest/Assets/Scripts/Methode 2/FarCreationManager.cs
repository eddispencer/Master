using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FarCreationManager : MonoBehaviour
{

    [SerializeField] private GameObject dummyWallNodePrefab;
    [SerializeField] private WallBuilder wallPrefab;
    [SerializeField] private List<GameObject> creationNodes = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNode(Vector3 pos)
    {
        var node = Instantiate(dummyWallNodePrefab, pos, Quaternion.identity, transform);
        creationNodes.Add(node);
        BuildWall();
    }

    private void BuildWall()
    {
        if (creationNodes.Count != 2) return;
        
        var wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        wall.start.transform.position = creationNodes[0].transform.position;
        wall.end.transform.position = creationNodes[1].transform.position;
        foreach (var node in creationNodes)
        {
            Destroy(node);
        }
        
        wall.BuildWall();
        creationNodes.Clear();
    }
}
