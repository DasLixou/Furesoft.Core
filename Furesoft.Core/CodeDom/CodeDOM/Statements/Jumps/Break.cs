﻿// The Furesoft.Core.CodeDom Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

using Furesoft.Core.CodeDom.Parsing;

namespace Furesoft.Core.CodeDom.CodeDOM
{
    /// <summary>
    /// Causes execution of the active loop or switch/case block to terminate.
    /// </summary>
    public class Break : Statement
    {
        /// <summary>
        /// Create a <see cref="Break"/>.
        /// </summary>
        public Break()
        { }

        /// <summary>
        /// The keyword associated with the <see cref="Statement"/>.
        /// </summary>
        public override string Keyword
        {
            get { return ParseToken; }
        }

        /// <summary>
        /// The token used to parse the code object.
        /// </summary>
        public const string ParseToken = "break";

        protected Break(Parser parser, CodeObject parent)
            : base(parser, parent)
        {
            parser.NextToken();  // Move past 'break'
            ParseTerminator(parser);
        }

        /// <summary>
        /// Pase a <see cref="Break"/>.
        /// </summary>
        public static Break Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            return new Break(parser, parent);
        }

        internal static void AddParsePoints()
        {
            Parser.AddParsePoint(ParseToken, Parse, typeof(IBlock));
        }

        /// <summary>
        /// True if the <see cref="Statement"/> has an argument.
        /// </summary>
        public override bool HasArgument
        {
            get { return false; }
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