using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProcessData;
using Sirenix.OdinInspector;


public class MapController : SerializedMonoBehaviour
{

    [HideInInspector]
    public static MapController Instance;

    [HideInInspector]
    static public float DndTime;    //时间

    [Header("当前的地图")]
    public MapEntity currentMap = null;

    [Header("所有的场景prefab集合")]
    public List<GameObject> sceneObjects = new List<GameObject>() ;

    [Header("所有的地图")]
    public List<MapEntity> mapEntities = new List<MapEntity>();

    [Header("所有的地图Asset")]
    public List<MapAsset> mapAssets = new List<MapAsset>();


    private void Start()
    {
        InitAsset();
    }


    [Button("地图跳转")]
    /// <summary>
    /// 切换场景的总方法
    /// </summary>
    public void ChangeMap(string mapName)   //注视3s的物体的名字，要与mapEntity对应
    {
        if (mapName == "")
            return;

        //跳转前先将将离开的地图state修改
        currentMap.mapState = MapState.HasBeenButNotOn;   
        ChangeImageState();

        //加载新的地图到currentMap
        LoadCurrentMap(mapName);

        //根据加载的asset自动生成场景、事件
        RandomGenerate();

        //触发事件, 触发结束（timeline播放完）后5s重新激活 摄像头中心射线  ，才能进行下一个跳场景。
        EventController.Instance.EventTrigger(currentMap.eventAsset);

        //新地图 MapState, UI修改
        currentMap.mapState = MapState.OnTheMap;
        ChangeImageState();
        
    }


    /// <summary>
    /// 将MapAsset文件信息写入 MapEntity
    /// </summary>
    public void InitAsset()
    {
        mapEntities.Clear();
        foreach (MapAsset ma in mapAssets)
        {
            MapEntity mapEntity = new MapEntity
            {
                mapName = ma.mapName,
                image = ma.image,
                sceneObject = ma.sceneObject,
                needRandomEvent = ma.needRandomEvent,
                eventAsset = ma.eventAsset,
                mapState = ma.mapState
            };
            if (mapEntity.mapState == MapState.OnTheMap)
                currentMap = mapEntity;
            mapEntities.Add(mapEntity);
        }
    }


    /// <summary>
    /// 加载当前场景
    /// </summary>
    public void LoadCurrentMap(string mapName)
    {              
        currentMap = GetMapByName(mapName);
        if (currentMap == null)
            Debug.LogError(mapName+"对应的mapEntity为空");
    }


