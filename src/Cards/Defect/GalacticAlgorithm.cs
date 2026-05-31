using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;


namespace SecondDraw.Cards.Defect;

[Pool(typeof(DefectCardPool))]
public class GalacticAlgorithm() : CustomCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.Static(StaticHoverTip.Channeling),
	];


	protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

		int numOfOrbs = ResolveEnergyXValue();
		if (base.IsUpgraded) {
			numOfOrbs++;
		}

		if (true) {
			numOfOrbs *= 2;
		}
		
		for (int i = 0; i < numOfOrbs; i++) {
			var randomOrb = OrbModel.GetRandomOrb(base.Owner.RunState.Rng.CombatOrbGeneration).ToMutable();
			await OrbCmd.Channel(choiceContext, randomOrb, base.Owner);
		}

    }
}
