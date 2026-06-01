using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using SecondDraw.Powers.Silent;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class WideGrin() : CustomCard(2, CardType.Skill, CardRarity.Common, TargetType.AllEnemies) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [
		CardKeyword.Sly,
	];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DynamicVar("StrengthLoss", 3),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<StrengthPower>(),
	];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

		foreach (var target in CombatState!.HittableEnemies) {
			await PowerCmd.Apply<WideGrinPower>(
				choiceContext,
				target,
				DynamicVars["StrengthLoss"].BaseValue,
				Owner.Creature,
				this
			);
		}
    }

	protected override void OnUpgrade() {
		DynamicVars["StrengthLoss"].UpgradeValueBy(1);
	}
}
