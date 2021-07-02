using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class StageCommand {
    public enum CommandType { 
        ACITVE_OBJECT = 0,
        DISACITVE_OBJECT = 1,
        PLAY_SOUND = 2,
        CHANGE_SOUND_SOURCE = 3,
        PAUSE_COUNTING = 4,
        STAGE_CLEAR = 5,
        BULLET_DESTROY = 6,
        USE_BARRAGE = 7,
        MOVE_ENEMY = 8,
        GRAPH_EVENT = 9
    }
    [LabelText("距上一命令执行后的延迟")]
    public float Time = 0;
    [LabelText("要执行的命令")]
    public CommandType Command = CommandType.ACITVE_OBJECT;
    [LabelText("目标对象")]
    [ShowIf( "@this.Command == CommandType.ACITVE_OBJECT || this.Command == CommandType.DISACITVE_OBJECT")]
    public GameObject Target;
    [ShowIf("Command",  CommandType.CHANGE_SOUND_SOURCE)]
    public AudioSource TargetSource;
    [ShowIf("Command",  CommandType.CHANGE_SOUND_SOURCE)]
    public AudioClip Clip;
    [ShowIf("Command",  CommandType.PLAY_SOUND)]
    public AudioSource PlaySource;
    [ShowIf("Command",  CommandType.STAGE_CLEAR)]
    public long BounsScore;
    [ShowIf("Command",  CommandType.USE_BARRAGE)]
    public Enemy enemyTarget;
    [ShowIf("Command",  CommandType.MOVE_ENEMY)]
    public Enemy MoveenemyTarget;
    [ShowIf("Command",  CommandType.MOVE_ENEMY)]
    public Vector2 TargetPoint;
    [ShowIf("Command", CommandType.GRAPH_EVENT)]
    public STGTriggerGraph Graph;
    [ShowIf("Command", CommandType.GRAPH_EVENT)]
    public string methodName;
}
[AddComponentMenu("东方STG框架/弹幕设计/关卡进程/关卡时间轴")]
public class StageTimeline : MonoBehaviour {
    [LabelText("延迟执行时长")]
    public float DelayProcess = 0;
    [LabelText("关卡命令(按帧执行)")]
    public List<StageCommand> CommandList = new List<StageCommand>();
    [Tooltip("是否在启动游戏时，将CommandList里所有指定的对象全部设定为不启用状态。")]
    [LabelText("自动关闭对象(详细见注释)")]
    public bool ObjectDisactive = true;
    private  float _localTime = 0;
    [HideInInspector]
    public float _delay = 0;
	void Start () {
        if (ObjectDisactive)
            for (int i = 0; i != CommandList.Count; ++i) {
                if (CommandList [i].Target != null)
                CommandList [i].Target.SetActive (false);
            }
    }
	
	// Update is called once per frame
	void Update () {
        if (Global.WrttienSystem || Global.GamePause || Global.RoadBossStage)
            return;
        _delay =  _delay+ 1 * Global.GlobalSpeed;
        if (_delay < DelayProcess)
            return;
        _localTime += 1 * Global.GlobalSpeed;
     //   Process = _localTime / maxTime * 100;

        for (int i = 0; i != CommandList.Count; ++i) {
            
            if (_localTime > CommandList[i].Time)
            {
                _localTime = 0;
                switch (CommandList[i].Command)
                {
                    case StageCommand.CommandType.ACITVE_OBJECT:
                        CommandList[i].Target.SetActive(true);
                        break;
                    case StageCommand.CommandType.DISACITVE_OBJECT:
                        CommandList[i].Target.SetActive(false);
                        break;
                    case StageCommand.CommandType.PLAY_SOUND:
                        CommandList [i].PlaySource.Play ();
                        break;
                    case StageCommand.CommandType.CHANGE_SOUND_SOURCE:
                        CommandList [i].TargetSource.clip = CommandList [i].Clip;
                        break;
                    case StageCommand.CommandType.PAUSE_COUNTING:
                        Debug.LogWarning ("已暂停道中时间轴计时，后续指令将不再执行，除非击败BOSS或满足特定条件。");
                        Global.RoadBossStage = true;
                        break;
                    case StageCommand.CommandType.BULLET_DESTROY:
                        Global.GameObjectPool_A.DestroyBullets (true, true, 0);
                        break;
                    case StageCommand.CommandType.USE_BARRAGE:
                        CommandList [i].enemyTarget.UseBarrages ();
                        break;
                    case StageCommand.CommandType.MOVE_ENEMY:
                        CommandList [i].MoveenemyTarget.Move ( CommandList [i].TargetPoint);
                        break;
                    case StageCommand.CommandType.STAGE_CLEAR:
                        if (Global.StageList_A [Global.StageIndex] != null)
                            Global.StageList_A [Global.StageIndex].StageClear (CommandList [i].BounsScore);
                        else
                            Debug.LogError ("未定义指定的关卡脚本，请添加。");
                        break;
                    case StageCommand.CommandType.GRAPH_EVENT:
                        XNode.Node nodeGot = null;
                        foreach (var a in CommandList[i].Graph.nodes)
                        {
                            if (a == null)
                                continue;
                            if (a.name == CommandList[i].methodName)
                            {
                                nodeGot = a;
                                break;
                            }
                        }
                        if (nodeGot != null)
                            nodeGot.ConnectDo("继续");
                        break;
                }
                CommandList.Remove(CommandList[i]);
                return;
            }
        }
    }
}
