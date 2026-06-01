using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class Corner() : CustomCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [
		CardKeyword.Exhaust,
	];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new CardsVar(6),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<Shiv>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

		var cardsInHand = PileType.Hand.GetPile(Owner).Cards.Count;
		var numberOfShivs = DynamicVars.Cards.BaseValue - cardsInHand;
		for (var i = 0; i < numberOfShivs; i++) {
			await Shiv.CreateInHand(Owner, CombatState!);
			await Cmd.Wait(0.1f);
		}
    }

	protected override void OnUpgrade() {
		DynamicVars.Cards.UpgradeValueBy(1);
	}
}
