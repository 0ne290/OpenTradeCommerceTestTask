# Что в решении за что отвечает:

1. Проект Core - логическое ядро сервиса. Изолирован от деталей конкретных реализаций кеша и переводчика с помощью интерфейсов. Содержит логику получения переводов от внешних сервисов и кеширования этих переводов (ну плюс еще по всякой мелочи типа обработки ошибок при неправильном вводе кодов языков).
2. Проект Infrastructure.Translator - реализация интерфейса сервиса перевода. Содержит всякую скучную логику для работы с Yandex Translate.
3. Проект Infrastructure.Cache - то же, что и предыдущий пункт, но для сервиса кеширования. Работает с кешированием в памяти.
4. Проект WebApi - ASP.NET Core MVC проект, реализующий сервер, обеспечивающий доступ к сервису через REST. Скоро прикручу еще и gRPC.
5. Проект ConsoleClient - после того, как я прикручу gRPC, буду работать над этим проектом. Его цель - быть клиент для предыдущего пункта.

# Как разворачивать для Windows:

1. Создайте PFX-сертификат в папке WebApi. Сделать это можно с помощью команды dotnet dev-certs https --clean --import путь_и_имя_сертификата.pfx -p ваш_пароль. Либо другим способом. Вот ссылка на статью про создание сертификата разработки: https://learn.microsoft.com/ru-ru/dotnet/core/tools/dotnet-dev-certs.
2. Отредактировать appsettings.json. Если конкретно, то заменить значения элементов в секциях Yandex Translate и Kestrel.Endpoints.https.Certificate на корректные.
3. Выполнить скрипт Windows_BuildServer.ps1 в, например, Power Shell с указанием пути к папке, в которую будет собираться приложение.
4. Запустить файл WebApi.exe в папке с построенным приложением.

# REST. Эндпоинты и форматы запросов:

1. METHOD: POST. URL: translator/translate. BODY: { "text": "Текст, который нужно перевести", "languages": [ "Первый код языка по стандарту ISO 639-1, на который нужно перевести текст", "Второй", "И т. д." ] }.
2. METHOD: GET. URL: translator/information. Пока не реализован.