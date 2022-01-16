using System;
using System.Collections.Generic;
using OpCode = Mono.Cecil.Cil.OpCode;
using CilInstruction = Mono.Cecil.Cil.Instruction;
using Furesoft.Core.CodeDom.Compiler;

namespace Furesoft.Core.CodeDom.Backends.CLR.Emit
{
    /// <summary>
    /// An instruction type for CIL instruction selection.
    /// </summary>
    internal abstract class CilCodegenInstruction
    {
        internal CilCodegenInstruction()
        { }
    }

    /// <summary>
    /// An actual CIL instruction that is emitted as-is.
    /// </summary>
    internal sealed class CilOpInstruction : CilCodegenInstruction
    {
        /// <summary>
        /// Creates a CIL instruction that is emitted as-is.
        /// </summary>
        /// <param name="op">The opcode to emit.</param>
        public CilOpInstruction(OpCode op)
            : this(CilInstruction.Create(op), null)
        { }

        /// <summary>
        /// Creates a CIL instruction that is emitted as-is.
        /// </summary>
        /// <param name="op">The CIL instruction to emit.</param>
        public CilOpInstruction(CilInstruction op)
            : this(op, null)
        { }

        /// <summary>
        /// Creates a CIL instruction that is emitted and patched
        /// afterward.
        /// </summary>
        /// <param name="op">The CIL instruction to emit.</param>
        /// <param name="patch">
        /// An action that patches the instruction.
        /// </param>
        public CilOpInstruction(
            CilInstruction op,
            Action<CilInstruction, IReadOnlyDictionary<BasicBlockTag, CilInstruction>> patch)
        {
            this.Op = op;
            this.Patch = patch;
        }

        /// <summary>
        /// Gets the operation encapsulated by this instruction.
        /// </summary>
        /// <value>A CIL instruction.</value>
        public CilInstruction Op { get; private set; }

        /// <summary>
        /// Gets an optional action that patches this instruction
        /// based on a branch target to instruction mapping.
        /// </summary>
        /// <value>
        /// An action or <c>null</c>.
        /// </value>
        public Action<CilInstruction, IReadOnlyDictionary<BasicBlockTag, CilInstruction>> Patch { get; private set; }
    }

    /// <summary>
    /// A codegen instruction that marks the start of a try block.
    /// </summary>
    internal sealed class CilTryStartMarker : CilCodegenInstruction
    {
        private CilTryStartMarker()
        { }

        public static readonly CilTryStartMarker Instance =
            new();
    }

    /// <summary>
    /// A codegen instruction that marks the point at which a try block
    /// transitions to its handler.
    /// </summary>
    internal sealed class CilHandlerStartMarker : CilCodegenInstruction
    {
        public CilHandlerStartMarker(Mono.Cecil.Cil.ExceptionHandler handler)
        {
            this.Handler = handler;
        }

        /// <summary>
        /// Gets the exception handler.
        /// </summary>
        /// <value>An exception handler.</value>
        public Mono.Cecil.Cil.ExceptionHandler Handler { get; private set; }
    }

    /// <summary>
    /// A codegen instruction that marks the end of an exception handler.
    /// </summary>
    internal sealed class CilHandlerEndMarker : CilCodegenInstruction
    {
        private CilHandlerEndMarker()
        { }

        public static readonly CilHandlerEndMarker Instance =
            new();
    }

    /// <summary>
    /// An instruction that marks an instruction as a branch target.
    /// </summary>
    internal sealed class CilMarkTargetInstruction : CilCodegenInstruction
    {
        /// <summary>
        /// Creates an instruction that marks a branch target.
        /// </summary>
        /// <param name="target">The branch target to mark.</param>
        public CilMarkTargetInstruction(BasicBlockTag target)
        {
            this.Target = target;
        }

        /// <summary>
        /// Gets the tag marked by this instruction.
        /// </summary>
        /// <value>A basic block tag.</value>
        public BasicBlockTag Target { get; private set; }
    }

    /// <summary>
    /// An instruction that reads from a virtual register.
    /// </summary>
    internal sealed class CilLoadRegisterInstruction : CilCodegenInstruction
    {
        /// <summary>
        /// Creates an instruction that reads from a virtual register.
        /// </summary>
        /// <param name="value">The value to load.</param>
        public CilLoadRegisterInstruction(ValueTag value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value loaded by this instruction.
        /// </summary>
        /// <value>A value tag.</value>
        public ValueTag Value { get; private set; }
    }

    /// <summary>
    /// An instruction that writes to a virtual register.
    /// </summary>
    internal sealed class CilStoreRegisterInstruction : CilCodegenInstruction
    {
        /// <summary>
        /// Creates an instruction that writes to a virtual register.
        /// </summary>
        /// <param name="value">The value to write to.</param>
        public CilStoreRegisterInstruction(ValueTag value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value written to by this instruction.
        /// </summary>
        /// <value>A value tag.</value>
        public ValueTag Value { get; private set; }
    }

    /// <summary>
    /// An instruction that computes the address of a virtual register.
    /// </summary>
    internal sealed class CilAddressOfRegisterInstruction : CilCodegenInstruction
    {
        /// <summary>
        /// Creates an instruction that computes the address of a virtual register.
        /// </summary>
        /// <param name="value">The virtual register.</param>
        public CilAddressOfRegisterInstruction(ValueTag value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the virtual register manipulated by this instruction.
        /// </summary>
        /// <value>A value tag.</value>
        public ValueTag Value { get; private set; }
    }
}
