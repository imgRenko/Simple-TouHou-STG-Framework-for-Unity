using UnityEngine;
[AddComponentMenu("东方STG框架/弹幕设计/常见事件/符卡事件/左侧信息栏关闭BOSS立绘")]
public class BossDestroyEvent : SpellCardEvent {
    public AudioSource Broken;
public override void OnSpellCardDestroy (System.Collections.Generic.List<Shooting> Target, SpellCard Spell, Enemy Character)
    {
        if (Character.isBoss) {
            Character.BossDestroy ();
            BossEvent e = Spell.GetComponent<BossEvent>();
            if (e!= null)
            e.Display = false;
        }
        if (Broken!=null)
            Broken.Play();
        CameraShake.shakeAmount = 0.1f;
    }
}
