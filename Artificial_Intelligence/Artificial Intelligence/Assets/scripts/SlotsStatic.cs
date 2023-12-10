using UnityEngine;

public class SlotsStatic : MonoBehaviour
{
    public int gameObjectsAmount;
    public GameObject prefab;
    public int rows;
    public GameObject ghost;

    void Start()
    {
        float pos = -2f;
        for (int i = 0; i < rows; i++) 
        {
            createRow((int)(gameObjectsAmount / rows), pos, prefab);
            pos -= 2f;
        }
    }

    void createRow(int num, float z, GameObject pf)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i)
        {
            Vector3 position = ghost.transform.TransformPoint(new Vector3(pos, 0f, z));
            GameObject temp = (GameObject)Instantiate(pf, position, ghost.transform.rotation);
            temp.AddComponent<Formation>();
            temp.GetComponent<Formation>().pos = new Vector3(pos, 0, z);
            temp.GetComponent<Formation>().target = ghost;
            pos += 2f;
        }
    }
}