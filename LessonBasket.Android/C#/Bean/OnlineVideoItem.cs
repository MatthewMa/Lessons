using System;
using System.Runtime.Serialization;

namespace LessonBasketDemo
{
	[DataContract]
	public class OnlineVideoItem:Java.Lang.Object
	{
		private string _title;
		private string _description;
		private int _size;
		//question count
		private string _url;


		[DataMember]
		public string Title {
			get {
				return this._title;
			}
			set {
				_title = value;
			}
		}

		[DataMember]
		public string Description {
			get {
				return this._description;
			}
			set {
				_description = value;
			}
		}

		[DataMember]
		public int Size {
			get {
				return this._size;
			}
			set {
				_size = value;
			}
		}

		[DataMember]
		public string Url {
			get {
				return this._url;
			}
			set {
				_url = value;
			}
		}

		public OnlineVideoItem (string title, string description, int size, string url, int src)
		{
			this.Title = title;
			this.Description = description;
			this.Size = size;
			this.Url = url;
		}

		public OnlineVideoItem ()
		{
		}
		
	}
}

