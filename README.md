# CodeChallenger
[Watch the Video](https://www.youtube.com/watch?v=pgPT3MVbuEk)

[🇺🇸 English](#english) | [🇷🇺 Русский](#русский)

## English

🚀 **CodeChallenger** is a multilingual demo service for solving programming challenges, inspired by LeetCode. Developed in just three days "on the knee", this service offers an interactive platform for testing solutions in various programming languages.

### Features

- **Multilingual UI**: Supports Russian, Kazakh, English, and Chinese languages for maximum user convenience.
- **Interactive problem-solving**: Built-in code editor with draft saving in `localStorage`, allowing users to return to their solutions at any time.
- **Instant feedback**: Uses RabbitMQ for asynchronous solution processing and short-polling for result retrieval.

### Technologies

- **Frontend**: MVC + JavaScript for dynamic interaction.
- **Message Broker**: RabbitMQ ensures reliable message delivery between services.
- **Database**: Flexible data storage setup with InMemory support by default and easy switching to PostgreSQL.
- **Temporary Storage**: Configurable choice between Redis and InMemory for temporary data storage.

### Architecture

![Service workflow diagram](assets/challenger.svg)

The diagram demonstrates the task processing flow: from submitting the solution to receiving the results.

### Deployment

All necessary scripts and instructions for building and running the application are provided in the repository:

- **Image Build**: `build_images.bat` for Windows or `build_images.sh` for Unix systems.
- **Docker Launch**: `docker-compose.yaml` contains all necessary dependencies for service deployment.

### Integration with ChatGPT

The database of challenges is generated using ChatGPT, ensuring a variety of up-to-date tasks:

- Data generation instruction: [GPT_instruction.txt](assets/GPT_instruction.txt)
- Sample request to ChatGPT: [HowToAskGPT.txt](assets/HowToAskGPT.txt)

### Getting Started

Follow the instructions in the `docker-compose.yaml` file to start the project.
If you have a startup idea or suggestions for this project's development, let's evolve this service together.

For any questions or suggestions, feel free to reach out through the GitHub issue system.

## Русский

🚀 **CodeChallenger** — это мультиязычный демо-сервис для решения программных задач, вдохновленный LeetCode. Разработан за три дня "на коленке", сервис предлагает интерактивную платформу для проверки решений на различных языках программирования.

### Особенности

- **Мультиязычный UI**: Поддержка русского, казахского, английского и китайского языков для максимального удобства пользователей.
- **Интерактивное решение задач**: Встроенный редактор кода с сохранением черновиков в `localStorage`, позволяющий возвращаться к решениям в любое время.
- **Мгновенная обратная связь**: Использование RabbitMQ для асинхронной обработки решений и шорт-пулинга для получения результатов.

### Технологии

- **Фронтенд**: MVC + JavaScript для динамичного взаимодействия.
- **Брокер сообщений**: RabbitMQ обеспечивает надежную доставку сообщений между сервисами.
- **База данных**: Гибкая настройка хранения данных с поддержкой InMemory по умолчанию и возможностью легкого перехода на PostgreSQL.
- **Временное хранилище**: Конфигурируемый выбор между Redis и InMemory для временного хранения данных.

### Архитектура

![Схема работы сервиса](assets/challenger.svg)

Диаграмма демонстрирует поток обработки задач: от отправки решения до получения результатов.

### Развертывание

Все необходимые скрипты и инструкции для сборки и запуска приложения предоставлены в репозитории:

- **Сборка образов**: `build_images.bat` для Windows или `build_images.sh` для Unix-систем.
- **Запуск с Docker**: `docker-compose.yaml` содержит все необходимые зависимости для запуска сервиса.

### Интеграция с ChatGPT

База данных задач генерируется с помощью ChatGPT, что обеспечивает разнообразие и актуальность заданий:

- Инструкция для генерации данных: [GPT_instruction.txt](assets/GPT_instruction.txt)
- Пример запроса к ChatGPT: [HowToAskGPT.txt](assets/HowToAskGPT.txt)

### Начало работы

Для запуска проекта следуйте инструкциям в файле `docker-compose.yaml`.
Если у вас есть идея для стартапа или предложения по развитию данного проекта, давайте вместе развивать этот сервис.

При возникновении вопросов или предложений, не стесняйтесь обращаться через систему issue GitHub.
