﻿using Furesoft.Core.CodeDom.Parsing;
using Furesoft.Core.CodeDom.CodeDOM.Base;

namespace Nova.CodeDOM
{
    /// <summary>
    /// Shifts the value of the left <see cref="Expression"/> LEFT by the value of the right <see cref="Expression"/>, and assigns the
    /// result to the left <see cref="Expression"/>.  The left <see cref="Expression"/> must be an assignable object ("lvalue").
    /// </summary>
    public class LeftShiftAssign : Assignment
    {
        /// <summary>
        /// The token used to parse the code object.
        /// </summary>
        public new const string ParseToken = "<<=";

        /// <summary>
        /// Create a <see cref="LeftShiftAssign"/> operator.
        /// </summary>
        public LeftShiftAssign(Expression left, Expression right)
            : base(left, right)
        { }

        protected LeftShiftAssign(Parser parser, CodeObject parent)
                    : base(parser, parent)
        { }

        /// <summary>
        /// The symbol associated with the operator.
        /// </summary>
        public override string Symbol
        {
            get { return ParseToken; }
        }

        /// <summary>
        /// Parse a <see cref="LeftShiftAssign"/> operator.
        /// </summary>
        public static new LeftShiftAssign Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            return new LeftShiftAssign(parser, parent);
        }

        /// <summary>
        /// The internal name of the <see cref="BinaryOperator"/>.
        /// </summary>
        public override string GetInternalName()
        {
            return LeftShift.InternalName;
        }

        internal static new void AddParsePoints()
        {
            Parser.AddOperatorParsePoint(ParseToken, Precedence, LeftAssociative, false, Parse);
        }
    }
}