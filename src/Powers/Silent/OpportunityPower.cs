using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;


namespace SecondDraw.Powers.Silent;

public class OpportunityPower : CustomPower {
    public override PowerType Type => PowerType.Buff;
	public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>(),
    ];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(2)
    ];


	public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        var isOwner = cardPlay.Card.Owner.Creature == Owner;
        var aboveEnergy = cardPlay.Card.EnergyCost.GetResolved() >= DynamicVars.Energy.IntValue;

		if (isOwner && aboveEnergy) {
			Flash();
            await Shiv.CreateInHand(Owner.Player!, Amount, CombatState);
		}

    }
}
