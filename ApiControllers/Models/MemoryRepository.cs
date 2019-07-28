using System.Collections.Generic;

namespace ApiControllers.Models
{
    public class MemoryRepository : IRepository
    {
        private readonly IEnumerable<Year> YearsList;
        private readonly IEnumerable<Make> MakesList;

        public MemoryRepository()
        {
            YearsList = new List<Year>
            {
                new Year { year = "2018" },
                new Year { year = "2017" },
                new Year { year = "2016" },
                new Year { year = "2015" },
                new Year { year = "2014" },
                new Year { year = "2013" },
                new Year { year = "2012" },
                new Year { year = "2011" },
                new Year { year = "2010" },
                new Year { year = "2009" },
                new Year { year = "2008" },
                new Year { year = "2007" },
                new Year { year = "2006" },
                new Year { year = "2005" },
                new Year { year = "2004" },
                new Year { year = "2003" },
                new Year { year = "2002" },
                new Year { year = "2001" },
                new Year { year = "2000" },
            };

            MakesList = new List<Make>
            {
                new Make { make = "ac"},
                new Make { make = "acura"},
                new Make { make = "alfa-romeo"},
                new Make { make = "alpina"},
                new Make { make = "ariel"},
                new Make { make = "ascari"},
                new Make { make = "aston-martin"},
                new Make { make = "audi"},
                new Make { make = "beijing"},
                new Make { make = "bentley"},
                new Make { make = "bizzarrini"},
                new Make { make = "bmw"},
                new Make { make = "bristol"},
                new Make { make = "bugatti"},
                new Make { make = "buick"},
                new Make { make = "cadillac"},
                new Make { make = "caterham"},
                new Make { make = "chevrolet"},
                new Make { make = "chrysler"},
                new Make { make = "citroen"},
                new Make { make = "daewoo"},
                new Make { make = "daihatsu"},
                new Make { make = "de-tomaso"},
                new Make { make = "dodge"},
                new Make { make = "donkervoort"},
                new Make { make = "eagle"},
                new Make { make = "ferrari"},
                new Make { make = "fiat"},
                new Make { make = "ford"},
                new Make { make = "gaz"},
                new Make { make = "ginetta"},
                new Make { make = "gmc"},
                new Make { make = "holden"},
                new Make { make = "honda"},
                new Make { make = "hummer"},
                new Make { make = "hyundai"},
                new Make { make = "infiniti"},
                new Make { make = "isuzu"},
                new Make { make = "jaguar"},
                new Make { make = "jeep"},
                new Make { make = "jensen"},
                new Make { make = "kia"},
                new Make { make = "lada"},
                new Make { make = "lamborghini"},
                new Make { make = "lancia"},
                new Make { make = "land-rover"},
                new Make { make = "lexus"},
                new Make { make = "lincoln"},
                new Make { make = "lotec"},
                new Make { make = "lotus"},
                new Make { make = "mahindra"},
                new Make { make = "marcos"},
                new Make { make = "matra-simca"},
                new Make { make = "mazda"},
                new Make { make = "mcc"},
                new Make { make = "mercedes-benz"},
                new Make { make = "mercury"},
                new Make { make = "mini"},
                new Make { make = "mitsubishi"},
                new Make { make = "morgan"},
                new Make { make = "nissan"},
                new Make { make = "noble"},
                new Make { make = "oldsmobile"},
                new Make { make = "opel"},
                new Make { make = "pagani"},
                new Make { make = "panoz"},
                new Make { make = "peugeot"},
                new Make { make = "pininfarina"},
                new Make { make = "plymouth"},
                new Make { make = "pontiac"},
                new Make { make = "porsche"},
                new Make { make = "proton"},
                new Make { make = "renault"},
                new Make { make = "riley"},
                new Make { make = "rolls-royce"},
                new Make { make = "rover"},
                new Make { make = "saab"},
                new Make { make = "saleen"},
                new Make { make = "samsung"},
                new Make { make = "saturn"},
                new Make { make = "seat"},
                new Make { make = "skoda"},
                new Make { make = "smart"},
                new Make { make = "ssangyong"},
                new Make { make = "subaru"},
                new Make { make = "suzuki"},
                new Make { make = "tata"},
                new Make { make = "toyota"},
                new Make { make = "tvr"},
                new Make { make = "vauxhall"},
                new Make { make = "vector"},
                new Make { make = "venturi"},
                new Make { make = "volkswagen"},
                new Make { make = "volvo"},
                new Make { make = "westfield"},
                new Make { make = "zaz"},
                new Make { make = "italdesign"},
                new Make { make = "maserati"},
                new Make { make = "mclaren"},
                new Make { make = "mg"},
                new Make { make = "spyker"},
                new Make { make = "steyr"},
                new Make { make = "dacia"},
                new Make { make = "koenigsegg"},
                new Make { make = "maybach"},
                new Make { make = "xedos"},
                new Make { make = "avanti"},
                new Make { make = "scion"},
                new Make { make = "studebaker"},
                new Make { make = "zastava"},
                new Make { make = "daimler"},
                new Make { make = "ssc"},
                new Make { make = "abarth"},
                new Make { make = "geely"},
                new Make { make = "tesla"},
                new Make { make = "brilliance"},
                new Make { make = "luxgen"},
                new Make { make = "zenvo"},
                new Make { make = "fisker"},
                new Make { make = "Alfa Romeo"},
                new Make { make = "Aston Martin"},
                new Make { make = "Land Rover"},
                new Make { make = "Ram"}
            };
        }

        public IEnumerable<Year> years => YearsList;
        public IEnumerable<Make> makes => MakesList;
    }

        /*private Dictionary<int, Reservation> items;

        public MemoryRepository()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation>
            {
                new Reservation { ClientName = "Alice", Location = "Board Room" },
                new Reservation { ClientName = "Bob", Location = "Lecture Hall" },
                new Reservation { ClientName = "Joe", Location = "Meeting Room 1"}
            }.ForEach(r => AddReservation(r));
        }

        public Reservation this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Reservation> Reservations => items.Values;

        public Reservation AddReservation(Reservation reservation)
        {
            if (reservation.ReservationId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                reservation.ReservationId = key;
            }
            items[reservation.ReservationId] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id) => items.Remove(id);

        public Reservation UpdateReservation(Reservation reservation)
            => AddReservation(reservation);
     }*/
    }
