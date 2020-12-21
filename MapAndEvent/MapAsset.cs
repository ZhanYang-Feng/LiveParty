using UnityEngine;
using UnityEngine.Timeline;
using ProcessData;
using UnityEngine.UI;
using Sirenix.OdinInspector;

/**********************************************
* 模块名: MapAsset.cs
* 功能描述：地图信息配置文件类
***********************************************/

[CreateAssetMenu(menuName = "MapAsset", fileName = "mapAsset", order = 2)]

public class MapAsset : SerializedScriptableObject
{
    [Header("地图名称")]
    public string mapName;

    [Header("地图所对应的3Dcanvas的ui")]       //总地图视角里的一个六边形
    public Image image;

    [Header("地图状态")]
    public MapState mapState;               

    [Header("地图里的具体场景（prefab）")]
    public GameObject sceneObject;

    [Header("是否要随机生成事件")]
    public bool needRandomEvent;

    [Header("事件")]
    public EventAsset eventAsset;
}

public class MapEntity
{
    [Header("地图名称")]
    public string mapName;

    [Header("地图所对应的3Dcanvas的ui")]       //总地图视角里的一个六边形
    public Image image;

    [Header("地图状态")]
    public MapState mapState;               

    [Header("地图里的具体场景（prefab）")]
    public GameObject sceneObject;

    [Header("是否要随机生成事件")]
    public bool needRandomEvent;

    [Header("事件")]
    public EventAsset eventAsset;

}