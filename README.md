# WLAS - Wait, Layout Auto-Switched?

## RU

### Введение

**WLAS** был вдохновлен программой *Keyboop* для MacOS — это невероятно удобное решение, которого пока нет на Windows в аналогичном open-source исполнении.

Поэтому я вооружился Claude и написал простенькое приложение. В первой версии уйма недочетов, но на то она и первая версия!

### Как это работает?

Скрипт перехватывает ввод с клавиатуры (*полностью локально на вашем устройстве*) и сохраняет текст в буфер. После нажатия `Space` или `Enter` происходят следующие шаги:

1. Скрипт сопоставляет введенные буквы (например, `g-h-b-d-t-n`) с раскладкой другого языка (`п-р-и-в-е-т`).
2. При обнаружении соответствия со словом из словаря производит автоматическую замену.

### Кастомизация словарей

Вы можете легко дополнить или изменить словари под себя в папке `Dictionaries`.

> **Примечание:** Базовые словари взяты из открытых источников, а словарь сленга сгенерирован с помощью нейросети.

---

## ENG

### Introduction

**WLAS** was inspired by the *Keyboop* app for MacOS — an incredibly convenient solution that hasn't existed on Windows as an open-source alternative until now.

That’s why I armed myself with Claude and built a simple application. The first version naturally has its shortcomings, but that’s what first releases are for!

### How Does It Work?

The script captures keyboard input (*fully locally on your device*) and stores it in a buffer. Once you press `Space` or `Enter`, it executes the following logic:

1. The script maps the typed characters (e.g., `р-у-д-д-щ`) to the corresponding keys in another layout (`h-e-l-l-o`).
2. If a valid word match is found, it automatically replaces the text.

### Customizing Dictionaries

You can easily edit or expand the dictionaries to fit your needs in the `Dictionaries` folder.

> **Note:** Default dictionaries were sourced from open data, and the slang dictionary was generated using an AI language model.
