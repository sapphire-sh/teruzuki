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

	public class RetweetsOfMeParameters : ITwitterParameters
	{
		public ulong Count { set { queries ["count"] = value.ToString (); } }

		public ulong SinceID { set { queries ["since_id"] = value.ToString (); } }

		public ulong MaxID { set { queries ["max_id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }

		public bool IncludeUserEntities { set { queries ["include_user_entities"] = value.ToString (); } }
	}

	public class RetweetsParameters : ITwitterParameters
	{
		public ulong ID { set { queries ["id"] = value.ToString (); } }

		public ulong Count { set { queries ["count"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }
	}

	public class ShowParameters : ITwitterParameters
	{
		public ulong ID { set { queries ["id"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool IncludeMyRetweet { set { queries ["include_my_retweet"] = value.ToString (); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }

		public ShowParameters (ulong id)
		{
			this.ID = id;
		}
	}

	public class OEmbedParameters : ITwitterParameters
	{
		public string URL { set { queries ["url"] = value.ToString (); } }

		public ulong MaxWidth { set { queries ["maxwidth"] = value.ToString (); } }

		public bool HideMedia { set { queries ["hide_media"] = value.ToString (); } }

		public bool HideThread { set { queries ["hide_thread"] = value.ToString (); } }

		public bool OmitScript { set { queries ["omit_script"] = value.ToString (); } }

		public string Align { set { queries ["align"] = value.ToString (); } }

		public List<string> Related { set { queries ["related"] = value.Aggregate ((a, b) => string.Format ("{0},{1}", a, b)); } }

		public string Lang { set { queries ["lang"] = value.ToString (); } }

		public string WidgetType { set { queries ["widget_type"] = value.ToString (); } }

		public bool HideTweet { set { queries ["hide_tweet"] = value.ToString (); } }
	}

	public class RetweetersIDsParameters : ITwitterParameters
	{
		public ulong ID { set { queries ["id"] = value.ToString (); } }

		public ulong Cursor { set { queries ["cursor"] = value.ToString (); } }

		public bool StringifyIDs { set { queries ["stringify_ids"] = value.ToString (); } }

		public RetweetersIDsParameters(ulong id) {
			this.ID = id;
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

		public UpdateParameters(string status) {
			this.Status = status;
		}
	}

	public class LookUpParameters : ITwitterParameters
	{
		public List<ulong> ID { set { queries ["id"] = value.Select (x => x.ToString ()).Aggregate ((a, b) => string.Format ("{0},{1}", a, b)); } }

		public bool IncludeEntities { set { queries ["include_entities"] = value.ToString (); } }

		public bool TrimUser { set { queries ["trim_user"] = value.ToString (); } }

		public bool Map { set { queries ["map"] = value.ToString (); } }

		public LookUpParameters(List<ulong> id) {
			this.ID = id;
		}
	}
}
