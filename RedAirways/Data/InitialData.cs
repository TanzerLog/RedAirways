using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedAirways.Model;

namespace RedAirways.Data
{
    public class InitialData
    {
        public static void Load()
        {
            Airport Sydney = new Airport("Sydney");
            Airport Melbourne = new Airport("Melbourne");
            Airport Brisbane = new Airport("Brisbane");
            Airport Hobart = new Airport("Hobart");
            Airport Perth = new Airport("Perth");
            Airport Adelaide = new Airport("Adelaide");
            Airport Darwin = new Airport("Darwin");

            new Plane("X01", "Boeing 747", 40);
            new Plane("X02", "Boeing 737", 30);
            new Plane("X03", "Concord", 20);

            new Flight(Plane.Planes[0], new DateTime(2023, 03, 01, 9, 00, 0), new DateTime(2023, 03, 01, 12, 00, 0), Sydney, Melbourne);
            new Flight(Plane.Planes[1], new DateTime(2023, 03, 01, 10, 00, 0), new DateTime(2023, 03, 01, 13, 00, 0), Melbourne, Sydney);
            new Flight(Plane.Planes[2], new DateTime(2023, 03, 06, 7, 00, 0), new DateTime(2023, 03, 06, 14, 00, 0), Hobart, Perth);
            new Flight(Plane.Planes[0], new DateTime(2023, 03, 02, 9, 00, 0), new DateTime(2023, 03, 02, 12, 00, 0), Melbourne, Brisbane);
            new Flight(Plane.Planes[1], new DateTime(2023, 03, 02, 10, 00, 0), new DateTime(2023, 03, 02, 13, 00, 0), Sydney, Adelaide);
        }
    }
}
