using System.Collections.Generic;

public class Koishi_Back : SpellCardEvent {
	public WrittenControl Next;
	public override void OnSpellCardDestroy (List<Shooting> Target, SpellCard Spell, Enemy Character)
	{
		base.OnSpellCardDestroy (Target, Spell, Character);
		Global.PlayerObject.GetComponent<Character> ().invincibleTime = 90;
		Next.SetWrittenSystem ();
	}
}
