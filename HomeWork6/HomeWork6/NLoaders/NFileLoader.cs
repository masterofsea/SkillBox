using System;
using System.IO;
using Microsoft.Extensions.Options;

namespace HomeWork6.NLoaders
{
    public class NFileLoader : INloader
    {
        private NLoaderConfiguration Configuration { get; }
        public NFileLoader(IOptions<NLoaderConfiguration> options)
        {
            Configuration = options.Value;
        }
        
        public int LoadN()
        {
            try
            {
                using var reader = new StreamReader(new FileStream(Configuration.Path, FileMode.Open, FileAccess.Read));

                var ret = int.Parse(reader.ReadLine());
                if (ret <= 0) throw new NloaderException("N must be not negative or equal to zero");
                
                return ret;
            }
            catch (Exception e)
            {
                throw new NloaderException(e.Message);
            }
        }
    }
}