    /// <summary>
    /// 根据名字来查对应的MapEntity
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public MapEntity GetMapByName(string name)
    {      
        foreach (MapEntity map in mapEntities)
        {
            if (map.mapName.Equals(name))
            {
                return map;
            }
               
        }
        return null;
    }

   
    /// <summary>
    /// 跳转时改变地图ui状态
    /// </summary>
    public void ChangeImageState()
    {
        if (!currentMap.image)
        {
            Debug.LogWarning("该地图在 地图Canvas上没有对应得image UI");
            return;
        }
     
        switch(currentMap.mapState)
        {
            case MapState.NotHasBeen:
                currentMap.image.color = new Color(0.1f, 0.1f, 0.1f);
                break;
            case MapState.HasBeenButNotOn:
                currentMap.image.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case MapState.OnTheMap:
                currentMap.image.color = new Color(1, 1, 1);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 根据需要自动生成场景、事件
    /// </summary>
    public void RandomGenerate()
    {
        if (!currentMap.sceneObject)
            currentMap.sceneObject = sceneObjects[Random.Range(0, sceneObjects.Count)];  // 0   -   length-1
        if (currentMap.needRandomEvent)
            currentMap.eventAsset = EventController.Instance.RandomEventGenerate();
        return;
    }


    
}



//public class MapController : SerializedMonoBehaviour
//{

//    [HideInInspector]
//    public static MapController Instance;

//    [HideInInspector]
//    static public float DndTime;    //时间

//    [Header("当前的地图")]
//    public MapEntity currentMap = null;

//    [Header("所有的场景prefab集合")]
//    public List<GameObject> sceneObjects = new List<GameObject>();

//    [Header("所有的地图")]
//    public List<MapEntity> mapEntities = new List<MapEntity>();


//    //用于连接场景物体与跳转的地图
//    [Header("物体-地图的map")]
//    [ShowInInspector]
//    [DictionaryDrawerSettings(KeyLabel = "跳转媒介", ValueLabel = "跳转地图")]
//    public Dictionary<GameObject, string> mapDictionary = new Dictionary<GameObject, string>();

//    [Header("所有的地图Asset")]
//    public List<MapAsset> mapAssets = new List<MapAsset>();


//    private void Start()
//    {
//        InitAsset();
//    }


//    [Button("地图跳转")]
//    /// <summary>
//    /// 切换场景的总方法
//    /// </summary>
//    public void ChangeMap(GameObject obj)   //obj为注视3s的物体
//    {
//        if (!obj || !mapDictionary.ContainsKey(obj))
//            return;

//        //跳转前先将将离开的地图state修改
//        currentMap.mapState = MapState.HasBeenButNotOn;
//        ChangeImageState();

//        //加载新的地图
//        LoadAsset(obj);

//        //根据加载的asset自动生成场景、事件
//        RandomGenerate();

//        //触发事件, 触发结束（timeline播放完）后5s重新激活 摄像头中心射线  ，才能进行下一个跳场景。
//        EventController.Instance.EventTrigger(currentMap.eventAsset);

//        //新地图 MapState, UI修改
//        currentMap.mapState = MapState.OnTheMap;
//        ChangeImageState();

//    }


//    /// <summary>
//    /// 将MapAsset文件信息写入 MapEntity
//    /// </summary>
//    public void InitAsset()
//    {
//        mapEntities.Clear();
//        foreach (MapAsset ma in mapAssets)
//        {
//            MapEntity mapEntity = new MapEntity
//            {
//                mapName = ma.mapName,
//                image = ma.image,
//                sceneObject = ma.sceneObject,
//                needRandomEvent = ma.needRandomEvent,
//                eventAsset = ma.eventAsset,
//                mapState = ma.mapState
//            };
//            if (mapEntity.mapState == MapState.OnTheMap)
//                currentMap = mapEntity;
//            mapEntities.Add(mapEntity);
//        }
//    }


//    /// <summary>
//    /// 加载当前场景
//    /// </summary>
//    public void LoadAsset(GameObject obj)
//    {

//        if (mapDictionary[obj] == "")
//        {
//            Debug.LogError("该物体对应的地图为空");
//            return;
//        }

//        currentMap = GetMapByName(mapDictionary[obj]);

//    }


//    /// <summary>
//    /// 根据名字来查对应的MapEntity
//    /// </summary>
//    /// <param name="name"></param>
//    /// <returns></returns>
//    public MapEntity GetMapByName(string name)
//    {
//        foreach (MapEntity map in mapEntities)
//        {
//            if (map.mapName.Equals(name))
//            {
//                return map;
//            }

//        }
//        return null;
//    }


//    /// <summary>
//    /// 跳转时改变地图ui状态
//    /// </summary>
//    public void ChangeImageState()
//    {
//        if (!currentMap.image)
//        {
//            Debug.LogWarning("该地图在 地图Canvas上没有对应得image UI");
//            return;
//        }

//        switch (currentMap.mapState)
//        {
//            case MapState.NotHasBeen:
//                currentMap.image.color = new Color(0.1f, 0.1f, 0.1f);
//                break;
//            case MapState.HasBeenButNotOn:
//                currentMap.image.color = new Color(0.5f, 0.5f, 0.5f);
//                break;
//            case MapState.OnTheMap:
//                currentMap.image.color = new Color(1, 1, 1);
//                break;
//            default:
//                break;
//        }
//    }

//    /// <summary>
//    /// 根据需要自动生成场景、事件
//    /// </summary>
//    public void RandomGenerate()
//    {
//        if (!currentMap.sceneObject)
//            currentMap.sceneObject = sceneObjects[Random.Range(0, sceneObjects.Count)];  // 0   -   length-1
//        if (currentMap.needRandomEvent)
//            currentMap.eventAsset = EventController.Instance.RandomEventGenerate();
//        return;
//    }



//}