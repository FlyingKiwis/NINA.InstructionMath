using NINA.Astrometry.Interfaces;
using NINA.Sequencer.Utility.DateTimeProvider;
using org.mariuszgromada.math.mxparser;
using NINA.InstructionMath.Extensions;

namespace NINA.InstructionMath.ExpressionUtil {
    public class ExpressionVariables 
    {
        public ExpressionVariables(INighttimeCalculator nighttimeCalculator) {
            _nighttimeCalculator = nighttimeCalculator;

            _duskProvider = new DuskProvider(_nighttimeCalculator);
            _dawnProvider = new DawnProvider(_nighttimeCalculator);
        }

        private readonly INighttimeCalculator _nighttimeCalculator;
        private readonly DuskProvider _duskProvider;
        private readonly DawnProvider _dawnProvider;

        public void AddToExpression(Expression expression) 
        {
            var astronomicDusk = new Constant("[dusk]", _duskProvider.GetDateTime(null).ToTimestamp());
            var astronomicDawn = new Constant("[dawn]", _dawnProvider.GetDateTime(null).ToTimestamp());

            expression.addConstants(astronomicDusk, astronomicDawn);
        }
    }
}
