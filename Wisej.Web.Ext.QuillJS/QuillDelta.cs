using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wisej.Core;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Represents a Delta object that describes content and formatting changes in the QuillJS editor.
	/// </summary>
	public class QuillDelta : IWisejSerializable
	{
		/// <summary>
		/// Creates a new empty Delta.
		/// </summary>
		public QuillDelta() { }

		/// <summary>
		/// Creates a Delta with initial operations.
		/// </summary>
		public QuillDelta(IEnumerable<QuillOperation> operations)
		{
			Operations.AddRange(operations);
		}

		/// <summary>
		/// Gets or sets the operations in this delta.
		/// </summary>
		public List<QuillOperation> Operations { get; } = new List<QuillOperation>();

		/// <summary>
		/// Creates a Delta from a JSON string.
		/// </summary>
		internal static QuillDelta Parse(dynamic ops)
		{
			var delta = new QuillDelta();

			foreach (var op in ops)
			{
				var operation = new QuillOperation();

				if (op.insert != null)
					operation.Insert = op.insert;

				if (op.delete != null)
					operation.Delete = op.delete;

				if (op.retain != null)
					operation.Retain = op.retain;

				if (op.attributes != null)
				{
					operation.Attributes = new Dictionary<string, object>();

					foreach (var attr in op.attributes)
					{
						operation.Attributes[attr.Name] = attr.Value;
					}
				}

				delta.Operations.Add(operation);
			}

			return delta;
		}

		/// <summary>
		/// Gets the plain text content of this Delta.
		/// </summary>
		public string GetText()
		{
			return string.Join("", Operations
				.Where(op => op.Insert != null)
				.Select(op => op.Insert));
		}

		/// <summary>
		/// Gets the length of the content in this Delta.
		/// </summary>
		public int Length()
		{
			return Operations.Sum(op =>
			{
				if (op.Insert != null)
					return op.Insert.Length;
				if (op.Delete.HasValue)
					return 0;
				if (op.Retain.HasValue)
					return op.Retain.Value;
				return 0;
			});
		}

		/// <summary>
		/// Slices the Delta from start to end.
		/// </summary>
		public QuillDelta Slice(int start = 0, int? end = null)
		{
			var ops = new List<QuillOperation>();
			var index = 0;
			var remaining = Operations.ToList();

			// skip operations before start
			while (index < start && remaining.Any())
			{
				var nextOp = remaining[0];
				var length = nextOp.Length();
				if (index + length <= start)
				{
					index += length;
					remaining.RemoveAt(0);
				}
				else
				{
					var offset = start - index;
					remaining[0] = nextOp.Slice(offset);
					index = start;
				}
			}

			// add operations within range
			if (end == null)
			{
				ops.AddRange(remaining);
			}
			else
			{
				while (index < end && remaining.Any())
				{
					var nextOp = remaining[0];
					var length = nextOp.Length();
					if (index + length <= end)
					{
						index += length;
						ops.Add(nextOp);
						remaining.RemoveAt(0);
					}
					else
					{
						var offset = (int)end - index;
						ops.Add(nextOp.Slice(0, offset));
						index = (int)end;
					}
				}
			}

			return new QuillDelta(ops);
		}

		/// <summary>
		/// Composes this Delta with another Delta.
		/// </summary>
		public QuillDelta Compose(QuillDelta other)
		{
			var thisIndex = 0;
			var otherIndex = 0;
			var ops = new List<QuillOperation>();

			while (thisIndex < Operations.Count && otherIndex < other.Operations.Count)
			{
				var thisOp = Operations[thisIndex];
				var otherOp = other.Operations[otherIndex];

				if (otherOp.Delete.HasValue)
				{
					ops.Add(otherOp);
					thisIndex++;
				}
				else if (thisOp.Delete.HasValue)
				{
					ops.Add(thisOp);
					otherIndex++;
				}
				else
				{
					var minLength = Math.Min(thisOp.Length(), otherOp.Length());
					var newOp = new QuillOperation
					{
						Insert = otherOp.Insert ?? thisOp.Insert,
						Attributes = MergeAttributes(thisOp.Attributes, otherOp.Attributes)
					};
					ops.Add(newOp);

					thisIndex += minLength >= thisOp.Length() ? 1 : 0;
					otherIndex += minLength >= otherOp.Length() ? 1 : 0;
				}
			}

			// add remaining operations
			ops.AddRange(Operations.Skip(thisIndex));
			ops.AddRange(other.Operations.Skip(otherIndex));

			return new QuillDelta(ops);
		}

		/// <summary>
		/// Inserts text at the current position.
		/// </summary>
		/// <param name="text">The text to insert.</param>
		public void Insert(string text)
		{
			Operations.Add(new QuillOperation { Insert = text });
		}

		/// <summary>
		/// Inserts text with specified attributes at the current position.
		/// </summary>
		/// <param name="text">The text to insert.</param>
		/// <param name="attributes">The formatting attributes to apply to the inserted text.</param>
		public void Insert(string text, Dictionary<string, object> attributes)
		{
			Operations.Add(new QuillOperation { Insert = text, Attributes = attributes });
		}

		/// <summary>
		/// Retains a specified number of characters without modifying them.
		/// </summary>
		/// <param name="length">The number of characters to retain.</param>
		public void Retain(int length)
		{
			if (length <= 0)
				throw new ArgumentException("Length must be greater than 0", nameof(length));

			Operations.Add(new QuillOperation { Retain = length });
		}

		/// <summary>
		/// Retains a specified number of characters and applies attributes to them.
		/// </summary>
		/// <param name="length">The number of characters to retain.</param>
		/// <param name="attributes">The attributes to apply to the retained characters.</param>
		public void Retain(int length, Dictionary<string, object> attributes)
		{
			if (length <= 0)
				throw new ArgumentException("Length must be greater than 0", nameof(length));
			if (attributes == null)
				throw new ArgumentNullException(nameof(attributes));

			Operations.Add(new QuillOperation { Retain = length, Attributes = attributes });
		}

		private static Dictionary<string, object> MergeAttributes(Dictionary<string, object> a, Dictionary<string, object> b)
		{
			if (a == null) return b;
			if (b == null) return a;

			var merged = new Dictionary<string, object>(a);
			foreach (var kvp in b)
			{
				merged[kvp.Key] = kvp.Value;
			}
			return merged;
		}

		#region IWisejSerializable

		bool IWisejSerializable.Serialize(TextWriter writer, WisejSerializerOptions options)
		{
			options |= WisejSerializerOptions.IgnoreNulls;
			writer.Write("{\"ops\":");
			writer.Write(JSON.Stringify(this.Operations, options));
			writer.Write("}");

			return true;
		}

		#endregion
	}

	/// <summary>
	/// Represents a single operation in a Delta.
	/// </summary>
	public class QuillOperation
	{
		/// <summary>
		/// Gets or sets the text to insert.
		/// </summary>
		public string Insert { get; set; }

		/// <summary>
		/// Gets or sets the number of characters to delete.
		/// </summary>
		public int? Delete { get; set; }

		/// <summary>
		/// Gets or sets the number of characters to retain.
		/// </summary>
		public int? Retain { get; set; }

		/// <summary>
		/// Gets or sets the formatting attributes.
		/// </summary>
		public Dictionary<string, object> Attributes { get; set; }

		/// <summary>
		/// Gets the length of this operation.
		/// </summary>
		public int Length()
		{
			if (Insert != null)
				return Insert.Length;
			if (Delete.HasValue)
				return Delete.Value;
			if (Retain.HasValue)
				return Retain.Value;

			return 0;
		}

		/// <summary>
		/// Creates a slice of this operation.
		/// </summary>
		public QuillOperation Slice(int start = 0, int? end = null)
		{
			end = end ?? Length();
			var slicedOp = new QuillOperation
			{
				Attributes = Attributes != null ? new Dictionary<string, object>(Attributes) : null
			};

			if (Insert != null)
			{
				slicedOp.Insert = Insert.Substring(start, (int)end - start);
			}
			else if (Delete.HasValue)
			{
				slicedOp.Delete = end - start;
			}
			else if (Retain.HasValue)
			{
				slicedOp.Retain = end - start;
			}

			return slicedOp;
		}
	}
}
