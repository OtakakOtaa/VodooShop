using NUnit.Framework;

namespace Tests
{
    public class CsvTest
    {
        [TestCase(OriginString)]
        public void ParsingTest(string originString)
        {
            CsvReader csvReader = new (originString);
            int currentRow = default;
            while (csvReader.TryRead()) 
            {
                for (var i = 0; csvReader.TryGetField(i, out var value); i++) 
                {
                    if(value != _expected[currentRow][i]) 
                        Assert.Fail();
                }
                currentRow++;
            }
        }

        #region Resources

        private const string OriginString = "1,200,fgfgfgf,35\r\n" +
                                            "1,200,fgfgfgf,35\r\n" +
                                            "1,200,fgfgfgf,35\r\n";

        private readonly string[][] _expected = 
        {
            new[] { "1", "200", "fgfgfgf", "35" },
            new[] { "1", "200", "fgfgfgf", "35" },
            new[] { "1", "200", "fgfgfgf", "35" }
        };

        #endregion
    }
}