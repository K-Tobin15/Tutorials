using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Reflection;
using Windows.UI.Xaml.Media;

namespace Shared.SampleData.StarTrek
{
    public static class Context
    {
        public static IEnumerable<Series> Series(bool includeCharacter = true, bool includeEpisodes = false)
        {
            var series = new List<Series>();

            var tos = new Series
            {
                Title = "Star Trek",
                Code = "TOS",
                Characters = 
                {
                    new Character{Actor = "William Shatner", Name= "James Kirk" },
                    new Character{Actor = "Leonard Nemoy", Name = "Mr. Spock" },
                    new Character{Actor = "DeForest Kelly", Name = "Leonard McCoy" },
                    new Character{Actor = "Nichelle Nichols", Name = "Nyota Uhura" },
                    new Character{Actor = "James Doohan", Name = "Montgomery Scott" },
                    new Character{Actor = "George Takei", Name = "Hikaru Sulu" },
                    new Character{Actor = "Walter Koenig", Name = "Pavel Chekov" },
                    new Character{Actor = "Majel Barrett", Name = "Christine Chapel" },
                }
            };
            series.Add(tos);

            var tng = new Series
            {
                Title = "The Next Generation",
                Code = "TNG",
                Characters = 
                {
                     new Character{Actor = "Patrick Stewart", Name= "Jean-Luc Picard"},
                     new Character{Actor = "Jonathan Frakes", Name= "William Riker"},
                     new Character{Actor = "LeVar Burton", Name= "Geordi La Forge"},
                     new Character{Actor = "Michael Dorn", Name= "Mr. Worf"},
                     new Character{Actor = "Brent Spiner", Name= "Mr. Data"},
                     new Character{Actor = "Marina Sirtis", Name= "Deanna Troi"},
                     new Character{Actor = "Gates McFadden", Name= "Beverly Crusher"},
                }
            };
            series.Add(tng);

            var ent = new Series
            {
                Title = "Enterprise",
                Code = "ENT",
                Characters = 
                {
                    new Character{Actor="Scott Bakula", Name="Jonathan Archer"  },
                    new Character{Actor="John Billingsley", Name="Dr. Phlox"  },
                    new Character{Actor="Jolene Blalock", Name="Sub-Commander TPol"  },
                    new Character{Actor="Connor Trinneer", Name="Trip Tucker"  },
                    new Character{Actor="Anthony Montgomery", Name="Travis Mayweather"  },
                    new Character{Actor="Dominic Keating", Name="Malcolm Reed"  },
                    new Character{Actor="Linda Park", Name="Hoshi Sato"  },
                }
            };
            series.Add(ent);

            return series;
        }

        public enum Layouts { WideStyle1With8, WideStyle1With7, WideStyle2With7, NarrowStyle1With7, NarrowStyle1With8, NarrowStyle2With7, Snap }
        public static int Arrange(IEnumerable<BaseModel> models, Layouts style, int snapHeight = 150)
        {
            int max = 0;
            int[][] setup = null;
            int[] remainder = null;
            switch (style)
            {
                case Layouts.WideStyle1With8:
                    setup = new[]
                    {
                        new[] { 300, 300 },
                        new[] { 150, 200 },
                        new[] { 150, 200 },
                        new[] { 300, 200 },
                        new[] { 300, 300 },
                        new[] { 175, 166 },
                        new[] { 175, 166 },
                        new[] { 175, 166 },
                    };
                    remainder = new[] { 150, 166 };
                    max = 600;
                    break;
                case Layouts.WideStyle1With7:
                    setup = new[]
                    {
                        new[] { 250, 250 },
                        new[] { 250, 250 },
                        new[] { 250, 166 },
                        new[] { 250, 332 },
                        new[] { 250, 250 },
                        new[] { 125, 250 },
                        new[] { 125, 250 },
                    };
                    remainder = new[] { 150, 166 };
                    max = 600;
                    break;
                case Layouts.WideStyle2With7:
                    setup = new[] { 
                        new[] { 350, 225 },
                        new[] { 175, 275 },
                        new[] { 175, 275 },
                        new[] { 275, 300 },
                        new[] { 225, 200 },
                        new[] { 225, 200 },
                        new[] { 175, 300 },
                    };
                    remainder = new[] { 150, 166 };
                    max = 600;
                    break;
                case Layouts.NarrowStyle1With7:
                    setup = new[] { 
                        new[] { 150, 300 },
                        new[] { 300, 150 },
                        new[] { 300, 150 },
                        new[] { 300, 300 },
                        new[] { 150, 300 },
                        new[] { 150, 150 },
                        new[] { 300, 150 },
                    };
                    remainder = new[] { 150, 150 };
                    max = 450;
                    break;
                case Layouts.NarrowStyle2With7:
                    setup = new[] { 
                        new[] { 300, 300 },
                        new[] { 150, 150 },
                        new[] { 150, 150 },
                        new[] { 150, 300 },
                        new[] { 150, 150 },
                        new[] { 150, 150 },
                        new[] { 300, 150 },
                    };
                    remainder = new[] { 150, 150 };
                    max = 450;
                    break;
                case Layouts.NarrowStyle1With8:
                    setup = new[] { 
                        new[] { 300, 300 },
                        new[] { 150, 150 },
                        new[] { 150, 300 },
                        new[] { 300, 150 },
                        new[] { 150, 150 },
                        new[] { 300, 150 },
                        new[] { 300, 150 },
                        new[] { 150, 150 },
                    };
                    remainder = new[] { 150, 150 };
                    max = 450;
                    break;
                case Layouts.Snap:
                    setup = new int[][] { };
                    remainder = new[] { 240, snapHeight };
                    break;
            }
            // initial
            foreach (var item in models.Take(setup.Length).Select((x, i) => new { Index = i, Item = x }))
            {
                item.Item.ColSpan = setup[item.Index][0];
                item.Item.RowSpan = setup[item.Index][1];
            }
            // remainder
            foreach (var item in models.Skip(setup.Length))
            {
                item.ColSpan = remainder[0];
                item.RowSpan = remainder[1];
            }
            // max for variable sezed wrapgrid
            return max;
        }
    }
}
