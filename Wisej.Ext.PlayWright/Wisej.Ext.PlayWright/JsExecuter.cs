namespace Wisej.Ext.PlayWright;

/// <summary>
/// JSExecutor class is used to Evaluate and Call JavaScript functions on a given control
/// </summary>
public static class JsExecuter
{
	static WisejWebDriver WebDriver => WisejWebDriver.Instance;

	private static string _namespace = "Wisej.WebDriver";

	//Executes javascript functions.
	private static async Task<T> ExecAsync<T>(string script, params object[] args)
	{
		var result = await WebDriver.Page.EvaluateAsync<T>(script, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public static async Task<T> EvalAsync<T>(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
		var result = await ExecAsync<T>(function, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="resourceName">Custom Resource Name</param>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public static async Task<T> EvalAsync<T>(string resourceName, string functionName, params object[] args)
	{
		var function = $"(arguments)=> {_namespace}.{resourceName}.{functionName}.apply(null,arguments)";
		var result = await ExecAsync<T>(function, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="resourceName">Custom Resource Name</param>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public static async Task<T> EvalAsync<T>(this IWidget widget, string functionName, params object[] args)
	{
		var typeName = widget.GetType().Name;

		var function = $"(arguments)=> {_namespace}.{typeName}.{functionName}.apply(null,arguments)";
		var result = await ExecAsync<T>(function, args);

		return result;
	}

	/// <summary>
	/// Evaluates a JavaScript function in an unrestricted context.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="functionName">Function name</param>
	/// <param name="args">Arguments</param>
	/// <returns></returns>
	public static async Task<T> EvalUnrestrictedAsync<T>(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
		var result = (T)await WebDriver.Page.EvaluateHandleAsync(function, args);

		return result;
	}

	public static async Task<T> EvalUnrestrictedAsync<T>(this IWidget widget,string functionName, params object[] args)
	{
		var typeName = widget.GetType().Name;

		var function = $"(arguments)=> {_namespace}.{typeName}.{functionName}.apply(null,arguments)";
		var result = (T)await WebDriver.Page.EvaluateHandleAsync(function, args);

		return result;
	}

	/// <summary>
	/// Calls a JavaScript function.
	/// </summary>
	/// <param name="functionName">Function Name</param>
	/// <param name="args">Args</param>
	public static async Task CallAsync(string functionName, params object[] args)
	{
		var function = $"(arguments)=> {_namespace}.{functionName}.apply(null,arguments)";
		await WebDriver.Page.EvaluateHandleAsync(function, args);
	}

	public static async Task CallAsync(this IWidget widget,string functionName, params object[] args)
	{
		var typeName = widget.GetType().Name;

		var function = $"(arguments)=> {_namespace}.{typeName}.apply(null,arguments)";

		await WebDriver.Page.EvaluateHandleAsync(function, args);
	}
}