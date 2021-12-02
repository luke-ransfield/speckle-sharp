﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Archicad.Communication.Commands
{
	sealed internal class GetModerlForElements : ICommand<IEnumerable<Model.ElementModel>>
	{
		#region --- Classes ---

		[JsonObject (MemberSerialization.OptIn)]
		public sealed class Parameters
		{
			#region --- Fields ---

			[JsonProperty ("elementIds")]
			private IEnumerable<string> ElementIds { get; }

			#endregion


			#region --- Ctor \ Dtor ---

			public Parameters (IEnumerable<string> elementIds)
			{
				ElementIds = elementIds;
			}

			#endregion
		}


		[JsonObject (MemberSerialization.OptIn)]
		private sealed class Result
		{
			#region --- Fields ---

			[JsonProperty ("models")]
			public IEnumerable<Model.ElementModel> Models { get; private set; }

			#endregion
		}

		#endregion


		#region --- Fields ---

		private IEnumerable<string> ElementIds { get; }

		#endregion


		#region --- Ctor \ Dtor ---

		public GetModerlForElements (IEnumerable<string> elementIds)
		{
			ElementIds = elementIds;
		}

		#endregion


		#region --- Functions ---

		public async Task<IEnumerable<Model.ElementModel>> Execute ()
		{
			Result result = await HttpCommandExecutor.Execute<Parameters, Result> ("GetModelForElements", new Parameters (ElementIds));
			return result.Models;
		}

		#endregion
	}
}
