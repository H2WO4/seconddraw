using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class BlackDeath() : CustomCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new PowerVar<PoisonPower>(4),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<PoisonPower>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

		await PowerCmd.Apply<PoisonPower>(
			choiceContext,
			cardPlay.Target,
			DynamicVars.Poison.BaseValue,
			Owner.Creature,this
		);
		EnergyCost.AddThisCombat(1);
		DynamicVars.Poison.UpgradeValueBy(DynamicVars.Poison.BaseValue);
    }

	protected override void OnUpgrade() {
		DynamicVars.Poison.UpgradeValueBy(1);
	}
}
