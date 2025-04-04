# Создание системы тестирования по анатомии

## Общие инструкции

Приложение необходимо разделить на 3 проекта:

1. **Domain**: Сущности (Question, Option) и интерфейсы.
2. **Infrastructure**: Реализация интерфейсов, подключение к PostgreSQL и работа с файлами.
3. **Web API**: Контроллеры для CRUD-операций и дополнительных функций.

---

## Часть 1: Создание базы данных

1. **Настройка базы данных**
   - Настройте PostgreSQL и создайте следующие таблицы:
     
     **Questions**:
     - `Id` (SERIAL PRIMARY KEY)
     - `QuestionText` (текст вопроса, VARCHAR(500))

     **Options**:
     - `Id` (SERIAL PRIMARY KEY)
     - `QuestionId` (FOREIGN KEY -> Questions.Id)
     - `OptionText` (текст варианта ответа, VARCHAR(255))
     - `IsCorrect` (правильный ли ответ, BOOLEAN)
     - `OptionLetter` (буква варианта A, B, C или D, CHAR(1))

   - Импортируйте начальные данные из файла anatomy.txt
   ```sql
   -- Пример добавления данных из файла anatomy.txt
   INSERT INTO Questions (QuestionNumber, QuestionText) 
   VALUES (1, 'Кадом шоҳаи анатомия сохти узвҳоро дар организми бемор меомӯзад?');

   INSERT INTO Options (QuestionId, OptionText, IsCorrect, OptionLetter) 
   VALUES 
   (1, 'анатомияи нормалӣ', false, 'A'),
   (1, 'анатомияи патологӣ', true, 'B'),
   (1, 'анатомияи топографӣ', false, 'C'),
   (1, 'анатомияи беморон', false, 'D');
   ```

## Часть 2: Реализация API

1. **CRUD операции**
   - Все ответы должны быть единого формата Response
   - Создайте следующие эндпоинты:
     - **Создание вопроса с вариантами**:
       - `POST /api/questions`
       - Добавление нового вопроса и его вариантов ответов в формате аналогичном anatomy.txt
     - **Получение вопросов**:
       - `GET /api/questions` - список всех вопросов с вариантами
       - `GET /api/questions/{id}` - получение конкретного вопроса с вариантами
     - **Обновление вопроса**:
       - `PUT /api/questions/{id}`
       - Обновление существующего вопроса и его вариантов
     - **Удаление вопроса**:
       - `DELETE /api/questions/{id}`
       - Удаление вопроса и связанных вариантов ответа

2. **Дополнительные методы API**

   1. **Импорт вопросов из файла**
      - **Эндпоинт:** `/api/questions/import`
      - **Описание:** Импорт вопросов из файла формата anatomy.txt
      - **Действия:**
        - Чтение файла
        - Парсинг формата "?номер. текст_вопроса"
        - Парсинг вариантов ответов с учетом префиксов "+" и "-"
        - Добавление данных в таблицы Questions и Options

    2. **Экспорт вопросов в файл**
      - **Эндпоинт:** `/api/questions/export`
      - **Описание:** Экспорт всех вопросов в файл `anatomy_export.txt`
      - **Формат файла должен соответствовать исходному файлу anatomy.txt:**
        ```
        ?1. Кадом шоҳаи анатомия сохти узвҳоро дар организми бемор меомӯзад?
        
        -А) анатомияи нормалӣ
        +В) анатомияи патологӣ
        -С) анатомияи топографӣ
        -D) анатомияи беморон
        ```

   3. **Анализ базы тестов**
      - **Эндпоинт:** `/api/questions/analyze`
      - **Описание:** Создание файла `anatomy_statistics.txt` со статистикой
      - **Формат статистики:**
        ```
        Общая статистика:
        - Всего вопросов: 50
        - Всего вариантов ответов: 200 (по 4 на каждый вопрос)
        
        Распределение правильных ответов:
        - Вариант A: 12 вопросов
        - Вариант B: 15 вопросов
        - Вариант C: 13 вопросов
        - Вариант D: 10 вопросов
        
        Анализ вопросов:
        - Среднее количество слов в вопросе: 10
        - Самый длинный вопрос: №15 (30 слов)
        - Самый короткий вопрос: №5 (4 слова)
        ```
