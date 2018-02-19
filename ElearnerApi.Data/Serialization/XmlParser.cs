using System;
using System.IO;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Serialization
{
    public static class XmlParser
    {
        public static T FromXml<T>(String xml)
        {
            try
            {
                T returnedXmlClass = default( T );

                using (TextReader reader = new StringReader( xml ))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer( typeof( T ) ).Deserialize( reader );
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                    }
                }

                return returnedXmlClass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}