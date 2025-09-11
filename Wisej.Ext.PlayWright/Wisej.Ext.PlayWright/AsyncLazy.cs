///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System.Runtime.CompilerServices;

namespace Wisej.Ext.PlayWright;

// <summary>
/// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
/// </summary>
/// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
public sealed class AsyncLazy<T>
{
	/// <summary>
	/// The underlying lazy task.
	/// </summary>
	private readonly Lazy<Task<T>> instance;

	/// <summary>
	/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;" /> class.
	/// </summary>
	/// <param name="factory">The delegate that is invoked on a background thread to produce the ValueAsync when it is needed.</param>
	public AsyncLazy(Func<T> factory)
	{
		this.instance = new Lazy<Task<T>>(() => Task.Run(factory));
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;" /> class.
	/// </summary>
	/// <param name="factory">
	/// The asynchronous delegate that is invoked on a background thread to produce the ValueAsync when
	/// it is needed.
	/// </param>
	public AsyncLazy(Func<Task<T>> factory)
	{
		this.instance = new Lazy<Task<T>>(() => Task.Run(factory));
	}

	/// <summary>
	/// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;" /> to be await'ed.
	/// </summary>
	public TaskAwaiter<T> GetAwaiter()
	{
		return this.instance.Value.GetAwaiter();
	}

	/// <summary>
	/// Starts the asynchronous initialization, if it has not already started.
	/// </summary>
	public void Start()
	{
		var unused = this.instance.Value;
	}
}