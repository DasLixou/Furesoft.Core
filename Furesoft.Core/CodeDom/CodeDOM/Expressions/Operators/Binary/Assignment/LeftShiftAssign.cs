﻿// The Nova Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

using Nova.Parsing;

namespace Nova.CodeDOM
{
    /// <summary>
    /// Shifts the value of the left <see cref="Expression"/> LEFT by the value of the right <see cref="Expression"/>, and assigns the
    /// result to the left <see cref="Expression"/>.  The left <see cref="Expression"/> must be an assignable object ("lvalue").
    /// </summary>
    public class LeftShiftAssign : Assignment
    {
        #region /* CONSTRUCTORS */

        /// <summary>
        /// Create a <see cref="LeftShiftAssign"/> operator.
        /// </summary>
        public LeftShiftAssign(Expression left, Expression right)
            : base(left, right)
        { }

        #endregion

        #region /* PROPERTIES */

        /// <summary>
        /// The symbol associated with the operator.
        /// </summary>
        public override string Symbol
        {
            get { return ParseToken; }
        }

        #endregion

        #region /* METHODS */

        /// <summary>
        /// The internal name of the <see cref="BinaryOperator"/>.
        /// </summary>
        public override string GetInternalName()
        {
            return LeftShift.InternalName;
        }

        #endregion

        #region /* PARSING */

        /// <summary>
        /// The token used to parse the code object.
        /// </summary>
        public new const string ParseToken = "<<=";

        internal static new void AddParsePoints()
        {
            Parser.AddOperatorParsePoint(ParseToken, Precedence, LeftAssociative, false, Parse);
        }

        /// <summary>
        /// Parse a <see cref="LeftShiftAssign"/> operator.
        /// </summary>
        public static new LeftShiftAssign Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            return new LeftShiftAssign(parser, parent);
        }

        protected LeftShiftAssign(Parser parser, CodeObject parent)
            : base(parser, parent)
        { }

        #endregion
    }
}
