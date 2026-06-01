using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using SecondDraw.Powers.Silent;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class Scent() : CustomCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new BlockVar(5, ValueProp.Move),
		new PowerVar<ScentPower>(3),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<StrengthPower>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

		await PowerCmd.Apply<ScentPower>(
			new ThrowingPlayerChoiceContext(),
			Owner.Creature,
			DynamicVars[nameof(ScentPower)].BaseValue,
			Owner.Creature,
			this
		);
    }

	protected override void OnUpgrade() {
		DynamicVars.Block.UpgradeValueBy(2);
		DynamicVars[nameof(ScentPower)].UpgradeValueBy(1);
	}
}
