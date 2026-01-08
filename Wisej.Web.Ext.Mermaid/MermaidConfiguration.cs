using System.Collections.Generic;
using Wisej.Core;

namespace Wisej.Web.Ext.Mermaid
{
	public enum MermaidSecurityLevel
	{
		Strict,
		Loose,
		Antiscript,
		Sandbox
	}

	public enum MermaidLogLevel
	{
		Trace,
		Debug,
		Info,
		Warn,
		Error,
		Fatal
	}

	public class MermaidConfiguration
	{
		public string Theme { get; set; }

		public Dictionary<string, object> ThemeVariables { get; set; }

		public MermaidSecurityLevel? SecurityLevel { get; set; }

		public MermaidLogLevel? LogLevel { get; set; }

		public bool? DeterministicIds { get; set; }

		public int? MaxTextSize { get; set; }

		public string FontFamily { get; set; }

		public bool? StartOnLoad { get; set; }

		public MermaidFlowchartConfiguration Flowchart { get; set; }

		public MermaidSequenceConfiguration Sequence { get; set; }

		internal dynamic ToOptions()
		{
			dynamic options = new DynamicObject();

			if (!string.IsNullOrWhiteSpace(this.Theme))
				options.theme = this.Theme;

			if (this.ThemeVariables != null && this.ThemeVariables.Count > 0)
				options.themeVariables = this.ThemeVariables;

			if (this.SecurityLevel.HasValue)
				options.securityLevel = this.SecurityLevel.Value.ToString().ToLowerInvariant();

			if (this.LogLevel.HasValue)
				options.logLevel = this.LogLevel.Value.ToString().ToLowerInvariant();

			if (this.DeterministicIds.HasValue)
				options.deterministicIds = this.DeterministicIds.Value;

			if (this.MaxTextSize.HasValue)
				options.maxTextSize = this.MaxTextSize.Value;

			if (!string.IsNullOrWhiteSpace(this.FontFamily))
				options.fontFamily = this.FontFamily;

			if (this.StartOnLoad.HasValue)
				options.startOnLoad = this.StartOnLoad.Value;

			if (this.Flowchart != null)
			{
				var flowchart = this.Flowchart.ToOptions();
				if (flowchart != null)
					options.flowchart = flowchart;
			}

			if (this.Sequence != null)
			{
				var sequence = this.Sequence.ToOptions();
				if (sequence != null)
					options.sequence = sequence;
			}

			return options;
		}
	}

	public class MermaidFlowchartConfiguration
	{
		public bool? UseMaxWidth { get; set; }

		public bool? HtmlLabels { get; set; }

		internal dynamic ToOptions()
		{
			if (!this.UseMaxWidth.HasValue && !this.HtmlLabels.HasValue)
				return null;

			dynamic options = new DynamicObject();

			if (this.UseMaxWidth.HasValue)
				options.useMaxWidth = this.UseMaxWidth.Value;

			if (this.HtmlLabels.HasValue)
				options.htmlLabels = this.HtmlLabels.Value;

			return options;
		}
	}

	public class MermaidSequenceConfiguration
	{
		public bool? ShowSequenceNumbers { get; set; }

		internal dynamic ToOptions()
		{
			if (!this.ShowSequenceNumbers.HasValue)
				return null;

			dynamic options = new DynamicObject();
			options.showSequenceNumbers = this.ShowSequenceNumbers.Value;
			return options;
		}
	}
}
