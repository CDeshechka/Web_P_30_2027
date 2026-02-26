Мной был создан проект OGE, состоящий из 4 моделей данных(Subject(Предмет), Schoolchildren(Школьник), Auditorium(Аудитория),EFModel).
Модель Subject имеет поля: Assessmentfortheoge(Оценка за ОГЭ), Academicyearassessment(ценка за год), Finalassessment(Общая оценка).
Модель Schoolchildren имеет поля: Firstname(Имя), Lastname(Фамилия), Age(Возраст), Email(Электронная почта), Dateofbirthday(Дата рождения).
Модель Auditorium имеет поля: Auditoriumnumber(Номер аудитории), Auditoriumcapacity(Вместимость аудитории), Auditoriumsubject(Предмет аудитории).
Модель EFModel имеет поля: Id(Идентификационный номер), Name(Имя).
Далее была создана миграция и база данных к этим моделям.
