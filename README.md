# AdvertismentTask
Для нормальной работы необходимо: SQL Server, подключение к интернету

Загрузка файлов для объявлений по неизвестной мне причине не работает в яндексе, так что стоит тестировать через гугл

Строка подключения к БД по которой обращается приложение: Server=localhost;Database=AdvertismentTask;Trusted_Connection=True;

Аккаунт администратора:

login - Admin

password - 123

После входа в админ аккаун непроверенные объявления можно посмотреть тут

![image](https://user-images.githubusercontent.com/100556773/187788312-48b3a770-dd29-458c-98dd-35ca205d2e90.png)

По умолчанию не проверенные объявления показываются первыми. Для демонстрации пагинации на странице админа кол-во объявлений на страницу ограниченно тремя.
![image](https://user-images.githubusercontent.com/100556773/187788561-6c7fde15-6cb4-42e6-8a40-894e51a0dd76.png)

Это можно изменить в AdminConroller
![image](https://user-images.githubusercontent.com/100556773/187788725-c225a05d-a573-4d22-bae1-898f5d429167.png)

