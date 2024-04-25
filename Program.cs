using System;

public class MyClass
{
    const double torad = Math.PI / 180.0;

    public static void Main(string[] args)
    {
        Console.WriteLine("*****************************************************************************************************");
        Console.WriteLine(" ");
        Console.WriteLine("Solving The Moon's Current Right Ascension and Declination for Date/Time: 2024-04-24, 07AM 00M 00S UTC ");
        Console.WriteLine("This corresponds to a Julian Date of: 2460424.791667");
        Console.WriteLine(" ");
        double[] radec = GetGeocentricMoonPos(2460424.791667); //2024-04-24, 07AM 00M 00S UTC

        double var1 = radec[0] / torad / 15.0;
        double var2 = radec[1] / torad;

        Console.WriteLine("The Moon's Right Ascension in decimals: " + var1);
        Console.WriteLine("The Moon's Declination in decimals is: " + var2);

        var timeSpan = TimeSpan.FromHours((double)var1);

        Console.WriteLine(" ");
        Console.WriteLine("The Moon's Right Ascension in hours/mins/secs is: {0} Hours {1} Minutes {2} Seconds", Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);

        double coord = var2;
        int sec = (int)Math.Round(coord * 3600);
        int deg = sec / 3600;
        sec = Math.Abs(sec % 3600);
        int min = sec / 60;
        sec %= 60;

        Console.WriteLine("The Moon's Declination in degrees/mins/secs is: {0} Degrees {1} Minutes {2} Seconds", deg, min, sec);

        //Moon Coordinate ra = 14.332209140386338 = 14h 19m 59s
        //Moon Coordinate dec = -16.01038576825404 = -16° 0' 37.38"

        //Convert the right ascension into decimal form using the following formula: hour + minute / 60 + second / 3600 = decimal value.
        //For example, if the right ascension is 2 hours, 30 minutes and 45 seconds, then this time in decimal form is 2 + 30 / 60 + 45 / 3600 = 2.5125.
        //Multiply the decimal time by 15 degrees.For example, 2.5125 x 15 = 37.6875 degrees.This value corresponds to the degree equivalent of 2 hours, 30 minutes and 45 seconds.

        Console.WriteLine(" ");
        Console.WriteLine("*****************************************************************************************************");
    }

    public static double Sind(double r)
    {
        return Math.Sin(r * torad);
    }

    public static double Cosd(double r)
    {
        return Math.Cos(r * torad);
    }

    // Low precision geocentric moon position (RA,DEC) from Astronomical Almanac page D22 (2017 ed)
    public static double[] GetGeocentricMoonPos(double jd)
    {
        double T = (jd - 2451545) / 36525;
        double L = 218.32 + 481267.881 * T + 6.29 * Sind(135.0 + 477198.87 * T) - 1.27 * Sind(259.3 - 413335.36 * T) + 0.66 * Sind(235.7 + 890534.22 * T) + 0.21 * Sind(269.9 + 954397.74 * T) - 0.19 * Sind(357.5 + 35999.05 * T) - 0.11 * Sind(186.5 + 966404.03 * T);
        double B = 5.13 * Sind(93.3 + 483202.02 * T) + 0.28 * Sind(228.2 + 960400.89 * T) - 0.28 * Sind(318.3 + 6003.15 * T) - 0.17 * Sind(217.6 - 407332.21 * T);
        double P = 0.9508 + 0.0518 * Cosd(135.0 + 477198.87 * T) + 0.0095 * Cosd(259.3 - 413335.36 * T) + 0.0078 * Cosd(235.7 + 890534.22 * T) + 0.0028 * Cosd(269.9 + 954397.74 * T);

        double SD = 0.2724 * P;
        double r = 1 / Sind(P);

        double l = Cosd(B) * Cosd(L);
        double m = 0.9175 * Cosd(B) * Sind(L) - 0.3978 * Sind(B);
        double n = 0.3978 * Cosd(B) * Sind(L) + 0.9175 * Sind(B);

        double ra = Math.Atan2(m, l);
        if (ra < 0) { ra += 2 * Math.PI; }
        double dec = Math.Asin(n);
        return new double[] { ra, dec };
    }
}