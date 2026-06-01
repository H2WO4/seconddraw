using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using SecondDraw.Cards.Silent;
using SecondDraw.Extensions;


namespace SecondDraw.Powers.Silent;

public class ScentStrengthPower : CustomTemporaryPowerModelWrapper<Scent, StrengthPower> {
	protected override bool InvertInternalPowerAmount => false;
    public override PowerType Type => PowerType.Buff;

    public override string CustomPackedIconPath => $"power.png".PowerImagePath();
    public override string CustomBigIconPath => $"power.png".BigPowerImagePath();
}
