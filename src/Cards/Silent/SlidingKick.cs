using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;


namespace SecondDraw.Cards.Silent;

[Pool(typeof(SilentCardPool))]
public class SlidingKick() : CustomCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {
	public override IEnumerable<CardKeyword> CanonicalKeywords => [];

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DamageVar(4, ValueProp.Move),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

		await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
			.FromCard(this)
			.Targeting(cardPlay.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);

		var prefs = new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 2);
		var selected = await CardSelectCmd.FromHandForDiscard(choiceContext, Owner, prefs, null, this);
		foreach (var card in selected) {
			await CardCmd.Discard(choiceContext, card);
		}
    }

	protected override void OnUpgrade() {
		DynamicVars.Damage.UpgradeValueBy(3);
	}
}
