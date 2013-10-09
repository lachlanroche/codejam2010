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
				long[] num = cj.ParseLineNumbers( sr );
				for( long i=0; i<num[0]; i++ ) {
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Problem( sr )));
				}
			}
		}
	}

	string Problem( StreamReader sr )
	{
		chicks.Clear();
		long[] num = ParseLineNumbers( sr );
		long N = num[0];
		long K = num[1];
		long B = num[2];
		long T = num[3];

		long[] x_i = ParseLineNumbers( sr );
		long[] v_i = ParseLineNumbers( sr );

		long result = 0;
		long k = 0;

		for( long i=0; i<N; i++ ) {
			var c = new Chick( x_i[i], v_i[i], B );
			if (c.finish <= T) {
				k++;
			}
			chicks.Add(c);
		}

		if (k < K) {
			return "IMPOSSIBLE";
		}

		for( int j=0; j<N; j++ ) {
			var J = chicks[j];
			for( int kk=j+1; kk<N; kk++ ) {
				var KK = chicks[kk];

				if (J.speed == KK.speed) {
					continue;
				}

				double t = (1.0*(J.start-KK.start))/(1.0*(KK.speed-J.speed));
				if (KK.Swapped(t) | J.Swapped(t)) {
					continue;
				}

				if (t < (1.0*T) && t>0) {
					// Console.WriteLine(t);
					// Console.WriteLine(string.Format("{0} {1} {2} {3}",J.start,J.speed,KK.start,KK.speed));
					result++;
				}
			}
		}

		return result.ToString();;
	}

	List<Chick> chicks = new List<Chick>();

	class Chick
	{
		public Chick( long start, long speed, long barn )
		{
			this.start = start;
			this.speed = speed;
			finish = (barn-start) / speed;
			if (0 != ((barn-start)%speed)) {
				finish ++;
			}
		}
		public long start;
		public long speed;
		public long finish;

		public bool Swapped( double t )
		{
			var result = intersections.ContainsKey(t);
			intersections[t] = t;
			return result;
		}
		Dictionary<double,double> intersections = new Dictionary<double,double>();
	}

	long[] ParseLineNumbers( StreamReader sr )
	{
		var result = new List<long>();

		string line = sr.ReadLine().Trim();
		if (verbose) Console.WriteLine(line);
		foreach( var s in line.Split()) {
			result.Add( long.Parse(s) );
		}

		return result.ToArray();
	}
}
