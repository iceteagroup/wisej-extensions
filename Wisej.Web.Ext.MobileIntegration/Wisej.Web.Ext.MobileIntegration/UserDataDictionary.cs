///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Collections.Generic;
using System.ComponentModel;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents a <see cref="Dictionary{TKey, TValue}"/> for managing user data.
	/// </summary>
	/// <typeparam name="Tkey"></typeparam>
	/// <typeparam name="Tvalue"></typeparam>
	public class UserDataDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, INotifyPropertyChanged
	{
		#region Implementation

		/// <summary>
		/// Adds the given <see cref="KeyValuePair{TKey, TValue}"/> to the <see cref="UserDataDictionary{Tkey, Tvalue}"/>.
		/// </summary>
		/// <param name="entry"></param>
		public void Add(KeyValuePair<Tkey, Tvalue> entry)
		{
			base[entry.Key] = entry.Value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(entry.Key.ToString()));
		}

		/// <summary>
		/// Gets or sets the value associated with the given key from the <see cref="UserDataDictionary{Tkey, Tvalue}"/>.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public new Tvalue this[Tkey key]
		{
			get { return base[key]; }
			set 
			{ 
				base[key] = value; 
				this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(key.ToString()));
			}
		}

		/// <summary>
		/// Removes the given key from the <see cref="UserDataDictionary{Tkey, Tvalue}"/>.
		/// </summary>
		/// <param name="key"></param>
		public new void Remove(Tkey key)
		{
			base.Remove(key);
			this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(key.ToString()));
		}

		/// <summary>
		/// Removes all keys and values from the <see cref="UserDataDictionary{Tkey, Tvalue}"/>.
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(""));
		}

		#endregion

		#region INotifyPropertyChanged

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add { this.PropertyChanged += value; }
			remove { this.PropertyChanged -= value; }
		}

		/// <summary>
		/// Fires when a <see cref="UserDataDictionary{Tkey, Tvalue}"/> key, value pair changes.
		/// </summary>
		public PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
