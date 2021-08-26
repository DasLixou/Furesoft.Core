﻿// The Furesoft.Core.CodeDom Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

using Furesoft.Core.CodeDom.Parsing;
using Furesoft.Core.CodeDom.Rendering;

namespace Furesoft.Core.CodeDom.CodeDOM
{
    /// <summary>
    /// Used as a child of a <see cref="Switch"/>.  Includes a constant expression plus a body (a statement or block).
    /// </summary>
    public class Case : SwitchItem
    {
        protected Expression _constantExpression;

        /// <summary>
        /// Create a <see cref="Case"/>.
        /// </summary>
        public Case(Expression constant, CodeObject body)
            : base(body)
        {
            ConstantExpression = constant;
        }

        /// <summary>
        /// Create a <see cref="Case"/>.
        /// </summary>
        public Case(Expression constant)
            : base(null)
        {
            ConstantExpression = constant;
        }

        /// <summary>
        /// The constant <see cref="Expression"/> of the <see cref="Case"/>.
        /// </summary>
        public Expression ConstantExpression
        {
            get { return _constantExpression; }
            set { SetField(ref _constantExpression, value, true); }
        }

        /// <summary>
        /// The keyword associated with the <see cref="Statement"/>.
        /// </summary>
        public override string Keyword
        {
            get { return ParseToken; }
        }

        /// <summary>
        /// The name of the <see cref="Case"/>.
        /// </summary>
        public override string Name
        {
            get { return ParseToken + " " + (_constantExpression != null ? _constantExpression.AsString() : null); }
        }

        /// <summary>
        /// Deep-clone the code object.
        /// </summary>
        public override CodeObject Clone()
        {
            Case clone = (Case)base.Clone();
            clone.CloneField(ref clone._constantExpression, _constantExpression);
            return clone;
        }

        /// <summary>
        /// The token used to parse the code object.
        /// </summary>
        public const string ParseToken = "case";

        protected Case(Parser parser, CodeObject parent)
            : base(parser, parent)
        {
            parser.NextToken();  // Move past 'case'
            SetField(ref _constantExpression, Expression.Parse(parser, this, true, ParseTokenTerminator), false);
            ParseTerminatorAndBody(parser);  // Parse ':' and body (if any)
        }

        /// <summary>
        /// Parse a <see cref="Case"/>.
        /// </summary>
        public static Case Parse(Parser parser, CodeObject parent, ParseFlags flags)
        {
            return new Case(parser, parent);
        }

        internal static void AddParsePoints()
        {
            Parser.AddParsePoint(ParseToken, Parse, typeof(Switch));
        }

        /// <summary>
        /// True if the <see cref="Statement"/> has parens around its argument.
        /// </summary>
        public override bool HasArgumentParens
        {
            get { return false; }
        }

        /// <summary>
        /// Determines if the code object only requires a single line for display.
        /// </summary>
        public override bool IsSingleLine
        {
            get { return (base.IsSingleLine && (_constantExpression == null || (!_constantExpression.IsFirstOnLine && _constantExpression.IsSingleLine))); }
            set
            {
                base.IsSingleLine = value;
                if (value && _constantExpression != null)
                {
                    _constantExpression.IsFirstOnLine = false;
                    _constantExpression.IsSingleLine = true;
                }
            }
        }

        protected override void AsTextArgument(CodeWriter writer, RenderFlags flags)
        {
            if (_constantExpression != null)
                _constantExpression.AsText(writer, flags);
        }
    }
}