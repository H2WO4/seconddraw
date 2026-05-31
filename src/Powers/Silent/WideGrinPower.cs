using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using SecondDraw.Cards.Silent;
using SecondDraw.Extensions;


namespace SecondDraw.Powers.Silent;

public class WideGrinPower : CustomTemporaryPowerModelWrapper<WideGrin, StrengthPower> {
	protected override bool InvertInternalPowerAmount => true;
    public override PowerType Type => PowerType.Debuff;

    public override string CustomPackedIconPath => $"power.png".PowerImagePath();
    public override string CustomBigIconPath => $"power.png".BigPowerImagePath();
}
