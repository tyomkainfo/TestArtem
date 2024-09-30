namespace nrnUtil
{
    /// <summary>
    /// JDF Files are used by EPSON TD-Bridge to automate Burning
    /// </summary>
    public class JDF
    {
        public string job_id = "";
        public string publisher = "PP-100II 1";
        public string copies = "1";
        public string out_stacker = "2";
        public string disc_type = "BD-DL";
        public string format = "UDF260";
        public string data = "c:\\burn";
        public string volume_label = "";
        public string label = "C:\\Datafile\\image1.tdd";
        public string speed = "2";
        public string replace_field = "";

        public void WriteToFolder(string folder)
        {
            string text = "JOB_ID=" + this.job_id + "\r\n";
            text = text + "COPIES=" + this.copies + "\r\n";
            text = text + "PUBLISHER=" + this.publisher + "\r\n";
            text = text + "DISC_TYPE=" + this.disc_type + "\r\n";
            text = text + "FORMAT=" + this.format + "\r\n";
            text = text + "DATA=" + this.data + "\r\n";
            text = text + "VOLUME_LABEL=" + this.volume_label + "\r\n";
            text = text + "LABEL=" + this.label + "\r\n";
            text = text + "WRITING_SPEED=" + this.speed + "\r\n";
            System.IO.File.WriteAllText(folder + "\\" + this.job_id + ".jdf", text);
        }
    }
}