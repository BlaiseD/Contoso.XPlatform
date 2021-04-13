namespace Contoso.Forms.Configuration
{
    public class CommandButtonDescriptor
    {
        public int Id { get; set; }
        public string ShortString { get; set; }
        public string LongString { get; set; }
        public bool Cancel { get; set; }
        public int? GridId { get; set; }
        public bool? GridCommandButton { get; set; }
        public bool? SelectFormButton { get; set; }
        public string ButtonIcon { get; set; }
        public string ClassString { get; set; }
        public string SymptomText { get; set; }
        public bool AddToSymptions { get; set; }
        public string VoiceRecognitionText { get; set; }
        public int HeightRequest { get; set; }
    }
}
