﻿// The Furesoft.Core.CodeDom Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

using Furesoft.Core.CodeDom.Parsing;

namespace Furesoft.Core.CodeDom.CodeDOM
{
    /// <summary>
    /// The common base class of <see cref="YieldReturn"/> and <see cref="YieldBreak"/>.
    /// </summary>
    public abstract class YieldStatement : Statement
    {
        protected YieldStatement()
        { }

        /// <summary>
        /// The token used to parse yield statements.
        /// </summary>
        public const string ParseToken1 = "yield";

        protected YieldStatement(Parser parser, CodeObject parent)
            : base(parser, parent)
        { }

        /// <summary>
        /// Parse a <see cref="YieldBreak"/> or <see cref="YieldReturn"/>.
        /// </summary>
        public static YieldStatement Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            // Only parse 'yield' if it's followed by 'break' or 'return'
            string nextTokenText = parser.PeekNextTokenText();
            if (nextTokenText == YieldBreak.ParseToken2)
                return new YieldBreak(parser, parent);
            if (nextTokenText == YieldReturn.ParseToken2)
                return new YieldReturn(parser, parent);
            return null;
        }

        internal static void AddParsePoints()
        {
            // Set parse-point on 1st of the 2 keywords
            Parser.AddParsePoint(ParseToken1, Parse, typeof(IBlock));
        }

        /// <summary>
        /// True if the <see cref="Statement"/> has a terminator character by default.
        /// </summary>
        public override bool HasTerminatorDefault
        {
            get { return true; }
        }
    }
}