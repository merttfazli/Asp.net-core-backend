using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string Deleted = "Silindi";
        public static string DataNotFound = "Veri bulunamadı";
        public static string Updated = "Güncellendi";
        public static string ProductNameAlreadyExists = "Bu isimde ürün zaten mevcut.";
        public static string MaxProductCountReachedForThisCategory = "Bu kategoriye 10 üründen fazla ürün ekleyemezsiniz.";
        public static string MaxCategoryCountReached = "Mevcut kategori sayısı 15'i geçemez.";
    }
}
