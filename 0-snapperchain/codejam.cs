using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class Codejam
{
	bool verbose = false;

	public static void Main( string[] args )
	{
		Codejam cj = new Codejam();

		using (var fs = new FileStream(args[0], FileMode.Open)) {
			using (var sr = new StreamReader(fs)) {
				int[] num = cj.ParseLineNumbers( sr );
				for( int i=0; i<num[0]; i++ ) {
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Snapper( sr ) ? "ON": "OFF"));
				}
			}
		}
	}

	bool Snapper( StreamReader sr )
	{
		int[] num = ParseLineNumbers( sr );
		int n = num[0];
   		int k = num[1];

		if (k == 0) {
			return false;
		}

		int _2_n = 1 << (n);

		return 0 == ((k+1) % _2_n);
	}

	int[] ParseLineNumbers( StreamReader sr )
	{
		var result = new List<int>();

		string line = sr.ReadLine().Trim();
		if (verbose) Console.WriteLine(line);
		foreach( var s in line.Split()) {
			result.Add( int.Parse(s) );
		}

		return result.ToArray();
	}
}
