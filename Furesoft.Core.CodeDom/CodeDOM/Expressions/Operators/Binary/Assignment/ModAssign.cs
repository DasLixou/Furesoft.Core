﻿using Furesoft.Core.CodeDom.Parsing;
using Furesoft.Core.CodeDom.CodeDOM.Base;

namespace Nova.CodeDOM
{
    /// <summary>
    /// Gets the remainder of the division of one <see cref="Expression"/> by another, and assigns it to the left <see cref="Expression"/>.
    /// The left <see cref="Expression"/> must be an assignable object ("lvalue").
    /// </summary>
    public class ModAssign : Assignment
    {
        /// <summary>
        /// The token used to parse the code object.
        /// </summary>
        public new const string ParseToken = "%=";

        /// <summary>
        /// Create a <see cref="ModAssign"/> operator.
        /// </summary>
        public ModAssign(Expression left, Expression right)
            : base(left, right)
        { }

        protected ModAssign(Parser parser, CodeObject parent)
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
        /// Parse a <see cref="ModAssign"/> operator.
        /// </summary>
        public static new ModAssign Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            return new ModAssign(parser, parent);
        }

        /// <summary>
        /// The internal name of the <see cref="BinaryOperator"/>.
        /// </summary>
        public override string GetInternalName()
        {
            return Mod.InternalName;
        }

        internal static new void AddParsePoints()
        {
            Parser.AddOperatorParsePoint(ParseToken, Precedence, LeftAssociative, false, Parse);
        }
    }
}