using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// タグ配列のデータを格納するクラス
	/// </summary>
	[DataContract]
	public sealed class TagsInfo
	{
		internal TagsInfo()
		{ }

		/// <summary>
		/// タグ配列
		/// </summary>
		public IReadOnlyList<TagInfo> Tags { get { return this._Tags; } }
		private List<TagInfo> _Tags;

		private List<TagInfo> AutoInterpolationTags { get { return this._Tags ?? ( this._Tags = new List<TagInfo>() ); } }

		[DataContract]
		class TagsWrapper
		{
			public TagsWrapper( string tag )
			{
				this._Tag = new List<string>() { tag };
			}

			public TagsWrapper( List<string> tags )
			{
				this._Tag = tags;
			}

			[DataMember( Name = "livetag" )]
			private object Tag
			{
				get { return null; }
				set
				{
					if( value == null )
					{
						return;
					}

					var type = value.GetType();
					if( type == typeof( string ) )
					{
						this._Tag = new List<string>();
						this._Tag.Add( ( string )value );
						return;
					}

					if( type == typeof( object[] ) )
					{
						this._Tag = ( ( object[] )value ).Cast<string>().ToList();
					}
				}
			}
			internal List<string> _Tag = null;
		}

		[DataMember( Name = "category" )]
		private TagsWrapper CategoryTagsImpl
		{
			get { return new TagsWrapper( this.AutoInterpolationTags.Where( t => t.IsCategoryTag ).Select( ti => ti.Value ).ToList() ); }
			set { this.AutoInterpolationTags.AddRange( value._Tag.Select( t => new TagInfo( t ) { IsCategoryTag = true } ) ); }
		}

		[DataMember( Name = "locked" )]
		private TagsWrapper LockedTagsImpl
		{
			get { return new TagsWrapper( this.AutoInterpolationTags.Where( t => t.IsLocked ).Select( ti => ti.Value ).ToList() ); }
			set { this.AutoInterpolationTags.AddRange( value._Tag.Select( t => new TagInfo( t ) { IsLocked = true } ) ); }
		}

		[DataMember( Name = "free" )]
		private TagsWrapper TagsImpl
		{
			get { return new TagsWrapper( this.AutoInterpolationTags.Where( t => !t.IsCategoryTag && !t.IsLocked ).Select( ti => ti.Value ).ToList() ); }
			set { this.AutoInterpolationTags.AddRange( value._Tag.Select( t => new TagInfo( t ) ) ); }
		}
	}
}