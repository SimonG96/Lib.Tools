// Author: Simon.Gockner
// Created: 2024-03-26
// Copyright(c) 2024 SimonG. All Rights Reserved.

using System.Reflection;

namespace Lib.Tools.Reflection;

/// <summary>
/// Helper class to call a generic method without generic type parameters 
/// </summary>
public static class GenericMethodCaller
{
    /// <summary>
    /// Call a generic method without generic type parameters
    /// </summary>
    /// <param name="caller">The caller of the method</param>
    /// <param name="functionName">The name of the method to call</param>
    /// <param name="genericParameter">The generic parameter as <see cref="Type"/> parameter</param>
    /// <param name="bindingFlags">The <see cref="BindingFlags"/> to find the method</param>
    /// <param name="parameters">The parameters of the method</param>
    /// <returns>The result of invoking the method</returns>
    /// <exception cref="Exception">Could not find the generic method</exception>
    /// <exception cref="Exception">Any <see cref="Exception"/> thrown after invoking the generic method</exception>
    public static object? Call(object caller, string functionName, Type genericParameter, BindingFlags bindingFlags, params object?[] parameters)
    {
        MethodInfo? method = caller.GetType().GetMethod(functionName, bindingFlags);
        MethodInfo? genericMethod = method?.MakeGenericMethod(genericParameter);

        if (genericMethod == null)
            throw new Exception(functionName);

        try //exceptions thrown by methods called with invoke are wrapped into another exception, the exception thrown by the invoked method can be returned by `Exception.GetBaseException()`
        {
            return genericMethod.Invoke(caller, parameters);
        }
        catch (Exception ex)
        {
            throw ex.GetBaseException();
        }
    }

    /// <summary>
    /// Call a private generic method without generic type parameters
    /// </summary>
    /// <param name="caller">The caller of the method</param>
    /// <param name="functionName">The name of the method to call</param>
    /// <param name="genericParameter">The generic parameter as <see cref="Type"/> parameter</param>
    /// <param name="parameters">The parameters of the method</param>
    /// <returns>The result of invoking the method</returns>
    /// <exception cref="Exception">Could not find the generic method</exception>
    /// <exception cref="Exception">Any <see cref="Exception"/> thrown after invoking the generic method</exception>
    public static object? CallPrivate(object caller, string functionName, Type genericParameter, params object?[] parameters) =>
        Call(caller, functionName, genericParameter, BindingFlags.NonPublic | BindingFlags.Instance, parameters);
}