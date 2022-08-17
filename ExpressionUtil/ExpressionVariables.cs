﻿using NINA.Astrometry.Interfaces;
using NINA.Sequencer.Utility.DateTimeProvider;
using org.mariuszgromada.math.mxparser;
using NINA.InstructionMath.Extensions;
using NINA.Sequencer;

namespace NINA.InstructionMath.ExpressionUtil {
    public class ExpressionVariables 
    {
        public ExpressionVariables(INighttimeCalculator nighttimeCalculator, ISequenceEntity entity) {
            _nighttimeCalculator = nighttimeCalculator;

            _duskProvider = new DuskProvider(_nighttimeCalculator);
            _dawnProvider = new DawnProvider(_nighttimeCalculator);
            _timeProvider = new TimeProvider();
        }

        private readonly ISequenceEntity _entity;
        private readonly INighttimeCalculator _nighttimeCalculator;
        private readonly DuskProvider _duskProvider;
        private readonly DawnProvider _dawnProvider;
        private readonly TimeProvider _timeProvider;

        public void AddToExpression(Expression expression) 
        {
            var astronomicDusk = new Constant("astro_dusk", _duskProvider.GetDateTime(_entity).ToTimestamp());
            var astronomicDawn = new Constant("astro_dawn", _dawnProvider.GetDateTime(_entity).ToTimestamp());
            var now = new Constant("time", _timeProvider.GetDateTime(_entity).ToTimestamp());

            expression.addConstants(astronomicDusk, astronomicDawn, now);
        }
    }
}
