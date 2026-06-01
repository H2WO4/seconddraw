using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using SecondDraw.Cards.Silent;


namespace SecondDraw.Powers.Silent;

public class ScentPower : CustomPower {
    public override PowerType Type => PowerType.Buff;
	public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Scent>(),
    ];

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (participants.Contains(Owner)) {
            await PowerCmd.Apply<ScentStrengthPower>(
                new ThrowingPlayerChoiceContext(),
                Owner,
                Amount,
                Owner,
                null
            );
            await PowerCmd.Remove(this);
        }
    }
}
