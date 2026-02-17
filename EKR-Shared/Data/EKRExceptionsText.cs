using System;
using System.Collections.Generic;
using System.Text;

namespace EKR_Shared.Data
{
    public static class EKRExceptionsText
    {
        public static string UnexpectedServerSideError => "UnexpectedServerSideError";
        public static string ServerSideException => "ServerSideException";
        public static string ClientSideException => "ClientSideException";
        public static string ProduceError => "Ошибка отправки в KAFKA";
        public static string OperationCancelled => "Операция была отменена";
        public static string UnableToProcess => "Невозможно обработать запрос";
        public static string DecryptError => "Ошибка расшифровки";
        public static string EncryptError => "Ошибка шифрования";
        public static string WrongHashError => "Hash сумма не совпадает";
        public static string HashCalculateError => "Ошибка вычисления Hash суммы";

    }
}