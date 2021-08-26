﻿// The Furesoft.Core.CodeDom Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

namespace Furesoft.Core.CodeDom.Parsing
{
    /// <summary>
    /// The common base class of <see cref="Token"/> and <see cref="UnusedCodeObject"/>.
    /// </summary>
    public abstract class ParsedObject
    {
        /// <summary>
        /// True if there are any trailing comments.
        /// </summary>
        public abstract bool HasTrailingComments { get; }

        /// <summary>
        /// True if inside a documentation comment.
        /// </summary>
        public abstract bool InDocComment { get; }

        /// <summary>
        /// Get the <see cref="ParsedObject"/> as a <see cref="Token"/>.
        /// </summary>
        public abstract Token AsToken();
    }
}