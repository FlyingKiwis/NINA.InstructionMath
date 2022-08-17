using NINA.InstructionMath.SequenceItems;
using NINA.Sequencer;
using NINA.Sequencer.SequenceItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NINA.InstructionMath.ExpressionEditor {
    public interface IExpressionItem : ISequenceEntity 
    {
        string GetExpression();

        void SetExpression(string expression);
    }
}
