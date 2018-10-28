using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileHydra
{
	[Serializable]
	class Mappings : List<Mapping>
	{
		public bool are_dirty;

		new public Mapping Add(Mapping map) {
			base.Add(map);
			are_dirty = true;
			return map;
		}

	}
}
