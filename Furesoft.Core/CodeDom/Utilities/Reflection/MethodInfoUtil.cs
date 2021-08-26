﻿// The Nova Project by Ken Beckett.
// Copyright (C) 2007-2012 Inevitable Software, all rights reserved.
// Released under the Common Development and Distribution License, CDDL-1.0: http://opensource.org/licenses/cddl1.php

using System;
using System.Linq;
using System.Reflection;

namespace Nova.Utilities
{
    /// <summary>
    /// Static helper methods for <see cref="MethodInfo"/>.
    /// </summary>
    public static class MethodInfoUtil
    {
        /// <summary>
        /// Find the base virtual method for this method if it's an override.
        /// </summary>
        public static MethodInfo FindBaseMethod(MethodInfo methodInfo)
        {
            if (methodInfo.IsVirtual)  // Can't distinguish 'override', but 'virtual' should be true?
            {
                var declaringType = methodInfo.DeclaringType;
                if (declaringType != null)
                {
                    var parameterTypes = Enumerable.ToArray(Enumerable.Select<ParameterInfo, Type>(methodInfo.GetParameters(), delegate (ParameterInfo parameterInfo) { return parameterInfo.ParameterType; }));
                    var baseType = declaringType.BaseType;
                    while (baseType != null)
                    {
                        var baseMethodInfo = baseType.GetMethod(methodInfo.Name, parameterTypes);
                        if (baseMethodInfo != null)
                            return baseMethodInfo;
                        baseType = baseType.BaseType;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find the type argument for the specified type parameter.
        /// </summary>
        public static Type FindTypeArgument(MethodInfo methodInfo, Type typeParameter)
        {
            if (methodInfo.IsGenericMethod)
            {
                foreach (var genericParameter in methodInfo.GetGenericMethodDefinition().GetGenericArguments())
                {
                    if (genericParameter == typeParameter)
                        return methodInfo.GetGenericArguments()[genericParameter.GenericParameterPosition];
                }
            }
            return null;
        }

        /// <summary>
        /// Find the index of the specified type parameter.
        /// </summary>
        public static int FindTypeParameterIndex(MethodInfo methodInfo, Type typeParameter)
        {
            if (methodInfo.IsGenericMethod)
            {
                foreach (var genericParameter in methodInfo.GetGenericMethodDefinition().GetGenericArguments())
                {
                    if (genericParameter == typeParameter)
                        return genericParameter.GenericParameterPosition;
                }
            }
            return -1;
        }

        /// <summary>
        /// Get the parameter with the specified name.
        /// </summary>
        public static ParameterInfo GetParameter(MethodBase methodBase, string name)
        {
            foreach (var parameterInfo in methodBase.GetParameters())
            {
                if (parameterInfo.Name == name)
                    return parameterInfo;
            }
            return null;
        }

        /// <summary>
        /// Get the type parameter at the specified index.
        /// </summary>
        public static Type GetTypeParameter(MethodInfo methodInfo, int index)
        {
            if (methodInfo.IsGenericMethod)
            {
                var typeParameters = methodInfo.GetGenericMethodDefinition().GetGenericArguments();
                if (index >= 0 && index < typeParameters.Length)
                    return typeParameters[index];
            }
            return null;
        }

        /// <summary>
        /// Check if the method is an override.
        /// </summary>
        public static bool IsOverride(MethodInfo methodInfo)
        {
            return (methodInfo.GetBaseDefinition() != methodInfo);
        }
    }
}