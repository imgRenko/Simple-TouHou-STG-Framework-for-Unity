using System.Collections.Generic;

public class Koishi_UNSPELL01 : SpellCardEvent {
	public Shooting chargeShooting;
	public Shooting chargeSecondShooting;
	public override void OnSpellCardUsage (List<Shooting> Target, SpellCard Spell, Enemy Character)
	{
		base.OnSpellCardUsage (Target, Spell, Character);
		Shooting.RecoverShooting (chargeShooting);
		Shooting.RecoverShooting (chargeSecondShooting);
	}
}
