using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Fujino.KCLauncher.XML
{
    public class KC_XmlManager
    {
        public static void KC_XmlDataWriter(object obj, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        // Message Alert
        public static Data_Settings Data_Settings_Reader(string filename)
        {
            Data_Settings data = new Data_Settings();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data_Settings));
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                data = (Data_Settings)serializer.Deserialize(stream);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("[ " + ex.Message + " ]", "Error Method [ Data_Settings_Reader ]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return data;
        }
    }
}
