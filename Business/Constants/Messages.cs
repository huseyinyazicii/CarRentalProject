using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        // CarImage
        public static string AddCarImageMessage = "Araç resmi başarıyla eklendi";
        public static string DeleteCarImageMessage = "Araç resmi başarıyla silindi";
        public static string EditCarImageMessage = "Araç resmi başarıyla güncellendi";
        public static string AboveImageAddingLimit = "Araç maksimum resim sayısına ulaştı. Resim ekleyemezsiniz";
        public static string CarImageNotFound = "Değiştirilmek istenen resim bulunamadı.";

        // User
        public static string GetSuccessUserMessage = "Üye bilgisi / bilgileri getirildi.";
        public static string GetErrorUserMessage = "Üye bilgisi / bilgileri getirilemedi.";

        public static string AuthorizationDenied = "Yetkilendirme reddedildi";
        public static string Registered = "Başarıyla kayıt olundu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string WrongPassword = "Yanlış parola";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAvailable = "Kullanıcı mevcut";
        public static string TokenCreated = "Token oluşturuldu.";
    }
}
