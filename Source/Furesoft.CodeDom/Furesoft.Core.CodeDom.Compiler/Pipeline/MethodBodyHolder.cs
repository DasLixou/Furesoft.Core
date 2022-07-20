using System;
using System.Collections.Concurrent;
using System.Threading;
using Furesoft.Core.CodeDom.Compiler.Core.TypeSystem;
using Furesoft.Core.CodeDom.Compiler.Core;

namespace Furesoft.Core.CodeDom.Compiler.Pipeline
{
    /// <summary>
    /// A container for a method body as it is being optimized.
    /// </summary>
    internal sealed class MethodBodyHolder : IDisposable
    {
        /// <summary>
        /// Creates a method body holder from an initial method body.
        /// </summary>
        /// <param name="initialBody">An initial method body.</param>
        public MethodBodyHolder(MethodBody initialBody)
        {
            readerWriterLock = new ReaderWriterLockSlim();
            currentBody = initialBody;
            specializationCache = new ConcurrentDictionary<IMethod, MethodBody>();
        }

        private ReaderWriterLockSlim readerWriterLock;

        private MethodBody currentBody;

        /// <summary>
        /// A dictionary that maps method specializations to their method bodies.
        /// These method bodies are generated by substituting type parameters in
        /// the current method body.
        /// </summary>
        private ConcurrentDictionary<IMethod, MethodBody> specializationCache;

        /// <summary>
        /// Gets or sets the method body.
        /// </summary>
        /// <returns>The method body.</returns>
        public MethodBody Body
        {
            get
            {
                MethodBody result;
                try
                {
                    readerWriterLock.EnterReadLock();
                    result = currentBody;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
                return result;
            }
            set
            {
                try
                {
                    readerWriterLock.EnterWriteLock();

                    // Update the method body.
                    currentBody = value;

                    // Clear the specialization cache.
                    specializationCache = new ConcurrentDictionary<IMethod, MethodBody>();
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Gets the method body for a specialization of the method to
        /// which this method body holder belongs.
        /// </summary>
        /// <param name="method">A method specialization.</param>
        /// <returns>The method body for the specialization.</returns>
        public MethodBody GetSpecializationBody(IMethod method)
        {
            if (method.GetRecursiveGenericDeclaration() == method)
            {
                return Body;
            }
            else
            {
                MethodBody result;
                try
                {
                    readerWriterLock.EnterReadLock();
                    result = specializationCache.GetOrAdd(method, GetActualSpecializationBody);
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
                return result;
            }
        }

        private MethodBody GetActualSpecializationBody(IMethod method)
        {
            var mapping = new TypeMappingVisitor(method.GetRecursiveGenericArgumentMapping());
            return currentBody.Map(new MemberMapping(mapping.Visit));
        }

        /// <summary>
        /// Disposes of this method body holder.
        /// </summary>
        public void Dispose()
        {
            readerWriterLock.Dispose();
            currentBody = null;
        }
    }
}
