using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using SecondDraw.Powers.Silent;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class Opportunity() : CustomCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new EnergyVar(2),
		new PowerVar<OpportunityPower>(2),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<Shiv>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

		await PowerCmd.Apply<OpportunityPower>(
			choiceContext,
			Owner.Creature,
			DynamicVars[nameof(OpportunityPower)].BaseValue,
			Owner.Creature,
			this
		);
    }

	protected override void OnUpgrade() {
		AddKeyword(CardKeyword.Innate);
	}
}
