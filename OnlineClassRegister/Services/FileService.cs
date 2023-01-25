namespace OnlineClassRegister.Services
{
    public class FileService
    {
        public List<string> ReadMaterialList(string filePath)
        {
            List<string> teachingMateriaList = new List<string>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    foreach (string part in parts)
                    {
                        teachingMateriaList.Add(part);
                    }
                }
            }

            return teachingMateriaList;
        }

        public void AppendToFile(string filePath, string newMaterial)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.Write(newMaterial + "|");
            }
        }
    }
}
