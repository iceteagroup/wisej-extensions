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
using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Ext.CognitiveServices
{
	/// <summary>
	/// 
	/// </summary>
	public enum VisualFeatures
	{
		[Description("categorizes image content according to a taxonomy defined in documentation.")]
		Categories,

		[Description("tags the image with a detailed list of words related to the image content.")]
		Description,

		[Description("describes the image content with a complete sentence.")]
		Color,

		[Description("detects if faces are present. If present, generate coordinates, gender and age.")]
		Faces,

		[Description("detects if image is clipart or a line drawing.")]
		Tags,

		[Description("determines the accent color, dominant color, and whether an image is black&white.")]
		ImageType,

		[Description("detects if the image is pornographic in nature (depicts nudity or a sex act). Sexually suggestive content is also detected.")]
		Adult
	}
	/// <summary>
	/// 
	/// </summary>
	public enum Details
	{
		[Description("identifies celebrities if detected in the image.")]
		Celebrities,

		[Description("identifies landmarks if detected in the image.")]
		Landmarks
	}
	public class Request
	{
		internal Request(string Result)
		{

		}


		internal void ParseResult (string result)
		{
			Result = result;
			// clear ???
			Description = String.Empty;
			Tags = null;
			Captions = null;
			Categories = null;
			Faces = null;
			Celebrities = null;

			if (!String.IsNullOrEmpty(Result))
			{
				dynamic json = WisejSerializer.Parse(Result);
				// description
				if (json.description != null)
				{
					Description = json.description;
					// captions
					if (json.description.captions != null)
					{
						for (int i = 0; i < json.description.captions.Length; i++)
						{
							Captions[i] = json.description.captions[i].text;							
						}
					}
				}				
				// tags
				if (json.tags != null)
				{
					for (int i = 0; i < json.tags.Length; i++)
					{
						Tags[i].name = json.tags[i].name ?? null;
						Tags[i].confidence = json.tags[i].confidence ?? null;
					}
				}				
				else if (json.description != null && json.description.tags != null)
				{
					for (int i = 0; i < json.description.tags.Length; i++)
					{
						Tags[i].name = json.description.tags[i].name ?? null;
					}
				}
				// categories
				if (json.categories != null)
				{
					for (int i = 0; i < json.categories.Length; i++)
					{
						Categories[i].name = json.categories[i].name ?? null;
						Categories[i].score = json.categories[i].score ?? null;
					}
				}
				// faces
				if (json.faces != null)
				{
					for (int i = 0; i < json.faces.Length; i++)
					{
						Faces[i].gender = json.faces[i].gender ?? null;
						Faces[i].age = json.faces[i].age ?? null;
						Faces[i].faceRectangle.left = json.faces[i].faceRectangle.left ?? null;
						Faces[i].faceRectangle.top = json.faces[i].faceRectangle.top ?? null;
						Faces[i].faceRectangle.width = json.faces[i].faceRectangle.width ?? null;
						Faces[i].faceRectangle.height = json.faces[i].faceRectangle.height ?? null;
					}
				}
				// celebrities
				if (json.description != null && json.description.detail != null && json.description.detail.celebrities != null)
				{
					for (int i = 0; i < json.description.detail.celebrities.Length; i++)
					{
						Celebrities[i].name = json.description.detail.celbrities[i].name ?? null;
						Celebrities[i].confidence = json.description.detail.celbrities[i].confidence ?? null;
						Celebrities[i].faceRectangle.left = json.description.detail.celbrities[i].faceRectangle.left ?? null;
						Celebrities[i].faceRectangle.top = json.description.detail.celbrities[i].faceRectangle.top ?? null;
						Celebrities[i].faceRectangle.width = json.description.detail.celbrities[i].faceRectangle.width ?? null;
						Celebrities[i].faceRectangle.height = json.description.detail.celbrities[i].faceRectangle.height ?? null;
					}
				}
				// metadata
				if (json.metadata != null)
				{
					// TODO: check					
				}

				// TODO: add landmarks, adult, 
			}
		}

		/// <summary>
		/// Returns the json Result
		/// </summary>
		public string Result { get; internal set; }

		public string Description { get; internal set; }

		public string[] Captions { get; internal set; }

		public Tag[] Tags { get; internal set; }

		public Category[] Categories { get; internal set; }

		public AdultInfo Adult { get; internal set; }

		public Face[] Faces { get; internal set; }

		public Celebrity[] Celebrities { get; internal set; }

		public Landmark[] Landmarks { get; internal set; }

		public MetaDataInfo MetaData { get; internal set; }


		public struct Tag
		{
			public string name;
			public double confidence;
		}
		public struct Category
		{
			public string name;
			public double score;
		}

		public struct Coord
		{
			public int left;
			public int top;
			public int width;
			public int height;
		}

		public struct AdultInfo
		{
			public bool isAdultContent;
			public bool isRacyContent;
			public double adultScore;
			public double racyScore;
		}
		
		public struct Face
		{
			public string gender;
			public int age;
			public Coord faceRectangle;
		}

		public struct Celebrity
		{
			public string name;
			public Coord faceRectangle;
			public double confidence;
		}

		public struct Landmark
		{
			public string name;
			public double confidence;
		}

		public struct MetaDataInfo
		{
			public string imageType;
			public int imageWidth;
			public int imageHeight;
		}
	}

}
