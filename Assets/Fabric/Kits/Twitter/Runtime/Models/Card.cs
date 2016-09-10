/*
 * Copyright (C) 2015 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

namespace Fabric.Twitter
{
	using System;
	using System.Collections.Generic;
	using Fabric.Internal.ThirdParty.MiniJSON;
	
	public class Card
	{
		public string cardType { get; private set; }
	
		public string appIPhoneId { get; private set; }
	
		public string appIPadId { get; private set; }
	
		public string appGooglePlayId { get; private set; }
	
		public string imageUri { get; private set; }
	
		internal Card (string cardType, string appIPhoneId, string appIPadId, string appGooglePlayId, string imageUri)
		{
			this.cardType = cardType;
			this.appIPhoneId = appIPhoneId;
			this.appIPadId = appIPadId;
			this.appGooglePlayId = appGooglePlayId;
			this.imageUri = imageUri;
		}
	
		internal Dictionary <string, object> ToDictionary ()
		{
			Dictionary<string, object> cardDictionary = new Dictionary<string, object> ();
			cardDictionary.Add ("cardType", cardType);
			cardDictionary.Add ("appIPhoneId", appIPhoneId);
			cardDictionary.Add ("appIPadId", appIPadId);
			cardDictionary.Add ("appGooglePlayId", appGooglePlayId);
			cardDictionary.Add ("imageUri", imageUri);
		
			return cardDictionary;
		}
	
		internal static string Serialize (Card card)
		{
			return Json.Serialize (card.ToDictionary ());
		}
	}
}