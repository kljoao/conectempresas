using System.ComponentModel.DataAnnotations;
    public class FormData{

        [Key]
        public int Ramal{get; set;}
        public String Nome{get; set;}

        public String Setor{get; set;}

        public String Email{get; set;}

        public String Cell{get; set;}
        public String PA{get; set;}
    }