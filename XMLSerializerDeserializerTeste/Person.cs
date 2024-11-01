using System.Xml.Serialization;

namespace XMLSerializerDeserializerTeste
{
    [XmlRoot("Person")]
    public class Person
    {
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("Age")]
        public int Age { get; set; }
    }

    [XmlRoot("Response")]
    public class ResponseData
    {
        [XmlElement("Greeting")]
        public string Greeting { get; set; }

        [XmlElement("AgeCategory")]
        public string AgeCategory { get; set; }
    }

}
