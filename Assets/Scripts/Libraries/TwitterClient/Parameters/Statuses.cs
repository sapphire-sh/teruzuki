using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace teruzuki.Twitter.Parameters.Statuses
{
	public class MentionsTimelineParameters : ITwitterParameters
	{
		public ulong Count { set { queries ["count"] = value.ToString (); } }

		public ulong SinceID { set { queries ["since_id"] = value.ToString (); } }

		public ulong MaxID { set { queries ["max_id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool ContributorDetails { set { queries ["contributor_details"] = value.ToString (); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }
	}

	public class UserTimelineParameters: ITwitterParameters
	{
		public ulong UserID { set { queries ["user_id"] = value.ToString (); } }

		public string ScreenName { set { queries ["screen_name"] = value.ToString (); } }

		public ulong SinceID { set { queries ["since_id"] = value.ToString (); } }

		public ulong Count { set { queries ["count"] = value.ToString (); } }

		public ulong MaxID { set { queries ["max_id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool ExcludeReplies { set { queries ["exclude_replies"] = value.ToString (); } }

		public bool ContributorDetails { set { queries ["contributor_details"] = value.ToString (); } }

		public bool IncludeRTs { set { queries ["include_rts"] = value.ToString (); } }
	}

	public class HomeTimelineParameters : ITwitterParameters
	{
		public ulong Count { set { queries ["count"] = value.ToString (); } }

		public ulong SinceID { set { queries ["since_id"] = value.ToString (); } }

		public ulong MaxID { set { queries ["max_id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool ExcludeReplies { set { queries ["exclude_replies"] = value.ToString (); } }

		public bool ContributorDetails { set { queries ["contributor_details"] = value.ToString (); } }

		public bool IncludeDetails { set { queries ["include_entities"] = value.ToString (); } }
	}

	public class ShowParameters : ITwitterParameters
	{
		public ulong ID { set { queries ["id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool IncludeMyRetweet { set { queries ["include_my_retweet"] = value.ToString (); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }

		public ShowParameters (ulong ID)
		{
			this.ID = ID;
		}
	}

	public class UpdateParameters : ITwitterParameters
	{
		public string Status { set { queries ["status"] = value.ToString (); } }

		public ulong InReplyToStatusID { set { queries ["in_reply_to_status_id"] = value.ToString (); } }

		public bool PossiblySensitive { set { queries ["possibly_sensitive"] = value.ToString (); } }

		public double Lat { set { queries ["lat"] = value.ToString (); } }

		public double Long { set { queries ["long"] = value.ToString (); } }

		public string PlaceID { set { queries ["place_id"] = value.ToString (); } }

		public bool DisplayCoordinates { set { queries ["display_coordinates"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public ulong MediaIDs { set { queries ["media_ids"] = value.ToString (); } }
	}

	public class LookUpParameters : ITwitterParameters
	{
		public List<ulong> ID { set { queries ["id"] = value.Select (x => x.ToString ()).Aggregate ((a, b) => string.Format ("{0},{1}", a, b)); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool Map { set { queries ["map"] = value.ToString (); } }
	}
}
