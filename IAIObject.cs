using System;
using System.IO;

namespace AIEdit
{
	public interface IAIObject
	{
		string Name { get; set; }
		string ID { get; }
		int Uses { get; }

		void IncUses();
		void DecUses();

		void Reset();

		IAIObject Copy(string newid, string newname);

		void Write(StreamWriter stream);
	}
}
