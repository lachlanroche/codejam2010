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
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Problem( sr )));
				}
			}
		}
	}

	int Problem( StreamReader sr )
	{
		int result = 0;
		int N = ParseLineNumbers( sr )[0];
		var W = new List<Wire>();

		for( int i=0; i<N; i++ ) {
			int[] ab = ParseLineNumbers( sr );
			var w = new Wire( ab[0], ab[1] );
			W.Add( w );
		}

		for( int i=0; i<N; i++ ) {
			for( int j=i+1; j<N; j++ ) {
				if (W[i].A > W[j].A && W[i].B < W[j].B) {
					result++;
				} else if (W[i].A < W[j].A && W[i].B > W[j].B) {
					result++;
				}
			}
		}

		return result;
	}

	public struct Wire
	{
		public int A;
		public int B;
		public Wire( int a, int b )
		{
			A = a;
			B = b;
		}
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
