///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System;
using System.Collections;
using System.ComponentModel.Design;

namespace Wisej.Web.Ext.ChartJS.Design
{
	/// <summary>
	/// Collection editor specialized for the DataSetCollection class and its variations.
	/// </summary>
	internal class DataSetCollectionEditor : CollectionEditor
	{
		public DataSetCollectionEditor(Type type) : base(type)
		{
		}

		protected override Type CreateCollectionItemType()
		{
			// return the type of the elements in the list.
			// need to find the generic type of the collection.
			DataSetCollection dataSets = (DataSetCollection)this.Context.PropertyDescriptor.GetValue(this.Context.Instance);
			return dataSets.CreateDataSet("").GetType();
		}

		protected override Type[] CreateNewItemTypes()
		{
			return new[]
			{
				typeof(LineDataSet),
				typeof(BarDataSet),
				typeof(PieDataSet),
				typeof(DoughnutDataSet),
				typeof(PolarAreaDataSet),
				typeof(RadarDataSet)
			};


		}
	}
}
