namespace AAC_Tests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void connectSelectTest()
        {
            Console.WriteLine("Hello, World!");

            Connection connection = new Connection();
            var result = connection.connect();
            if (!result.success)
            {
                Console.WriteLine(result.message);
                return;
            }
            Console.WriteLine("Success");
        }
    }
}