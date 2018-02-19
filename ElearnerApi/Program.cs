using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

/// <summary>
/// Author: Joshua Kaluba
/// </summary>
namespace ElearnerApi
{
    /// <summary>
    /// This class starts the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The starting point of the application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost( args ).Run( );
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder( args )
                .UseStartup<Startup>( )
                    .UseUrls( "http://localhost:19001" )
                        .Build( );
    }
}