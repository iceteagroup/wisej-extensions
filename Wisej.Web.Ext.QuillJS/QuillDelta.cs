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
	/// <remarks>
	/// A delta is a list of <see cref="QuillOperation"/> items (insert, retain or delete). A delta that contains only
	/// insert operations describes a whole document and can be loaded with <see cref="QuillJSEditor.SetDeltaAsync"/>;
	/// a delta that also contains retain and delete operations describes a change and can be applied with
	/// <see cref="QuillJSEditor.UpdateContent"/>. The delta is serialized to the client as <c>{"ops":[...]}</c>.
	/// </remarks>
	public class QuillDelta : IWisejSerializable
	{
		/// <summary>
		/// Initializes a new empty instance of the <see cref="QuillDelta"/> class.
		/// </summary>
		public QuillDelta() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="QuillDelta"/> class with the specified operations.
		/// </summary>
		/// <param name="operations">The initial operations to add to the delta.</param>
		public QuillDelta(IEnumerable<QuillOperation> operations)
		{
			Operations.AddRange(operations);
		}

		/// <summary>
		/// Returns the list of operations in this delta.
		/// </summary>
		/// <remarks>
		/// The list can be modified directly or through <see cref="Insert(string)"/> and <see cref="Retain(int)"/>.
		/// </remarks>
		/// <example>
		/// Adding a delete operation, which doesn't have a helper method:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Retain(10);
		/// delta.Operations.Add(new QuillOperation { Delete = 5 });
		/// this.quillJSEditor1.UpdateContent(delta);
		/// ]]></code>
		/// </example>
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
		/// Returns the plain text content of this Delta.
		/// </summary>
		/// <returns>The concatenation of the text of all the insert operations.</returns>
		/// <example>
		/// Reading the plain text of the editor content:
		/// <code><![CDATA[
		/// var delta = await this.quillJSEditor1.GetDeltaAsync();
		/// AlertBox.Show(delta.GetText());
		/// ]]></code>
		/// </example>
		public string GetText()
		{
			return string.Join("", Operations
				.Where(op => op.Insert != null)
				.Select(op => op.Insert));
		}

		/// <summary>
		/// Returns the length of the content in this Delta.
		/// </summary>
		/// <returns>The sum of the lengths of the insert and retain operations; delete operations count as 0.</returns>
		/// <example>
		/// Checking the number of characters in the editor:
		/// <code><![CDATA[
		/// var delta = await this.quillJSEditor1.GetDeltaAsync();
		/// if (delta.Length() > 5000)
		///     AlertBox.Show("The text is too long.");
		/// ]]></code>
		/// </example>
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
		/// Returns a new Delta containing the operations between the <paramref name="start"/> and <paramref name="end"/> positions.
		/// </summary>
		/// <param name="start">The zero-based starting position. The default is 0.</param>
		/// <param name="end">The zero-based position where the slice ends (exclusive), or null to slice to the end.</param>
		/// <returns>A new <see cref="QuillDelta"/>; operations that span the boundaries are split.</returns>
		/// <example>
		/// Extracting the first 100 characters of the content with their formatting:
		/// <code><![CDATA[
		/// var delta = await this.quillJSEditor1.GetDeltaAsync();
		/// var preview = delta.Slice(0, 100);
		/// ]]></code>
		/// </example>
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
		/// <param name="other">The delta to compose with this delta.</param>
		/// <returns>A new <see cref="QuillDelta"/> combining the operations of both deltas; this instance is not modified.</returns>
		/// <remarks>
		/// This is a simplified implementation that pairs whole operations and merges their attributes; it doesn't split
		/// operations of different lengths like the QuillJS <c>compose()</c> function does.
		/// </remarks>
		/// <example>
		/// Combining a document with a change that makes the first operation bold:
		/// <code><![CDATA[
		/// var document = new QuillDelta();
		/// document.Insert("Hello");
		/// document.Insert(" World\n");
		///
		/// var bold = new QuillDelta();
		/// bold.Retain(5, new Dictionary<string, object> { { "bold", true } });
		///
		/// var result = document.Compose(bold);
		/// ]]></code>
		/// </example>
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
		/// Adds an insert operation to this delta.
		/// </summary>
		/// <param name="text">The text to insert.</param>
		/// <remarks>
		/// The text is inserted at the position reached by the preceding operations. A document delta
		/// should end with a new line character ("\n").
		/// </remarks>
		/// <example>
		/// Loading a simple document into the editor:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Insert("Meeting notes\n");
		/// await this.quillJSEditor1.SetDeltaAsync(delta);
		/// ]]></code>
		/// </example>
		public void Insert(string text)
		{
			Operations.Add(new QuillOperation { Insert = text });
		}

		/// <summary>
		/// Adds an insert operation with the specified formatting attributes to this delta.
		/// </summary>
		/// <param name="text">The text to insert.</param>
		/// <param name="attributes">The formatting attributes to apply to the inserted text, i.e. "bold" = true, or null.</param>
		/// <remarks>
		/// Attribute names are the QuillJS format names listed in <see cref="QuillFormat.Formats"/>. Line formats
		/// (i.e. "header", "list", "align") must be applied to the new line character that ends the line.
		/// </remarks>
		/// <example>
		/// Building a document with a header and bold text:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Insert("Report");
		/// delta.Insert("\n", new Dictionary<string, object> { { QuillFormat.Formats.Header, 1 } });
		/// delta.Insert("Total: ");
		/// delta.Insert("1,250", new Dictionary<string, object> { { QuillFormat.Formats.Bold, true } });
		/// delta.Insert("\n");
		/// await this.quillJSEditor1.SetDeltaAsync(delta);
		/// ]]></code>
		/// </example>
		public void Insert(string text, Dictionary<string, object> attributes)
		{
			Operations.Add(new QuillOperation { Insert = text, Attributes = attributes });
		}

		/// <summary>
		/// Adds a retain operation that skips the specified number of characters without modifying them.
		/// </summary>
		/// <param name="length">The number of characters to retain.</param>
		/// <exception cref="ArgumentException"><paramref name="length"/> is less than or equal to 0.</exception>
		/// <example>
		/// Inserting text after the first 10 characters of the editor content:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Retain(10);
		/// delta.Insert("inserted text ");
		/// this.quillJSEditor1.UpdateContent(delta);
		/// ]]></code>
		/// </example>
		public void Retain(int length)
		{
			if (length <= 0)
				throw new ArgumentException("Length must be greater than 0", nameof(length));

			Operations.Add(new QuillOperation { Retain = length });
		}

		/// <summary>
		/// Adds a retain operation that applies the specified formatting attributes to the retained characters.
		/// </summary>
		/// <param name="length">The number of characters to retain.</param>
		/// <param name="attributes">The attributes to apply to the retained characters. Use a null value to remove a format.</param>
		/// <exception cref="ArgumentException"><paramref name="length"/> is less than or equal to 0.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="attributes"/> is null.</exception>
		/// <example>
		/// Making the first 5 characters of the editor content bold:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Retain(5, new Dictionary<string, object> { { QuillFormat.Formats.Bold, true } });
		/// this.quillJSEditor1.UpdateContent(delta);
		/// ]]></code>
		/// </example>
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
	/// <remarks>
	/// An operation is either an insert (<see cref="Insert"/>), a delete (<see cref="Delete"/>) or a retain (<see cref="Retain"/>);
	/// only one of the three should be set. <see cref="Attributes"/> applies to insert and retain operations.
	/// </remarks>
	public class QuillOperation
	{
		/// <summary>
		/// Returns or sets the text to insert.
		/// </summary>
		public string Insert { get; set; }

		/// <summary>
		/// Returns or sets the number of characters to delete, or null if this is not a delete operation.
		/// </summary>
		public int? Delete { get; set; }

		/// <summary>
		/// Returns or sets the number of characters to retain, or null if this is not a retain operation.
		/// </summary>
		public int? Retain { get; set; }

		/// <summary>
		/// Returns or sets the formatting attributes, or null.
		/// </summary>
		/// <remarks>
		/// The keys are QuillJS format names (see <see cref="QuillFormat.Formats"/>) and the values are the format values,
		/// i.e. true for "bold", "#ff0000" for "color" or 2 for "header".
		/// </remarks>
		/// <example>
		/// Creating a red, bold insert operation:
		/// <code><![CDATA[
		/// var op = new QuillOperation
		/// {
		///     Insert = "Warning",
		///     Attributes = new Dictionary<string, object>
		///     {
		///         { QuillFormat.Formats.Bold, true },
		///         { QuillFormat.Formats.Color, "#ff0000" }
		///     }
		/// };
		/// ]]></code>
		/// </example>
		public Dictionary<string, object> Attributes { get; set; }

		/// <summary>
		/// Returns the length of this operation.
		/// </summary>
		/// <returns>The length of the inserted text, or the number of deleted or retained characters, or 0 if nothing is set.</returns>
		/// <example>
		/// Counting the characters inserted by a delta:
		/// <code><![CDATA[
		/// int inserted = delta.Operations.Where(op => op.Insert != null).Sum(op => op.Length());
		/// ]]></code>
		/// </example>
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
		/// <param name="start">The zero-based starting position within the operation. The default is 0.</param>
		/// <param name="end">The position where the slice ends (exclusive), or null to slice to the end of the operation.</param>
		/// <returns>A new <see cref="QuillOperation"/> of the same kind with a copy of the <see cref="Attributes"/>.</returns>
		/// <example>
		/// Taking the first 3 characters of an insert operation:
		/// <code><![CDATA[
		/// var op = new QuillOperation { Insert = "Hello" };
		/// var first = op.Slice(0, 3); // Insert = "Hel"
		/// ]]></code>
		/// </example>
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
