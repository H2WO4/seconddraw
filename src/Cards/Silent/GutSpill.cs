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
public class GutSpill() : CustomCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DamageVar(2, ValueProp.Move),
		new RepeatVar(3),
		new DynamicVar("StrengthLoss", 2),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<StrengthPower>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

		await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
			.FromCard(this)
			.Targeting(cardPlay.Target)
			.WithHitCount(DynamicVars.Repeat.IntValue)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);

		await PowerCmd.Apply<GutSpillPower>(
			choiceContext,
			cardPlay.Target,
			DynamicVars["StrengthLoss"].BaseValue,
			Owner.Creature,
			this
		);
    }

	protected override void OnUpgrade() {
		DynamicVars.Damage.UpgradeValueBy(1);
		DynamicVars["StrengthLoss"].UpgradeValueBy(1);
	}
}
