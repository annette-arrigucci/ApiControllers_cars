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
                new Year { year = "2019" },
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
                new Make { make = "acura"},
                new Make { make = "audi"},
                new Make { make = "bentley"},
                new Make { make = "bmw"},
                new Make { make = "buick"},
                new Make { make = "cadillac"},
                new Make { make = "chevrolet"},
                new Make { make = "chrysler"},
                new Make { make = "daewoo"},
                new Make { make = "dodge"},
                new Make { make = "ferrari"},
                new Make { make = "fiat"},
                new Make { make = "ford"},
                new Make { make = "gmc"},
                new Make { make = "honda"},
                new Make { make = "hummer"},
                new Make { make = "hyundai"},
                new Make { make = "infiniti"},
                new Make { make = "isuzu"},
                new Make { make = "jaguar"},
                new Make { make = "jeep"},
                new Make { make = "kia"},
                new Make { make = "lamborghini"},
                new Make { make = "land-rover"},
                new Make { make = "lexus"},
                new Make { make = "lincoln"},
                new Make { make = "mazda"},
                new Make { make = "mercedes-benz"},
                new Make { make = "mercury"},
                new Make { make = "mini"},
                new Make { make = "mitsubishi"},
                new Make { make = "nissan"},
                new Make { make = "oldsmobile"},
                new Make { make = "opel"},
                new Make { make = "plymouth"},
                new Make { make = "pontiac"},
                new Make { make = "porsche"},
                new Make { make = "saab"},
                new Make { make = "samsung"},
                new Make { make = "saturn"},
                new Make { make = "smart"},
                new Make { make = "subaru"},
                new Make { make = "suzuki"},
                new Make { make = "toyota"},
                new Make { make = "volkswagen"},
                new Make { make = "volvo"},
                new Make { make = "maserati"},
                new Make { make = "tesla"},
            };
        }

        public IEnumerable<Year> years => YearsList;
        public IEnumerable<Make> makes => MakesList;
    }
}
