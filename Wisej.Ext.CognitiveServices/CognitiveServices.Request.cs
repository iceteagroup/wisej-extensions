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
	/// Specifies the visual features to return when requesting an image analysis
	/// from the Azure Cognitive Services Computer Vision API.
	/// </summary>
	/// <remarks>
	/// The names of the enum members are passed as a comma separated list in the <c>visualFeatures</c>
	/// query parameter of <see cref="P:Wisej.Ext.CognitiveServices.CognitiveServices.RequestParameters"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // request categories, tags and a description of the image.
	/// VisualFeatures[] features = { VisualFeatures.Categories, VisualFeatures.Tags, VisualFeatures.Description };
	/// this.cognitiveServices1.RequestParameters = "visualFeatures=" + String.Join(",", features);
	/// ]]></code>
	/// </example>
	public enum VisualFeatures
	{
		/// <summary>
		/// Categorizes image content according to a taxonomy defined in documentation.
		/// </summary>
		[Description("categorizes image content according to a taxonomy defined in documentation.")]
		Categories,

		/// <summary>
		/// Tags the image with a detailed list of words related to the image content.
		/// </summary>
		[Description("tags the image with a detailed list of words related to the image content.")]
		Description,

		/// <summary>
		/// Describes the image content with a complete sentence.
		/// </summary>
		[Description("describes the image content with a complete sentence.")]
		Color,

		/// <summary>
		/// Detects if faces are present. If present, generate coordinates, gender and age.
		/// </summary>
		[Description("detects if faces are present. If present, generate coordinates, gender and age.")]
		Faces,

		/// <summary>
		/// Detects if image is clipart or a line drawing.
		/// </summary>
		[Description("detects if image is clipart or a line drawing.")]
		Tags,

		/// <summary>
		/// Determines the accent color, dominant color, and whether an image is black&amp;white.
		/// </summary>
		[Description("determines the accent color, dominant color, and whether an image is black&white.")]
		ImageType,

		/// <summary>
		/// Detects if the image is pornographic in nature (depicts nudity or a sex act). Sexually suggestive content is also detected.
		/// </summary>
		[Description("detects if the image is pornographic in nature (depicts nudity or a sex act). Sexually suggestive content is also detected.")]
		Adult
	}
	/// <summary>
	/// Specifies the domain-specific details to return when requesting an image analysis
	/// from the Azure Cognitive Services Computer Vision API.
	/// </summary>
	/// <remarks>
	/// The names of the enum members are passed as a comma separated list in the <c>details</c>
	/// query parameter of <see cref="P:Wisej.Ext.CognitiveServices.CognitiveServices.RequestParameters"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // request a description and identify celebrities and landmarks.
	/// this.cognitiveServices1.RequestParameters =
	///     "visualFeatures=" + VisualFeatures.Description +
	///     "&details=" + Details.Celebrities + "," + Details.Landmarks;
	/// ]]></code>
	/// </example>
	public enum Details
	{
		/// <summary>
		/// Identifies celebrities if detected in the image.
		/// </summary>
		[Description("identifies celebrities if detected in the image.")]
		Celebrities,

		/// <summary>
		/// Identifies landmarks if detected in the image.
		/// </summary>
		[Description("identifies landmarks if detected in the image.")]
		Landmarks
	}
	/// <summary>
	/// Represents the result of an image analysis request processed by the
	/// <see cref="T:Wisej.Ext.CognitiveServices.CognitiveServices"/> component.
	/// </summary>
	/// <remarks>
	/// Instances of this class are created by the <see cref="T:Wisej.Ext.CognitiveServices.CognitiveServices"/>
	/// component and are available through <see cref="P:Wisej.Ext.CognitiveServices.CognitiveServices.LastRequest"/>
	/// after <see cref="M:Wisej.Ext.CognitiveServices.CognitiveServices.StartAnalysisRequest(System.Byte[])"/> completes.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.cognitiveServices1.StartAnalysisRequest(File.ReadAllBytes(Application.MapPath("Images/photo.jpg")));
	///
	/// // later, once the analysis has completed:
	/// Request request = this.cognitiveServices1.LastRequest;
	/// if (request != null)
	///     this.labelDescription.Text = request.Description;
	/// ]]></code>
	/// </example>
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
		/// Returns the raw JSON string returned by the Cognitive Services API.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// AlertBox.Show(request.Result);
		/// ]]></code>
		/// </example>
		public string Result { get; internal set; }

		/// <summary>
		/// Returns the description of the image content.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// this.label1.Text = request.Description;
		/// ]]></code>
		/// </example>
		public string Description { get; internal set; }

		/// <summary>
		/// Returns the captions describing the image in complete sentences, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// if (request.Captions != null)
		///     this.label1.Text = String.Join(Environment.NewLine, request.Captions);
		/// ]]></code>
		/// </example>
		public string[] Captions { get; internal set; }

		/// <summary>
		/// Returns the tags related to the image content, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// foreach (var tag in request.Tags)
		///     this.listBox1.Items.Add($"{tag.name} ({tag.confidence:P0})");
		/// ]]></code>
		/// </example>
		public Tag[] Tags { get; internal set; }

		/// <summary>
		/// Returns the categories that classify the image content, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// foreach (var category in request.Categories)
		///     this.listBox1.Items.Add($"{category.name} ({category.score:P0})");
		/// ]]></code>
		/// </example>
		public Category[] Categories { get; internal set; }

		/// <summary>
		/// Returns the adult and racy content information detected in the image.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// if (request.Adult.isAdultContent || request.Adult.isRacyContent)
		///     this.pictureBox1.Visible = false;
		/// ]]></code>
		/// </example>
		public AdultInfo Adult { get; internal set; }

		/// <summary>
		/// Returns the faces detected in the image, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// foreach (var face in request.Faces)
		///     this.listBox1.Items.Add($"{face.gender}, {face.age} at {face.faceRectangle.left},{face.faceRectangle.top}");
		/// ]]></code>
		/// </example>
		public Face[] Faces { get; internal set; }

		/// <summary>
		/// Returns the celebrities identified in the image, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// foreach (var celebrity in request.Celebrities)
		///     this.listBox1.Items.Add($"{celebrity.name} ({celebrity.confidence:P0})");
		/// ]]></code>
		/// </example>
		public Celebrity[] Celebrities { get; internal set; }

		/// <summary>
		/// Returns the landmarks identified in the image, or null if none were returned.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// foreach (var landmark in request.Landmarks)
		///     this.listBox1.Items.Add($"{landmark.name} ({landmark.confidence:P0})");
		/// ]]></code>
		/// </example>
		public Landmark[] Landmarks { get; internal set; }

		/// <summary>
		/// Returns the metadata of the analyzed image (format and size).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// Request request = this.cognitiveServices1.LastRequest;
		/// this.label1.Text = $"{request.MetaData.imageType}: {request.MetaData.imageWidth} x {request.MetaData.imageHeight}";
		/// ]]></code>
		/// </example>
		public MetaDataInfo MetaData { get; internal set; }


		/// <summary>
		/// Represents a tag associated with the image content.
		/// </summary>
		public struct Tag
		{
			/// <summary>
			/// The name of the tag.
			/// </summary>
			public string name;
			/// <summary>
			/// The confidence score of the tag, between 0 and 1.
			/// </summary>
			public double confidence;
		}
		/// <summary>
		/// Represents a category that classifies the image content.
		/// </summary>
		public struct Category
		{
			/// <summary>
			/// The name of the category.
			/// </summary>
			public string name;
			/// <summary>
			/// The confidence score of the category, between 0 and 1.
			/// </summary>
			public double score;
		}

		/// <summary>
		/// Represents a rectangle in the image, in pixels.
		/// </summary>
		public struct Coord
		{
			/// <summary>
			/// The left coordinate of the rectangle.
			/// </summary>
			public int left;
			/// <summary>
			/// The top coordinate of the rectangle.
			/// </summary>
			public int top;
			/// <summary>
			/// The width of the rectangle.
			/// </summary>
			public int width;
			/// <summary>
			/// The height of the rectangle.
			/// </summary>
			public int height;
		}

		/// <summary>
		/// Represents the adult and racy content information detected in the image.
		/// </summary>
		public struct AdultInfo
		{
			/// <summary>
			/// Indicates whether the image contains adult content.
			/// </summary>
			public bool isAdultContent;
			/// <summary>
			/// Indicates whether the image contains racy (sexually suggestive) content.
			/// </summary>
			public bool isRacyContent;
			/// <summary>
			/// The adult content score, between 0 and 1.
			/// </summary>
			public double adultScore;
			/// <summary>
			/// The racy content score, between 0 and 1.
			/// </summary>
			public double racyScore;
		}
		
		/// <summary>
		/// Represents a face detected in the image.
		/// </summary>
		public struct Face
		{
			/// <summary>
			/// The estimated gender of the person.
			/// </summary>
			public string gender;
			/// <summary>
			/// The estimated age of the person.
			/// </summary>
			public int age;
			/// <summary>
			/// The location of the face in the image.
			/// </summary>
			public Coord faceRectangle;
		}

		/// <summary>
		/// Represents a celebrity identified in the image.
		/// </summary>
		public struct Celebrity
		{
			/// <summary>
			/// The name of the celebrity.
			/// </summary>
			public string name;
			/// <summary>
			/// The location of the celebrity's face in the image.
			/// </summary>
			public Coord faceRectangle;
			/// <summary>
			/// The confidence score of the identification, between 0 and 1.
			/// </summary>
			public double confidence;
		}

		/// <summary>
		/// Represents a landmark identified in the image.
		/// </summary>
		public struct Landmark
		{
			/// <summary>
			/// The name of the landmark.
			/// </summary>
			public string name;
			/// <summary>
			/// The confidence score of the identification, between 0 and 1.
			/// </summary>
			public double confidence;
		}

		/// <summary>
		/// Represents the metadata of the analyzed image.
		/// </summary>
		public struct MetaDataInfo
		{
			/// <summary>
			/// The format of the image, e.g. "Jpeg" or "Png".
			/// </summary>
			public string imageType;
			/// <summary>
			/// The width of the image, in pixels.
			/// </summary>
			public int imageWidth;
			/// <summary>
			/// The height of the image, in pixels.
			/// </summary>
			public int imageHeight;
		}
	}

}
