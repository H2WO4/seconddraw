
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class PhantomStrike() : CustomCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DamageVar(8, ValueProp.Move),
		new CalculationBaseVar(1),
		new CalculationExtraVar(1),
		new CalculatedVar("CalculatedHits").WithMultiplier((CardModel card, Creature? _) =>
			GetSlyInHand(card.Owner).Count()
		),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromKeyword(CardKeyword.Sly),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

		var repeat = (int)((CalculatedVar)DynamicVars["CalculatedHits"]).Calculate(cardPlay.Target);
		await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
			.FromCard(this)
			.Targeting(cardPlay.Target)
			.WithHitCount(repeat)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
    }

	private static IEnumerable<CardModel> GetSlyInHand(Player owner) {
		return owner.PlayerCombatState!.Hand.Cards.Where((CardModel c) => c.Keywords.Contains(CardKeyword.Sly));
	}

	protected override void OnUpgrade() {
		DynamicVars.Damage.UpgradeValueBy(2);
	}
}
