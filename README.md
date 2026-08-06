# WLAS - Wait, Layout Auto-Switched?
<img width="2000" height="1000" alt="Banner" src="https://github.com/user-attachments/assets/0b9c31e7-0351-4aad-a318-0fcb5ade3098" />

## RU

### Введение

**WLAS** был вдохновлен программой *Keyboop* для MacOS — это невероятно удобное решение, которого пока нет на Windows в аналогичном open-source исполнении.

Поэтому я вооружился Claude и написал простенькое приложение. В первой версии уйма недочетов, но на то она и первая версия!

> **Разработка:** Весь код проекта полностью написан с помощью ИИ **Claude**.

### Как это работает?

Скрипт перехватывает ввод с клавиатуры и сохраняет текст в буфер. После нажатия `Space` или `Enter` происходят следующие шаги:

1. Скрипт сопоставляет введенные буквы (например, `g-h-b-d-t-n`) с раскладкой другого языка (`п-р-и-в-е-т`).
2. При обнаружении соответствия со словом из словаря производит автоматическую замену.

### Конфиденциальность и безопасность

> **Локальная работа:** Приложение **не собирает и не отправляет никаких данных**. Весь анализ и проверка слов происходят исключительно на вашем устройстве с использованием локально установленных словарей.

### Кастомизация и поддержка словарей

Вы можете легко дополнить или изменить словари под свои нужды в папке `Dictionaries`.

> **Примечание:** Базовые словари взяты из открытых источников, а словарь сленга сгенерирован с помощью нейросети. Текущие версии предоставлены исключительно для старта — автор планирует дорабатывать и улучшать их в будущих обновлениях.

### Сообщество и контакты

За остальными работами и обновлениями автора можно следить в Telegram-канале: [EICHLYsoul](https://t.me/EICHLYsoul)

---

## ENG

### Introduction

**WLAS** was inspired by the *Keyboop* app for MacOS — an incredibly convenient solution that hasn't existed on Windows as an open-source alternative until now.

That’s why I armed myself with Claude and built a simple application. The first version naturally has its shortcomings, but that’s what first releases are for!

> **Development:** The code for this project was written entirely with the assistance of **Claude**.

### How Does It Work?

The script captures keyboard input and stores it in a buffer. Once you press `Space` or `Enter`, it executes the following logic:

1. The script maps the typed characters (e.g., `р-у-д-д-щ`) to the corresponding keys in another layout (`h-e-l-l-o`).
2. If a valid word match is found, it automatically replaces the text.

### Privacy & Security

> **Fully Local:** The application **does not collect or transmit any user data**. All text processing and dictionary checks happen entirely offline, directly on your machine.

### Customization & Dictionary Support

You can easily edit or expand the dictionaries to fit your needs in the `Dictionaries` folder.

> **Note:** Default dictionaries were sourced from open data, and the slang dictionary was generated using an AI language model. These initial versions are provided just to get things started — the author plans to refine and expand them over time.

### Community & Contact

You can follow other projects and updates from the author on Telegram: [EICHLYsoul](https://t.me/EICHLYsoul)
