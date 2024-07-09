using System.Collections.Generic;
using UnityEngine;
public class  ObjectPooler: MonoBehaviour 
{
    public static ObjectPooler instance;
    private Dictionary<string, Queue<PoolObject>> ObjectPool;

    [System.Serializable]
    public class Pool
    {
        public PoolObject poolObj;
        public int size;
    }
    public List<Pool> pools;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        ObjectPool = new Dictionary<string, Queue<PoolObject>>();

        foreach (Pool pool in pools)
        {
            Queue<PoolObject> objectPool = new Queue<PoolObject>();

            for (int i = 0; i < pool.size; i++)
            {
                PoolObject obj = Instantiate(pool.poolObj);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            ObjectPool.Add(pool.poolObj.poolTag, objectPool);
        }
    } 
    public void InsertPoolObject(PoolObject _obj)
    {
        _obj.gameObject.SetActive(false);
        if (!ObjectPool.ContainsKey(_obj.poolTag))
        {
            Queue<PoolObject> poolObjects = new Queue<PoolObject>();
            ObjectPool.Add(_obj.poolTag, poolObjects);
        }

        ObjectPool[_obj.poolTag].Enqueue(_obj);

    }
    public PoolObject GetPoolObject(string _name)
    {
        PoolObject obj = null;
        if (ObjectPool[_name].Count > 0)
        {
            obj = ObjectPool[_name].Dequeue();
        }
        else
        {
            obj = Create(_name);
        }
        return obj;
    }
    public PoolObject GetPoolObject(string _name, Vector3 _transform, Quaternion _quaternion)
    {
        PoolObject obj = GetPoolObject(_name);
        obj.transform.position = _transform;
        obj.transform.rotation = _quaternion;
        return obj;
    }
    public PoolObject GetPoolObject(string _name, Transform _transform)
    {
        PoolObject obj = GetPoolObject(_name);
        obj.transform.SetParent(_transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        return obj;
    }
    private PoolObject Create(string _name)
    {
        PoolObject obj = null;
        foreach (Pool poolObject in pools)
        {
            if (poolObject.poolObj.poolTag == _name)
            {
                obj = Instantiate(poolObject.poolObj);
                obj.gameObject.SetActive(false);
                return obj;
            }
        }
        Debug.LogError("null ¹ÝÈ¯");
        return obj;
    }

}
